namespace Spells
{
    public class KogMawCausticSpittle : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 60, 110, 160, 210, 260 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            ApplyDamage(attacker, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0.7f, 1, false, false, attacker);
            AddBuff(attacker, target, new Buffs.KogMawCausticSpittleCharged(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.SHRED, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class KogMawCausticSpittle : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "KogMawCausticSpittle",
            BuffTextureName = "KogMaw_CausticSpittle.dds",
            IsPetDurationBuff = true,
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float[] effect0 = { 0.1f, 0.15f, 0.2f, 0.25f, 0.3f };
        int[] effect1 = { 10, 15, 20, 25, 30 };
        public override void OnActivate()
        {
            SetBuffToolTipVar(1, 10);
        }
        public override void OnUpdateStats()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float attackSpeed = effect0[level - 1];
            IncPercentAttackSpeedMod(owner, attackSpeed);
        }
        public override void OnLevelUpSpell(int slot)
        {
            if (slot == 0)
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float spittleAttackSpeed = effect1[level - 1];
                SetBuffToolTipVar(1, spittleAttackSpeed);
            }
        }
    }
}