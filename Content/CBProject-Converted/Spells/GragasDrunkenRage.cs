namespace Spells
{
    public class GragasDrunkenRage : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 50f, 50f, 50f, 50f, 50f, },
            ChannelDuration = 1f,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { 15, 22.5f, 30, 37.5f, 45 };
        int[] effect1 = { 30, 40, 50, 60, 70 };
        float[] effect2 = { 0.1f, 0.12f, 0.14f, 0.16f, 0.18f };
        public override void ChannelingStart()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.GragasBodySlamSelfSlow)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.GragasBodySlamSelfSlow), owner);
            }
            float nextBuffVars_ManaTick = effect0[level - 1];
            AddBuff(owner, owner, new Buffs.GragasDrunkenRage(nextBuffVars_ManaTick), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.HEAL, 0, true, false, false);
        }
        public override void ChannelingSuccessStop()
        {
            int nextBuffVars_DamageIncrease = effect1[level - 1];
            float nextBuffVars_DamageReduction = effect2[level - 1];
            AddBuff(owner, owner, new Buffs.GragasDrunkenRageSelf(nextBuffVars_DamageIncrease, nextBuffVars_DamageReduction), 1, 1, 20, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            SpellBuffRemove(owner, nameof(Buffs.GragasDrunkenRage), owner);
        }
        public override void ChannelingCancelStop()
        {
            SpellBuffRemove(owner, nameof(Buffs.GragasDrunkenRage), owner);
        }
    }
}
namespace Buffs
{
    public class GragasDrunkenRage : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "L_BUFFBONE_GLB_HAND_LOC", "R_BUFFBONE_GLB_HAND_LOC", },
            AutoBuffActivateEffect = new[] { "gragas_drunkenRage_attack_buf.troy", "gragas_drunkenRage_attack_buf.troy", },
            BuffName = "GragasDrunkenRage",
            BuffTextureName = "GragasDrunkenRage.dds",
        };
        float manaTick;
        public GragasDrunkenRage(float manaTick = default)
        {
            this.manaTick = manaTick;
        }
        public override void OnActivate()
        {
            //RequireVar(this.manaTick);
            IncPAR(owner, manaTick, PrimaryAbilityResourceType.MANA);
        }
        public override void OnDeactivate(bool expired)
        {
            IncPAR(owner, manaTick, PrimaryAbilityResourceType.MANA);
            SpellBuffRemove(owner, nameof(Buffs.GragasDrunkenRage), (ObjAIBase)owner);
        }
    }
}