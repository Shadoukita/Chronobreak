namespace Buffs
{
    public class Dragonbuff : BuffScript
    {
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            AddBuff((ObjAIBase)owner, attacker, new Buffs.FireoftheGreatDragon(), 1, 1, 180, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
        }
    }
}