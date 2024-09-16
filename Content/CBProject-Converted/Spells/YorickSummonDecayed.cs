namespace Spells
{
    public class YorickSummonDecayed : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
        };
        float[] effect0 = { -0.1f, -0.125f, -0.15f, -0.175f, -0.2f };
        int[] effect1 = { 60, 95, 130, 165, 200 };
        float[] effect2 = { -0.2f, -0.25f, -0.3f, -0.35f, -0.4f };
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            TeamId teamID = GetTeamID_CS(owner);
            Minion other1 = SpawnMinion("Inky", "YorickDecayedGhoul", "YorickPHPet.lua", targetPos, teamID, false, false, true, false, false, false, 0, false, false, (Champion)owner);
            AddBuff(other1, attacker, new Buffs.YorickSummonDecayed(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            int level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float nextBuffVars_MoveSpeedMod = effect0[level - 1];
            AddBuff(attacker, other1, new Buffs.YorickDecayedDiseaseCloud(nextBuffVars_MoveSpeedMod), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, other1, new Buffs.YorickDecayedLogic(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            SpellEffectCreate(out _, out _, "yorick_necroExplosion.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, other1, default, default, other1, default, default, false, default, default, false, false);
            float baseDamage = effect1[level - 1];
            float yorickAD = GetFlatPhysicalDamageMod(owner); // UNUSED
            nextBuffVars_MoveSpeedMod = effect2[level - 1];
            foreach (AttackableUnit unit in GetUnitsInArea(owner, other1.Position3D, 250, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                SpellEffectCreate(out _, out _, "yorick_necroExplosion_unit_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                AddBuff(owner, unit, new Buffs.YorickDecayedSlow(nextBuffVars_MoveSpeedMod), 100, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                ApplyDamage(owner, unit, baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, 0, false, false, owner);
            }
        }
    }
}
namespace Buffs
{
    public class YorickSummonDecayed : BuffScript
    {
        bool isDead; // UNUSED
        float lastTimeExecuted;
        public override void OnActivate()
        {
            SetGhosted(attacker, true);
            bool nearbyChampion = false; // UNUSED
            bool checkBuilding = true; // UNUSED
            isDead = false;
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 ghoulPosition = GetUnitPosition(attacker);
            SpellEffectCreate(out _, out _, "yorick_necroExplosion_deactivate.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ghoulPosition, owner, default, default, true, default, default, false, false);
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
                bool nearbyChampion = false;
                bool checkBuilding = true;
                foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, attacker.Position3D, 850, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
                {
                    ApplyTaunt(unit, attacker, 1.5f);
                    nearbyChampion = true;
                    checkBuilding = false;
                }
                if (!nearbyChampion)
                {
                    foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, attacker.Position3D, 750, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, 1, default, true))
                    {
                        ApplyTaunt(unit, attacker, 1.5f);
                        checkBuilding = false;
                    }
                }
                if (checkBuilding)
                {
                    foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, attacker.Position3D, 750, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectTurrets, 1, default, true))
                    {
                        ApplyTaunt(unit, attacker, 1.5f);
                    }
                }
            }
        }
    }
}