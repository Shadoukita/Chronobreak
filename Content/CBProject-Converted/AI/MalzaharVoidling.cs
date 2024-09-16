using Chronobreak.GameServer.Scripting.CSharp.Converted;

namespace AIScripts;

//Status: 100% Identical to Lua script
public class MalzaharVoidlingAI : CAIScript
{
    protected override float FAR_MOVEMENT_DISTANCE => 800;
    protected override float MINION_MAX_VISION_DISTANCE => 1200;
    protected override float TELEPORT_DISTANCE => 2000;
    protected override float FEAR_WANDER_DISTANCE => 500;

    public override bool OnInit()
    {
        SetState(AIState.AI_PET_IDLE);
        InitTimer(TimerScanDistance, 0.15f, true);
        InitTimer(TimerFindEnemies, 0.15f, true);
        StopTimer(TimerFeared);
        StopTimer(TimerFlee);
        return false;
    }

    public override bool OnOrder(OrderType orderType, AttackableUnit target, Vector2 position)
    {
        var petOwner = GetGoldRedirectTarget();
        if (petOwner == null)
        {
            Die(Me, DamageSource.DAMAGE_SOURCE_INTERNALRAW);
            return false;
        }
        if (orderType == OrderType.AttackMove)
        {
            SetPetReturnRadius(Me, 10);
            SetStateAndCloseToTarget(AIState.AI_PET_RETURN, petOwner);
            return true;
        }
        if (orderType == OrderType.Hold)
        {
            SetPetReturnRadius(Me, 10);
            SetStateAndCloseToTarget(AIState.AI_PET_RETURN, petOwner);
            return true;
        }
        return true;
    }

    public override void OnTargetLost(LostTargetEvent eventType, AttackableUnit target)
    {
        var state = GetState();
        if (state == AIState.AI_HALTED)
        {
            return;
        }
        if (state is AIState.AI_PET_MOVE or AIState.AI_PET_HARDMOVE or AIState.AI_PET_HARDRETURN or AIState.AI_FEARED or AIState.AI_FLEEING or AIState.AI_PET_HARDSTOP)
        {
            return;
        }
        var newTarget = FindTargetInAcR();
        if (newTarget == null)
        {
            var petOwner = GetGoldRedirectTarget();
            if (petOwner == null)
            {
                Die(Me, DamageSource.DAMAGE_SOURCE_INTERNALRAW);
                return;
            }
            if (state == AIState.AI_PET_HARDIDLE_ATTACKING)
            {
                NetSetState(AIState.AI_PET_HARDIDLE);
                return;
            }

            if (state == AIState.AI_PET_RETURN_ATTACKING)
            {
                SetStateAndCloseToTarget(AIState.AI_PET_RETURN, petOwner);
                return;
            }

            if (state == AIState.AI_PET_ATTACKMOVE_ATTACKING)
            {
                SetStateAndCloseToTarget(AIState.AI_PET_ATTACKMOVE, petOwner);
                return;
            }
        }
        else
        {
            if (state != AIState.AI_PET_HARDATTACK)
            {
                if (state != AIState.AI_PET_ATTACK)
                {
                    if (state != AIState.AI_TAUNTED)
                    {
                        return;
                    }
                }
            }
            SetStateAndCloseToTarget(AIState.AI_PET_ATTACK, newTarget);
            return;
        }
        NetSetState(AIState.AI_PET_IDLE);
    }

    public override void OnTauntBegin()
    {
        var state = GetState();
        if (state == AIState.AI_HALTED)
        {
            return;
        }
        var tauntTarget = GetTauntTarget();
        if (tauntTarget != null)
        {
            SetStateAndCloseToTarget(AIState.AI_TAUNTED, tauntTarget);
        }
    }

    public override void OnTauntEnd()
    {
        var state = GetState();
        if (state == AIState.AI_HALTED)
        {
            return;
        }
        var tauntTarget = GetTauntTarget();
        if (tauntTarget != null)
        {
            SetStateAndCloseToTarget(AIState.AI_PET_ATTACK, tauntTarget);
        }
        else
        {
            NetSetState(AIState.AI_PET_IDLE);
            TimerFindEnemies();
        }
    }

    public override void OnFearBegin()
    {
        var state = GetState();
        if (state == AIState.AI_HALTED)
        {
            return;
        }
        var wanderPoint = MakeWanderPoint(GetFearLeashPoint(), FEAR_WANDER_DISTANCE);
        SetStateAndMove(AIState.AI_FEARED, wanderPoint);
        TurnOffAutoAttack(StopReason.IMMEDIATELY);
        ResetAndStartTimer(TimerFeared);
    }

    public override void OnFearEnd()
    {
        var state = GetState();
        if (state == AIState.AI_HALTED)
        {
            return;
        }
        StopTimer(TimerFeared);
        NetSetState(AIState.AI_PET_IDLE);
        TimerFindEnemies();
    }

    private void TimerFeared()
    {
        var state = GetState();
        if (state == AIState.AI_HALTED)
        {
            return;
        }
        var wanderPoint = MakeWanderPoint(GetFearLeashPoint(), FEAR_WANDER_DISTANCE);
        SetStateAndMove(AIState.AI_FEARED, wanderPoint);
    }

    public override void OnFleeBegin()
    {
        var state = GetState();
        if (state == AIState.AI_HALTED)
        {
            return;
        }
        var fleePoint = MakeFleePoint();
        SetStateAndMove(AIState.AI_FLEEING, fleePoint);
        TurnOffAutoAttack(StopReason.IMMEDIATELY);
        ResetAndStartTimer(TimerFlee);
    }

