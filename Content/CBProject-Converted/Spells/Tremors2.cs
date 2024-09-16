namespace Spells
{
    public class Tremors2 : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 65, 130, 195 };
        int[] effect1 = { 8, 8, 8, 8, 8 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float nextBuffVars_TremDamage = effect0[level - 1];
            AddBuff(attacker, target, new Buffs.Tremors2(nextBuffVars_TremDamage), 1, 1, effect1[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class Tremors2 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Tremors2",
            BuffTextureName = "Armordillo_RecklessCharge.dds",
        };
        float tremDamage;
        EffectEmitter tremorsFx;
        float lastTimeExecuted;
        public Tremors2(float tremDamage = default)
        {
            this.tremDamage = tremDamage;
        }
        public override void OnActivate()
        {
            //RequireVar(this.tremDamage);
            SpellEffectCreate(out tremorsFx, out _, "Tremors_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectBuildings | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectTurrets, default, true))
            {
                ApplyDamage(attacker, unit, tremDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.3f, 0, false, false, attacker);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(tremorsFx);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectBuildings | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectTurrets, default, true))
                {
                    ApplyDamage(attacker, unit, tremDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.3f, 0, false, false, attacker);
                }
            }
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            SpellEffectRemove(tremorsFx);
        }
    }
}