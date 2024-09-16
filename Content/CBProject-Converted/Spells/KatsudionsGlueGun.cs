namespace Spells
{
    public class KatsudionsGlueGun : SpellScript
    {
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (GetBuffCountFromCaster(target, owner, nameof(Buffs.Slow)) > 0)
            {
                SpellBuffRemove(target, nameof(Buffs.Slow), attacker);
                DebugSay(owner, "DISPELL SLOW !!");
            }
            else
            {
                float nextBuffVars_MoveSpeedMod = -0.5f;
                DebugSay(owner, "TARGET SLOWED 50% !!");
                AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false);
            }
        }
    }
}