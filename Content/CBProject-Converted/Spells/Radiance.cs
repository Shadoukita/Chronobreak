namespace Spells
{
    public class Radiance : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class Radiance : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "BUFFBONE_CSTM_WEAPON_1", "BUFFBONE_CSTM_WEAPON_2", "spine", },
            AutoBuffActivateEffect = new[] { "Taric_HammerFlare.troy", "Taric_HammerFlare.troy", "Taric_ShoulderFlare.troy", },
            BuffName = "Radiance",
            BuffTextureName = "GemKnight_Radiance.dds",
            NonDispellable = true,
            SpellToggleSlot = 4,
        };
        float damageIncrease;
        float abilityPower;
        EffectEmitter particle;
        float lastTimeExecuted;
        public Radiance(float damageIncrease = default, float abilityPower = default)
        {
            this.damageIncrease = damageIncrease;
            this.abilityPower = abilityPower;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageIncrease);
            //RequireVar(this.abilityPower);
            IncFlatPhysicalDamageMod(owner, this.damageIncrease);
            IncFlatMagicDamageMod(owner, this.abilityPower);
            TeamId teamOfOwner = GetTeamID_CS(owner);
            SpellEffectCreate(out particle, out _, "taricgemstorm.troy", default, teamOfOwner, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, false, false);
            float damageIncrease = this.damageIncrease * 0.5f;
            float nextBuffVars_DamageIncrease = damageIncrease;
            float abilityPower = this.abilityPower * 0.5f;
            float nextBuffVars_AbilityPower = abilityPower;
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, default, true))
            {
                AddBuff(attacker, unit, new Buffs.RadianceAura(nextBuffVars_DamageIncrease, nextBuffVars_AbilityPower), 1, 1, 1.25f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                ApplyAssistMarker(attacker, unit, 10);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, damageIncrease);
            IncFlatMagicDamageMod(owner, abilityPower);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                float damageIncrease = this.damageIncrease * 0.5f;
                float nextBuffVars_DamageIncrease = damageIncrease;
                float abilityPower = this.abilityPower * 0.5f;
                float nextBuffVars_AbilityPower = abilityPower;
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, default, true))
                {
                    AddBuff(attacker, unit, new Buffs.RadianceAura(nextBuffVars_DamageIncrease, nextBuffVars_AbilityPower), 1, 1, 1.25f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                    ApplyAssistMarker(attacker, unit, 10);
                }
            }
        }
    }
}