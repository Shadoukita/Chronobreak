namespace Spells
{
    public class CassiopeiaPetrifyingGazeSpell : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        float[] effect0 = { -0.6f, -0.6f, -0.6f };
        int[] effect1 = { 200, 325, 450 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            BreakSpellShields(target);
            TeamId teamID = GetTeamID_CS(target);
            if (IsInFront(target, attacker))
            {
                AddBuff(attacker, target, new Buffs.CassiopeiaPetrifyingGaze(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
            }
            else
            {
                float nextBuffVars_MoveSpeedMod = effect0[level - 1];
                AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 1, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, true, false);
                SpellEffectCreate(out _, out _, "CassPetrifyMiss_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, target, false, target, "root", default, target, default, default, true, default, default, false);
            }
            ApplyDamage(attacker, target, effect1[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 1, false, false, attacker);
        }
    }
}
namespace Buffs
{
    public class CassiopeiaPetrifyingGazeSpell : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head", },
            AutoBuffActivateEffect = new[] { "Stun_glb.troy", },
        };
    }
}