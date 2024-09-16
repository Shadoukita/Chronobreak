namespace Spells
{
    public class BloodScent : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 45f, 40f, 35f, 30f, 25f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
            SpellVOOverrideSkins = new[] { "HyenaWarwick", },
        };
        public override void SelfExecute()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.BloodScent_internal)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.BloodScent_internal), owner, 0);
            }
            else
            {
                AddBuff(owner, owner, new Buffs.BloodScent_internal(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class BloodScent : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", "", "", },
            AutoBuffActivateEffect = new[] { "", "", "", "", },
            BuffName = "Haste",
            BuffTextureName = "Wolfman_Bloodscent.dds",
        };
        float moveSpeedBuff;
        EffectEmitter part1;
        EffectEmitter part3;
        EffectEmitter part2;
        EffectEmitter part4;
        float[] effect0 = { 0.2f, 0.25f, 0.3f, 0.35f, 0.4f };
        public BloodScent(float moveSpeedBuff = default)
        {
            this.moveSpeedBuff = moveSpeedBuff;
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            //RequireVar(this.moveSpeedBuff);
            SpellEffectCreate(out part1, out _, "wolfman_bloodscent_activate_speed.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out part3, out _, "wolfman_bloodscent_activate_blood_buff.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_hand", default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out part2, out _, "wolfman_bloodscent_activate_blood_buff.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_hand", default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out part4, out _, "wolfman_bloodscent_activate_blood_buff_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, owner, default, default, false, false, false, false, false);
            int ownerSkinID = GetSkinID(owner);
            if (ownerSkinID == 7)
            {
                OverrideAnimation("Run", "Run2", owner);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(part1);
            SpellEffectRemove(part2);
            SpellEffectRemove(part3);
            SpellEffectRemove(part4);
            int ownerSkinID = GetSkinID(owner);
            if (ownerSkinID == 7)
            {
                StopCurrentOverrideAnimation("Run", owner, false);
                OverrideAnimation("Run", "Run", owner);
            }
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(attacker, moveSpeedBuff);
        }
        public override void OnLevelUpSpell(int slot)
        {
            if (slot == 2)
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                moveSpeedBuff = effect0[level - 1];
            }
        }
    }
}