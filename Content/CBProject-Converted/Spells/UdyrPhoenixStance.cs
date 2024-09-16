namespace Spells
{
    public class UdyrPhoenixStance : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.UdyrBearStance)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.UdyrBearStance), owner);
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.UdyrTigerStance)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.UdyrTigerStance), owner);
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.UdyrTurtleStance)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.UdyrTurtleStance), owner);
            }
            float cooldownPerc = GetPercentCooldownMod(owner);
            cooldownPerc++;
            cooldownPerc *= 1.5f;
            float currentCD = GetSlotSpellCooldownTime(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (currentCD <= cooldownPerc)
            {
                SetSlotSpellCooldownTime(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldownPerc);
            }
            currentCD = GetSlotSpellCooldownTime(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (currentCD <= cooldownPerc)
            {
                SetSlotSpellCooldownTime(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldownPerc);
            }
            currentCD = GetSlotSpellCooldownTime(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (currentCD <= cooldownPerc)
            {
                SetSlotSpellCooldownTime(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldownPerc);
            }
            AddBuff(owner, owner, new Buffs.UdyrPhoenixStance(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
            SpellEffectCreate(out _, out _, "PhoenixStance.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            AddBuff(owner, owner, new Buffs.UdyrPhoenixActivation(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class UdyrPhoenixStance : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "UdyrPhoenixStance",
            BuffTextureName = "Udyr_PhoenixStance.dds",
            PersistsThroughDeath = true,
            SpellToggleSlot = 4,
        };
        int casterID; // UNUSED
        EffectEmitter phoenix;
        public override void OnActivate()
        {
            casterID = PushCharacterData("UdyrPhoenix", owner, false);
            SpellEffectCreate(out phoenix, out _, "phoenixpelt.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, owner, default, default, false);
            charVars.Count = 0;
            OverrideAutoAttack(4, SpellSlotType.ExtraSlots, owner, 1, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(phoenix);
            RemoveOverrideAutoAttack(owner, true);
        }
    }
}