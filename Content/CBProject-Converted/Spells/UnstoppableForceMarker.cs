namespace Buffs
{
    public class UnstoppableForceMarker : BuffScript
    {
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            bool returnValue = true;
            if (maxStack == 76)
            {
                returnValue = false;
            }
            if (type == BuffType.SNARE)
            {
                Say(owner, "game_lua_Ragnarok");
                returnValue = false;
            }
            if (type == BuffType.CHARM)
            {
                Say(owner, "game_lua_Ragnarok");
                returnValue = false;
            }
            if (type == BuffType.SLOW)
            {
                Say(owner, "game_lua_Ragnarok");
                returnValue = false;
            }
            if (type == BuffType.FEAR)
            {
                Say(owner, "game_lua_Ragnarok");
                returnValue = false;
            }
            if (type == BuffType.SLEEP)
            {
                Say(owner, "game_lua_Ragnarok");
                returnValue = false;
            }
            if (type == BuffType.STUN)
            {
                Say(owner, "game_lua_Ragnarok");
                returnValue = false;
            }
            if (type == BuffType.TAUNT)
            {
                Say(owner, "game_lua_Ragnarok");
                returnValue = false;
            }
            if (type == BuffType.SUPPRESSION)
            {
                Say(owner, "game_lua_Ragnarok");
                returnValue = false;
            }
            return returnValue;
        }
    }
}