    public override void OnFleeEnd()
    {
        var state = GetState();
        if (state == AIState.AI_HALTED)
        {
            return;
        }
        StopTimer(TimerFlee);
        NetSetState(AIState.AI_PET_IDLE);
        TimerFindEnemies();
    }

    private void TimerFlee()
    {
        var state = GetState();
        if (state == AIState.AI_HALTED)
        {
            return;
        }
        var fleePoint = MakeFleePoint();
        SetStateAndMove(AIState.AI_FLEEING, fleePoint);
    }

    public override void OnCanMove()
    {
        var state = GetState();
        if (state == AIState.AI_HALTED)
        {
            return;
        }
        if (GetCanAttack(Me) == false)
        {
            return;
        }
        NetSetState(AIState.AI_PET_IDLE);
        TimerFindEnemies();
    }

    public override void OnCanAttack()
    {
        var state = GetState();
        if (state == AIState.AI_HALTED)
        {
            return;
        }
        if (GetCanMove(Me) == false)
        {
            return;
        }
        NetSetState(AIState.AI_PET_IDLE);
        TimerFindEnemies();
    }

    private void TimerScanDistance()
    {
        var state = GetState();
        if (state == AIState.AI_HALTED)
        {
            return;
        }
        if (GetCanAttack(Me) == false)
        {
            return;
        }
        var tempOwner = GetGoldRedirectTarget();
        if (tempOwner == null)
        {
            Die(Me, DamageSource.DAMAGE_SOURCE_INTERNALRAW);
            return;
        }
        var canMove = GetCanMove(Me);
        float distanceToOwner = DistanceBetweenObjectBounds(Me, tempOwner);
        if (canMove)
        {
            if (distanceToOwner > TELEPORT_DISTANCE)
            {
                SetActorPositionFromObject(Me, tempOwner);
                NetSetState(AIState.AI_PET_IDLE);
                return;
            }
        }
        if (state != AIState.AI_TAUNTED)
        {
            if (canMove)
            {
                if (distanceToOwner > FAR_MOVEMENT_DISTANCE)
                {
                    if (distanceToOwner > MINION_MAX_VISION_DISTANCE)
                    {
                        SetLastPosPointWithObj(tempOwner);
                        SetStateAndMoveInPos(AIState.AI_PET_MOVE);
                    }
                    else
                    {
                        SetStateAndCloseToTarget(AIState.AI_PET_MOVE, tempOwner);
                    }
                    return;
                }
            }
        }
        state = GetState();
        var noEnemiesNear = FindTargetInAcR() == null;
        if (state == AIState.AI_PET_IDLE)
        {
            if (GetPetReturnRadius(Me) < 0)
            {
                SetStateAndCloseToTarget(AIState.AI_PET_RETURN, tempOwner);
                return;
            }
        }
        if (state == AIState.AI_PET_IDLE)
        {
            if (distanceToOwner > GetPetReturnRadius(Me))
            {
                if (noEnemiesNear)
                {
                    SetStateAndCloseToTarget(AIState.AI_PET_RETURN, tempOwner);
                    return;
                }
            }
        }
        if (state != AIState.AI_PET_MOVE && state != AIState.AI_PET_RETURN && state != AIState.AI_PET_HARDRETURN)
        {
            return;
        }
        if (distanceToOwner <= GetPetReturnRadius(Me))
        {
            if (0 < GetPetReturnRadius(Me))
            {
                NetSetState(AIState.AI_PET_IDLE);
            }
            else
            {
                SetStateAndCloseToTarget(AIState.AI_PET_RETURN, tempOwner);
            }
        }
    }

    private void TimerFindEnemies()
    {
        if (GetState() == AIState.AI_HALTED)
        {
            return;
        }
        if (GetGoldRedirectTarget() == null)
        {
            Die(Me, DamageSource.DAMAGE_SOURCE_INTERNALRAW);
            return;
        }
        var state = GetState();
        if (GetCanAttack(Me) == false)
        {
            return;
        }
        if (state == AIState.AI_PET_MOVE || state == AIState.AI_PET_HARDMOVE || state == AIState.AI_PET_HARDSTOP)
        {
            return;
        }
        if (state != AIState.AI_PET_IDLE && state != AIState.AI_PET_RETURN && state != AIState.AI_PET_ATTACKMOVE && state != AIState.AI_PET_HARDIDLE)
        {
            return;
        }
        var newTarget = FindTargetInAcRUsingGoldRedirectTarget();
        if (newTarget == null)
        {
            TurnOffAutoAttack(StopReason.TARGET_LOST);
            return;
        }
        switch (state)
        {
            case AIState.AI_PET_IDLE:
                SetStateAndCloseToTarget(AIState.AI_PET_ATTACK, newTarget);
                break;
            case AIState.AI_PET_RETURN:
                SetStateAndCloseToTarget(AIState.AI_PET_RETURN_ATTACKING, newTarget);
                break;
            case AIState.AI_PET_ATTACKMOVE:
                SetStateAndCloseToTarget(AIState.AI_PET_ATTACKMOVE_ATTACKING, newTarget);
                break;
            case AIState.AI_PET_HARDIDLE:
                NetSetState(AIState.AI_PET_HARDIDLE_ATTACKING);
                break;
        }
    }

    public override void HaltAI()
    {
        StopTimer(TimerScanDistance);
        StopTimer(TimerFindEnemies);
        StopTimer(TimerFeared);
        TurnOffAutoAttack(StopReason.IMMEDIATELY);
        NetSetState(AIState.AI_HALTED);
    }
}