namespace Buffs
{
    public class Hardening : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Hardening",
            BuffTextureName = "GreenTerror_ChitinousExoplates.dds",
        };
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            float hardnessPercent = GetPercentHardnessMod(owner);
            float percentReduction = 1 - hardnessPercent;
            if (owner.Team != attacker.Team)
            {
                if(
                    type
                    is BuffType.SNARE
                    or BuffType.SLOW
                    or BuffType.FEAR
                    or BuffType.CHARM
                    or BuffType.SLEEP
                    or BuffType.STUN
                    or BuffType.TAUNT
                    or BuffType.SILENCE
                    or BuffType.BLIND
                ){
                    duration *= percentReduction;
                }
                duration = Math.Max(0.3f, duration);
            }
            return true;
        }
        public override void OnUpdateStats()
        {
            float percentReduction = GetPercentHardnessMod(owner);
                  percentReduction *= 100;
            if (percentReduction >= 0)
            {
                SetBuffToolTipVar(1, percentReduction);
            }
            else
            {
                SetBuffToolTipVar(1, 0);
            }
        }
    }
}