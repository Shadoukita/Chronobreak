namespace Spells
{
    public class PantheonRJump : Pantheon_GrandSkyfall_Jump { }
    public class Pantheon_GrandSkyfall_Jump : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            ChannelDuration = 1.5f,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { 90, 166.67f, 235.33f };
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            targetPos = GetNearestPassablePosition(owner, targetPos);
            charVars.TargetPos = targetPos;
            AddBuff(owner, owner, new Buffs.Pantheon_GrandSkyfall_Jump(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            FaceDirection(owner, targetPos);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pantheon_AegisShield2)) == 0 && GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pantheon_AegisShield)) == 0)
            {
                AddBuff(owner, owner, new Buffs.Pantheon_Aegis_Counter(), 5, 1, 25000, BuffAddType.STACKS_AND_OVERLAPS, BuffType.AURA, 0, false, false, false);
                int count = GetBuffCountFromAll(owner, nameof(Buffs.Pantheon_Aegis_Counter));
                if (count >= 4)
                {
                    AddBuff(owner, owner, new Buffs.Pantheon_AegisShield(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                    SpellBuffClear(owner, nameof(Buffs.Pantheon_Aegis_Counter));
                }
            }
        }
        public override void ChannelingSuccessStop()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SpellBuffRemove(owner, nameof(Buffs.Pantheon_GrandSkyfall_Jump), owner, 0);
            Vector3 targetPos = charVars.TargetPos;
            int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            Vector3 nextBuffVars_TargetPos = targetPos;
            SetCanCast(owner, true);
            SpellCast(owner, default, targetPos, targetPos, 1, SpellSlotType.ExtraSlots, level, true, false, false, true, false, false);
            SpellEffectCreate(out _, out _, "pantheon_grandskyfall_up.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, target, default, default, true, default, default, false, false);
            object nextBuffVars_Particle = charVars.Particle; // UNUSED
            AddBuff(owner, owner, new Buffs.Pantheon_GrandSkyfall(nextBuffVars_TargetPos), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            float smnCooldown0 = GetSlotSpellCooldownTime(owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            float smnCooldown1 = GetSlotSpellCooldownTime(owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            AddBuff(owner, owner, new Buffs.Pantheon_GS_ParticleRed(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            if (smnCooldown0 < 2.75f)
            {
                SetSlotSpellCooldownTimeVer2(2, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, owner, false);
            }
            if (smnCooldown1 < 2.75f)
            {
                SetSlotSpellCooldownTimeVer2(2, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, owner, false);
            }
        }
        public override void ChannelingCancelStop()
        {
            SetSlotSpellCooldownTimeVer2(10, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float manaRefund = effect0[level - 1];
            IncPAR(owner, manaRefund, PrimaryAbilityResourceType.MANA);
            SpellBuffRemove(owner, nameof(Buffs.Pantheon_GrandSkyfall_Jump), owner, 0);
            SpellBuffRemove(owner, nameof(Buffs.Pantheon_GS_Particle), owner, 0);
            SpellBuffRemove(owner, nameof(Buffs.Pantheon_GS_ParticleRed), owner, 0);
        }
    }
}
namespace Buffs
{
    public class Pantheon_GrandSkyfall_Jump : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Pantheon Grand Skyfall",
            BuffTextureName = "Pantheon_GrandSkyfall.dds",
        };
        EffectEmitter part;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out part, out _, "pantheon_grandskyfall_cas.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position3D, target, default, default, false, default, default, false, false);
            Vector3 targetPos = charVars.TargetPos; // UNUSED
            AddBuff((ObjAIBase)owner, owner, new Buffs.Pantheon_GS_Particle(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(part);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
        }
    }
}
