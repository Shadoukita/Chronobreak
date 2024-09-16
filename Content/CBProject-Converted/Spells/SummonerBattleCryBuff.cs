namespace Buffs
{
    public class SummonerBattleCryBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "root", },
            AutoBuffActivateEffect = new[] { "OntheHunt_buf.troy", "", },
            BuffName = "SummonerBattleCry",
            BuffTextureName = "Summoner_BattleCry.dds",
        };
        float allyAPMod;
        float allyAttackSpeedMod;
        public SummonerBattleCryBuff(float allyAPMod = default, float allyAttackSpeedMod = default)
        {
            this.allyAPMod = allyAPMod;
            this.allyAttackSpeedMod = allyAttackSpeedMod;
        }
        public override void OnActivate()
        {
            int level = GetLevel(owner); // UNUSED
            //RequireVar(this.allyAPMod);
            //RequireVar(this.allyAttackSpeedMod);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnUpdateStats()
        {
            IncScaleSkinCoef(0.1f, owner);
            IncFlatMagicDamageMod(owner, allyAPMod);
            IncPercentAttackSpeedMod(owner, allyAttackSpeedMod);
        }
    }
}