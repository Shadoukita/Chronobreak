using Chronobreak.GameServer.API;
using Chronobreak.GameServer.Scripting.CSharp.Converted;
using Chronobreak.GameServer.Scripting.Lua;

namespace AIScripts;
//Status: 100% Identical to Lua scripts
internal class LeashedAI : CAIScript
{
    protected override float FEAR_WANDER_DISTANCE => 500;
    protected const float LEASH_RADIUS = 850;
    protected const float LEASH_PROTECTION_RADIUS = 750;
    protected const float INNER_RELEASH_RADIUS = 750;
    protected const float RELEASH_RADIUS = 1150;
    protected const float VERY_FAR_DISTANCE = 9000;
    protected const int LEASH_COUNTER_THRESHOLD = 10;

    protected const float REGEN_PERCENT_PER_SECOND = 0.125f;

    private Vector2 _leashedPos;
    private Vector3 _leashedDirection;
    private MinionRoamState _originalState;

    private int _leashCounter;

    public override bool OnInit()
    {
        base.OnInit();
        SetState(AIState.AI_ATTACK);
        _originalState = GetRoamState();
        _leashedPos = GetPosition();
        _leashedDirection = Me.Direction;
        InitTimer(TimerRetreat, 0.5f, true);
        InitTimer(TimerAttack, 0f, true);
        InitTimer(TimerRegen, 1f, true);
        InitTimer(TimerFeared, 0.5f, true);
        StopTimer(TimerFlee);
        StopTimer(TimerFeared);
        InitTimer(TimerFlee, 0.5f, true);
        StopTimer(TimerFlee);
        return false;
    }


    private void TimerRegen()
    {
        var state = GetState();
        if (state == AIState.AI_HALTED) return;

        var regenAmount = REGEN_PERCENT_PER_SECOND * Me.Stats.HealthPoints.Total;
        var currentHp = Me.Stats.CurrentHealth;
        if (currentHp > 0 && currentHp < Me.Stats.HealthPoints.Total)
        {
            ApiFunctionManager.Heal(Me, regenAmount, Me, AddHealthType.RGEN);
        }
    }

    public void Retreat()
    {
        SetStateAndMoveToLeashedPos(AIState.AI_RETREAT);
    }

    private void SetStateAndMoveToLeashedPos(AIState state)
    {
        SetState(state);
        SetTarget(null);
        Me.CancelAutoAttack(false, true);
        Me.IssueOrDelayOrder(OrderType.MoveTo, null, _leashedPos);
    }

    public bool OnOrder(OrderType orderType, AttackableUnit orderTarget)
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return false;

        if (orderType == OrderType.AttackTo)
        {
            StopTimer(TimerRegen);
            SetStateAndCloseToTarget(AIState.AI_ATTACK, orderTarget);
            SetRoamState(MinionRoamState.Hostile);
            return true;
        }

