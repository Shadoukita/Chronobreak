namespace Spells
{
    public class OdinGuardianSpellAttackCast : SpellScript
    {
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Stun)) == 0)
            {
                SpellCast(owner, target, default, default, 0, SpellSlotType.ExtraSlots, 1, false, false, false, false, false, false);
            }
        }
    }
}