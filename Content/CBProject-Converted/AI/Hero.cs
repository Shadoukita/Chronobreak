using Chronobreak.GameServer.Scripting.CSharp.Converted;

namespace AIScripts;

//Status: Broken C# script, using the Lua script at the moment
public class HeroAIDisabled : CAIScript
{
    protected override float FEAR_WANDER_DISTANCE => 500;
    protected override float FLEE_RUN_DISTANCE => 2000;

    public override bool OnInit()
    {
        base.OnInit();
        ClearTargetPosInPos();
        ClearTarget();
        SetState(AIState.AI_IDLE);
        InitTimer(TimerDistanceScan, 0.2f, true);
        InitTimer(TimerCheckAttack, 0.2f, true);
        InitTimer(TimerFeared, 1, true);
        InitTimer(TimerFlee, 0.5f, true);
        StopTimer(TimerFeared);
        StopTimer(TimerFlee);
        return false;
    }

    public override bool OnOrder(OrderType orderType, AttackableUnit target, Vector2 position)
    {
        var currentState = GetState();
        if (currentState is AIState.AI_HALTED or AIState.AI_TAUNTED or AIState.AI_FEARED or AIState.AI_FLEEING or AIState.AI_CHARMED) return false;

        switch (orderType)
        {
            case OrderType.Taunt:
                SetStateAndCloseToTarget(AIState.AI_HARDATTACK, target);
                ClearTargetPosInPos();
                return true;
            case OrderType.AttackTo:
                {
                    SetStateAndCloseToTarget(AIState.AI_HARDATTACK, target);
                    AssignTargetPosInPos(position);
                    if (TargetInAttackRange())
                        TurnOnAutoAttack(GetTarget());
                    else
                        TurnOffAutoAttack(StopReason.MOVING);
                    return true;
                }
            case OrderType.AttackTerrainSustained or OrderType.AttackTerrainOnce:
                {
                    var isSustained = orderType == OrderType.AttackTerrainSustained;
                    if (TargetPositionInAttackRange(position))
                    {
                        TurnOnAutoAttackTerrain(position, isSustained);
                    }
                    else
                    {
                        if (!TargetPositionInAttackRange(position))
                        {
                            SetStateAndMove(AIState.AI_HARDATTACK, position);
                            AssignTargetPosInPos(position);
                            TurnOffAutoAttack(StopReason.MOVING);
                        }
                    }

                    return true;
                }
            case OrderType.AttackMove:
                {
                    var newTarget = FindTargetInAcR();
                    if (newTarget != null)
                    {
                        SetStateAndCloseToTarget(AIState.AI_SOFTATTACK, newTarget);
                    }
                    else
                    {
                        SetStateAndMoveInPos(AIState.AI_ATTACKMOVESTATE, position);
                        AssignTargetPosInPos(position);
                    }

                    return true;
                }
            case OrderType.MoveTo:
                SetStateAndMoveInPos(AIState.AI_MOVE, position);
                AssignTargetPosInPos(position);
                return true;

        }
        TimerCheckAttack();
        return false;
    }


