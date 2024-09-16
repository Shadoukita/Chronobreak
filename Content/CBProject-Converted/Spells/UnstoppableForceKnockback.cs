namespace Buffs
{
    public class UnstoppableForceKnockback : BuffScript
    {
        int level;
        int[] effect0 = { 50, 100, 150, 200, 250 };
        public UnstoppableForceKnockback(int level = default)
        {
            this.level = level;
        }
        public override void OnActivate()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.UnstoppableForceStun)) == 0)
            {
                //RequireVar(this.level);
                int level = this.level;
                float distance = DistanceBetweenObjects(attacker, owner); // UNUSED
                Vector3 landingPos = GetRandomPointInAreaUnit(owner, 310, 300);
                float distanceTwo = DistanceBetweenObjectAndPoint(attacker, landingPos); // UNUSED
                Move(owner, landingPos, 1000, 35, 0);
                ApplyDamage(attacker, owner, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 1);
            }
        }
    }
}