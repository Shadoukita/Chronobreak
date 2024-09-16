namespace Spells
{
    public class BlindMonkWTwo : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            ChainMissileParameters = new()
            {
                CanHitCaster = false,
                CanHitSameTarget = false,
                CanHitSameTargetConsecutively = false,
                MaximumHits = 4,
            },
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 10, 15, 20, 25, 30 };
        float[] effect1 = { 0.05f, 0.1f, 0.15f, 0.2f, 0.25f };
        public override void SelfExecute()
        {
            float nextBuffVars_TotalArmor = effect0[level - 1];
            float nextBuffVars_LifestealPercent = effect1[level - 1];
            AddBuff(owner, owner, new Buffs.BlindMonkWTwo(nextBuffVars_TotalArmor, nextBuffVars_LifestealPercent), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.BlindMonkWManager)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.BlindMonkWManager), owner);
            }
        }
    }
}
namespace Buffs
{
    public class BlindMonkWTwo : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "BlindMonkIronWill",
            BuffTextureName = "BlindMonKWTwo.dds",
        };
        float totalArmor;
        float lifestealPercent;
        EffectEmitter turntostone;
        public BlindMonkWTwo(float totalArmor = default, float lifestealPercent = default)
        {
            this.totalArmor = totalArmor;
            this.lifestealPercent = lifestealPercent;
        }
        public override void OnActivate()
        {
            //RequireVar(this.totalArmor);
            //RequireVar(this.lifestealPercent);
            SpellEffectCreate(out turntostone, out _, "blindMonk_W_ironWill_armor.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false);
            IncFlatArmorMod(owner, totalArmor);
            IncPercentLifeStealMod(owner, lifestealPercent);
            IncPercentSpellVampMod(owner, lifestealPercent);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(turntostone);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, totalArmor);
            IncPercentLifeStealMod(owner, lifestealPercent);
            IncPercentSpellVampMod(owner, lifestealPercent);
        }
    }
}