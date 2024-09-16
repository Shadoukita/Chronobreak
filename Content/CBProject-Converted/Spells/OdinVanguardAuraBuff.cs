namespace Buffs
{
    public class OdinVanguardAuraBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "VanguardBuff",
            BuffTextureName = "Summoner_rally.dds",
        };
        float defenseMod;
        public override void OnActivate()
        {
            int ownerLevel = GetLevel(attacker);
            defenseMod = 5 * ownerLevel;
            defenseMod += 10;
            ApplyAssistMarker(attacker, owner, 10);
            SetBuffToolTipVar(1, defenseMod);
        }
        public override void OnUpdateStats()
        {
            IncFlatSpellBlockMod(owner, defenseMod);
            IncFlatArmorMod(owner, defenseMod);
        }
    }
}