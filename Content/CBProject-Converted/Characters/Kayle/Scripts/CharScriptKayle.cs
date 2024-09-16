namespace CharScripts
{
    public class CharScriptKayle : CharScript
    {
        float lastTimeExecuted;
        int[] effect0 = { 20, 30, 40, 50, 60 };
        float[] effect1 = { 1.06f, 1.07f, 1.08f, 1.09f, 1.1f };
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(2, ref lastTimeExecuted, true))
            {
                int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level == 0)
                {
                    level = 1;
                }
                level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float kayleAP = GetFlatMagicDamageMod(owner);
                kayleAP *= 0.2f;
                float damageMod = effect0[level - 1];
                float attackDamage = kayleAP + damageMod;
                float baseAD = GetBaseAttackDamage(owner);
                float totalAD = GetTotalAttackDamage(owner);
                float bonusAD = totalAD - baseAD;
                bonusAD *= 1;
                SetSpellToolTipVar(attackDamage, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
                SetSpellToolTipVar(bonusAD, 2, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is Champion)
            {
                AddBuff(attacker, target, new Buffs.JudicatorHolyFervorDebuff(), 5, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.SHRED, 0, true, false, false);
            }
        }
        public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (GetBuffCountFromCaster(target, owner, nameof(Buffs.JudicatorReckoning)) > 0)
            {
                int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level > 0)
                {
                    float damagePercent = effect1[level - 1];
                    damageAmount *= damagePercent;
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.JudicatorHolyFervor(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}