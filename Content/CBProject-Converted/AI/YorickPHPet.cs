using Chronobreak.GameServer.Scripting.CSharp.Converted;

namespace AIScripts;

//Status: 100% Identical to Lua script
public class YorickPHPetAI : CAIScript
{
    protected override float FAR_MOVEMENT_DISTANCE => 2500;
    protected override float MINION_MAX_VISION_DISTANCE => 1200;
    protected override float TELEPORT_DISTANCE => 2500;
    protected override float FEAR_WANDER_DISTANCE => 400;

    public override bool OnInit()
    {
        SetState(AIState.AI_PET_IDLE);
        InitTimer(TimerScanDistance, 0.15f, true);
        InitTimer(TimerFindEnemies, 0.15f, true);
        InitTimer(TimerFeared, 1, true);
        InitTimer(TimerFlee, 0.5f, true);
        StopTimer(TimerFeared);
        StopTimer(TimerFlee);
        return false;
    }

    public override bool OnOrder(OrderType orderType, AttackableUnit target, Vector2 position)
    {
        return true;
    }

    public override void OnTargetLost(LostTargetEvent eventType, AttackableUnit target)
    {
        if (GetState() == AIState.AI_HALTED) return;

        if (GetState() == AIState.AI_PET_MOVE ||
            GetState() == AIState.AI_PET_HARDMOVE ||
            GetState() == AIState.AI_PET_HARDRETURN ||
            GetState() == AIState.AI_FEARED ||
            GetState() == AIState.AI_FLEEING ||
            GetState() == AIState.AI_PET_HARDSTOP)
            return;

        var newTarget = FindTargetInAcR();

        if (newTarget == null)
        {
            var owner = GetGoldRedirectTarget();

            if (owner == null)
            {
                Die(Me, DamageSource.DAMAGE_SOURCE_INTERNALRAW);
                return;
            }

            if (GetState() == AIState.AI_PET_HARDIDLE_ATTACKING)
            {
                NetSetState(AIState.AI_PET_HARDIDLE);
                return;
            }

            if (GetState() == AIState.AI_PET_RETURN_ATTACKING)
            {
                SetStateAndCloseToTarget(AIState.AI_PET_RETURN, Me);
                return;
            }

            if (GetState() == AIState.AI_PET_ATTACKMOVE_ATTACKING)
            {
                SetStateAndCloseToTarget(AIState.AI_PET_ATTACKMOVE, Me);
                return;
            }
        }
        else
        {
            if (GetState() != AIState.AI_PET_HARDATTACK)
                if (GetState() != AIState.AI_PET_ATTACK)
                    if (GetState() != AIState.AI_TAUNTED)
                    {
                        SetStateAndCloseToTarget(AIState.AI_PET_ATTACK, newTarget);
                        return;
                    }

            if (GetState() == AIState.AI_PET_HARDIDLE_ATTACKING)
            {
                NetSetState(AIState.AI_PET_HARDIDLE_ATTACKING);

                SetTarget(newTarget);
                return;
            }

            if (GetState() == AIState.AI_PET_RETURN_ATTACKING)
            {
                SetStateAndCloseToTarget(AIState.AI_PET_RETURN_ATTACKING, newTarget);
                return;
            }

            if (GetState() == AIState.AI_PET_ATTACKMOVE_ATTACKING)
            {
                SetStateAndCloseToTarget(AIState.AI_PET_ATTACKMOVE_ATTACKING, newTarget);
                return;
            }
        }

        NetSetState(AIState.AI_PET_IDLE);
    }

    public override void OnTauntBegin()
    {
        if (GetState() == AIState.AI_HALTED) return;

        var tauntTarget = GetTauntTarget();

        if (tauntTarget != null) SetStateAndCloseToTarget(AIState.AI_TAUNTED, tauntTarget);
    }

    public override void OnTauntEnd()
    {
        if (GetState() == AIState.AI_HALTED) return;

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
        if (GetState() == AIState.AI_HALTED) return;

        var wanderPoint = MakeWanderPoint(GetFearLeashPoint(), FEAR_WANDER_DISTANCE);
        SetStateAndMove(AIState.AI_FEARED, wanderPoint);
        TurnOffAutoAttack(StopReason.IMMEDIATELY);
        ResetAndStartTimer(TimerFeared);
    }

