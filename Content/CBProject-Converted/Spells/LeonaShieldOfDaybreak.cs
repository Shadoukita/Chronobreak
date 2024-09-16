namespace Spells
{
    public class LeonaShieldOfDaybreak : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 12, 11, 10, 9, 8 };
        int[] effect1 = { 1, 1, 1, 1, 1 };
        int[] effect2 = { 35, 55, 75, 95, 115 };
        public override void SelfExecute()
        {
            SetSlotSpellCooldownTimeVer2(0, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            int nextBuffVars_SpellCooldown = effect0[level - 1];
            int nextBuffVars_SilenceDuration = effect1[level - 1]; // UNUSED
            int nextBuffVars_BonusDamage = effect2[level - 1]; // UNUSED
            AddBuff(owner, owner, new Buffs.LeonaShieldOfDaybreak(nextBuffVars_SpellCooldown), 1, 1, 6, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class LeonaShieldOfDaybreak : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", "", },
            BuffName = "LeonaShieldOfDaybreak",
            BuffTextureName = "LeonaShieldOfDaybreak.DDS",
        };
        EffectEmitter temp;
        float spellCooldown;
        float rangeIncrease;
        public LeonaShieldOfDaybreak(float spellCooldown = default)
        {
            this.spellCooldown = spellCooldown;
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out temp, out _, "Leona_ShieldOfDaybreak_cas.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_SHIELD_TOP", default, owner, default, default, true, default, default, false, false);
            //RequireVar(this.spellCooldown);
            //RequireVar(this.bonusDamage);
            //RequireVar(this.silenceDuration);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            CancelAutoAttack(owner, true);
            rangeIncrease = 0;
            IncFlatAttackRangeMod(owner, 30 + rangeIncrease);
        }
        public override void OnDeactivate(bool expired)
        {
            float spellCooldown = this.spellCooldown;
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = multiplier * spellCooldown;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SpellEffectRemove(temp);
        }
        public override void OnUpdateStats()
        {
            IncFlatAttackRangeMod(owner, 30 + rangeIncrease);
        }
        public override void OnPreAttack(AttackableUnit target)
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SkipNextAutoAttack((ObjAIBase)owner);
            SpellCast((ObjAIBase)owner, target, default, default, 0, SpellSlotType.ExtraSlots, level, false, false, false, false, false, false);
            SpellBuffRemove(owner, nameof(Buffs.LeonaShieldOfDaybreak), (ObjAIBase)owner, 0);
        }
    }
}