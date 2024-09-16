namespace Spells
{
    public class OffensiveMasteryBuff : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class OffensiveMasteryBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "FortifyBuff",
            BuffTextureName = "Summoner_fortify.dds",
        };
        int level;
        int[] effect0 = { 2, 4 };
        public OffensiveMasteryBuff(int level = default)
        {
            this.level = level;
        }
        public override void OnActivate()
        {
            //RequireVar(this.level);
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            int level = this.level;
            if (target is ObjAIBase && target is not BaseTurret && target is not Champion)
            {
                float damageBonus = effect0[level - 1];
                damageAmount += damageBonus;
            }
        }
    }
}