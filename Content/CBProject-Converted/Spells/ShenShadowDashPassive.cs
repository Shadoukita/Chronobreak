namespace Buffs
{
    public class ShenShadowDashPassive : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnTakeDamage(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource)
        {
            if (attacker is ObjAIBase) //TODO: Optimize
            {
                attacker = GetBuffCasterUnit();
                if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.ShenShadowDashCooldown)) == 0)
                {
                    AddBuff(attacker, attacker, new Buffs.ShenShadowDashCooldown(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
                    float cD = GetSlotSpellCooldownTime(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if (cD > 0)
                    {
                        float newCD = cD - 1;
                        SetSlotSpellCooldownTimeVer2(newCD, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
                    }
                }
            }
        }
    }
}