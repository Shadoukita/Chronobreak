namespace Spells
{
    public class YorickSummonSpectral : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
        };
        float[] effect0 = { 0.15f, 0.2f, 0.25f, 0.3f, 0.35f };
        int[] effect1 = { 8, 16, 24, 32, 40 };
        public override void SelfExecute()
        {
            int level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            Vector3 targetPos = GetSpellTargetPos(spell);
            TeamId teamID = GetTeamID_CS(owner);
            Minion other1 = SpawnMinion("Clyde", "YorickSpectralGhoul", "YorickPHPet.lua", targetPos, teamID, false, false, true, false, false, false, 0, false, false, (Champion)owner);
            float nextBuffVars_MovementSpeedPercent = effect0[level - 1];
            float nextBuffVars_AttackDamageMod = effect1[level - 1];
            AddBuff(other1, attacker, new Buffs.YorickSummonSpectral(nextBuffVars_AttackDamageMod, nextBuffVars_MovementSpeedPercent), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, other1, new Buffs.YorickSpectralLogic(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.YorickActiveSpectral(nextBuffVars_MovementSpeedPercent), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class YorickSummonSpectral : BuffScript
    {
        EffectEmitter spectraFX; // UNUSED
        float attackDamageMod;
        float movementSpeedPercent;
        bool isDead; // UNUSED
        float lastTimeExecuted;
        public YorickSummonSpectral(float attackDamageMod = default, float movementSpeedPercent = default)
        {
            this.attackDamageMod = attackDamageMod;
            this.movementSpeedPercent = movementSpeedPercent;
        }
        public override void OnActivate()
        {
            SetGhosted(owner, true);
            SpellEffectCreate(out spectraFX, out _, "YorickPHSpectral.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, attacker, default, default, attacker, default, default, false, default, default, false, false);
            //RequireVar(this.attackDamageMod);
            //RequireVar(this.movementSpeedPercent);
            isDead = false;
        }
        public override void OnDeactivate(bool expired)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.YorickActiveSpectral)) > 0)
            {
                SpellBuffClear(owner, nameof(Buffs.YorickActiveSpectral));
            }
            Vector3 ghoulPosition = GetUnitPosition(attacker);
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "yorick_spectralGhoul_death.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ghoulPosition, owner, default, ghoulPosition, true, default, default, false, false);
            ApplyDamage(attacker, attacker, 9999, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 0, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(attacker, attackDamageMod);
            IncPercentMovementSpeedMod(attacker, movementSpeedPercent);
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
                foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, attacker.Position3D, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.YorickSpectralPrimaryTarget), true))
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