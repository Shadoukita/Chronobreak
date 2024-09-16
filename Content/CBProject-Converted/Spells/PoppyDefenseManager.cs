namespace Buffs
{
    public class PoppyDefenseManager : BuffScript
    {
        public override void OnActivate()
        {
            charVars.ArmorCount = 1;
        }
        public override void OnBeingHit(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, HitResult hitResult)
        {
            float nextBuffVars_ArmorCount = charVars.ArmorCount;
            AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyDefenseOfDemacia(nextBuffVars_ArmorCount), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
            charVars.ArmorCount++;
            charVars.ArmorCount = Math.Min(charVars.ArmorCount, 20);
            SpellEffectCreate(out _, out _, "poppydef_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_finger", default, target, default, default, false);
        }
    }
}