namespace Spells
{
    public class ShyvanaTransformDamage : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class ShyvanaTransformDamage : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Move",
            BuffTextureName = "Minotaur_TriumphantRoar.dds",
            NonDispellable = true,
            PopupMessage = new[] { "game_floatingtext_Knockup", },
        };
        float gravity;
        float speed;
        Vector3 position;
        float idealDistance;
        public ShyvanaTransformDamage(float gravity = 0, float speed = 0, Vector3 position = default, float idealDistance = 0)
        {
            this.gravity = gravity;
            this.speed = speed;
            this.position = position;
            this.idealDistance = idealDistance;
        }
        public override void OnActivate()
        {
            //RequireVar(this.gravity);
            //RequireVar(this.speed);
            //RequireVar(this.position);
            //RequireVar(this.idealDistance);
            Move(owner, position, speed, gravity, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, idealDistance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            ApplyAssistMarker(attacker, owner, 10);
            SetCanCast(owner, false);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            TeamId teamID = GetTeamID_CS(attacker);
            SpellEffectCreate(out _, out _, "shyvana_ult_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanCast(owner, true);
            SetCanAttack(owner, true);
            SetCanMove(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetCanCast(owner, false);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
        }
        public override void OnMoveEnd()
        {
            SpellBuffRemoveCurrent(owner);
        }
    }
}