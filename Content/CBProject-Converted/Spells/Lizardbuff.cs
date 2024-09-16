namespace Buffs
{
    public class Lizardbuff : BuffScript
    {
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            AddBuff((ObjAIBase)owner, attacker, new Buffs.BlessingoftheLizardElder(), 1, 1, 180, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
            SpellEffectCreate(out _, out _, "NeutralMonster_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, false);
        }
    }
}