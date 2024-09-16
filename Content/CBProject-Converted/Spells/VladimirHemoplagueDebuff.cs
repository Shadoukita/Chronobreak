namespace Spells
{
    public class VladimirHemoplagueDebuff : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
            SpellFXOverrideSkins = new[] { "BloodkingVladimir", },
        };
    }
}
namespace Buffs
{
    public class VladimirHemoplagueDebuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "VladimirHemoplagueDebuff",
            BuffTextureName = "Vladimir_Hemoplague.dds",
        };
        float damageIncrease;
        float damagePerLevel;
        int vladSkinID;
        EffectEmitter varrr1;
        public VladimirHemoplagueDebuff(float damageIncrease = default, float damagePerLevel = default)
        {
            this.damageIncrease = damageIncrease;
            this.damagePerLevel = damagePerLevel;
        }
        public override void OnActivate()
        {
            ObjAIBase caster = GetBuffCasterUnit();
            //RequireVar(this.damageIncrease);
            //RequireVar(this.damagePerLevel);
            ApplyAssistMarker(attacker, owner, 10);
            vladSkinID = GetSkinID(caster);
            if (vladSkinID == 5)
            {
                SpellEffectCreate(out varrr1, out _, "VladHemoplague_BloodKing_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "spine", default, owner, default, default, false, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out varrr1, out _, "VladHemoplague_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "spine", default, owner, default, default, false, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            if (vladSkinID == 5)
            {
                SpellEffectCreate(out _, out _, "VladHemoplague_BloodKing_proc.troy", default, TeamId.TEAM_NEUTRAL, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out _, out _, "VladHemoplague_proc.troy", default, TeamId.TEAM_NEUTRAL, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
            if (expired)
            {
                ApplyDamage(attacker, owner, damagePerLevel, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 1, false, false, attacker);
            }
            SpellEffectRemove(varrr1);
        }
        public override void OnUpdateStats()
        {
            IncPercentPhysicalReduction(owner, damageIncrease);
            IncPercentMagicReduction(owner, damageIncrease);
        }
    }
}