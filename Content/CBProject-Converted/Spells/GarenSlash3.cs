namespace Spells
{
    public class GarenQ: GarenSlash3 {}
    public class GarenSlash3 : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { 0.15f, 0.2f, 0.25f, 0.3f, 0.35f };
        int[] effect1 = { 12, 11, 10, 9, 8 };
        float[] effect2 = { 1.5f, 2, 2.5f, 3, 3.5f };
        int[] effect3 = { 30, 45, 60, 75, 90 };
        public override void SelfExecute()
        {
            SetSlotSpellCooldownTimeVer2(0, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            float nextBuffVars_SpeedMod = effect0[level - 1];
            AddBuff(owner, owner, new Buffs.GarenFastMove(nextBuffVars_SpeedMod), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            int nextBuffVars_SpellCooldown = effect1[level - 1];
            float nextBuffVars_SilenceDuration = effect2[level - 1]; // UNUSED
            int nextBuffVars_BonusDamage = effect3[level - 1]; // UNUSED
            AddBuff(owner, owner, new Buffs.GarenSlash3(nextBuffVars_SpellCooldown), 1, 1, 6, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class GarenSlash3 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", "", },
            BuffName = "GarenSlash",
            BuffTextureName = "Garen_DecisiveStrike.dds",
        };
        EffectEmitter geeves1;
        EffectEmitter geeves2;
        float spellCooldown;
        public GarenSlash3(float spellCooldown = default)
        {
            this.spellCooldown = spellCooldown;
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out geeves1, out _, "garen_descisiveStrike_indicator.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_WEAPON_2", default, owner, default, default, true, false, false, false, false);
            SpellEffectCreate(out geeves2, out _, "garen_descisiveStrike_indicator_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_WEAPON_2", default, owner, default, default, true, false, false, false, false);
            //RequireVar(this.spellCooldown);
            //RequireVar(this.bonusDamage);
            //RequireVar(this.silenceDuration);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            CancelAutoAttack(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            float spellCooldown = this.spellCooldown;
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = multiplier * spellCooldown;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SpellEffectRemove(geeves1);
            SpellEffectRemove(geeves2);
        }
        public override void OnPreAttack(AttackableUnit target)
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SkipNextAutoAttack((ObjAIBase)owner);
            SpellCast((ObjAIBase)owner, target, default, default, 0, SpellSlotType.ExtraSlots, level, false, false, false, false, false, false);
            SpellBuffRemove(owner, nameof(Buffs.GarenSlash3), (ObjAIBase)owner, 0);
        }
    }
}