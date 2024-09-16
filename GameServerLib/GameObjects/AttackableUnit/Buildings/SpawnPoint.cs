using System.Collections.Generic;
using System.Numerics;
using GameServerCore.Enums;
using Chronobreak.GameServer.Content;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;

namespace Chronobreak.GameServer.GameObjects.AttackableUnits.Buildings;

public class SpawnPoint : ObjBuilding
{
    float NextLifeUpdate;

    public SpawnPoint(
        string name,
        Vector2 position,
        TeamId team
        ) :
        base(name, "", 0, position, 0, 0, team, null)
    {
        //Check
        SetStatus(StatusFlags.Targetable, false);
        Stats.IsTargetableToTeam = SpellDataFlags.NonTargetableAll;
        SetStatus(StatusFlags.Invulnerable, true);
    }

    private readonly Dictionary<Champion, bool> _inRegenPool = [];

    internal override void Update()
    {
        NextLifeUpdate -= Game.Time.DeltaTime;
        if (NextLifeUpdate <= 0)
        {
            float regenPercent = Game.Config.GameConfig.GameMode is "ARAM" ? GlobalData.SpawnPointVariables.HealthRegenPercentARAM : GlobalData.SpawnPointVariables.HealthRegenPercent;
            foreach (Champion champion in Game.ObjectManager.GetChampionsInRangeFromTeam(Position, GlobalData.SpawnPointVariables.RegenRadius, Team, true))
            {
                _inRegenPool[champion] = true;

                champion.TakeHeal(new(champion, champion.Stats.HealthPoints.Total * regenPercent));
                if (champion.Stats.PrimaryAbilityResourceType is PrimaryAbilityResourceType.MANA or PrimaryAbilityResourceType.Energy)
                {
                    champion.RestorePAR(champion.Stats.ManaPoints.Total * regenPercent);
                }
            }

            foreach (var (champion, currentState) in _inRegenPool)
            {
                _inRegenPool[champion] = false;

                bool lastState = champion.InPool;
                champion.InPool = currentState;

                if (!currentState && lastState)
                {
                    champion.ItemInventory.ClearUndoHistory();
                }
            }
            NextLifeUpdate = GlobalData.SpawnPointVariables.RegenTickInterval * 1000;
        }
    }

    internal override void Sync(int userId, TeamId team, bool visible, bool forceSpawn = false)
    {
    }

    protected override void OnSpawn(int userId, TeamId team, bool doVision)
    {
    }
}