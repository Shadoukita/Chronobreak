using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Chronobreak.GameServer;
using Chronobreak.GameServer.Content;
using Chronobreak.GameServer.Logging;
using log4net;
using Newtonsoft.Json;

namespace GameServerLib.Content;

internal static class ContentLoader
{
    private static readonly ILog _logger = LoggerProvider.GetLogger();
    public static ContentPathCache Cache { get; private set; } = new();

    //TODO: Rework to return cache rather than storing it in this class.
    public static void TryLoad(string directory = "")
    {
        try
        {
            if (!Game.Config.UseCacheFile)
            {
                LoadContent();
                return;
            }

            var cacheLocation = $@"{directory}\ContentCache.json";
            if (!File.Exists(cacheLocation))
            {
                _logger.Warn($"ContentCache.json doesn't exist in the provided directory: {cacheLocation}.");
                _logger.Warn("Generating new ContentCache.json...");
                LoadContent();
                WriteToFile(cacheLocation);
            }
            else
            {
                using (var filestream = File.OpenText(cacheLocation))
                {
                    Cache = new JsonSerializer().Deserialize<ContentPathCache>(new JsonTextReader(filestream)) ?? new ContentPathCache();
                }
                _logger.Info($"Loaded ContentCache.json!");
            }
        }
        catch (Exception e)
        {
            _logger.Error(e);
        }
    }

    private static void LoadContent()
    {
        _logger.Info("Loading Character File Paths...");
        LoadCharacters();

        _logger.Info("Loading Spell File Paths...");
        LoadSpells();

        _logger.Info("Loading Item File Paths...");
        LoadItems();

        _logger.Info("Loading Talent File Paths...");
        LoadTalents();

        _logger.Info("Loading Particle File Paths...");
        LoadParticles();

        _logger.Info("Loading LUA Script File Paths...");
        CacheLuaScripts();
    }

    static void CacheFilePath(Dictionary<string, string> dict, string path)
    {
        string fileName = Path.GetFileName(path).ToLowerInvariant();
        if (dict.TryGetValue(fileName, out string? value))
        {
            KeyCollision(fileName, value, path);
        }

        lock (dict)
        {
            dict[fileName] = path;
        }
    }


    private static void LoadItems()
    {
        int count = 0;
        Parallel.ForEach(Directory.EnumerateFiles(ContentManager.ItemsPath, ""), file =>
        {
            if (file.EndsWith(".ini"))
            {
                CacheFilePath(Cache.INIData, file);
                return;
            }

            if (file.EndsWith(".preload"))
            {
                CacheFilePath(Cache.PreloadScripts, file);
            }
            count++;
        });
    }

    private static void LoadTalents()
    {
        Parallel.ForEach(Directory.EnumerateFiles(ContentManager.TalentsPath, ""), file =>
        {
            if (file.EndsWith(".ini"))
            {
                CacheFilePath(Cache.INIData, file);
                return;
            }

            if (file.EndsWith(".preload"))
            {
                CacheFilePath(Cache.PreloadScripts, file);
            }
        });
    }

    private static void LoadParticles()
    {
        Parallel.ForEach([ContentManager.ParticlesPath, ContentManager.ParticlesSharedPath], dir =>
        {
            Parallel.ForEach(Directory.EnumerateFiles(dir, "*.troy", SearchOption.AllDirectories), file =>
            {
                CacheFilePath(Cache.INIData, file);
            });
        });
    }

    private static void LoadSpells()
    {
        Parallel.ForEach([ContentManager.SpellsPath, ContentManager.SpellsSharedPath], dir =>
        {
            Parallel.ForEach(Directory.EnumerateFiles(dir, "", SearchOption.AllDirectories), file =>
            {
                if (file.EndsWith(".ini"))
                {
                    CacheFilePath(Cache.INIData, file);
                }
                else if (file.EndsWith(".preload"))
                {
                    CacheFilePath(Cache.PreloadScripts, file);
                }
            });
        });
    }

    private static void LoadCharacters()
    {
        Parallel.ForEach(Directory.EnumerateDirectories(ContentManager.CharactersPath), dir =>
        {
            string characterName = Path.GetFileName(dir)!;
            string fileName = $"{characterName}.ini";
            string filePath = Path.Join(dir, fileName);

            if (File.Exists(filePath))
            {
                CacheFilePath(Cache.INIData, filePath);
            }

            fileName = $"CharScript{characterName}.preload";
            filePath = Path.Join(dir, fileName);
            if (!File.Exists(filePath))
            {
                filePath = Path.Join(dir, "Scripts", fileName);
                if (!File.Exists(filePath))
                {
                    goto LoadSkins;
                }
            }
            CacheFilePath(Cache.PreloadScripts, filePath);

            //Load Character spells
            string spellsPath = Path.Join(dir, "Spells");
            if (Directory.Exists(spellsPath))
            {
                Parallel.ForEach(Directory.EnumerateFiles(spellsPath, "", SearchOption.AllDirectories), file =>
                {
                    if (file.EndsWith(".ini"))
                    {
                        CacheFilePath(Cache.INIData, file);
                        return;
                    }

                    if (file.EndsWith(".preload"))
                    {
                        CacheFilePath(Cache.PreloadScripts, file);
                    }
                });
            }

        LoadSkins:
            var skinsPath = $"{dir}/Skins";
            if (!Directory.Exists(skinsPath))
            {
                skinsPath = $"{dir}/skins";
                if (!Directory.Exists(skinsPath))
                {
                    return;
                }
            }

            Parallel.ForEach(Directory.EnumerateDirectories(skinsPath), (skinDir) =>
            {
                foreach (var file in Directory.EnumerateFiles(skinDir, "*.troy"))
                {
                    CacheFilePath(Cache.INIData, file);
                }

                string partDirPath = Path.Join(skinDir, "Particles");
                if (!Directory.Exists(partDirPath))
                {
                    return;
                }

                foreach (string file in Directory.EnumerateFiles(partDirPath, "*.troy"))
                {
                    CacheFilePath(Cache.INIData, file);
                }
            });
        });
    }

    private static void CacheLuaScripts()
    {
        Parallel.ForEach(Directory.EnumerateFiles(ContentManager.ContentPath, "*.lua", SearchOption.AllDirectories), file =>
        {
            CacheFilePath(Cache.LuaScripts, file);
        });
    }


    public static string? GetDataPath(string name)
    {
        Cache.INIData.TryGetValue(name.ToLowerInvariant(), out string? file);
        return file;
    }

    private static void KeyCollision(string key, string oldPath, string newPath)
    {
        if (!Game.Config.EnableContentLogs)
        {
            return;
        }

        _logger.Warn($"ContentCache Key Collision: {key}\n" +
                     $"Old: {oldPath}\n" +
                     $"New: {newPath}");
    }

    private static void WriteToFile(string file)
    {
        var json = JsonConvert.SerializeObject(Cache, Formatting.Indented);
        File.WriteAllText(file, json);
        _logger.Info($"Generated Cache File: {file}");
    }
}