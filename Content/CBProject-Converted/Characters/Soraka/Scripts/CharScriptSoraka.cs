namespace CharScripts
{
    public class CharScriptSoraka : CharScript
    {
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref charVars.LastTimeExecuted, false))
            {
                foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, default, true))
                {
                    if (!IsDead(owner))
                    {
                        AddBuff(owner, unit, new Buffs.ConsecrationAuraNoParticle(), 1, 1, 1.15f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 25000, true, false);
                    }
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.ConsecrationAura(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}