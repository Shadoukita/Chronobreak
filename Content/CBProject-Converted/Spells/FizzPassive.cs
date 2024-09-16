namespace Buffs
{
    public class FizzPassive : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "FizzPassive",
            BuffTextureName = "FizzPassive.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float defense;
        int[] effect0 = { 4, 4, 4, 6, 6, 6, 8, 8, 8, 10, 10, 10, 12, 12, 12, 14, 14, 14, 16 };
        public override void OnActivate()
        {
            defense = 4;
            SetBuffToolTipVar(1, defense);
            SetGhosted(owner, true);
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level >= 1 && target is ObjAIBase && target is not BaseTurret)
            {
                AddBuff((ObjAIBase)owner, target, new Buffs.FizzSeastoneTrident(), 1, 1, 3.1f, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
            }
        }
        public override void OnBeingHit(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, HitResult hitResult)
        {
            if (damageType == DamageType.DAMAGE_TYPE_PHYSICAL)
            {
                damageAmount -= defense;
            }
        }
        public override void OnLevelUp()
        {
            int level = GetLevel(owner);
            defense = effect0[level - 1];
            SetBuffToolTipVar(1, defense);
        }
        public override void OnUpdateStats()
        {
            SetGhosted(owner, true);
        }
    }
}