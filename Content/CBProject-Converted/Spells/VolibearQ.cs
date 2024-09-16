namespace Spells
{
    public class VolibearQ : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 12, 11, 10, 9, 8 };
        float[] effect1 = { 0.45f, 0.45f, 0.45f, 0.45f, 0.45f };
        public override void SelfExecute()
        {
            int nextBuffVars_SpellCooldown = effect0[level - 1];
            AddBuff(owner, owner, new Buffs.VolibearQ(nextBuffVars_SpellCooldown), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            SetSlotSpellCooldownTimeVer2(0, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            float nextBuffVars_SpeedMod = effect1[level - 1];
            AddBuff(owner, owner, new Buffs.VolibearQSpeed(nextBuffVars_SpeedMod), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class VolibearQ : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", "", },
            BuffName = "VolibearQ",
            BuffTextureName = "VolibearQ.dds",
        };
        EffectEmitter c;
        EffectEmitter a;
        EffectEmitter b;
        EffectEmitter particleID;
        EffectEmitter particleID2;
        EffectEmitter particleID3;
        float spellCooldown;
        public VolibearQ(float spellCooldown = default)
        {
            this.spellCooldown = spellCooldown;
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner); // UNUSED
            SpellEffectCreate(out c, out _, "Volibear_Q_cas_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out a, out _, "volibear_Q_attack_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_BUFFBONE_GLB_HAND_LOC", default, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out b, out _, "volibear_Q_attack_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_BUFFBONE_GLB_HAND_LOC", default, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out particleID, out _, "volibear_Q_lightning_cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "l_forearm", default, owner, "l_middle_finger", default, false, false, false, false, false);
            SpellEffectCreate(out particleID2, out _, "volibear_Q_lightning_cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "r_forearm", default, owner, "r_middle_finger", default, false, false, false, false, false);
            SpellEffectCreate(out particleID3, out _, "volibear_Q_lightning_cast_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "l_uparm", default, owner, "r_uparm", default, false, false, false, false, false);
            //RequireVar(this.spellCooldown);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SetDodgePiercing(owner, true);
            CancelAutoAttack(owner, true);
            OverrideAnimation("Idle1", "Spell1_Idle", owner);
            OverrideAnimation("Idle2", "Spell1_Idle", owner);
            OverrideAnimation("Idle3", "Spell1_Idle", owner);
            OverrideAnimation("Idle4", "Spell1_Idle", owner);
            OverrideAnimation("Run", "Spell1_Run", owner);
            OverrideAnimation("Spell4", "Spell1_Idle", owner);
            OverrideAutoAttack(0, SpellSlotType.ExtraSlots, owner, 1, false);
        }
        public override void OnDeactivate(bool expired)
        {
            float spellCooldown = this.spellCooldown;
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = multiplier * spellCooldown;
            SetSlotSpellCooldownTimeVer2(newCooldown, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SpellEffectRemove(a);
            SpellEffectRemove(b);
            SpellEffectRemove(c);
            SpellEffectRemove(particleID);
            SpellEffectRemove(particleID2);
            SpellEffectRemove(particleID3);
            ClearOverrideAnimation("Idle1", owner);
            ClearOverrideAnimation("Idle2", owner);
            ClearOverrideAnimation("Idle3", owner);
            ClearOverrideAnimation("Idle4", owner);
            ClearOverrideAnimation("Run", owner);
            ClearOverrideAnimation("Spell4", owner);
            RemoveOverrideAutoAttack(owner, false);
        }
        public override void OnPreAttack(AttackableUnit target)
        {
            if (target is ObjAIBase && target is not BaseTurret)
            {
                float offset = GetOffsetAngle(target, attacker.Position3D);
                charVars.BouncePos = GetPointByUnitFacingOffset(target, 400, offset);
            }
        }
    }
}