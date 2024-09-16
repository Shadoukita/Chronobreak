namespace Spells
{
    public class Fling : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = { 100, 150, 200, 250, 300 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            Vector3 targetPos = GetUnitPosition(target);
            Vector3 landPos = GetPointByUnitFacingOffset(owner, 420, 180);
            float distance = DistanceBetweenPoints(targetPos, landPos);
            float delayTimer = distance / 1160;
            ApplyDamage(attacker, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 1, 1, false, false, attacker);
            AddBuff(attacker, target, new Buffs.Fling(), 1, 1, delayTimer, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class Fling : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PopupMessage = new[] { "game_floatingtext_Knockup", },
        };
        bool hasHitGround; // UNUSED
        public override void OnActivate()
        {
            hasHitGround = false;
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            SetCanCast(owner, false);
            Vector3 landPos = GetPointByUnitFacingOffset(attacker, 420, 180);
            Move(owner, landPos, 1000, 60, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 420);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID = GetTeamID_CS(attacker);
            SetCanAttack(owner, true);
            SetCanCast(owner, true);
            SetCanMove(owner, true);
            SpellEffectCreate(out _, out _, "fling_land.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, target, default, default, true);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            SetCanCast(owner, false);
        }
    }
}