    public override void OnFearEnd()
    {
        if (GetState() == AIState.AI_HALTED) return;
        StopTimer(TimerFeared);
        NetSetState(AIState.AI_PET_IDLE);
        TimerFindEnemies();
    }

    private void TimerFeared()
    {
        if (GetState() == AIState.AI_HALTED) return;

        var wanderPoint = MakeWanderPoint(GetFearLeashPoint(), FEAR_WANDER_DISTANCE);
        SetStateAndMove(AIState.AI_FEARED, wanderPoint);
    }

    public override void OnFleeBegin()
    {
        if (GetState() == AIState.AI_HALTED) return;

        var fleePoint = MakeFleePoint();
        SetStateAndMove(AIState.AI_FLEEING, fleePoint);
        TurnOffAutoAttack(StopReason.IMMEDIATELY);
        ResetAndStartTimer(TimerFlee);
    }

    public override void OnFleeEnd()
    {
        if (GetState() == AIState.AI_HALTED) return;

        StopTimer(TimerFlee);
        NetSetState(AIState.AI_PET_IDLE);
        TimerFindEnemies();
    }

    private void TimerFlee()
    {
        if (GetState() == AIState.AI_HALTED) return;

        var fleePoint = MakeFleePoint();
        SetStateAndMove(AIState.AI_FLEEING, fleePoint);
    }

    public override void OnCanMove()
    {
        if (GetState() == AIState.AI_HALTED) return;

        NetSetState(AIState.AI_PET_IDLE);
        TimerFindEnemies();
    }

    public override void OnCanAttack()
    {
        if (GetState() == AIState.AI_HALTED) return;

        NetSetState(AIState.AI_PET_IDLE);
        TimerFindEnemies();
    }

    private void TimerScanDistance()
    {
        if (GetState() == AIState.AI_HALTED) return;

        var tempOwner = GetGoldRedirectTarget();
        if (tempOwner == null)
        {
            Die(Me, DamageSource.DAMAGE_SOURCE_INTERNALRAW);
            return;
        }

        var canMove = GetCanMove(Me);
        var distanceToOwner = DistanceBetweenObjectBounds(Me, tempOwner);

        if (canMove)
            if (distanceToOwner > TELEPORT_DISTANCE)
            {
                SetActorPositionFromObject(Me, tempOwner);
                NetSetState(AIState.AI_PET_IDLE);
                return;
            }

        if (canMove)
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

        var state = GetState();
        var noEnemiesNear = FindTargetInAcR() == null;

        if (state == AIState.AI_PET_IDLE)
            if (distanceToOwner > GetPetReturnRadius(Me))
                if (noEnemiesNear)
                {
                    SetStateAndCloseToTarget(AIState.AI_PET_RETURN, tempOwner);
                    return;
                }

        if (state != AIState.AI_PET_MOVE && state != AIState.AI_PET_RETURN && state != AIState.AI_PET_HARDRETURN)
            if (distanceToOwner <= GetPetReturnRadius(Me))
            {
                NetSetState(AIState.AI_PET_IDLE);
                return;
            }

        if (!IsMoving())
            if (state == AIState.AI_PET_HARDMOVE)
            {
                NetSetState(AIState.AI_PET_HARDIDLE);
                return;
            }

        if (state == AIState.AI_PET_SPAWNING)
            if (IsNetworkLocal())
                NetSetState(AIState.AI_PET_IDLE);
    }


    private void TimerFindEnemies()
    {
        if (GetState() == AIState.AI_HALTED) return;

        if (GetGoldRedirectTarget() == null)
        {
            Die(Me, DamageSource.DAMAGE_SOURCE_INTERNALRAW);
            return;
        }

        var state = GetState();

        if (state is AIState.AI_PET_MOVE or AIState.AI_PET_HARDMOVE or AIState.AI_PET_HARDSTOP) return;

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

        if (state != AIState.AI_PET_ATTACK && state != AIState.AI_PET_HARDATTACK &&
            state != AIState.AI_PET_RETURN_ATTACKING &&
            state != AIState.AI_PET_ATTACKMOVE_ATTACKING && state != AIState.AI_PET_HARDIDLE_ATTACKING &&
            state != AIState.AI_TAUNTED)
            return;

        if (TargetInAttackRange())
        {
            SetTarget(newTarget);
            TurnOnAutoAttack(newTarget);
        }
        else if (!TargetInCancelAttackRange())
        {
            TurnOffAutoAttack(StopReason.MOVING);
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