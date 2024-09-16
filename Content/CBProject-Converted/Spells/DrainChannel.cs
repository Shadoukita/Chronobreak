namespace Spells
{
    public class DrainChannel : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            ChannelDuration = 5f,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            SpellFXOverrideSkins = new[] { "SurprisePartyFiddlesticks", },
        };
        EffectEmitter particleID;
        float drainExecuted;
        EffectEmitter glow;
        EffectEmitter confetti;
        float[] effect0 = { 0.6f, 0.65f, 0.7f, 0.75f, 0.8f };
        int[] effect1 = { 30, 45, 60, 75, 90 };
        public override void ChannelingStart()
        {
            int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float abilityPower = GetFlatMagicDamageMod(owner);
            AddBuff(owner, target, new Buffs.DrainChannel(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.Fearmonger_marker(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.HEAL, 0, true, false, false);
            SpellEffectCreate(out particleID, out _, "Drain.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "spine", default, target, "spine", default, false, false, false, false, false);
            drainExecuted = GetTime();
            float nextBuffVars_DrainPercent = effect0[level - 1];
            bool nextBuffVars_DrainedBool = false;
            AddBuff(owner, owner, new Buffs.GlobalDrain(nextBuffVars_DrainPercent, nextBuffVars_DrainedBool), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            float baseDamage = effect1[level - 1];
            float bonusDamage = abilityPower * 0.225f;
            float damageToDeal = bonusDamage + baseDamage;
            ApplyDamage(owner, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0, 1, false, false, owner);
            TeamId teamID = GetTeamID_CS(owner); // UNUSED
            int fiddlesticksSkinID = GetSkinID(owner);
            if (fiddlesticksSkinID == 6)
            {
                SpellEffectCreate(out glow, out _, "Party_DrainGlow.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, target, "spine", default, false, false, false, false, false);
                SpellEffectCreate(out confetti, out _, "Party_HornConfetti.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "BUFFBONE_CSTM_HORN", default, attacker, default, default, false, false, false, false, false);
            }
        }
        public override void ChannelingUpdateActions()
        {
            if (ExecutePeriodically(0.5f, ref drainExecuted, false))
            {
                float distance = DistanceBetweenObjects(target, owner);
                if (distance >= 650)
                {
                    StopChanneling(owner, ChannelingStopCondition.Cancel, ChannelingStopSource.LostTarget);
                }
                if (IsDead(target))
                {
                    StopChanneling(owner, ChannelingStopCondition.Cancel, ChannelingStopSource.LostTarget);
                }
                else
                {
                    if (IsDead(owner))
                    {
                        SpellEffectRemove(particleID);
                    }
                    else
                    {
                        int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        float nextBuffVars_DrainPercent = effect0[level - 1];
                        bool nextBuffVars_DrainedBool = false;
                        AddBuff(owner, owner, new Buffs.GlobalDrain(nextBuffVars_DrainPercent, nextBuffVars_DrainedBool), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        float abilityPower = GetFlatMagicDamageMod(owner);
                        float baseDamage = effect1[level - 1];
                        float bonusDamage = abilityPower * 0.225f;
                        float damageToDeal = bonusDamage + baseDamage;
                        ApplyDamage(owner, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0, 1, false, false, attacker);
                    }
                }
            }
        }
        public override void ChannelingSuccessStop()
        {
            if (target is not ObjAIBase)
            {
                SpellBuffRemove(target, nameof(Buffs.Drain), owner, 0);
            }
            SpellBuffRemove(owner, nameof(Buffs.Fearmonger_marker), owner, 0);
            SpellEffectRemove(particleID);
            TeamId teamID = GetTeamID_CS(owner); // UNUSED
            int fiddlesticksSkinID = GetSkinID(owner);
            if (fiddlesticksSkinID == 6)
            {
                SpellEffectRemove(glow);
                SpellEffectRemove(confetti);
            }
        }
        public override void ChannelingCancelStop()
        {
            if (target is not ObjAIBase)
            {
                SpellBuffRemove(target, nameof(Buffs.Drain), owner, 0);
            }
            SpellBuffRemove(owner, nameof(Buffs.Fearmonger_marker), owner, 0);
            SpellEffectRemove(particleID);
            TeamId teamID = GetTeamID_CS(owner); // UNUSED
            int fiddlesticksSkinID = GetSkinID(owner);
            if (fiddlesticksSkinID == 6)
            {
                SpellEffectRemove(glow);
                SpellEffectRemove(confetti);
            }
        }
    }
}
namespace Buffs
{
    public class DrainChannel : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Drain",
            BuffTextureName = "Fiddlesticks_ConjureScarecrow.dds",
            IsDeathRecapSource = true,
        };
    }
}