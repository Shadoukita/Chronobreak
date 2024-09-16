namespace Spells
{
    public class AbponsPaperFan : SpellScript
    {
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (GetBuffCountFromCaster(target, owner, nameof(Buffs.Stun)) > 0)
            {
                SpellBuffRemove(target, nameof(Buffs.Stun), attacker);
                DebugSay(owner, "DISPELL STUN !!");
            }
            else
            {
                float nextBuffVars_ShieldHealth = 1000; // UNUSED
                DebugSay(owner, "TWHAP!  Target STUNNED !!");
                AddBuff(attacker, target, new Buffs.Stun(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false);
            }
        }
    }
}