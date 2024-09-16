namespace Buffs
{
    public class Taunt : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Taunt",
            BuffTextureName = "GSB_taunt.dds",
            PopupMessage = new[] { "game_floatingtext_Taunted", },
        };

        public override BuffScriptMetaData BuffMetaData { get; } = new()
        {
            BuffType = BuffType.TAUNT,
            BuffAddType = BuffAddType.STACKS_AND_OVERLAPS,
            CanMitigateDuration = true
        };

        bool removePart;
        EffectEmitter part;
        public override void OnActivate()
        {
            StopChanneling((ObjAIBase)owner, ChannelingStopCondition.Cancel, ChannelingStopSource.StunnedOrSilencedOrTaunted);
            SetTaunted(owner, true);
            if (attacker is Champion)
            {
                ApplyAssistMarker(attacker, owner, 10);
            }
            string attackerName = GetUnitSkinName(attacker); // UNUSED
            TeamId teamID = GetTeamID_CS(attacker);
            removePart = false;
            if (GetBuffCountFromCaster(owner, attacker, nameof(Buffs.GalioIdolOfDurandMarker)) > 0)
            {
                SpellEffectCreate(out part, out _, "galio_taunt_unit_indicator.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, owner, default, default, false, default, default, false, false);
                removePart = true;
            }
            if (GetBuffCountFromCaster(owner, attacker, nameof(Buffs.ShenShadowDashCooldown)) > 0)
            {
                SpellEffectCreate(out part, out _, "Global_Taunt_multi_unit.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, owner, default, default, false, default, default, false, false);
                SpellEffectCreate(out _, out _, "shen_shadowDash_unit_impact.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
                removePart = true;
            }
            if (GetBuffCountFromCaster(owner, attacker, nameof(Buffs.PuncturingTauntArmorDebuff)) > 0)
            {
                SpellEffectCreate(out part, out _, "global_taunt.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, target, default, default, false, default, default, false, false);
                removePart = true;
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SetTaunted(owner, false);
            if (removePart)
            {
                SpellEffectRemove(part);
            }
        }
        public override void OnUpdateStats()
        {
            if (IsDead(attacker))
            {
                SpellBuffRemoveCurrent(owner);
            }
            else
            {
                SetTaunted(owner, true);
            }
        }
    }
}