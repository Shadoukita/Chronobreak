namespace Spells
{
    public class RadianceAura : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class RadianceAura : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "RadianceAura",
            BuffTextureName = "GemKnight_Radiance.dds",
        };
        float damageIncrease;
        float abilityPower;
        EffectEmitter particl3;
        public RadianceAura(float damageIncrease = default, float abilityPower = default)
        {
            this.damageIncrease = damageIncrease;
            this.abilityPower = abilityPower;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageIncrease);
            //RequireVar(this.abilityPower);
            if (owner is not Champion)
            {
                damageIncrease /= 3;
                abilityPower = 0;
            }
            if (owner is Champion)
            {
                TeamId teamOfOwner = GetTeamID_CS(owner);
                SpellEffectCreate(out particl3, out _, "Taric_GemStorm_Aura.troy", default, teamOfOwner, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            if (owner is Champion)
            {
                SpellEffectRemove(particl3);
            }
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, damageIncrease);
            IncFlatMagicDamageMod(owner, abilityPower);
        }
    }
}