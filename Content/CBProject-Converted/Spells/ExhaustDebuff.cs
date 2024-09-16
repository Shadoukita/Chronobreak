namespace Buffs
{
    public class ExhaustDebuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "summoner_banish.troy", },
            BuffName = "ExhaustDebuff",
            BuffTextureName = "35.dds",
        };
        float armorMod;
        public ExhaustDebuff(float armorMod = default)
        {
            this.armorMod = armorMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.armorMod);
            if (armorMod != 0)
            {
                ApplyAssistMarker(attacker, owner, 10);
            }
        }
        public override void OnUpdateStats()
        {
            if (armorMod != 0)
            {
                IncFlatArmorMod(owner, armorMod);
                IncFlatSpellBlockMod(owner, armorMod);
            }
        }
    }
}