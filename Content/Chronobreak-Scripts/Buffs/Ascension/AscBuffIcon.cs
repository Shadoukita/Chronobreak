namespace Buffs
{
    internal class AscBuffIcon : BuffScript
    {
        public override BuffScriptMetaData BuffMetaData { get; } = new()
        {
            BuffType = BuffType.COMBAT_ENCHANCER,
            BuffAddType = BuffAddType.REPLACE_EXISTING
        };

        public override void OnUpdateStats()
        {
            //TODO: Add 100% mana/energy cost reduction and 50% Health cost reduction

            int level = GetLevel(target);
            IncFlatHPPoolMod(target, 50.0f * level);
            IncFlatPhysicalDamageMod(target, 12.0f * level);
            IncFlatMagicDamageMod(target, 12.0f * level);
            IncPercentArmorPenetrationMod(target, 0.15f);
            IncPercentMagicPenetrationMod(target, 0.15f);
            IncPercentCooldownMod(target, 0.25f);
            IncScaleSkinCoef(0.5f, target);

            //TODO: Add buff tooltip updates when we find out why they can be updated ATM.
        }
    }
}