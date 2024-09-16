namespace Spells
{
    public class KriggersBarrier : SpellScript
    {
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (GetBuffCountFromCaster(target, owner, nameof(Buffs.BlackShield)) > 0)
            {
                SpellBuffRemove(target, nameof(Buffs.BlackShield), attacker);
                DebugSay(owner, "DISPELL BlackShield");
            }
            else
            {
                float nextBuffVars_ShieldHealth = 1000;
                DebugSay(owner, "ADD BlackShield 1000 Health");
                AddBuff(attacker, target, new Buffs.BlackShield(nextBuffVars_ShieldHealth), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.SPELL_IMMUNITY, 0, true, false);
            }
        }
    }
}