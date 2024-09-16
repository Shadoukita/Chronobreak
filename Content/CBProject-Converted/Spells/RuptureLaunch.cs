namespace Buffs
{
    public class RuptureLaunch : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Rupture",
            BuffTextureName = "GreenTerror_SpikeSlam.dds",
            PopupMessage = new[] { "game_floatingtext_Knockup", },
        };
        float moveSpeedMod;
        public RuptureLaunch(float moveSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageAmount);
            //RequireVar(this.moveSpeedMod);
            Vector3 bouncePos = GetRandomPointInAreaUnit(owner, 10, 10);
            Move(owner, bouncePos, 10, 20, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 10, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanAttack(owner, true);
            SetCanCast(owner, true);
            SetCanMove(owner, true);
            float nextBuffVars_MoveSpeedMod = moveSpeedMod;
            AddBuff(attacker, owner, new Buffs.RuptureTarget(nextBuffVars_MoveSpeedMod), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
        }
    }
}