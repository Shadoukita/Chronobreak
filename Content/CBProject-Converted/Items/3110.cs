namespace ItemPassives
{
    public class ItemID_3110 : ItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateStats()
        {
            if (!IsDead(owner) && ExecutePeriodically(0.9f, ref lastTimeExecuted, false))
            {
                AddBuff(owner, owner, new Buffs.FrozenHeart(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes))
                {
                    AddBuff(owner, unit, new Buffs.FrozenHeartAura(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0);
                }
            }
        }
        public override void OnActivate()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.FrozenHeart)) == 0)
            {
                AddBuff(owner, owner, new Buffs.FrozenHeart(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
            }
        }
    }
}