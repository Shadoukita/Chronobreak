namespace Spells
{
    public class LeapStrike : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        public override bool CanCast()
        {
            bool returnValue = true;
            bool canMove = GetCanMove(owner);
            bool canCast = GetCanCast(owner);
            if (!canMove)
            {
                returnValue = false;
            }
            if (!canCast)
            {
                returnValue = false;
            }
            return returnValue;
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float gravityVar;
            float speedVar;
            AddBuff((ObjAIBase)target, attacker, new Buffs.LeapStrike(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            float distance = DistanceBetweenObjects(attacker, target);
            if (distance >= 600)
            {
                gravityVar = 100;
                speedVar = 1450;
            }
            else if (distance >= 500)
            {
                gravityVar = 110;
                speedVar = 1300;
            }
            else if (distance >= 400)
            {
                gravityVar = 120;
                speedVar = 1150;
            }
            else if (distance >= 300)
            {
                gravityVar = 130;
                speedVar = 1100;
            }
            else if (distance >= 200)
            {
                gravityVar = 150;
                speedVar = 1000;
            }
            else if (distance >= 100)
            {
                gravityVar = 300;
                speedVar = 900;
            }
            else //if(distance >= 0)
            {
                gravityVar = 1000;
                speedVar = 900;
            }
            Vector3 targetPos = GetUnitPosition(target);
            Move(attacker, targetPos, speedVar, gravityVar, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, distance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            float factor = distance / 700;
            factor = Math.Max(factor, 0.25f);
            factor = Math.Min(factor, 0.9f);
            PlayAnimation("Spell2", factor, attacker, false, false, false);
            if (owner.Team == target.Team && GetBuffCountFromCaster(target, default, nameof(Buffs.SharedWardBuff)) > 0)
            {
                AddBuff(attacker, target, new Buffs.Destealth(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class LeapStrike : BuffScript
    {
        public override void OnMoveEnd()
        {
            SpellBuffRemove(owner, nameof(Buffs.LeapStrike), attacker, 0);
        }
        public override void OnMoveSuccess()
        {
            ObjAIBase attacker = GetBuffCasterUnit();
            if (attacker.Team != owner.Team)
            {
                SpellCast((ObjAIBase)owner, attacker, attacker.Position3D, attacker.Position3D, 0, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
            }
        }
    }
}