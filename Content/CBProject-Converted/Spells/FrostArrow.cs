namespace Spells
{
    public class FrostArrow : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (target is ObjAIBase && target is not BaseTurret)
            {
                AddBuff(owner, owner, new Buffs.FrostArrowApplicator(), 1, 1, 0.1f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            float baseAttackDamage = GetBaseAttackDamage(owner);
            ApplyDamage(attacker, target, baseAttackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
        }
    }
}
namespace Buffs
{
    public class FrostArrow : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Global_Freeze.troy", },
            BuffName = "Frost Arrow",
            BuffTextureName = "3022_Frozen_Heart.dds",
            IsDeathRecapSource = true,
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        float movementSpeedMod;
        public FrostArrow(float movementSpeedMod = default)
        {
            this.movementSpeedMod = movementSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.movementSpeedMod);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, movementSpeedMod);
        }
    }
}