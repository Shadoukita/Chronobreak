namespace Spells
{
    public class MoveAway : SpellScript
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
    public class MoveAway : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MoveAway",
            BuffTextureName = "Minotaur_TriumphantRoar.dds",
            NonDispellable = true,
            PopupMessage = new[] { "game_floatingtext_Knockup", },
        };
        float distance;
        float gravity;
        float speed;
        Vector3 center;
        float idealDistance;
        public MoveAway(float distance = 0, float gravity = 0, float speed = 0, Vector3 center = default, float idealDistance = 0)
        {
            this.distance = distance;
            this.gravity = gravity;
            this.speed = speed;
            this.center = center;
            this.idealDistance = idealDistance;
        }
        public override void OnActivate()
        {
            //RequireVar(this.distance);
            //RequireVar(this.idealDistance);
            //RequireVar(this.gravity);
            //RequireVar(this.speed);
            //RequireVar(this.center);
            MoveAway(owner, center, speed, gravity, distance, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, idealDistance);
            ApplyAssistMarker(attacker, owner, 10);
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanAttack(owner, true);
            SetCanCast(owner, true);
            SetCanMove(owner, true);
        }
    }
}