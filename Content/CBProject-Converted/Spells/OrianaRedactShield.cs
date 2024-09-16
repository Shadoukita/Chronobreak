namespace Spells
{
    public class OrianaRedactShield : SpellScript
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
    public class OrianaRedactShield : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "OrianaProtectShield.troy", },
            BuffName = "OrianaRedactShield",
            BuffTextureName = "OriannaCommandRedact.dds",
            OnPreDamagePriority = 3,
            DoOnPreDamageInExpirationOrder = true,
        };
        float damageBlock;
        bool willRemove;
        float oldArmorAmount;
        public OrianaRedactShield(float damageBlock = default, bool willRemove = default)
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
                SpellEffectCreate(out _, out _, "OrianaProtectShield.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false, false);
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
                TeamId teamID = GetTeamID_CS(owner); // UNUSED
                damageAmount -= damageBlock;
                damageBlock = 0;
                ReduceShield(owner, oldArmorAmount, true, true);
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}