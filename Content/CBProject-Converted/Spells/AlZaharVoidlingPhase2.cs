namespace Buffs
{
    public class AlZaharVoidlingPhase2 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "AlZaharVoidlingPhase2",
            BuffTextureName = "AlZahar_SummonVoidling.dds",
        };
        float damageInc;
        float armorInc;
        public override void OnActivate()
        {
            SpellEffectCreate(out _, out _, "voidlingtransform.prt", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            float baseArmor = GetArmor(owner);
            float baseDamage = GetTotalAttackDamage(owner);
            damageInc = baseDamage * 0.5f;
            armorInc = baseArmor * 0.5f;
            IncScaleSkinCoef(0.5f, owner);
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, damageInc);
            IncFlatArmorMod(owner, armorInc);
            IncScaleSkinCoef(0.5f, owner);
        }
    }
}