namespace Buffs
{
    public class SpiritFireArmorReduction : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SpiritFire",
            BuffTextureName = "Nasus_SpiritFire.dds",
        };
        Vector3 targetPos;
        float armorReduction;
        public SpiritFireArmorReduction(Vector3 targetPos = default, float armorReduction = default)
        {
            this.targetPos = targetPos;
            this.armorReduction = armorReduction;
        }
        public override void OnActivate()
        {
            //RequireVar(this.armorReduction);
            //RequireVar(this.targetPos);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, armorReduction);
            if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.SpiritFireAoE)) == 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
            else
            {
                Vector3 targetPos = this.targetPos;
                Vector3 ownerPos = GetUnitPosition(owner);
                float dist = DistanceBetweenPoints(targetPos, ownerPos);
                if (dist >= 450)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}