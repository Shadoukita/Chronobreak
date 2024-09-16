namespace Buffs
{
    public class RallyingBanner : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Stark's Fervor",
            BuffTextureName = "3050_Rallying_Banner.dds",
        };
        float armorMod;
        public RallyingBanner(float armorMod = default)
        {
            this.armorMod = armorMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.armorMod);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, armorMod);
            if (IsDead(attacker))
            {
                SpellBuffRemoveCurrent(owner);
            }
            else
            {
                float dist = DistanceBetweenObjects(attacker, owner);
                if (dist >= 1200)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}