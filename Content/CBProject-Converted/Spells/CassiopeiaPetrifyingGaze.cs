namespace Spells
{
    public class CassiopeiaPetrifyingGaze : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            SpellCast(owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, false, true, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class CassiopeiaPetrifyingGaze : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head", },
            AutoBuffActivateEffect = new[] { "Stun_glb.troy", },
            BuffName = "Stun",
            BuffTextureName = "Minotaur_TriumphantRoar.dds",
        };
        EffectEmitter turntostone;
        public override void OnActivate()
        {
            SetStunned(owner, true);
            ApplyAssistMarker(attacker, owner, 10);
            PauseAnimation(owner, true);
            int level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots); // UNUSED
            SpellEffectCreate(out turntostone, out _, "TurnToStone.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetStunned(owner, false);
            SpellEffectCreate(out _, out _, "TurnBack.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            PauseAnimation(owner, false);
            SpellEffectRemove(turntostone);
        }
        public override void OnUpdateStats()
        {
            SetStunned(owner, true);
        }
    }
}