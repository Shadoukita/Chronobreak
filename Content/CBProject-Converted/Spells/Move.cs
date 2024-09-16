namespace Spells
{
    public class Move : SpellScript
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
    public class Move : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Move",
            BuffTextureName = "Minotaur_TriumphantRoar.dds",
            NonDispellable = true,
            PopupMessage = new[] { "game_floatingtext_Knockup", },
        };
        float speed;
        float gravity;
        Vector3 position;
        float idealDistance;
        public Move(float speed = default, float gravity = default, Vector3 position = default, float idealDistance = 0)
        {
            this.speed = speed;
            this.gravity = gravity;
            this.position = position;
            this.idealDistance = idealDistance;
        }
        public override void OnActivate()
        {
            //RequireVar(this.gravity);
            //RequireVar(this.speed);
            //RequireVar(this.position);
            //RequireVar(this.idealDistance);
            Move(owner, position, speed, gravity, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, idealDistance);
            ApplyAssistMarker(attacker, owner, 10);
            SetCanCast(owner, false);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
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
    }
}