namespace Spells
{
    public class TormentedSoilDebuff : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class TormentedSoilDebuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "TormentedSoilDebuff",
            BuffTextureName = "FallenAngel_TormentedSoil.dds",
        };
        Vector3 targetPos;
        float mRminus;
        public TormentedSoilDebuff(Vector3 targetPos = default, float mRminus = default)
        {
            this.targetPos = targetPos;
            this.mRminus = mRminus;
        }
        public override void OnActivate()
        {
            //RequireVar(this.mRminus);
        }
        public override void OnUpdateStats()
        {
            IncFlatSpellBlockMod(owner, mRminus);
            if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.TormentedSoil)) == 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
            else
            {
                Vector3 targetPos = this.targetPos;
                Vector3 ownerPos = GetUnitPosition(owner);
                float dist = DistanceBetweenPoints(targetPos, ownerPos);
                if (dist >= 308)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}