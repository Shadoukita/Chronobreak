namespace Spells
{
    public class ShenShadowStep : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float gravityVar;
            float speedVar;
            AddBuff((ObjAIBase)target, attacker, new Buffs.ShenShadowStep(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
            float distance = DistanceBetweenObjects(attacker, target);
            if (distance >= 600)
            {
                gravityVar = 70;
                speedVar = 1150;
            }
            else if (distance >= 500)
            {
                gravityVar = 80;
                speedVar = 1150;
            }
            else if (distance >= 400)
            {
                gravityVar = 100;
                speedVar = 1080;
            }
            else if (distance >= 300)
            {
                gravityVar = 120;
                speedVar = 1010;
            }
            else if (distance >= 200)
            {
                gravityVar = 150;
                speedVar = 950;
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
            Move(attacker, target.Position3D, speedVar, gravityVar, 100, ForceMovementType.FURTHEST_WITHIN_RANGE);
        }
    }
}
namespace Buffs
{
    public class ShenShadowStep : BuffScript
    {
        bool hasDealtDamage;
        public override void OnActivate()
        {
            hasDealtDamage = false;
        }
        public override void OnUpdateActions()
        {
            if (!hasDealtDamage)
            {
                float distance = DistanceBetweenObjects(owner, attacker);
                if (distance <= 500)
                {
                    hasDealtDamage = true;
                    SpellCast((ObjAIBase)owner, attacker, attacker.Position3D, attacker.Position3D, 0, SpellSlotType.ExtraSlots, 1, true, false, false);
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}