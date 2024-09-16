namespace Buffs
{
    public class AlZaharVoidlingDetonation : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 20000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions, default, true))
            {
                if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.AlZaharVoidling)) > 0)
                {
                    ApplyDamage(attacker, unit, 2000, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_RAW, 1, 1, 1, false, false);
                }
            }
        }
    }
}