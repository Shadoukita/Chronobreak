using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GameServerCore.Enums;
using GameServerLib.Content;
using Chronobreak.GameServer.GameObjects;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.Inventory;
using Chronobreak.GameServer.Logging;
using log4net;
using Chronobreak.GameServer.Scripting.Lua;

namespace Chronobreak.GameServer.Content;

/// <summary>
/// Class that handles loading and handling information from the server-side parsed lol_game_client
/// </summary>
internal class ContentManager
{
    private static ILog _logger = LoggerProvider.GetLogger();

    /// <summary>
    /// Path to the server side directory containing the parsed lol_game_client and script packages. 
    /// </summary>
    internal static string ContentPath { get; set; }
    /// <summary>
    /// Path to the Talents directory within the DATA directory.
    /// </summary>
    internal static string TalentsPath { get; private set; }
    /// <summary>
    /// Path to the Spells directory within the DATA directory.
    /// </summary>
    internal static string SpellsPath { get; private set; }
    /// <summary>
    /// Path to the SharedSpells directory within the DATA directory.
    /// </summary>
    internal static string SpellsSharedPath { get; private set; }
    /// <summary>
    /// Path to the Characters directory within the DATA directory.
    /// </summary>
    internal static string CharactersPath { get; private set; }
    /// <summary>
    /// Path to the Items directory within the DATA directory.
    /// </summary>
    internal static string ItemsPath { get; private set; }
    /// <summary>
    /// Path to the directory of the map being used by the sever instance within the
    /// parsed lol_game_client/LEVELS directory.
    /// </summary>
    internal static string MapPath { get; private set; }
    /// <summary>
    /// Path to the scene directory within the map directory.
    /// </summary>
    internal static string ScenePath { get; private set; }
    /// <summary>
    /// Path to the Particles directory within the DATA directory.
    /// </summary>
    internal static string ParticlesPath { get; private set; }
    /// <summary>
    /// Path to the SharedParticles directory within the DATA directory.
    /// </summary>
    internal static string ParticlesSharedPath { get; private set; }
    /// <summary>
    /// Map .ini contents that contains configurations for the map in use.
    /// </summary>
    internal static INIContentFile MapConfig { get; private set; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Model"></param>
    /// <param name="SkinId"></param>
    /// <param name="LifeTime"></param>
    public record class ParticleData(string Model, int SkinId, float LifeTime);
    /// <summary>
    /// MonsterData tables that server has currently loaded.
    /// </summary>
    internal static Dictionary<int, MonsterDataTable> MonsterDataTables { get; } = [];
    /// <summary>
    /// Offset for the initial spawn of champion in a match.
    /// </summary>
    internal static Dictionary<TeamId, Dictionary<int, List<SpawnOffsetInfo>>> HeroSpawnOffset { get; } = [];

    /// <summary>
    /// Game feature flags that are currently enabled for the match.
    /// TODO: Move this somewhere related server running settings instead.
    /// </summary>
    internal static GameFeatures GameFeatures { get; private set; } = 0 | GameFeatures.FoundryOptions
                                                                 | GameFeatures.EarlyWarningForFOWMissiles
                                                                 | GameFeatures.ItemUndo
                                                                 | GameFeatures.TurretRangeIndicators
                                                                 | GameFeatures.NewMinionSpawnOrder
                                                                 | GameFeatures.AlternateBountySystem
                                                                 | GameFeatures.ParticleSkinNameTech
                                                                 | GameFeatures.JungleTimers
                                                                 | GameFeatures.TimerSyncForReplay
                                                                 | GameFeatures.NonRefCountedCharacterStates
                                                                 | GameFeatures.ActiveItemUI;

    // Variables used only within ContentManager

    private static Dictionary<string, CharData> CharactersData { get; } = [];
    private static Dictionary<string, SpellData> SpellsData { get; } = [];
    private static Dictionary<int, ItemData> ItemsData { get; } = [];
    private static Dictionary<string, TalentData> TalentsData { get; } = [];
    private static Dictionary<string, List<ParticleData>> ParticlesData { get; } = [];

    static ContentManager()
    {
        ContentPath = string.Empty;
        TalentsPath = "DATA/Talents";
        SpellsPath = "DATA/Spells";
        SpellsSharedPath = "DATA/Shared/Spells";
        CharactersPath = "DATA/Characters";
        ItemsPath = "DATA/Items";
        ParticlesPath = "DATA/Particles";
        ParticlesSharedPath = "DATA/Shared/Particles";
        MapPath = "LEVELS/Map";
        ScenePath = "Scene";
        MapConfig = new();
    }

    internal ContentManager(Config config)
    {
        ContentPath = Path.Join(config.ContentPath);
        TalentsPath = Path.Join(ContentPath, TalentsPath);
        SpellsPath = Path.Join(ContentPath, SpellsPath);
        SpellsSharedPath = Path.Join(ContentPath, SpellsSharedPath);
        CharactersPath = Path.Join(ContentPath, CharactersPath);
        ItemsPath = Path.Join(ContentPath, ItemsPath);
        ParticlesPath = Path.Join(ContentPath, ParticlesPath);
        ParticlesSharedPath = Path.Join(ContentPath, ParticlesSharedPath);
        MapPath = Path.Join(ContentPath, MapPath + config.GameConfig.Map);
        ScenePath = Path.Join(MapPath, ScenePath);
        MapConfig = new INIContentFile(Path.Join(ScenePath, "CFG/ObjectCFG.cfg"));

        foreach (var contentDirectory in new[]
        {
            ContentPath,
            MapPath,
            TalentsPath,
            ItemsPath,
            SpellsPath,
            SpellsSharedPath,
            CharactersPath,
            ParticlesPath,
            ParticlesSharedPath,
            ScenePath
        })
        {
            if (!Directory.Exists(contentDirectory))
            {
                _logger.Warn($"This Content directory doesnt exist: {contentDirectory}");
            }
        }

        _logger.Info("Loading Server Content...");
        LoadContent();
    }

    /// <summary>
    /// Begins loading content data from the server-side parsed lol_game_client that will be used by the match.
    /// TODO: It pulls all possible data from the game-client till selective loading is setup.
    /// </summary>
    private void LoadContent()
    {
        var gameFeaturesCfg = "GameFeatures.cfg";
        var gameFeaturesCfgPath = $"{ContentPath}/DATA/cfg/defaults/{gameFeaturesCfg}";
        if (File.Exists(gameFeaturesCfgPath))
        {
            GameFeatures = 0;
            var gameFeaturesCfgIni = new INIContentFile(gameFeaturesCfgPath);
            var section = "Features";
            foreach (var key in gameFeaturesCfgIni.GetKeys(section))
            {
                if (Enum.TryParse<GameFeatures>(key, out var feature))
                {
                    var enabled = gameFeaturesCfgIni.GetBool(section, key);
                    GameFeatures = enabled ? GameFeatures | feature : GameFeatures & ~feature;
                }
                else
                {
                    _logger.Warn($"Unknown key {key} in {gameFeaturesCfg}");
                }
            }
        }

        INIContentFile spawnOffset = new($"{CharactersPath}/HeroSpawnOffsets.ini");
        foreach (var section in spawnOffset.Sections)
        {
            var team = TeamId.TEAM_ORDER;
            if (section.StartsWith("Chaos"))
            {
                team = TeamId.TEAM_CHAOS;
            }

            var count = int.Parse(Regex.Replace(section, "[^0-9]", string.Empty));

            HeroSpawnOffset.TryAdd(team, []);
            HeroSpawnOffset[team].TryAdd(count, []);

            for (var i = 1; i <= count; i++)
            {
                string[] positions = spawnOffset.GetString(section, $"Pos{i}").Split(' ');
                Vector2 pos = new(float.Parse(positions[0]), float.Parse(positions[2]));

                HeroSpawnOffset[team][count].Add(new()
                {
                    Index = i - 1,
                    FacingDirection = spawnOffset.GetInt(section, $"Facing{i}"),
                    PositionOffset = pos
                });
            }
        }

        for (var index = 1; File.Exists(MapPath + $"/MonsterDataTable{index}.ini"); index++)
        {
            var monsterDataTable = new INIContentFile(Path.Join(MapPath, $"MonsterDataTable{index}.ini"));
            MonsterDataTables[index] = new MonsterDataTable(monsterDataTable);
        }

        ContentLoader.TryLoad(ContentPath);

        foreach (var player in Game.Config.Players)
        {
            LoadPlayer(player);
        }
    }

    private static void LoadPlayer(PlayerConfig player)
    {
        _ = GetSpellData(player.Summoner1.ToLowerInvariant());
        _ = GetSpellData(player.Summoner2.ToLowerInvariant());
        var charDirs = Directory.EnumerateDirectories(CharactersPath, $"{player.Champion}*", SearchOption.AllDirectories);
        Parallel.ForEach(charDirs, characterPath =>
        {
            _ = GetCharData(Path.GetFileName(characterPath));

            //Skin
            string skinsPath = Path.Join(characterPath, "Skins");
            if (!Directory.Exists(skinsPath))
            {
                skinsPath = Path.Combine(characterPath, "skins");
                if (!Directory.Exists(skinsPath))
                {
                    goto GetParticles;
                }
            }

            if (player.Skin > 0)
            {
                skinsPath = Path.Combine(skinsPath, $"Skin{player.Skin:D2}");
            }
            else
            {
                skinsPath = Path.Combine(skinsPath, "Base");
            }

            if (!Directory.Exists(skinsPath))
            {
                foreach (var file in Directory.EnumerateFiles(skinsPath, "*.troy"))
                {
                    var particleName = Path.GetFileName(file);
                    LoadParticle(particleName, player.Champion, player.Skin);
                }
            }

        GetParticles:
            var partDirPath = Path.Join(skinsPath, "Particles");
            if (Directory.Exists(partDirPath))
            {
                foreach (string file in Directory.EnumerateFiles(partDirPath, "*.troy"))
                {
                    //Check
                    _ = GetParticleData(Path.GetFileName(file));
                }
            }
        });
    }

    #region MapData
    internal static MapData GetMapData(string gameModeExpCurveOverride, int mapId)
    {
        MapData toReturnMapData = new(mapId);

        if (!Directory.Exists(ScenePath))
        {
            return toReturnMapData;
        }

        if (File.Exists($"{ScenePath}/MapObjects.mob"))
        {
            MobFile mobFile = new($"{ScenePath}/MapObjects.mob");
            toReturnMapData.MapObjects = new(mobFile.MapObjects);
        }

        string[] filesToLoad = Directory.GetFiles(ScenePath, "*.sco");
        foreach (var file in filesToLoad)
        {
            var name = Path.GetFileNameWithoutExtension(file);

            if (toReturnMapData.MapObjects.Any(x => x.Name == name))
            {
                continue;
            }

            //Loads a Map Object from the Map's Scene folder
            var lines = File.ReadAllLines($"{ScenePath}/{name}.sco").ToList();
            var positionStr = lines.Find(x => x.StartsWith("CentralPoint"))!.Split('=')[1];
            var coords = positionStr.Split(' ');
            Vector3 pos = new()
            {
                X = float.Parse(coords[1]),
                Y = float.Parse(coords[2]),
                Z = float.Parse(coords[3])
            };

            var mapObject = new MapObject(name, pos);

            toReturnMapData.MapObjects.Add(mapObject);
        }

        toReturnMapData.MapObjects.RemoveAll(x => x.Name.Contains("AIPath"));

        string section = "ExpGrantedOnDeath";
        string expFileName = "ExpCurve";
        if (!string.IsNullOrEmpty(gameModeExpCurveOverride))
        {
            // EXPCurve, DeathTimes, and StatsProgression.
            expFileName = gameModeExpCurveOverride;
        }

        // We skip the first level, meaning there are 29 level instances, but we only assign 2->29 (that's 29).
        // To fix this (assign 2->30), we add 1 to the Count.
        INIContentFile contentFile = ReadMapINIData(toReturnMapData.ExpCurve, $"{MapPath}/{expFileName}.ini", "EXP", "Level", 1);
        toReturnMapData.BaseExpMultiple = contentFile.GetFloat(section, "BaseExpMultiple");
        toReturnMapData.LevelDifferenceExpMultiple = contentFile.GetFloat(section, "LevelDifferenceExpMultiple");
        toReturnMapData.MinimumExpMultiple = contentFile.GetFloat(section, "MinimumExpMultiple");

        _ = ReadMapINIData(toReturnMapData.StatsProgression, $"{MapPath}/StatsProgression.ini", "PerLevelStatsFactor", "Level");

        contentFile = ReadMapINIData(toReturnMapData.ItemInclusionList, $"{MapPath}/Items.ini", "ItemInclusionList", "Item");
        ReadMapINIData(toReturnMapData.UnpurchasableItemList, contentFile, "UnpurchasableItemList", "Item");
        ReadMapINIData(toReturnMapData.UnpurchasableItemList, contentFile, $"UnpurchasableItemList_{Game.Config.GameConfig.GameMode.ToUpperInvariant()}", "Item");

        contentFile = new($"{MapPath}/DeathTimes.ini");
        section = "TimeDeadPerLevel";

        int length = contentFile.GetKeys(section).Length;
        for (int i = 1; i < length; i++)
        {
            var name = (i <= 9) ? $"Level0{i}" : $"Level{i}";
            toReturnMapData.DeathTimes.Add(contentFile.GetFloat(section, name));
        }

        //TODO: ConstData class
        section = $"{MapPath}/Constants.var";
        if (File.Exists(section))
        {
            foreach (var line in File.ReadAllLines(section))
            {
                if (line.StartsWith(';') || string.IsNullOrEmpty(line))
                {
                    continue;
                }

                string[] split = line.Trim().Split('=');
                if (float.TryParse(split[1].Split(';')[0], out float value))
                {
                    toReturnMapData.MapConstants.Add(split[0].Trim(), value);
                }
            }
        }
        return toReturnMapData;
    }

    private static INIContentFile ReadMapINIData<T>(List<T> list, string path, string section, string prefix, int indexStartOverride = 0)
    {
        INIContentFile contentFile = new(path);
        ReadMapINIData(list, contentFile, section, prefix, indexStartOverride);
        return contentFile;
    }
    private static void ReadMapINIData<T>(List<T> list, INIContentFile contentFile, string section, string prefix, int indexStartOverride = 0)
    {
        int length = contentFile.GetKeys(section).Length;
        for (int i = indexStartOverride; i < length; i++)
        {
            list.Add((T)Convert.ChangeType(contentFile.GetFloat(section, $"{prefix}{i}", 0), typeof(T)));
        }
    }

    #endregion
    #region CharData
    internal static CharData? GetCharData(string characterName)
    {
        if (string.IsNullOrEmpty(characterName))
        {
            return null;
        }

        characterName = characterName.ToLowerInvariant();
        if (CharactersData.TryGetValue(characterName, out CharData? charData))
        {
            return charData;
        }

        return LoadCharacter(characterName);
    }
    private static CharData? LoadCharacter(string characterName)
    {
        string iniFileName = characterName.EndsWith(".ini") ? characterName : characterName + ".ini";
        string? filePath = ContentLoader.GetDataPath(iniFileName);

        if (string.IsNullOrEmpty(filePath))
        {
            _logger.Error($"Could not find CharData for Character \"{characterName}\"!");
            return null;
        }

        CharData data = new(new(filePath));

        lock (CharactersData)
        {
            CharactersData[characterName] = data;
        }


        ProcessPreloadFile($"CharScript{characterName}.preload");
        LogContentLoaded($"Character - {characterName}");

        return data;
    }
    #endregion
    #region SpellData
    internal static SpellData? GetSpellData(string spellName)
    {
        if (string.IsNullOrEmpty(spellName))
        {
            return null;
        }

        spellName = spellName.ToLowerInvariant();
        if (SpellsData.TryGetValue(spellName, out SpellData? spellData))
        {
            return spellData;
        }

        return LoadSpell(spellName);
    }
    private static SpellData? LoadSpell(string spellName)
    {
        string iniName = spellName.EndsWith(".ini") ? spellName : spellName + ".ini";
        string? filePath = ContentLoader.GetDataPath(iniName);

        SpellData? data = null;
        if (string.IsNullOrEmpty(filePath))
        {
            _logger.Warn($"{spellName}: Could not find SpellData for \"{spellName}\"");
            goto CacheSpell;
        }

        data = new(new(filePath));
        SpellFlagsMarker.SwitchFlagsIfNeeded(data);

    CacheSpell:
        lock (SpellsData)
        {
            SpellsData[spellName] = data;
        }

        ProcessPreloadFile(spellName);
        LogContentLoaded("Spell - " + spellName);

        return data;
    }
    #endregion
    #region TalentData
    internal static TalentData? GetTalentData(string talendtId)
    {
        if (string.IsNullOrEmpty(talendtId))
        {
            return null;
        }

        talendtId = talendtId.ToLowerInvariant();
        if (TalentsData.TryGetValue(talendtId, out TalentData? talentsData))
        {
            return talentsData;
        }

        return LoadTalent(talendtId);
    }
    private static TalentData? LoadTalent(string talendtId)
    {
        string iniName = talendtId.EndsWith(".ini") ? talendtId : talendtId + ".ini";
        string? file = ContentLoader.GetDataPath(iniName);

        if (string.IsNullOrEmpty(file))
        {
            _logger.Error($"Could not find TalentData for Talent \"{talendtId}\"!");
            return null;
        }

        TalentData data = new(new INIContentFile(file));

        lock (TalentsData)
        {
            TalentsData[data.Id] = data;
        }

        ProcessPreloadFile($"{talendtId}.preload");
        LogContentLoaded("Talent - " + talendtId);

        return data;
    }
    #endregion
    #region ItemData
    internal static ItemData? GetItemData(int id)
    {
        if (ItemsData.TryGetValue(id, out ItemData? itemData))
        {
            return itemData;
        }

        return LoadItem(id);
    }
    private static ItemData? LoadItem(int id)
    {
        if (ItemsData.TryGetValue(id, out ItemData? data))
        {
            return data;
        }

        string idStr = id.ToString();
        string? filePath = ContentLoader.GetDataPath($"{idStr}.ini");

        if (string.IsNullOrEmpty(filePath))
        {
            _logger.Error($"Could not find ItemData for Item \"{id}\"!");
            return null;
        }

        data = new(new(filePath));

        lock (ItemsData)
        {
            ItemsData.TryAdd(data.Id, data);
        }

        ProcessPreloadFile($"{idStr}.preload");
        LogContentLoaded("Item - " + idStr);

        return data;
    }
    #endregion
    #region ParticleData
    internal static ParticleData? GetParticleData(string name, params GameObject[] characters)
    {
        if (!name.EndsWith(".troy"))
        {
            name += ".troy";
        }

        ParticleData? data = ReturnParticleData(name, characters);
        if (data is not null)
        {
            return data;
        }

        //?????
        GameObject? unit = characters.FirstOrDefault();
        return LoadParticle(name, (unit as AttackableUnit)?.Model ?? "", (unit as ObjAIBase)?.SkinID ?? -1);
    }
    private static ParticleData? ReturnParticleData(string name, GameObject[] characters)
    {
        if (ParticlesData.TryGetValue(name, out List<ParticleData>? list) && list.Count > 0)
        {
            ParticleData? particleData = null;
            foreach (var character in characters)
            {
                if (character is not AttackableUnit u)
                {
                    continue;
                }

                string model = u.Model.ToLowerInvariant();

                if (character is ObjAIBase ai)
                {
                    particleData = list.Find(data => data.Model == model && data.SkinId == ai.SkinID);
                    if (particleData is not null)
                    {
                        return particleData;
                    }
                }

                particleData = list.Find(data => data.Model == model && data.SkinId == -1);
                if (particleData is not null)
                {
                    return particleData;
                }
            }

            return list.Find(data => data.Model == "" && data.SkinId == -1) ?? list.FirstOrDefault();
        }

        return null;
    }
    public static ParticleData? LoadParticle(string particleName, string model = "", int skinId = 0)
    {
        var filePath = ContentLoader.GetDataPath(particleName);

        if (string.IsNullOrEmpty(filePath))
        {
            _logger.Error($"Could not find ParticleData for particle \"{particleName}\"!");
            return null;
        }

        INIContentFile contentFile = new(filePath);

        float maxEffectLifetime = 0;
        foreach (var section in contentFile.GetMultiString("System", "GroupPart", null))
        {
            if (string.IsNullOrEmpty(section))
            {
                continue;
            }

            // "e-life" - emitter, "p-life" - particle, "f-life" - fluid
            var eLife = contentFile.GetFloat(section, "e-life", -1);
            var pLife = contentFile.GetFloat(section, "p-life", -1);
            var fLife = contentFile.GetFloat(section, "f-life", -1);
            if (eLife == -1 || pLife == -1 || fLife == -1)
            {
                maxEffectLifetime = float.PositiveInfinity;
                break;
            }

            //TODO: e-timeoffset? build-up-time?
            maxEffectLifetime = Math.Max(maxEffectLifetime, eLife + Math.Max(pLife, fLife));
        }

        ParticleData data = new(model, skinId, maxEffectLifetime);
        lock (ParticlesData)
        {
            if (ParticlesData.TryGetValue(particleName, out List<ParticleData>? dataList))
            {
                dataList.Add(data);
            }
            else
            {
                ParticlesData[particleName] = [data];
            }
        }

        LogContentLoaded($"Particle - {contentFile.Name}.troy");
        return data;
    }
    #endregion

    private static void LogContentLoaded(string name)
    {
        if (!Game.Config.EnableContentLogs)
        {
            return;
        }
        _logger.Info($"Loaded {name}");
    }

    [Obsolete("This method has to just call the script callback")]
    internal static void ProcessPreloadFile(string fileName)
    {
        var preloadScript = LuaScriptEngine.CreateTableReferringGlobal();
        LuaScriptEngine.DoScript(fileName, preloadScript);
        var mTable = preloadScript.Get("PreLoadBuildingBlocks");

        if (mTable.IsNil())
        {
            return;
        }

        //TODO: EXECUTE THE CALLBACK INSTEAD OF BRUTEFORCING THIS
        var pairs = mTable.Table.Pairs.ToArray();
        foreach (var pair in pairs)
        {
            var table = pair.Value.Table;

            var plpName = table.Get("Params").Table.Values.ToArray()[0].String;

            if (plpName.EndsWith(".troy"))
            {
                GetParticleData(plpName);
            }
            else if (plpName.Contains("CharScript"))
            {
                GetCharData(plpName);
            }
            else
            {
                GetSpellData(plpName);
            }

            continue;
            //TODO: Figure out how to get the name of the function to call the other preload values directly?
            var plFunction = table.Get("Function").Callback.Name;
        }
    }
}
