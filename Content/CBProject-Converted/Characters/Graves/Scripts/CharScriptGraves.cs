namespace CharScripts
{
    public class CharScriptGraves : CharScript
    {
        float lastTimeExecuted;
        int[] effect0 = { 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3 };
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(5, ref lastTimeExecuted, false))
            {
                float totalAD = GetTotalAttackDamage(owner);
                float baseAD = GetBaseAttackDamage(owner);
                float bonusAD = totalAD - baseAD;
                float aD1 = bonusAD * 0.8f;
                float aD3A = bonusAD * 1.4f;
                float aD3B = bonusAD * 1.2f;
                SetSpellToolTipVar(aD1, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
                SetSpellToolTipVar(aD3A, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
                SetSpellToolTipVar(aD3B, 2, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
            }
        }
        public override void SetVarsByLevel()
        {
            charVars.ArmorAmount = effect0[level - 1];
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is ObjAIBase && target is not BaseTurret)
            {
                int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level >= 0)
                {
                    float cD = GetSlotSpellCooldownTime(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if (cD > 0)
                    {
                        cD--;
                        SetSlotSpellCooldownTimeVer2(cD, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
                    }
                }
            }
        }
        public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            AddBuff(owner, owner, new Buffs.GravesPassiveCounter(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellBuffRenew(owner, nameof(Buffs.GravesPassiveGrit), 4);
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            AddBuff(owner, owner, new Buffs.GravesPassiveCounter(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellBuffRenew(owner, nameof(Buffs.GravesPassiveGrit), 4);
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.GravesPassive(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            charVars.PassiveDuration = 3;
            charVars.PassiveMaxStacks = 10;
            charVars.ArmorAmount = 1;
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class CharScriptGraves : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Pineapple",
            BuffTextureName = "Pineapple.dds",
        };
    }
}