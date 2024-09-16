namespace Buffs
{
    public class InnervatingLocketBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Innervating Locket",
            BuffTextureName = "3032_Innervating_Locket.dds",
        };
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                if (target == attacker)
                {
                    IncPAR(owner, 10, PrimaryAbilityResourceType.MANA);
                }
            }
        }
    }
}