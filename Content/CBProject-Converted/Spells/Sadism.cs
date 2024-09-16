namespace Spells
{
    public class Sadism : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 90f, 75f, 60f, },
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            SpellVOOverrideSkins = new[] { "CorporateMundo", },
        };
        float[] effect0 = { 0.15f, 0.25f, 0.35f };
        int[] effect1 = { 12, 12, 12 };
        public override void SelfExecute()
        {
            float health = GetHealth(target, PrimaryAbilityResourceType.MANA);
            float healthLoss = -0.2f * health;
            IncHealth(owner, healthLoss, owner);
            float nextBuffVars_SpeedMod = effect0[level - 1];
            AddBuff(attacker, target, new Buffs.Sadism(nextBuffVars_SpeedMod), 1, 1, effect1[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.HASTE, 0, true, false, false);
            SpellEffectCreate(out _, out _, "dr_mundo_sadism_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "pelvis", default, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out _, out _, "dr_mundo_sadism_cas_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_hand", default, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out _, out _, "dr_mundo_sadism_cas_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_hand", default, target, default, default, false, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class Sadism : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "dr_mundo_heal.troy", },
            BuffName = "Sadism",
            BuffTextureName = "DrMundo_Sadism.dds",
            NonDispellable = true,
        };
        float speedMod;
        int[] effect0 = { 12, 12, 12 };
        public Sadism(float speedMod = default)
        {
            this.speedMod = speedMod;
        }
        public override void OnActivate()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            //RequireVar(this.speedMod);
            AddBuff(attacker, target, new Buffs.SadismHeal(), 1, 1, effect0[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, speedMod);
        }
    }
}