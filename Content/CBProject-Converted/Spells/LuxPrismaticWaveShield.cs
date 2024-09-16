namespace Spells
{
    public class LuxPrismaticWaveShield : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class LuxPrismaticWaveShield : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "LuxPrismaticWave_shield.troy", },
            BuffName = "LuxShield",
            BuffTextureName = "LuxPrismaWrap.dds",
            OnPreDamagePriority = 3,
            DoOnPreDamageInExpirationOrder = true,
        };
        float damageBlock;
        bool willRemove;
        float oldArmorAmount;
        public LuxPrismaticWaveShield(float damageBlock = default, bool willRemove = default)
        {
            this.damageBlock = damageBlock;
            this.willRemove = willRemove;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageBlock);
            ApplyAssistMarker(attacker, owner, 10);
            IncreaseShield(owner, damageBlock, true, true);
        }
        public override void OnDeactivate(bool expired)
        {
            if (!willRemove)
            {
                SpellEffectCreate(out _, out _, "shen_Feint_self_deactivate.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            }
            if (damageBlock > 0)
            {
                RemoveShield(owner, damageBlock, true, true);
            }
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            oldArmorAmount = damageBlock;
            if (damageBlock >= damageAmount)
            {
                damageBlock -= damageAmount;
                damageAmount = 0;
                oldArmorAmount -= damageBlock;
                ReduceShield(owner, oldArmorAmount, true, true);
            }
            else
            {
                TeamId teamID = GetTeamID_CS(owner);
                damageAmount -= damageBlock;
                damageBlock = 0;
                ReduceShield(owner, oldArmorAmount, true, true);
                SpellEffectCreate(out _, out _, "SpellEffect_proc.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true);
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}