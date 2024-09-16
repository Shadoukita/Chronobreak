namespace AIScripts;

//Status: 90% Confirmed identical to Lua script
class MinionAI : Minions
{
    private float LastAttackScan; //TODO:
    public MinionAI()
    {
        MAX_ENGAGE_DISTANCE = 2500;
        FEAR_WANDER_DISTANCE = 500;
        DELAY_FIND_ENEMIES = 0.25f;
    }
    public override void OnTauntBegin()
    {
        if (GetState() == AIState.AI_HALTED) return;
        var tauntTarget = GetTauntTarget();
        if (tauntTarget != null)
        {
            SetStateAndCloseToTarget(AIState.AI_TAUNTED, tauntTarget);
            StopTimer(TimerAntiKite);
        }
    }
    public override void OnTauntEnd()
    {
        if (GetState() == AIState.AI_HALTED) return;
        var tauntTarget = GetTauntTarget();
        if (tauntTarget != null)
        {
            SetStateAndCloseToTarget(AIState.AI_ATTACKMOVE_ATTACKING, tauntTarget);
            ResetAndStartTimer(TimerAntiKite);
        }
        else
            FindTargetOrMove();
    }
    protected override void TimerFindEnemies()
    {
        var c = GetState();
        if (c == AIState.AI_HALTED) return;
        if (c == AIState.AI_ATTACKMOVESTATE)
        {
            var d = FindTargetInAcR();
            if (d == null)
            {
                TurnOffAutoAttack(StopReason.TARGET_LOST);
                return;
            }
            SetStateAndCloseToTarget(AIState.AI_ATTACKMOVE_ATTACKING, d);
            ResetAndStartTimer(TimerAntiKite);
        }
        else if (c == AIState.AI_TAUNTED && GetTauntTarget() != null)
        {
            var f = GetTauntTarget();
            SetStateAndCloseToTarget(AIState.AI_TAUNTED, f);
        }
        
        if (c != AIState.AI_ATTACKMOVE_ATTACKING && c != AIState.AI_TAUNTED)
            return;
        
        if (GetTarget() == null)
            FindTargetOrMove();
        else
        {
            var e = GetTarget();
            if (DistanceBetweenMeAndObject(e) > MAX_ENGAGE_DISTANCE)
                FindTargetOrMove();
        }

        if (TargetInAttackRange())
        {
            if (c != AIState.AI_TAUNTED)
                ResetAndStartTimer(TimerAntiKite);
            var e = GetTarget();
            TurnOnAutoAttack(e);
        }
        else if (!TargetInCancelAttackRange())
        {
            TurnOffAutoAttack(StopReason.MOVING);
            LastAttackScan = 0;
        }
    }
    protected override void FindTargetOrMove()
    {
        if (GetState() == AIState.AI_HALTED) return;
        var d = FindTargetInAcR();
        if (d != null)
        {
            if (!LastAutoAttackFinished())
            {
                InitTimer(TimerFindEnemies, DELAY_FIND_ENEMIES, true);
                return;
            }
            SetStateAndCloseToTarget(AIState.AI_ATTACKMOVE_ATTACKING, d);
            ResetAndStartTimer(TimerAntiKite);
        }
        else
        {
            SetStateAndMoveToForwardNav(AIState.AI_ATTACKMOVESTATE);
            StopTimer(TimerAntiKite);
            LastAttackScan = 0;
        }
    }
    protected override void TimerMoveForward()
    {
        var c = GetState();
        if (c == AIState.AI_HALTED) return;
        if (c == AIState.AI_IDLE)
            FindTargetOrMove();
        else if (c == AIState.AI_ATTACKMOVESTATE)
        {
            SetStateAndMoveToForwardNav(AIState.AI_ATTACKMOVESTATE);
            LastAttackScan = 0;
        }
    }
}