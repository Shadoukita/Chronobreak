namespace CharScripts
{
    public class CharScriptJarvanIV : CharScript
    {
        int[] effect0 = { 10, 3, 3, 3, 3 };
        float[] effect1 = { 0.1f, 0.03f, 0.03f, 0.03f, 0.03f };
        public override void OnUpdateActions()
        {
            float bonusAD = GetFlatPhysicalDamageMod(owner);
            float bonusAD2 = bonusAD * 1.2f;
            bonusAD *= 1.5f;
            SetSpellToolTipVar(bonusAD2, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
            SetSpellToolTipVar(bonusAD, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
        }
        public override void OnPreAttack(AttackableUnit target)
        {
            if (target is ObjAIBase)
            {
                if (target is not BaseTurret)
                {
                    if (GetBuffCountFromCaster(target, attacker, nameof(Buffs.JarvanIVMartialCadenceCheck)) > 0)
                    {
                        RemoveOverrideAutoAttack(owner, true);
                    }
                    else
                    {
                        OverrideAutoAttack(0, SpellSlotType.ExtraSlots, owner, 1, true);
                    }
                }
                else
                {
                    RemoveOverrideAutoAttack(owner, true);
                }
            }
            else
            {
                RemoveOverrideAutoAttack(owner, true);
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, attacker, new Buffs.JarvanIVMartialCadence(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
        }
        public override void OnLevelUpSpell(int slot)
        {
            int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float armorBoost = effect0[level - 1];
            float attackSpeedBoost = effect1[level - 1];
            if (slot == 2)
            {
                IncPermanentFlatArmorMod(owner, armorBoost);
                IncPermanentPercentAttackSpeedMod(owner, attackSpeedBoost);
            }
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}