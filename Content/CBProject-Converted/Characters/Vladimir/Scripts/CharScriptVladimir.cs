namespace CharScripts
{
    public class CharScriptVladimir : CharScript
    {
        float lastTimeExecuted;
        public override void OnUpdateStats()
        {
            float maxHP = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            float baseHP = 400;
            float healthPerLevel = 85;
            int level = GetLevel(owner);
            float levelHealth = level * healthPerLevel;
            float totalBaseHealth = levelHealth + baseHP;
            float totalBonusHealth = maxHP - totalBaseHealth;
            totalBonusHealth *= 0.15f;
            SetSpellToolTipVar(totalBonusHealth, 1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.4f, ref lastTimeExecuted, false))
            {
                if (IsDead(owner))
                {
                    AddBuff(owner, owner, new Buffs.VladDeathParticle(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
                else
                {
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.VladDeathParticle)) > 0)
                    {
                        SpellBuffRemove(owner, nameof(Buffs.VladDeathParticle), owner);
                    }
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.VladimirBloodGorged(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}