namespace Spells
{
    public class OdinNeutralInvulnerable : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 90f, 90f, 90f, 90f, 90f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class OdinNeutralInvulnerable : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "minatuar_unbreakableWill_cas.troy", "feroscioushowl_cas2.troy", },
            BuffName = "JudicatorIntervention",
            BuffTextureName = "Judicator_EyeforanEye.dds",
        };
        EffectEmitter particle1;
        public override void OnActivate()
        {
            SetInvulnerable(owner, true);
            ApplyAssistMarker(attacker, owner, 10);
            SetTargetable(owner, false);
            SpellEffectCreate(out particle1, out _, "OdinNeutralInvulnerable.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetInvulnerable(owner, false);
            SpellEffectRemove(particle1);
            SetTargetable(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetInvulnerable(owner, true);
            SetTargetable(owner, false);
        }
    }
}