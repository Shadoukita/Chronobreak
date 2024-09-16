namespace Spells
{
    public class XerathLocusOfPower : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class XerathLocusOfPower : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "XerathLocusOfPower",
            BuffTextureName = "Xerath_LocusOfPower.dds",
            SpellToggleSlot = 2,
        };
        float magicPen;
        EffectEmitter particle;
        EffectEmitter particlea;
        EffectEmitter particleb;
        EffectEmitter particlec;
        int[] effect0 = { 20, 16, 12, 8, 4 };
        public XerathLocusOfPower(float magicPen = default)
        {
            this.magicPen = magicPen;
        }
        public override void OnActivate()
        {
            TeamId teamOfOwner = TeamId.TEAM_UNKNOWN; // UNITIALIZED
            //RequireVar(this.magicPen);
            IncPercentMagicPenetrationMod(owner, magicPen);
            SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.XerathLocusOfPowerToggle));
            SetSlotSpellCooldownTimeVer2(0.5f, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            float cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SetSpell((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.XerathArcanopulseExtended));
            SetSlotSpellCooldownTimeVer2(cooldown, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            float cooldown2 = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.XerathMageChainsExtended));
            SetSlotSpellCooldownTimeVer2(cooldown2, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            float cooldown3 = GetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SetSpell((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.XerathArcaneBarrageWrapperExt));
            SetSlotSpellCooldownTimeVer2(cooldown3, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            SpellEffectCreate(out particle, out _, "Xerath_LocusOfPower_buf.troy", default, teamOfOwner, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out particlea, out _, "Xerath_LocusOfPower_beam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_GLB_CHANNEL_LOC", default, owner, "spine", default, false, false, false, false, false);
            SpellEffectCreate(out particleb, out _, "Xerath_LocusOfPower_beam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_CHANNEL_2", default, owner, "spine", default, false, false, false, false, false);
            SpellEffectCreate(out particlec, out _, "Xerath_LocusOfPower_beam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_CHANNEL_3", default, owner, "spine", default, false, false, false, false, false);
            SetCanMove(owner, false);
            OverrideAnimation("Idle1", "Spell2_chan", owner);
            OverrideAnimation("Idle2", "Spell2_chan", owner);
            OverrideAnimation("Idle3", "Spell2_chan", owner);
            OverrideAnimation("Idle4", "Spell2_chan", owner);
            string flashCheck = GetSlotSpellName((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if (flashCheck == nameof(Spells.SummonerFlash))
            {
                SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_SUMMONER);
            }
            flashCheck = GetSlotSpellName((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if (flashCheck == nameof(Spells.SummonerFlash))
            {
                SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_SUMMONER);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
            SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.XerathLocusOfPower));
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float cooldown = effect0[level - 1];
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = cooldown * multiplier;
            SetSlotSpellCooldownTimeVer2(newCooldown, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            SpellEffectRemove(particle);
            SpellEffectRemove(particlea);
            SpellEffectRemove(particleb);
            SpellEffectRemove(particlec);
            cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SetSpell((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.XerathArcanopulse));
            SetSlotSpellCooldownTimeVer2(cooldown, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            float cooldown2 = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.XerathMageChains));
            SetSlotSpellCooldownTimeVer2(cooldown2, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SetSpell((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.XerathArcaneBarrageWrapper));
            SetSlotSpellCooldownTimeVer2(cooldown, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            ClearOverrideAnimation("Idle1", owner);
            ClearOverrideAnimation("Idle2", owner);
            ClearOverrideAnimation("Idle3", owner);
            ClearOverrideAnimation("Idle4", owner);
            string flashCheck = GetSlotSpellName((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if (flashCheck == nameof(Spells.SummonerFlash))
            {
                SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_SUMMONER);
            }
            flashCheck = GetSlotSpellName((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if (flashCheck == nameof(Spells.SummonerFlash))
            {
                SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_SUMMONER);
            }
            AddBuff((ObjAIBase)owner, owner, new Buffs.XerathEnergize(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
        public override void OnUpdateStats()
        {
            IncPercentMagicPenetrationMod(owner, magicPen);
        }
        public override void OnUpdateActions()
        {
            SetCanMove(owner, false);
        }
    }
}