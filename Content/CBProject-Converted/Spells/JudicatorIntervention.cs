namespace Spells
{
    public class JudicatorIntervention : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 90f, 90f, 90f, 90f, 90f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { 2, 2.5f, 3 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            AddBuff(owner, owner, new Buffs.KayleInterventionAnim(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, target, new Buffs.JudicatorIntervention(), 1, 1, effect0[level - 1], BuffAddType.RENEW_EXISTING, BuffType.INVULNERABILITY, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class JudicatorIntervention : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "JudicatorIntervention",
            BuffTextureName = "Judicator_EyeforanEye.dds",
        };
        EffectEmitter self;
        EffectEmitter cas;
        public override void OnActivate()
        {
            SetInvulnerable(owner, true);
            if (attacker == owner)
            {
                SpellEffectCreate(out self, out _, "eyeforaneye_self.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false, false);
            }
            else
            {
                SpellEffectCreate(out cas, out _, "eyeforaneye_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false, false);
            }
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SetInvulnerable(owner, false);
            if (attacker == owner)
            {
                SpellEffectRemove(self);
            }
            else
            {
                SpellEffectRemove(cas);
            }
        }
        public override void OnUpdateStats()
        {
            SetInvulnerable(owner, true);
        }
    }
}