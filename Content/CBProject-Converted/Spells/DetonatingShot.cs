namespace Spells
{
    public class DetonatingShot : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            SpellFXOverrideSkins = new[] { "RocketTristana", },
        };
        int[] effect0 = { 22, 28, 34, 40, 46 };
        int[] effect1 = { 5, 5, 5, 5, 5 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            IssueOrder(owner, OrderType.AttackTo, default, target);
            SpellBuffRemove(owner, nameof(Buffs.DetonatingShot), owner, 0);
            int nextBuffVars_dotdmg = effect0[level - 1];
            AddBuff(attacker, target, new Buffs.ExplosiveShotDebuff(nextBuffVars_dotdmg), 1, 1, effect1[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 1, true, false, false);
            AddBuff((ObjAIBase)target, target, new Buffs.Internal_50MS(), 1, 1, effect1[level - 1], BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, target, new Buffs.GrievousWound(), 1, 1, effect1[level - 1], BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class DetonatingShot : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Detonating Shot",
            BuffTextureName = "Tristana_ExplosiveShot.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        int[] effect0 = { 50, 75, 100, 125, 150 };
        public override void OnKill(AttackableUnit target)
        {
            if (GetBuffCountFromCaster(target, owner, nameof(Buffs.DetonatingShot_Target)) > 0)
            {
                SpellEffectCreate(out _, out _, "DetonatingShot_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, false, false, false, false);
                int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position3D, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    BreakSpellShields(unit);
                    SpellEffectCreate(out _, out _, "tristana_explosiveShot_unit_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                    ApplyDamage((ObjAIBase)owner, unit, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.25f, 1, false, false, attacker);
                }
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is ObjAIBase && owner.Team != target.Team)
            {
                AddBuff((ObjAIBase)owner, target, new Buffs.DetonatingShot_Target(), 1, 1, 0.1f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}