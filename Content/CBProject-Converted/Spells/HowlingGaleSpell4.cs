namespace Spells
{
    public class HowlingGaleSpell4 : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 160, 220, 280, 340, 400 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            ObjAIBase attacker = GetBuffCasterUnit();
            if (attacker != target)
            {
                BreakSpellShields(target);
                ApplyDamage(attacker, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.8f, 1, false, false, attacker);
                Vector3 bouncePos = GetRandomPointInAreaUnit(target, 100, 100);
                Vector3 nextBuffVars_Position = bouncePos;
                float nextBuffVars_IdealDistance = 100;
                float nextBuffVars_Speed = 100;
                float nextBuffVars_Gravity = 20;
                AddBuff(attacker, target, new Buffs.Move(nextBuffVars_Speed, nextBuffVars_Gravity, nextBuffVars_Position), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class HowlingGaleSpell4 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "DarkBinding_tar.troy", "", },
            BuffName = "Dark Binding",
            BuffTextureName = "FallenAngel_DarkBinding.dds",
        };
    }
}