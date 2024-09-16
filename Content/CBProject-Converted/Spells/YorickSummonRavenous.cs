namespace Spells
{
    public class YorickSummonRavenous : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
        };
        public override void SelfExecute()
        {
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 targetPos = GetSpellTargetPos(spell);
            Minion other1 = SpawnMinion("Blinky", "YorickRavenousGhoul", "YorickPHPet.lua", targetPos, teamID, false, false, true, false, false, false, 0, false, false, (Champion)owner);
            AddBuff(other1, attacker, new Buffs.YorickSummonRavenous(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, other1, new Buffs.YorickRavenousLogic(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class YorickSummonRavenous : BuffScript
    {
        bool isDead; // UNUSED
        float lastTimeExecuted;
        public override void OnActivate()
        {
            SetGhosted(attacker, true);
            isDead = false;
        }
        public override void OnDeactivate(bool expired)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.YorickActiveRavenous)) > 0)
            {
                SpellBuffClear(owner, nameof(Buffs.YorickActiveRavenous));
            }
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 ghoulPosition = GetUnitPosition(attacker);
            SpellEffectCreate(out _, out _, "yorick_ravenousGhoul_death.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ghoulPosition, owner, default, default, true, default, default, false, false);
            ApplyDamage(attacker, attacker, 9999, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 0, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            float currentHealth = GetHealth(attacker, PrimaryAbilityResourceType.MANA);
            if (currentHealth <= 0)
            {
                isDead = true;
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, true))
            {
                foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, attacker.Position3D, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.YorickRavenousPrimaryTarget), true))
                {
                    ApplyTaunt(unit, attacker, 1.5f);
                }
                bool isTaunted = GetTaunted(attacker);
                if (!isTaunted)
                {
                    bool nearbyChampion = false;
                    bool checkBuilding = true;
                    foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, attacker.Position3D, 1050, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
                    {
                        ApplyTaunt(unit, attacker, 1.5f);
                        nearbyChampion = true;
                        checkBuilding = false;
                    }
                    if (!nearbyChampion)
                    {
                        foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, attacker.Position3D, 750, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectMinions, 1, default, true))
                        {
                            ApplyTaunt(unit, attacker, 1.5f);
                            checkBuilding = false;
                        }
                    }
                    if (checkBuilding)
                    {
                        foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, attacker.Position3D, 750, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectBuildings, 1, default, true))
                        {
                            ApplyTaunt(unit, attacker, 1.5f);
                        }
                    }
                }
            }
        }
    }
}