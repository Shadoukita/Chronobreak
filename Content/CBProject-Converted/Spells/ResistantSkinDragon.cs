namespace Buffs
{
    public class ResistantSkinDragon : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Resistant Skin Dragon",
            BuffTextureName = "GreenTerror_ChitinousExoplates.dds",
            NonDispellable = true,
        };
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            bool returnValue = true;
            if (owner.Team != attacker.Team)
            {
                if (scriptName == nameof(Buffs.GlobalWallPush))
                {
                    returnValue = false;
                }
                else if (type == BuffType.FEAR)
                {
                    returnValue = false;
                }
                else if (type == BuffType.CHARM)
                {
                    returnValue = false;
                }
                else if (type == BuffType.SILENCE)
                {
                    returnValue = false;
                }
                else if (type == BuffType.SLEEP)
                {
                    returnValue = false;
                }
                else if (type == BuffType.SLOW)
                {
                    returnValue = false;
                }
                else if (type == BuffType.SNARE)
                {
                    returnValue = false;
                }
                else if (type == BuffType.STUN)
                {
                    returnValue = false;
                }
                else if (type == BuffType.TAUNT)
                {
                    returnValue = false;
                }
                else if (type == BuffType.BLIND)
                {
                    returnValue = false;
                }
                else if (type == BuffType.SUPPRESSION)
                {
                    returnValue = false;
                }
                else if (type == BuffType.SHRED)
                {
                    returnValue = false;
                }
                else
                {
                    returnValue = true;
                }
            }
            else
            {
                returnValue = true;
            }
            return returnValue;
        }
    }
}