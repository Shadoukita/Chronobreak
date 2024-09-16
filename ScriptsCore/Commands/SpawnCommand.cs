namespace Commands;

public class SpawnCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    public override string Command => "spawn";
    public override string Syntax => $"{Command} champblue [champion], champchaos [champion], minionsblue, minionschaos, regionblue [size, time], regionpurple [size, time]";

    public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
    {
        var split = arguments.ToLower().Split(' ');

        if (split.Length < 2)
        {
            SyntaxError();
            ShowSyntax();
        }
        else if (split[1].StartsWith("minions"))
        {
            split[1] = split[1].Replace("minions", "team_").ToUpper();
            if (!Enum.TryParse(split[1], out TeamId team) || team == TeamId.TEAM_NEUTRAL)
            {
                SyntaxError();
                ShowSyntax();
                return;
            }

            SpawnMinionsForTeam(team, champion);
        }
        else if (split[1].StartsWith("champ"))
        {
            split[1] = split[1].Replace("champ", "team_").ToUpper();
            if (!Enum.TryParse(split[1], out TeamId team) || team == TeamId.TEAM_NEUTRAL)
            {
                SyntaxError();
                ShowSyntax();
                return;
            }

            if (split.Length > 2)
            {
                string championModel = arguments.Split(' ')[2];

                if (ApiHandlers.GetCharData(championModel) is null)
                {
                    SyntaxMessage("Character Name: " + championModel + " invalid.");
                    ShowSyntax();
                    return;
                }

                SpawnChampForTeam(team, champion, championModel);
                return;
            }

            SpawnChampForTeam(team, champion, "Katarina");
        }
        else if (split[1].StartsWith("region"))
        {
            float size = 250.0f;
            float time = -1f;

            split[1] = split[1].Replace("region", "team_").ToUpper();
            if (!Enum.TryParse(split[1], out TeamId team) || team == TeamId.TEAM_NEUTRAL)
            {
                SyntaxError();
                ShowSyntax();
                return;
            }

            if (split.Length > 2)
            {
                size = float.Parse(arguments.Split(' ')[2]);

                if (split.Length > 3)
                {
                    time = float.Parse(arguments.Split(' ')[2]);
                }
            }
            else if (split.Length > 4)
            {
                SyntaxError();
                ShowSyntax();
                return;
            }

            SpawnRegionForTeam(team, champion, size, time);
        }
    }

    public void SpawnMinionsForTeam(TeamId team, Champion champion)
    {
        var championPos = champion.Position;
        var random = new Random();

        string teamName = team is TeamId.TEAM_ORDER ? "Blue" : "Red";

        Minion[] minions =
        [
            new(null, championPos, $"{teamName}_Minion_Basic", "MELEE", team, AIScript: "idle.lua"),
            new(null, championPos, $"{teamName}_Minion_MechCannon", "CANNON", team, AIScript: "idle.lua"),
            new(null, championPos, $"{teamName}_Minion_Wizard", $"CASTER", team, AIScript: "idle.lua"),
            new(null, championPos, $"{teamName}_Minion_MechMelee", $"SUPER", team, AIScript: "idle.lua")
        ];

        const int MaxDistance = 400;
        foreach (var minion in minions)
        {
            minion.SetPosition(championPos + new Vector2(random.Next(-MaxDistance, MaxDistance), random.Next(-MaxDistance, MaxDistance)), false);
            minion.PauseAI(true);
            minion.StopMovement();
            minion.UpdateMoveOrder(OrderType.Hold);
            ApiHandlers.AddGameObject(minion);
        }
    }

    public void SpawnChampForTeam(TeamId team, Champion champion, string model)
    {
        var clientInfoTemp = new ClientInfo("", team, 0, 0, 0, $"{model} Bot", ["SummonerHeal", "SummonerFlash"], -1);

        Game.PlayerManager.AddPlayer(clientInfoTemp);

        Champion c = new(model, clientInfoTemp, team: team);

        clientInfoTemp.Champion = c;

        c.SetPosition(champion.Position, false);
        c.StopMovement();
        c.UpdateMoveOrder(OrderType.Stop);
        c.Experience.LevelUp();
        IncPermanentFlatHPPoolMod(c, 10000 - c.Stats.HealthPoints.Total);
        
        ApiHandlers.AddGameObject(c);

        ChatManager.Send($"Spawned Bot {c.Name} as {c.Model} with NetID: {c.NetId}.");
    }

    public void SpawnRegionForTeam(TeamId team, Champion champion, float radius = 250.0f, float lifetime = -1.0f)
    {
        ApiFunctionManager.AddPosPerceptionBubble(champion.Position, radius, lifetime, team, true);
    }
}