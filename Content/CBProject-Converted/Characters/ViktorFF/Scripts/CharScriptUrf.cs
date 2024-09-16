namespace CharScripts
{
    public class CharScriptUrf : CharScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(2, ref lastTimeExecuted, false))
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.HalloweenUrfCD)) == 0)
                {
                    foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, default, true))
                    {
                        if (GetBuffCountFromCaster(unit, unit, nameof(Buffs.EternalThirstIcon)) > 0)
                        {
                            int warwickID = GetSkinID(unit);
                            if (warwickID == 2)
                            {
                                SpellCast(owner, unit, default, unit.Position3D, 0, SpellSlotType.SpellSlots, 1, false, false, false, false, false, false);
                                AddBuff(owner, owner, new Buffs.HalloweenUrfWarwick(), 1, 1, 4.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
                            }
                            else
                            {
                                AddBuff(owner, owner, new Buffs.HalloweenUrfAppear(), 1, 1, 6.25f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
                            }
                        }
                        else
                        {
                            AddBuff(owner, owner, new Buffs.HalloweenUrfAppear(), 1, 1, 6.25f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
                        }
                    }
                }
            }
        }
        public override void OnActivate()
        {
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            SetCanAttack(owner, false);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetInvulnerable(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetNoRender(owner, true);
        }
    }
}