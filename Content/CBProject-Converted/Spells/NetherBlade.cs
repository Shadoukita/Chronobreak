namespace Spells
{
    public class NetherBlade : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            AddBuff(attacker, owner, new Buffs.NetherBladeBuff(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, false, false, false);
        }
    }
}
namespace Buffs
{
    public class NetherBlade : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "NetherBlade",
            BuffTextureName = "Voidwalker_NullBlade.dds",
            NonDispellable = true,
            SpellToggleSlot = 2,
        };
        int[] effect0 = { 8, 11, 14, 17, 20 };
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float manaGainAmount = effect0[level - 1];
            if (target is Champion)
            {
                float modifiedManaGainAmount = manaGainAmount * 3;
                IncPAR(owner, modifiedManaGainAmount, PrimaryAbilityResourceType.MANA);
            }
            else
            {
                IncPAR(owner, manaGainAmount, PrimaryAbilityResourceType.MANA);
            }
            if (target is ObjAIBase)
            {
                SpellEffectCreate(out _, out _, "Netherblade_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, target, default, default, false, default, default, false, false);
            }
        }
    }
}