using Chronobreak.GameServer.Scripting.CSharp.Converted;

namespace AIScripts;

public class Minions: CAIScript
{
    protected float MAX_ENGAGE_DISTANCE;
    protected float FEAR_WANDER_DISTANCE;
    protected float DELAY_FIND_ENEMIES;
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
    protected virtual void TimerFindEnemies(){}
    protected virtual void TimerMoveForward(){}
    protected virtual void TimerDistanceScan(){}
    protected virtual void TimerCheckAttack(){}
    protected virtual void FindTargetOrMove(){}
    public override void OnTargetLost(LostTargetEvent eventType, AttackableUnit target)
    {
        var state = GetState();
        switch (state)
        {
            case AIState.AI_HALTED:
                return;
            case AIState.AI_ATTACKMOVE_ATTACKING or AIState.AI_TAUNTED:
                FindTargetOrMove();
                break;
        }
    }
    public override void OnPathToTargetBlocked()
    {
        var state = GetState();
        switch (state)
        {
            case AIState.AI_HALTED:
                return;
            case AIState.AI_ATTACKMOVE_ATTACKING:
                AddToIgnore(0.1f);
                FindTargetOrMove();
                break;
        }
    }
    public override void OnCallForHelp(ObjAIBase attacker, ObjAIBase target)
    {
        var state = GetState();
        if (state == AIState.AI_HALTED) return;
        if (target == null || state is not (AIState.AI_ATTACKMOVESTATE or AIState.AI_ATTACKMOVE_ATTACKING))
            return;
        SetStateAndCloseToTarget(AIState.AI_ATTACKMOVE_ATTACKING, target);
        ResetAndStartTimer(TimerAntiKite);
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
        if (GetState() == AIState.AI_HALTED) 
            return;
        StopTimer(TimerFeared);
        FindTargetOrMove();
    }
    protected virtual void TimerFeared()
    {
        if (GetState() == AIState.AI_HALTED) 
            return;
        var wanderPoint = MakeWanderPoint(GetFearLeashPoint(), FEAR_WANDER_DISTANCE);
        SetStateAndMove(AIState.AI_FEARED, wanderPoint);
    }
    public override void OnFleeBegin()
    {
        if (GetState() == AIState.AI_HALTED) 
            return;
        var fleePoint = MakeFleePoint();
        SetStateAndMove(AIState.AI_FLEEING, fleePoint);
        TurnOffAutoAttack(StopReason.IMMEDIATELY);
        ResetAndStartTimer(TimerFlee);
    }
    public override void OnFleeEnd()
    {
        if (GetState() == AIState.AI_HALTED) 
            return;
        StopTimer(TimerFlee);
        FindTargetOrMove();
    }
    protected virtual void TimerFlee()
    {
        if (GetState() == AIState.AI_HALTED) 
            return;
        var fleePoint = MakeFleePoint();
        SetStateAndMove(AIState.AI_FLEEING, fleePoint);
    }
    public override void OnCanMove()
    {
        if (GetState() == AIState.AI_HALTED) 
            return;
        NetSetState(AIState.AI_IDLE);
        FindTargetOrMove();
    }
    public override void OnCanAttack()
    {
        if (GetState() == AIState.AI_HALTED) 
            return;
        NetSetState(AIState.AI_IDLE);
        FindTargetOrMove();
    }
    protected virtual void TimerAntiKite()
    {
        if (GetState() == AIState.AI_HALTED) 
            return;
        if (GetState() != AIState.AI_ATTACKMOVE_ATTACKING || !IsMoving())
            return;
        AddToIgnore(0.1f);
        FindTargetOrMove();
    }
    public override bool OnCollisionEnemy(AttackableUnit y)
    {
        var state = GetState();
        if (state == AIState.AI_HALTED)
            return false;
        if (state is AIState.AI_TAUNTED or AIState.AI_FEARED or AIState.AI_FLEEING)
            return true;

        SetStateAndCloseToTarget(AIState.AI_ATTACKMOVE_ATTACKING, y);
        return false;
    }
    public override bool OnCollisionOther(AttackableUnit y)
    {
        var state = GetState();
        if (state == AIState.AI_HALTED) 
            return false;
        if (state is AIState.AI_TAUNTED or AIState.AI_FEARED or AIState.AI_FLEEING) 
            return true;
        var target = FindTargetInAcR();
        if (target != null)
            SetStateAndCloseToTarget(AIState.AI_ATTACKMOVE_ATTACKING, target);
        return false;
    }
    public override void OnReachedDestinationForGoingToLastLocation()
    {
        if (GetState() == AIState.AI_HALTED) return;
        NetSetState(AIState.AI_IDLE);
        TimerDistanceScan();
        TimerCheckAttack();
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