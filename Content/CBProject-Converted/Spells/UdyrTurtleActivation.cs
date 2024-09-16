namespace Spells
{
    public class UdyrTurtleActivation : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class UdyrTurtleActivation : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "UdyrTurtleActivation",
            BuffTextureName = "Udyr_TurtleStance.dds",
            OnPreDamagePriority = 3,
            DoOnPreDamageInExpirationOrder = true,
        };
        float shieldAmount;
        EffectEmitter turtleparticle;
        EffectEmitter turtleShield;
        float oldArmorAmount;
        public UdyrTurtleActivation(float shieldAmount = default)
        {
            this.shieldAmount = shieldAmount;
        }
        public override void OnActivate()
        {
            //RequireVar(this.shieldAmount);
            SpellEffectCreate(out turtleparticle, out _, "TurtleStance.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true);
            SpellEffectCreate(out turtleShield, out _, "TurtleStance_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true);
            IncreaseShield(owner, shieldAmount, true, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(turtleShield);
            SpellEffectRemove(turtleparticle);
            if (shieldAmount > 0)
            {
                RemoveShield(owner, shieldAmount, true, true);
            }
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            oldArmorAmount = shieldAmount;
            if (shieldAmount >= damageAmount)
            {
                shieldAmount -= damageAmount;
                damageAmount = 0;
                oldArmorAmount -= shieldAmount;
                ReduceShield(owner, oldArmorAmount, true, true);
            }
            else
            {
                damageAmount -= shieldAmount;
                shieldAmount = 0;
                ReduceShield(owner, oldArmorAmount, true, true);
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}