namespace Spells
{
    public class JaxEmpower : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 25, 35, 45, 55, 65 };
        public override void SelfExecute()
        {
            float nextBuffVars_DamagePerStack = effect0[level - 1];
            AddBuff(owner, owner, new Buffs.JaxEmpower(nextBuffVars_DamagePerStack), 1, 1, 8, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class JaxEmpower : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "EmpowerCleave",
            BuffTextureName = "Armsmaster_Empower.dds",
        };
        EffectEmitter particle;
        float damagePerStack;
        float lastTimeExecuted;
        public JaxEmpower(float damagePerStack = default)
        {
            this.damagePerStack = damagePerStack;
        }
        public override void OnActivate()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.JaxRelentlessAssaultMarker)) == 0)
            {
                OverrideAutoAttack(1, SpellSlotType.ExtraSlots, owner, 1, false);
            }
            SpellEffectCreate(out particle, out _, "Empower_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "weaponstreak", default, owner, default, default, false, false, false, false, false);
            //RequireVar(this.damagePerStack);
        }
        public override void OnDeactivate(bool expired)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.JaxRelentlessAssaultMarker)) > 0)
            {
                OverrideAutoAttack(2, SpellSlotType.ExtraSlots, owner, 1, false);
            }
            else
            {
                RemoveOverrideAutoAttack(owner, false);
            }
            SpellEffectRemove(particle);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, true))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.JaxEmpowerSeal(), 3, 1, 1, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            SpellEffectCreate(out _, out _, "TiamatMelee_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, false, false, false, false);
            int count = GetBuffCountFromAll(owner, nameof(Buffs.EmpowerCleave));
            float damageBonus = damagePerStack * count;
            float radiusOfCleave = 125 * count;
            float aoEDamage = damageAmount * 0.6f;
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position3D, radiusOfCleave, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if (target != unit)
                {
                    ApplyDamage(attacker, unit, aoEDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
                }
            }
            ApplyDamage(attacker, target, damageBonus, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
            SpellBuffRemove(owner, nameof(Buffs.JaxEmpower), (ObjAIBase)owner, 0);
            SpellBuffRemoveStacks(owner, (ObjAIBase)owner, nameof(Buffs.JaxEmpowerSeal), 0);
        }
    }
}