namespace Spells
{
    public class IreliaTranscendentBlades : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 60, 50, 40, 0, 0 };
        int[] effect1 = { 10, 10, 10 };
        public override void SelfExecute()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.IreliaTranscendentBladesSpell)) > 0)
            {
                int level = GetSpellLevelPlusOne(spell);
                Vector3 targetPos = GetSpellTargetPos(spell);
                Vector3 ownerPos = GetUnitPosition(owner);
                float distance = DistanceBetweenPoints(ownerPos, targetPos); // UNUSED
                int count = GetBuffCountFromAll(owner, nameof(Buffs.IreliaTranscendentBladesSpell));
                SpellCast(owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                SetSlotSpellCooldownTime(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0.5f);
                SpellBuffRemove(owner, nameof(Buffs.IreliaTranscendentBladesSpell), owner);
                if (count <= 1)
                {
                    SpellBuffRemove(owner, nameof(Buffs.IreliaTranscendentBlades), owner);
                }
            }
            else
            {
                float nextBuffVars_NewCd = effect0[level - 1];
                float nextBuffVars_Blades = 4;
                AddBuff(owner, owner, new Buffs.IreliaTranscendentBlades(nextBuffVars_Blades, nextBuffVars_NewCd), 1, 1, effect1[level - 1], BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff(owner, owner, new Buffs.IreliaTranscendentBladesSpell(), 4, 4, 10, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                AddBuff(owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                PlayAnimation("Spell4", 1.5f, owner, false, true, true);
                SetSlotSpellCooldownTime(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0.25f);
            }
        }
    }
}
namespace Buffs
{
    public class IreliaTranscendentBlades : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", "", },
            AutoBuffActivateEffect = new[] { "", "", "", "", },
            BuffName = "IreliaTranscendentBlades",
            BuffTextureName = "Irelia_TranscendentBladesReady.dds",
            NonDispellable = true,
        };
        float blades;
        float newCd;
        EffectEmitter ultMagicParticle;
        EffectEmitter particle1;
        EffectEmitter particle2;
        EffectEmitter particle3;
        EffectEmitter particle4;
        public IreliaTranscendentBlades(float blades = default, float newCd = default)
        {
            this.blades = blades;
            this.newCd = newCd;
        }
        public override void OnActivate()
        {
            //RequireVar(this.blades);
            //RequireVar(this.newCd);
            SetTargetingType(3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, TargetingType.Location, owner);
            SetPARCostInc((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, -100, PrimaryAbilityResourceType.MANA);
            SpellBuffRemove(owner, nameof(Buffs.IreliaIdleParticle), (ObjAIBase)owner);
            TeamId ireliaTeamID = GetTeamID_CS(owner);
            SpellEffectCreate(out ultMagicParticle, out _, "irelia_ult_magic_resist.troy", default, ireliaTeamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false);
            SpellEffectCreate(out particle1, out _, "Irelia_ult_dagger_active_04.troy", default, ireliaTeamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_DAGGER1", default, target, default, default, false, default, default, false);
            SpellEffectCreate(out particle2, out _, "Irelia_ult_dagger_active_04.troy", default, ireliaTeamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_DAGGER2", default, target, default, default, false, default, default, false);
            SpellEffectCreate(out particle3, out _, "Irelia_ult_dagger_active_04.troy", default, ireliaTeamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_DAGGER4", default, target, default, default, false, default, default, false);
            SpellEffectCreate(out particle4, out _, "Irelia_ult_dagger_active_04.troy", default, ireliaTeamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_DAGGER5", default, target, default, default, false, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(ultMagicParticle);
            SetTargetingType(3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, TargetingType.SelfAOE, owner);
            if (blades == 1)
            {
                SpellEffectRemove(particle1);
            }
            if (blades == 2)
            {
                SpellEffectRemove(particle1);
                SpellEffectRemove(particle3);
            }
            if (blades == 3)
            {
                SpellEffectRemove(particle1);
                SpellEffectRemove(particle2);
                SpellEffectRemove(particle3);
            }
            if (blades == 4)
            {
                SpellEffectRemove(particle1);
                SpellEffectRemove(particle2);
                SpellEffectRemove(particle3);
                SpellEffectRemove(particle4);
            }
            SetPARCostInc((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
            AddBuff((ObjAIBase)owner, owner, new Buffs.IreliaIdleParticle(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = multiplier * newCd;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            spellName = GetSpellName(spell);
            if (spellName == nameof(Spells.IreliaTranscendentBlades))
            {
                blades -= 1;
                int count = GetBuffCountFromAll(owner, nameof(Buffs.IreliaTranscendentBladesSpell));
                if (count == 4)
                {
                    SpellEffectRemove(particle4);
                }
                if (count == 3)
                {
                    SpellEffectRemove(particle2);
                }
                if (count == 2)
                {
                    SpellEffectRemove(particle3);
                }
                if (count == 1)
                {
                    SpellEffectRemove(particle1);
                }
            }
        }
    }
}