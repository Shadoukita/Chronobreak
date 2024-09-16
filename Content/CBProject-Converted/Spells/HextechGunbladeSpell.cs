namespace Spells
{
    public class HextechGunbladeSpell : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            ApplyDamage(attacker, target, 300, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 1, true, true, attacker);
            float nextBuffVars_MoveSpeedMod = -0.5f;
            AddBuff(attacker, target, new Buffs.BilgewaterCutlass(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.STACKS_AND_RENEWS, BuffType.SLOW, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class HextechGunbladeSpell : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "HextechGunblade",
            BuffTextureName = "3146_Hextech_Gunblade.dds",
        };
        float moveSpeedMod;
        EffectEmitter slow;
        public HextechGunbladeSpell(float moveSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
            SpellEffectCreate(out slow, out _, "Global_Slow.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(slow);
        }
        public override void OnUpdateStats()
        {
            float moveSpeedMod = this.moveSpeedMod;
            IncPercentMultiplicativeMovementSpeedMod(owner, moveSpeedMod);
        }
    }
}