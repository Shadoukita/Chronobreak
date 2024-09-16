namespace Buffs
{
    public class SwainTacticalSupremacy : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SwainTacticalSupremacy",
            BuffTextureName = "GSB_invulnerability.dds",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        float[] effect0 = { 0.95f, 1.04f, 1.12f, 1.21f, 1.3f, 1.39f, 1.48f, 1.57f, 1.66f, 1.75f, 1.84f, 1.93f, 2.02f, 2.11f, 2.2f, 2.29f, 2.38f, 2.47f };
        public override void OnKill(AttackableUnit target)
        {
            int level = GetLevel(owner);
            float nextBuffVars_BaseManaRegen = effect0[level - 1]; // UNUSED
            AddBuff((ObjAIBase)owner, owner, new Buffs.SwainDampeningFieldMana(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
        }
    }
}