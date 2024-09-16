namespace Buffs
{
    public class VeigarEventHorizonMarker : BuffScript
    {
        float stunDuration;
        Vector3 targetPos;
        public VeigarEventHorizonMarker(float stunDuration = default, Vector3 targetPos = default)
        {
            this.stunDuration = stunDuration;
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            //RequireVar(this.stunDuration);
            //RequireVar(this.targetPos);
            Vector3 targetPos = this.targetPos;
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            float speed = GetMovementSpeed(owner);
            float plusBonus = speed * 0.15f;
            plusBonus += 5;
            float upperBound = 350 + plusBonus;
            float lowerBound = 350 - plusBonus;
            if (distance >= lowerBound && distance <= upperBound)
            {
                AddBuff(attacker, owner, new Buffs.VeigarEventHorizonPrevent(), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                BreakSpellShields(owner);
                ApplyStun(attacker, owner, stunDuration);
                SpellBuffRemove(owner, nameof(Buffs.VeigarEventHorizonMarker), attacker);
            }
        }
        public override void OnUpdateActions()
        {
            Vector3 targetPos = this.targetPos;
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            float speed = GetMovementSpeed(owner);
            float plusBonus = speed * 0.15f;
            plusBonus += 5;
            float upperBound = 350 + plusBonus;
            float lowerBound = 350 - plusBonus;
            if (distance >= lowerBound && distance <= upperBound && !IsDead(attacker))
            {
                AddBuff(attacker, owner, new Buffs.VeigarEventHorizonPrevent(), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                BreakSpellShields(owner);
                ApplyStun(attacker, owner, stunDuration);
                SpellBuffRemove(owner, nameof(Buffs.VeigarEventHorizonMarker), attacker);
            }
        }
    }
}