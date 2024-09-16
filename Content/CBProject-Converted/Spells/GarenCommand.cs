namespace Spells
{
    public class GarenW: GarenCommand {}
    public class GarenCommand : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        EffectEmitter particle; // UNUSED
        int[] effect0 = { 65, 100, 135, 170, 205 };
        float[] effect1 = { 0.2f, 0.24f, 0.28f, 0.32f, 0.36f };
        int[] effect2 = { 3, 3, 3, 3, 3 };
        public override void SelfExecute()
        {
            TeamId teamID = GetTeamID_CS(owner);
            float abilityPower = GetFlatMagicDamageMod(attacker);
            float armorAmount = effect0[level - 1];
            float nextBuffVars_DamageReduction = effect1[level - 1];
            float buffDuration = effect2[level - 1];
            abilityPower *= 0.8f;
            float totalArmorAmount = abilityPower + armorAmount;
            float nextBuffVars_TotalArmorAmount = totalArmorAmount;
            SpellEffectCreate(out particle, out _, "garen_command_cas.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, attacker, "C_BUFFBONE_GLB_CENTER_LOC", default, attacker, default, default, true, default, default, false, false);
            AddBuff(attacker, attacker, new Buffs.GarenCommand(nextBuffVars_DamageReduction, nextBuffVars_TotalArmorAmount), 1, 1, buffDuration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class GarenCommand : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "GarenCommand",
            BuffTextureName = "Garen_CommandingPresence.dds",
        };
        float damageReduction;
        EffectEmitter particle;
        float totalArmorAmount;
        public GarenCommand(float damageReduction = default, float totalArmorAmount = default)
        {
            this.damageReduction = damageReduction;
            this.totalArmorAmount = totalArmorAmount;
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots); // UNUSED
            //RequireVar(this.damageReduction);
            IncPercentMagicReduction(owner, damageReduction);
            SpellEffectCreate(out particle, out _, "garen_commandingPresence_unit_buf_self.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_BUFFBONE_GLB_CHEST_LOC", default, owner, default, default, true, default, default, false, false);
            SetBuffToolTipVar(1, totalArmorAmount);
            IncPercentPhysicalReduction(owner, damageReduction);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
        }
        public override void OnUpdateStats()
        {
            IncPercentPhysicalReduction(owner, damageReduction);
            IncPercentMagicReduction(owner, damageReduction);
        }
    }
}