namespace Spells
{
    public class ZhonyasRing : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = false,
        };
    }
}
namespace Buffs
{
    public class ZhonyasRing : BuffScript
    {
        float abilityPower;
        public override void OnActivate()
        {
            float aP = GetFlatMagicDamageMod(owner);
            abilityPower = aP * 0.3f;
        }
        public override void OnUpdateStats()
        {
            IncFlatMagicDamageMod(owner, abilityPower);
        }
        public override void OnUpdateActions()
        {
            float aP = GetFlatMagicDamageMod(owner);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.MasteryBlastBuff)) > 0)
            {
                aP /= 1.04f;
            }
            aP -= abilityPower;
            abilityPower = aP * 0.3f;
        }
    }
}