namespace Spells
{
    public class MordekaiserCreepingDeath : SpellScript
    {
        int[] effect0 = { 24, 38, 52, 66, 80 };
        int[] effect1 = { 10, 15, 20, 25, 30 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float nextBuffVars_DamagePerTick = effect0[level - 1];
            float nextBuffVars_DefenseStats = effect1[level - 1];
            if (GetBuffCountFromCaster(target, owner, nameof(Buffs.MordekaiserCreepingDeathCheck)) > 0)
            {
                if (target.Team == owner.Team)
                {
                    AddBuff(owner, target, new Buffs.MordekaiserCreepingDeath(nextBuffVars_DamagePerTick, nextBuffVars_DefenseStats), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
            else
            {
                if (target.Team != owner.Team)
                {
                    AddBuff((ObjAIBase)target, owner, new Buffs.MordekaiserCreepingDeathDebuff(nextBuffVars_DamagePerTick), 100, 1, 0.001f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
    }
}
namespace Buffs
{
    public class MordekaiserCreepingDeath : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "MordekaiserCreepingDeathBuff",
            BuffTextureName = "MordekaiserCreepingDeath.dds",
        };
        float damagePerTick;
        float defenseStats;
        EffectEmitter b;
        float lastTimeExecuted;
        public MordekaiserCreepingDeath(float damagePerTick = default, float defenseStats = default)
        {
            this.damagePerTick = damagePerTick;
            this.defenseStats = defenseStats;
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            //RequireVar(this.damagePerTick);
            //RequireVar(this.defenseStats);
            ApplyAssistMarker(attacker, owner, 10);
            IncPermanentFlatArmorMod(owner, defenseStats);
            IncPermanentFlatSpellBlockMod(owner, defenseStats);
            if (owner != attacker)
            {
                int mordekaiserSkinID = GetSkinID(attacker);
                if (mordekaiserSkinID == 1)
                {
                    SpellEffectCreate(out b, out _, "mordekaiser_creepingDeath_auraGold.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
                }
                else if (mordekaiserSkinID == 2)
                {
                    SpellEffectCreate(out b, out _, "mordekaiser_creepingDeath_auraRed.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
                }
                else
                {
                    SpellEffectCreate(out b, out _, "mordekaiser_creepingDeath_aura.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
                }
            }
            else
            {
                SpellEffectCreate(out b, out _, "mordekaiser_creepingDeath_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            if (GetBuffCountFromCaster(owner, attacker, nameof(Buffs.MordekaiserCreepingDeathCheck)) > 0)
            {
                SpellBuffClear(owner, nameof(Buffs.MordekaiserCreepingDeathCheck));
            }
            float defenseStats = this.defenseStats * -1;
            IncPermanentFlatArmorMod(owner, defenseStats);
            IncPermanentFlatSpellBlockMod(owner, defenseStats);
            SpellEffectRemove(b);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, true))
            {
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    float nextBuffVars_DamagePerTick = damagePerTick;
                    AddBuff((ObjAIBase)unit, attacker, new Buffs.MordekaiserCreepingDeathDebuff(nextBuffVars_DamagePerTick), 100, 1, 0.001f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
    }
}