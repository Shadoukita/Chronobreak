namespace Buffs
{
    public class RivenPassive : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "RivenPassive",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        int[] effect0 = { 5, 5, 5, 7, 7, 7, 9, 9, 9, 11, 11, 11, 13, 13, 13, 15, 15, 15, 15 };
        public override void OnActivate()
        {
            SetBuffToolTipVar(1, 5);
            SetBuffToolTipVar(3, 3);
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            spellName = GetSpellName(spell);
            if (spellName == nameof(Spells.RivenMartyr))
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots); // UNUSED
                AddBuff((ObjAIBase)owner, owner, new Buffs.RivenMartyr(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            if (spellName == nameof(Spells.RivenFengShuiEngine))
            {
                float attackDamage = GetTotalAttackDamage(owner);
                float baseAD = GetBaseAttackDamage(owner);
                attackDamage -= baseAD;
                float passiveAD = 0.6f * attackDamage;
                float bonusBaseAD = 0.12f * baseAD;
                passiveAD = 0.6f + bonusBaseAD;
                SetBuffToolTipVar(2, passiveAD);
            }
            if (!spellVars.DoesntTriggerSpellCasts)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.RivenPassiveAABoost(), 3, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            int count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.RivenPassiveAABoost));
            if (count > 0 && target is ObjAIBase && target is not BaseTurret)
            {
                int level = GetLevel(owner);
                float attackDamage = GetTotalAttackDamage(owner);
                float baseAD = GetBaseAttackDamage(owner);
                attackDamage -= baseAD;
                float passiveAD = 0.5f * attackDamage;
                float baseDamage = effect0[level - 1];
                float bonusDamage = baseDamage + passiveAD;
                damageAmount += bonusDamage;
                if (count > 1)
                {
                    SpellBuffRemove(owner, nameof(Buffs.RivenPassiveAABoost), (ObjAIBase)owner, 5);
                }
                else
                {
                    SpellBuffClear(owner, nameof(Buffs.RivenPassiveAABoost));
                }
            }
        }
        public override void OnLevelUp()
        {
            int level = GetLevel(owner);
            float damageAmp = effect0[level - 1];
            SetBuffToolTipVar(1, damageAmp);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(10, ref lastTimeExecuted, false))
            {
                float attackDamage = GetTotalAttackDamage(owner);
                float baseAD = GetBaseAttackDamage(owner);
                attackDamage -= baseAD;
                float passiveAD = 0.5f * attackDamage;
                SetBuffToolTipVar(2, passiveAD);
            }
        }
    }
}