        return false;
    }

    public override void OnTakeDamage(AttackableUnit attacker)
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        Vector2 currentPosition = GetPosition();
        var target = FindTargetNearPosition(currentPosition, LEASH_RADIUS);

        if (target == null && attacker != null)
        {
            return;
        }

        var roamState = GetRoamState();

        if (roamState == MinionRoamState.Inactive && currentState != AIState.AI_RETREAT && currentState != AIState.AI_TAUNTED &&
            currentState != AIState.AI_FEARED && currentState != AIState.AI_FLEEING)
        {
            StopTimer(TimerRegen);
            SetStateAndCloseToTarget(AIState.AI_ATTACK, target);
            SetRoamState(MinionRoamState.Hostile);
        }
        else if (roamState == MinionRoamState.Hostile && currentState == AIState.AI_ATTACK)
        {
            var currentTarget = GetTarget();
            if (currentTarget == null)
            {
                return;
            }

            float distanceToCurrentTarget = DistanceBetweenObjectCenterAndPoint(currentTarget, currentPosition);
            float distanceToNewTarget = DistanceBetweenObjectCenterAndPoint(target, currentPosition);

            if (distanceToNewTarget + 25 < distanceToCurrentTarget)
            {
                SetStateAndCloseToTarget(AIState.AI_ATTACK, target);
                SetRoamState(MinionRoamState.Hostile);
                _leashCounter++;
                if (_leashCounter > LEASH_COUNTER_THRESHOLD) 
                    Retreat();
            } 
            else if (target is not Champion && distanceToNewTarget + 25 < distanceToCurrentTarget)
            {
                SetStateAndCloseToTarget(AIState.AI_ATTACK, target);
                SetRoamState(MinionRoamState.Hostile);
                _leashCounter++;
                if (_leashCounter > LEASH_COUNTER_THRESHOLD) 
                    Retreat();
            }
        }
    }

    public override void OnLeashedCallForHelp(ObjAIBase attacker, ObjAIBase victim)
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        Vector2 currentPosition = GetPosition();
        AttackableUnit target = FindTargetNearPosition(currentPosition, LEASH_RADIUS);

        if (target == null)
        {
            target = attacker;
            if (attacker == null) return;
        }

        var roamState = GetRoamState();
        if (roamState == MinionRoamState.Inactive)
        {
            if (currentState != AIState.AI_RETREAT && currentState != AIState.AI_TAUNTED &&
                currentState != AIState.AI_FEARED && currentState != AIState.AI_FLEEING)
            {
                StopTimer(TimerRegen);
                SetStateAndCloseToTarget(AIState.AI_ATTACK, target);
                SetRoamState(MinionRoamState.Hostile);
            }
        }
        else if (roamState == MinionRoamState.Hostile && currentState == AIState.AI_ATTACK)
        {
            var myTarget = GetTarget();

            if (myTarget != null)
            {
               float distToAttacker = DistanceBetweenObjectCenterAndPoint(myTarget, currentPosition);
               float distToNewTarget = DistanceBetweenObjectCenterAndPoint(target, currentPosition);
            
               if (distToAttacker > distToNewTarget + 25)
               {
                   SetStateAndCloseToTarget(AIState.AI_ATTACK, target);
                   SetRoamState(MinionRoamState.Hostile);
            
                   _leashCounter++;
                   if (_leashCounter > LEASH_COUNTER_THRESHOLD) Retreat();
               }
            }
        }

        if (currentState == AIState.AI_RETREAT)
        {
            var distToAttackerFromLeashPoint = DistanceBetweenObjectCenterAndPoint(target, _leashedPos);

            if (distToAttackerFromLeashPoint <= LEASH_RADIUS && _leashCounter < LEASH_COUNTER_THRESHOLD)
            {
                _leashCounter++;
                StopTimer(TimerRegen);
                SetStateAndCloseToTarget(AIState.AI_ATTACK, target);
                SetRoamState(MinionRoamState.Hostile);

            } else if (GetDistToLeashedPos() <= INNER_RELEASH_RADIUS && distToAttackerFromLeashPoint <= RELEASH_RADIUS && _leashCounter < LEASH_COUNTER_THRESHOLD)
            {
                _leashCounter++;
                StopTimer(TimerRegen);
                SetStateAndCloseToTarget(AIState.AI_ATTACK, target);
                SetRoamState(MinionRoamState.Hostile);
            }
        }
    }

    public override void OnTargetLost(LostTargetEvent eventType, AttackableUnit target)
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        var petOwner = Functions.GetOwner(target) ?? GetGoldRedirectTarget(target);
        if (petOwner != null)
        {
            SetStateAndCloseToTarget(AIState.AI_ATTACK, petOwner);
        } 
        else
        {
            FindNewTarget();
        }
    }

    public void TimerRetreat()
    {
        var currentState = GetState();
        var roamState = GetRoamState();

        if (roamState is MinionRoamState.Inactive or MinionRoamState.RunInFear) return;
        if (currentState == AIState.AI_HALTED) return;


        var distToLeash = GetDistToLeashedPos();
        var target = GetTarget();

        float targetDist = LEASH_RADIUS + 1;

        if (target != null)
        {
            targetDist = DistanceBetweenObjectCenterAndPoint(target, GetMyLeashedPos());
        }

        if (distToLeash is > LEASH_PROTECTION_RADIUS and < LEASH_RADIUS && targetDist > LEASH_RADIUS && currentState != AIState.AI_RETREAT 
            && _leashCounter < LEASH_COUNTER_THRESHOLD)
        {
            FindNewTarget();
            if (GetTarget() != null)
            {
                _leashCounter++;
            }
        }
        else if (distToLeash > LEASH_RADIUS && currentState != AIState.AI_RETREAT)
        {
            //isLeashing = true
            ResetAndStartTimer(TimerRegen);
            Retreat();
        }

        if (currentState == AIState.AI_ATTACK && !IsMoving() && GetCanMove(Me))
        {
            FindNewTarget();
        }

        if (currentState == AIState.AI_RETREAT && !IsMoving())
        {
            if (GetDistToRetreat() < 100)
            {
                OnStoppedMoving();
            }
            else
            {
                Retreat();
            }
        }
    }

    private float GetDistToLeashedPos()
    {
        return Vector2.Distance(_leashedPos, Me.Position);
    }

    private Vector2 GetMyLeashedPos()
    {
        return _leashedPos;
    }

    private float GetDistToRetreat()
    {
        return LEASH_RADIUS - Vector2.Distance(_leashedPos, Me.Position);
    }

    public override void OnStoppedMoving()
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;
        if (currentState == AIState.AI_RETREAT)
        {
            _leashCounter = 0;
            Me.FaceDirection(_leashedDirection); //  SetLeashOrientation();
            SetState(AIState.AI_ATTACK);
            SetRoamState(_originalState);
        }
    }

    public void TimerAttack()
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;
        var roamState = GetRoamState();

        if (roamState is MinionRoamState.Inactive or MinionRoamState.RunInFear ||
            currentState == AIState.AI_RETREAT) return;

        if (currentState != AIState.AI_ATTACK && currentState != AIState.AI_TAUNTED)
        {
            return;
        }

        var target = GetTarget();
        if (target != null)
        {
            if (TargetInAttackRange())
            {
                TurnOnAutoAttack(target);
            }
            else
            {
                if (!TargetInCancelAttackRange())
                {
                    TurnOffAutoAttack(StopReason.MOVING);
                }
            }
        }
        else
        {
            FindNewTarget();
        }
    }

    public void FindNewTarget()
    {
        var currentState = GetState();
        var roamState = GetRoamState();

        if (currentState == AIState.AI_HALTED) return;

        if (roamState is MinionRoamState.Inactive or MinionRoamState.RunInFear ||
            currentState == AIState.AI_RETREAT) return;

        var leashedPos = GetMyLeashedPos();
        var target = FindTargetNearPosition(_leashedPos, LEASH_RADIUS);
        var leashDistance = LEASH_RADIUS + 1;

        if (target != null)
        {
            leashDistance = DistanceBetweenObjectCenterAndPoint(target, leashedPos);
        }

        if (target != null)
        {
            if (leashDistance <= LEASH_RADIUS)
            {
                StopTimer(TimerRegen);
                SetStateAndCloseToTarget(AIState.AI_ATTACK, target);
            }
        }
        else
        {
            ResetAndStartTimer(TimerRegen);
            Retreat();
        }
    }


    public override void OnTauntBegin()
    {
        var state = GetState();
        if (state == AIState.AI_HALTED)
        {
            return;
        }

        var target = GetTauntTarget();
        if (target != null)
        {
            if (state != AIState.AI_FEARED)
            {
                StopTimer(TimerRegen);
                SetStateAndCloseToTarget(AIState.AI_TAUNTED, target);
                SetRoamState(MinionRoamState.Hostile);
            }
        }
    }

    public override void OnTauntEnd()
    {
        var state = GetState();
        if (state == AIState.AI_HALTED)
        {
            return;
        }

        var target = GetTauntTarget();
        if (target != null)
        {
            if (state != AIState.AI_FEARED)
            {
                StopTimer(TimerRegen);
                SetStateAndCloseToTarget(AIState.AI_ATTACK, target);
                SetRoamState(MinionRoamState.Hostile);
            }
        }
        else
        {
            NetSetState(AIState.AI_ATTACK);
            TimerRetreat();
            TimerAttack();
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
        SetRoamState(MinionRoamState.RunInFear);
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
        SetRoamState(MinionRoamState.Hostile);
        NetSetState(AIState.AI_ATTACK);
        TimerRetreat();
        TimerAttack();
    }

    public void TimerFeared()
    {
        var state = GetState();
        if (state == AIState.AI_HALTED)
        {
            return;
        }

        var wanderPoint = MakeWanderPoint(GetFearLeashPoint(), FEAR_WANDER_DISTANCE);
        SetRoamState(MinionRoamState.RunInFear);
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
        SetRoamState(MinionRoamState.RunInFear);
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
        SetRoamState(MinionRoamState.Hostile);
        NetSetState(AIState.AI_ATTACK);
        TimerRetreat();
        TimerAttack();
    }

    public void TimerFlee()
    {
        var state = GetState();
        if (state == AIState.AI_HALTED)
        {
            return;
        }

        var fleePoint = MakeFleePoint();
        SetStateAndMove(AIState.AI_FLEEING, fleePoint);
        SetRoamState(MinionRoamState.RunInFear);
    }


    public override void HaltAI()
    {
        StopTimer(TimerRetreat);
        StopTimer(TimerAttack);
        StopTimer(TimerFeared);
        StopTimer(TimerFlee);
        TurnOffAutoAttack(StopReason.IMMEDIATELY);
        NetSetState(AIState.AI_HALTED);
    }
}