namespace Spells
{
    public class LeonaSolarBarrier2 : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class LeonaSolarBarrier2 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "LeonaSolarBarrier",
            BuffTextureName = "LeonaSolarBarrier.dds",
            SpellToggleSlot = 2,
        };
        float defenseBonus;
        EffectEmitter particle;
        public LeonaSolarBarrier2(float defenseBonus = default)
        {
            this.defenseBonus = defenseBonus;
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            //RequireVar(this.defenseBonus);
            IncFlatArmorMod(owner, defenseBonus);
            IncFlatSpellBlockMod(owner, defenseBonus);
            SpellEffectCreate(out particle, out _, "Leona_SolarBarrier2_buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
            OverrideAnimation("Idle1", "Spell2_idle", owner);
            OverrideAnimation("Idle2", "Spell2_idle", owner);
            OverrideAnimation("Idle3", "Spell2_idle", owner);
            OverrideAnimation("Idle4", "Spell2_idle", owner);
            OverrideAnimation("Attack1", "Spell2_attack", owner);
            OverrideAnimation("Attack2", "Spell2_attack", owner);
            OverrideAnimation("Attack3", "Spell2_attack", owner);
            OverrideAnimation("Crit", "Spell2_attack", owner);
            OverrideAnimation("Run", "Spell2_run", owner);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            ClearOverrideAnimation("Idle1", owner);
            ClearOverrideAnimation("Idle2", owner);
            ClearOverrideAnimation("Idle3", owner);
            ClearOverrideAnimation("Idle4", owner);
            ClearOverrideAnimation("Attack1", owner);
            ClearOverrideAnimation("Attack2", owner);
            ClearOverrideAnimation("Attack3", owner);
            ClearOverrideAnimation("Crit", owner);
            ClearOverrideAnimation("Run", owner);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, defenseBonus);
            IncFlatSpellBlockMod(owner, defenseBonus);
        }
    }
}