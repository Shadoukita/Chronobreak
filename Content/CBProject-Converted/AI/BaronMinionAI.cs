using Chronobreak.GameServer.Scripting.CSharp.Converted;

namespace AI;

//Status: 100% Identical to Lua Script
public class BaronMinionAI : CAIScript
{
    private const float MAX_ENGAGE_DISTANCE = 2500;
    private const float FEAR_WANDER_DISTANCE = 500;
    private const float DELAY_FIND_ENEMIES = 0.25f;

    public override void OnTauntBegin()
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        var tauntTarget = GetTauntTarget();
        if (tauntTarget != null)
        {
            SetStateAndCloseToTarget(AIState.AI_TAUNTED, tauntTarget);
            StopTimer(TimerAntiKite);
        }
    }

    public override void OnTauntEnd()
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        var tauntTarget = GetTauntTarget();
        if (tauntTarget != null)
        {
            SetStateAndCloseToTarget(AIState.AI_ATTACKMOVE_ATTACKING, tauntTarget);
            ResetAndStartTimer(TimerAntiKite);
        }
        else
        {
            FindTargetOrMove();
        }
    }

    private void TimerFindEnemies()
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        if (currentState == AIState.AI_ATTACKMOVESTATE)
        {
            var target = FindTargetInAcRWithFilter(AITargetType.AI_TARGET_MINIONS);
            if (target == null)
            {
                TurnOffAutoAttack(StopReason.TARGET_LOST);
                return;
            }

            SetStateAndCloseToTarget(AIState.AI_ATTACKMOVE_ATTACKING, target);
            ResetAndStartTimer(TimerAntiKite);
        }
        else if (currentState == AIState.AI_TAUNTED)
        {
            var tauntTarget = GetTauntTarget();
            if (tauntTarget != null) SetStateAndCloseToTarget(AIState.AI_TAUNTED, tauntTarget);
        }

        if (currentState != AIState.AI_ATTACKMOVE_ATTACKING && currentState != AIState.AI_TAUNTED) return;

        if (GetTarget() == null || DistanceBetweenMeAndObject(GetTarget()) > MAX_ENGAGE_DISTANCE) FindTargetOrMove();

        if (TargetInAttackRange())
        {
            if (currentState != AIState.AI_TAUNTED) ResetAndStartTimer(TimerAntiKite);
            TurnOnAutoAttack(GetTarget());
        }
        else if (!TargetInCancelAttackRange())
        {
            TurnOffAutoAttack(StopReason.MOVING);
        }
    }

    private void FindTargetOrMove()
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        var target = FindTargetInAcRWithFilter(AITargetType.AI_TARGET_MINIONS);
        if (target != null)
        {
            if (!LastAutoAttackFinished())
            {
                InitTimer(TimerFindEnemies, DELAY_FIND_ENEMIES, true);
                return;
            }

            SetStateAndCloseToTarget(AIState.AI_ATTACKMOVE_ATTACKING, target);
            ResetAndStartTimer(TimerAntiKite);
        }
        else
        {
            SetStateAndMoveToForwardNav(AIState.AI_ATTACKMOVESTATE);
            StopTimer(TimerAntiKite);
        }
    }

    private void TimerMoveForward()
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        if (currentState == AIState.AI_IDLE)
        {
            FindTargetOrMove();
        }
        else if (currentState == AIState.AI_ATTACKMOVESTATE)
        {
            SetStateAndMoveToForwardNav(AIState.AI_ATTACKMOVESTATE);
        }
    }

    public override bool OnInit()
    {
        
        SetState(AIState.AI_IDLE);
        InitTimer(TimerFindEnemies, DELAY_FIND_ENEMIES, true);
        InitTimer(TimerMoveForward, 0, true);
        InitTimer(TimerAntiKite, 4, false);
        InitTimer(TimerFeared, 1, true);
        InitTimer(TimerFlee, 0.5f, true);

        StopTimer(TimerAntiKite);
        StopTimer(TimerFeared);
        StopTimer(TimerFlee);

        return false;
    }

    public override void OnTargetLost(LostTargetEvent eventType, AttackableUnit target)
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        if (currentState == AIState.AI_ATTACKMOVE_ATTACKING || currentState == AIState.AI_TAUNTED) FindTargetOrMove();
    }

    public override void OnPathToTargetBlocked()
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        if (currentState == AIState.AI_ATTACKMOVE_ATTACKING)
        {
            AddToIgnore(0.1f);
            FindTargetOrMove();
        }
    }

    public override void OnCallForHelp(ObjAIBase attacker, ObjAIBase victim)
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;
    }

    public override void OnFearBegin()
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        var wanderPoint = MakeWanderPoint(GetFearLeashPoint(), FEAR_WANDER_DISTANCE);
        SetStateAndMove(AIState.AI_FEARED, wanderPoint);
        TurnOffAutoAttack(StopReason.IMMEDIATELY);
        ResetAndStartTimer(TimerFeared);
    }

    public override void OnFearEnd()
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        StopTimer(TimerFeared);
        FindTargetOrMove();
    }

    private void TimerFeared()
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        var wanderPoint = MakeWanderPoint(GetFearLeashPoint(), FEAR_WANDER_DISTANCE);
        SetStateAndMove(AIState.AI_FEARED, wanderPoint);
    }

    public override void OnFleeBegin()
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        var fleePoint = MakeFleePoint();
        SetStateAndMove(AIState.AI_FLEEING, fleePoint);
        TurnOffAutoAttack(StopReason.IMMEDIATELY);
        ResetAndStartTimer(TimerFlee);
    }

    public override void OnFleeEnd()
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        StopTimer(TimerFlee);
        FindTargetOrMove();
    }

    private void TimerFlee()
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        var fleePoint = MakeFleePoint();
        SetStateAndMove(AIState.AI_FLEEING, fleePoint);
    }

    private void TimerAntiKite()
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        if (currentState == AIState.AI_ATTACKMOVE_ATTACKING)
        {
            if (IsMoving())
            {
                AddToIgnore(0.1f);
                FindTargetOrMove();
            }
        }
    }
    public override void OnCanMove()
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        NetSetState(AIState.AI_IDLE);
        FindTargetOrMove();
    }

    public override void OnCanAttack()
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        NetSetState(AIState.AI_IDLE);
        FindTargetOrMove();
    }

    public override void OnReachedDestinationForGoingToLastLocation()
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        NetSetState(AIState.AI_IDLE);
        FindTargetOrMove();
    }

    public override bool OnCollisionEnemy(AttackableUnit target)
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return true;

        return true;
    }

    public override bool OnCollisionOther(AttackableUnit target)
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return true;

        if (currentState != AIState.AI_TAUNTED && currentState != AIState.AI_FEARED &&
            currentState != AIState.AI_FLEEING)
        {
            FindTargetOrMove();
            return false;
        }

        return true;
    }

    public override void HaltAI()
    {
        StopTimer(TimerFindEnemies);
        StopTimer(TimerMoveForward);
        StopTimer(TimerAntiKite);
        StopTimer(TimerFeared);
        TurnOffAutoAttack(StopReason.IMMEDIATELY);
        NetSetState(AIState.AI_HALTED);
    }
}