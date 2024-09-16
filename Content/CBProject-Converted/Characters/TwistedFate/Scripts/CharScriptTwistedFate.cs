namespace CharScripts
{
    public class CharScriptTwistedFate : CharScript
    {
        float lastTimeExecuted;
        int[] effect0 = { 55, 80, 105, 130, 155 };
        float[] effect1 = { -0.03f, -0.06f, -0.09f, -0.12f, -0.15f };
        float[] effect2 = { 0.03f, 0.06f, 0.09f, 0.12f, 0.15f };
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(4, ref lastTimeExecuted, false))
            {
                if (!IsDead(owner))
                {
                    foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                    {
                        AddBuff(owner, unit, new Buffs.SecondSight(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                    }
                }
            }
        }
        public override void OnActivate()
        {
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
            {
                AddBuff(owner, unit, new Buffs.SecondSight(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            }
            SealSpellSlot(2, SpellSlotType.SpellSlots, owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            charVars.Count = 0;
        }
        public override void OnLevelUpSpell(int slot)
        {
            if (slot == 2)
            {
                int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                int nextBuffVars_BonusDamage = effect0[level - 1];
                float nextBuffVars_CooldownBonus = effect1[level - 1];
                float nextBuffVars_AttackSpeedBonus = effect2[level - 1];
                AddBuff(owner, owner, new Buffs.CardmasterStack(nextBuffVars_BonusDamage, nextBuffVars_CooldownBonus, nextBuffVars_AttackSpeedBonus), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}