    public override void OnTargetLost(LostTargetEvent eventType, AttackableUnit lostTarget)
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        if (currentState != AIState.AI_ATTACK_GOING_TO_LAST_KNOWN_LOCATION &&
            eventType == LostTargetEvent.LOST_VISIBILITY &&
            currentState != AIState.AI_SOFTATTACK && lostTarget != null)
            SetStateAndCloseToTarget(AIState.AI_ATTACK_GOING_TO_LAST_KNOWN_LOCATION, lostTarget);
        else
            TimerCheckAttack();
    }

    public override void OnStopMove()
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        ClearTargetPosInPos();
    }


    protected void TimerCheckAttack()
    {
        var state = GetState();
        if (state == AIState.AI_HALTED) return;


        if (state != AIState.AI_SOFTATTACK && state != AIState.AI_HARDATTACK &&
            state != AIState.AI_TAUNTED && state != AIState.AI_CHARMED)
        {
            if (IsMoving()) return;
        }
        else if (!IsTargetLost() || GetTarget() == null)
        {
            if (LastAutoAttackFinished())
            {
                InitTimer(TimerCheckAttack, 0.1f, true);
                return;
            }

            if (IsAutoAcquireTargetEnabled() && state != AIState.AI_SOFTATTACK) return;

            var newTarget = FindTargetInAcR();
            if (newTarget != null)
            {
                if (state == AIState.AI_CHARMED)
                    SetStateAndCloseToTarget(AIState.AI_CHARMED, newTarget);
                else if (CanSeeMe(newTarget)) SetStateAndCloseToTarget(AIState.AI_SOFTATTACK, newTarget);

                return;
            }

            if (state == AIState.AI_CHARMED) SpellBuffRemoveType(Me, BuffType.TAUNT);

            NetSetState(AIState.AI_STANDING);
            return;
        }
        else if (TargetInAttackRange())
        {
            var target = GetTarget();
            TurnOnAutoAttack(target);
            return;
        }
        else if (!TargetInCancelAttackRange())
        {
            TurnOffAutoAttack(StopReason.MOVING);
        }

        InitTimer(TimerCheckAttack, 0.1f, true);
    }

    protected void TimerDistanceScan()
    {
        var state = GetState();
        switch (state)
        {
            case AIState.AI_HALTED:
            case AIState.AI_HARDIDLE:
                return;
            case AIState.AI_STANDING when IsAutoAcquireTargetEnabled():
            case AIState.AI_IDLE when IsAutoAcquireTargetEnabled():
                {
                    var target = GetTargetOrFindTargetInAcR();
                    if (target != null && CanSeeMe(target))
                    {
                        SetStateAndCloseToTarget(AIState.AI_SOFTATTACK, target);
                    }
                    break;
                }
            case AIState.AI_MOVE when !IsMoving() && IsAutoAcquireTargetEnabled():
            {
                var target = GetTargetOrFindTargetInAcR();
                if (target != null)
                {
                    if (CanSeeMe(target))
                    {
                        target = SetStateAndCloseToTarget(AIState.AI_SOFTATTACK, target);
                        TurnOnAutoAttack(target);
                        break;
                    }
                }
                NetSetState(AIState.AI_IDLE);
                break;
            }
            case AIState.AI_ATTACKMOVESTATE:
                {
                    var target = GetTargetOrFindTargetInAcR();
                    if (target != null)
                    {
                        SetStateAndCloseToTarget(AIState.AI_SOFTATTACK, target);
                        return;
                    }

                    if (DistanceBetweenMeAndTarget() <= 100)
                    {
                        NetSetState(AIState.AI_STANDING);
                        ClearTarget();
                    }

                    break;
                }
            case AIState.AI_ATTACK_GOING_TO_LAST_KNOWN_LOCATION:
                {
                    var target = GetLostTargetIfVisible();
                    if (target != null) SetStateAndCloseToTarget(AIState.AI_HARDATTACK, target);
                    break;
                }
        }
    }

    protected virtual void TimerFeared()
    {
        var state = GetState();
        if (state == AIState.AI_HALTED) return;

        var wanderPoint = MakeWanderPoint(GetFearLeashPoint(), FEAR_WANDER_DISTANCE);
        SetStateAndMove(AIState.AI_FEARED, wanderPoint);
    }

    protected virtual void TimerFlee()
    {
        var state = GetState();
        if (state == AIState.AI_HALTED) return;

        var wanderPoint = MakeFleePoint();
        SetStateAndMove(AIState.AI_FLEEING, wanderPoint);
    }

    public override void OnTauntBegin()
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        var tauntTarget = GetTauntTarget();
        if (tauntTarget != null) SetStateAndCloseToTarget(AIState.AI_TAUNTED, tauntTarget);
    }

    public override void OnTauntEnd()
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        var tauntTarget = GetTauntTarget();
        if (tauntTarget != null)
        {
            SetStateAndCloseToTarget(AIState.AI_SOFTATTACK, tauntTarget);
        }
        else
        {
            NetSetState(AIState.AI_IDLE);
            TimerDistanceScan();
            TimerCheckAttack();
        }
    }

    public override void OnFearBegin()
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        var wanderPoint = MakeWanderPoint(GetFearLeashPoint(), FEAR_WANDER_DISTANCE);
        SetStateAndMove(AIState.AI_FEARED, wanderPoint);
        TurnOffAutoAttack(StopReason.MOVING);
        ResetAndStartTimer(TimerFeared);
    }

    public override void OnFearEnd()
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        StopTimer(TimerFeared);
        NetSetState(AIState.AI_IDLE);
        TimerDistanceScan();
        TimerCheckAttack();
    }

    public override void OnFleeBegin()
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        var fleePoint = MakeFleePoint();
        SetStateAndMove(AIState.AI_FLEEING, fleePoint);
        TurnOffAutoAttack(StopReason.MOVING);
        ResetAndStartTimer(TimerFlee);
    }

    public override void OnFleeEnd()
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        StopTimer(TimerFlee);
        NetSetState(AIState.AI_IDLE);
        TimerDistanceScan();
        TimerCheckAttack();
    }

    public override void OnCharmBegin()
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        NetSetState(AIState.AI_CHARMED);
        TurnOffAutoAttack(StopReason.IMMEDIATELY);
        TimerCheckAttack();
    }

    public override void OnCharmEnd()
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        NetSetState(AIState.AI_IDLE);
        TimerDistanceScan();
        TimerCheckAttack();
    }

    public override void OnAICommand()
    {
        var state = GetState();
        if (state == AIState.AI_HALTED) return;
    }

    public override void OnReachedDestinationForGoingToLastLocation()
    {
        var state = GetState();
        if (state == AIState.AI_HALTED) return;

        NetSetState(AIState.AI_IDLE);
        TimerDistanceScan();
        TimerCheckAttack();
    }

    public override void HaltAI()
    {
        StopTimer(TimerFeared);
        StopTimer(TimerFlee);
        StopTimer(TimerDistanceScan);
        StopTimer(TimerCheckAttack);
        TurnOffAutoAttack(StopReason.IMMEDIATELY);
        NetSetState(AIState.AI_HALTED);
    }
}