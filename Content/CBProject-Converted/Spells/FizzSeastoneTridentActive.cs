namespace Buffs
{
    public class FizzSeastoneTridentActive : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "FizzMalison",
            BuffTextureName = "Irelia_EquilibriumStrike.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
            SpellToggleSlot = 2,
        };
        public override void OnActivate()
        {
            IncAcquisitionRangeMod(owner, 100);
            OverrideAnimation("Attack1", "Crit", owner);
            OverrideAnimation("Attack2", "Crit", owner);
            CancelAutoAttack(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            IncAcquisitionRangeMod(owner, 0);
            ClearOverrideAnimation("Attack1", owner);
        }
        public override void OnUpdateStats()
        {
            IncAcquisitionRangeMod(owner, 100);
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            AddBuff((ObjAIBase)target, target, new Buffs.Internal_50MS(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, target, new Buffs.GrievousWound(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            Say(owner, "YO!");
        }
    }
}