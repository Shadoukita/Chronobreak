namespace Spells
{
    public class ViktorDeathRayDOT : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class ViktorDeathRayDOT : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Viktor_Laserhit.troy", "", },
            BuffName = "ViktorDeathRayBurning",
            BuffTextureName = "ViktorDeathRayAUG.dds",
        };
        float damageForDot;
        float lastTimeExecuted;
        public ViktorDeathRayDOT(float damageForDot = default)
        {
            this.damageForDot = damageForDot;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageForDot);
        }
        public override void OnUpdateActions()
        {
            ObjAIBase caster = GetBuffCasterUnit();
            if (ExecutePeriodically(1, ref lastTimeExecuted, true))
            {
                ApplyDamage(caster, owner, damageForDot, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
            }
        }
    }
}