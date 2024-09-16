namespace CharScripts
{
    public class CharScriptGangplank : CharScript
    {
        public override void OnUpdateActions()
        {
            int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level > 0 && GetBuffCountFromCaster(owner, owner, nameof(Buffs.RaiseMoraleTeamBuff)) == 0)
            {
                AddBuff(attacker, attacker, new Buffs.RaiseMorale(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            int level2 = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level2 > 0)
            {
                float cooldown2 = GetSlotSpellCooldownTime(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (cooldown2 <= 0)
                {
                    AddBuff(attacker, owner, new Buffs.PirateScurvy(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 1, true, false, false);
                }
            }
            else
            {
                AddBuff(attacker, owner, new Buffs.PirateScurvy(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 1, true, false, false);
            }
            float attackDamage = GetTotalAttackDamage(owner);
            SetSpellToolTipVar(attackDamage, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.Scurvy(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.IsPirate(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            int skinID = GetSkinID(owner);
            if (skinID == 4)
            {
                PlayAnimation("gangplank_key", 0, owner, true, false, false);
            }
        }
        public override void OnResurrect()
        {
            int skinID = GetSkinID(owner);
            if (skinID == 4)
            {
                PlayAnimation("gangplank_key", 0, owner, true, false, false);
            }
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}