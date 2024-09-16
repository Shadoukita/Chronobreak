namespace Spells
{
    public class VolibearQExtra : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class VolibearQExtra : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "GarenSlash",
            BuffTextureName = "17.dds",
            PopupMessage = new[] { "game_floatingtext_Knockup", },
        };
        Vector3 bouncePos;
        public VolibearQExtra(Vector3 bouncePos = default)
        {
            this.bouncePos = bouncePos;
        }
        public override void OnActivate()
        {
            //RequireVar(this.bouncePos);
            ObjAIBase caster = GetBuffCasterUnit(); // UNUSED
            float idealDistance = 70;
            float speed = 150;
            float gravity = 60;
            Move(owner, bouncePos, speed, gravity, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, idealDistance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
            SetCanAttack(owner, true);
            SetCanCast(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
        }
    }
}