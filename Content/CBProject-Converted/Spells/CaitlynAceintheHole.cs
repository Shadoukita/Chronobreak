namespace Spells
{
    public class CaitlynAceintheHole : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            ChannelDuration = 1.25f,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        EffectEmitter particleID;
        public override void ChannelingStart()
        {
            FaceDirection(owner, target.Position3D);
            AddBuff(attacker, target, new Buffs.CaitlynAceintheHole(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            SpellEffectCreate(out particleID, out _, "caitlyn_laser_beam_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_WEAPON_5", default, target, "spine", default, false, false, false, false, false);
        }
        public override void ChannelingSuccessStop()
        {
            //bool isStealthed = GetStealthed(target); // UNUSED
            FaceDirection(owner, target.Position3D);
            //TeamId team = GetTeamID(attacker); // UNUSED
            SpellCast(owner, target, target.Position3D, target.Position3D, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
            AddBuff(attacker, attacker, new Buffs.IfHasBuffCheck(), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellEffectRemove(particleID);
        }
        public override void ChannelingCancelStop()
        {
            SetSlotSpellCooldownTime(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 5);
            SpellEffectRemove(particleID);
            SpellBuffRemove(target, nameof(Buffs.CaitlynAceintheHole), owner, 0);
        }
    }
}
namespace Buffs
{
    public class CaitlynAceintheHole : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { null, "", },
            AutoBuffActivateEffect = new[] { "caitlyn_ace_target_indicator.troy", "caitlyn_ace_target_indicator_02.troy", },
            BuffName = "CaitlynAceintheHole",
            BuffTextureName = "Caitlyn_AceintheHole.dds",
        };
        Region bubbleID;
        public override void OnActivate()
        {
            TeamId team = GetTeamID_CS(attacker);
            bubbleID = AddUnitPerceptionBubble(team, 50, owner, 4, default, default, true);
        }
        public override void OnDeactivate(bool expired)
        {
            Region nextBuffVars_BubbleID = bubbleID;
            AddBuff(attacker, owner, new Buffs.CaitlynAceintheHoleVisibility(nextBuffVars_BubbleID), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SealSpellSlot(3, SpellSlotType.SpellSlots, attacker, false, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnUpdateActions()
        {
            bool zombie = GetIsZombie(owner);
            if (zombie)
            {
                StopChanneling(attacker, ChannelingStopCondition.Cancel, ChannelingStopSource.Die);
                StopChanneling((ObjAIBase)owner, ChannelingStopCondition.Cancel, ChannelingStopSource.Die);
            }
        }
        public override void OnZombie(ObjAIBase attacker)
        {
            StopChanneling(attacker, ChannelingStopCondition.Cancel, ChannelingStopSource.Die);
        }
    }
}