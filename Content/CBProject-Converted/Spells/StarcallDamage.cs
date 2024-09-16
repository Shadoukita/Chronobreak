namespace Buffs
{
    public class StarcallDamage : BuffScript
    {
        float damageToDeal;
        int starcallShred;
        public StarcallDamage(float damageToDeal = default, int starcallShred = default)
        {
            this.damageToDeal = damageToDeal;
            this.starcallShred = starcallShred;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageToDeal);
            //RequireVar(this.starcallShred);
        }
        public override void OnDeactivate(bool expired)
        {
            float nextBuffVars_ResistanceMod = starcallShred;
            AddBuff(attacker, owner, new Buffs.Starcall(nextBuffVars_ResistanceMod), 10, 1, 8, BuffAddType.STACKS_AND_RENEWS, BuffType.SHRED, 0, true, false, false);
            ApplyDamage(attacker, owner, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, attacker);
        }
    }
}