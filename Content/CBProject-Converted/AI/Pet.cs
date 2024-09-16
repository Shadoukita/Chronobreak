using Chronobreak.GameServer.Scripting.CSharp.Converted;

namespace AIScripts;
//Status: Modified script, not like original
public class PetAI : CAIScript
{
    protected override float FAR_MOVEMENT_DISTANCE => 1000;
    protected override float TELEPORT_DISTANCE => 2000;
    protected override float FEAR_WANDER_DISTANCE => 500;
    protected override float FLEE_RUN_DISTANCE => 2000;

    public override bool OnInit()
    {
        SetState(AIState.AI_PET_IDLE);
        InitTimer(TimerScanDistance, 0.15f, true);
        InitTimer(TimerFindEnemies, 0.15f, true);
        InitTimer(TimerFeared, 1f, true);
        StopTimer(TimerFeared);
        return false;
    }

    private static bool IsHardOrderType(OrderType type)
    {
        switch (type)
        {
            case OrderType.PetHardAttack:
            case OrderType.PetHardMove:
            case OrderType.PetHardReturn:
            case OrderType.PetHardStop:
                return true;
            default:
                return false;
        }
    }

    public override bool OnOrder(OrderType orderType, AttackableUnit target, Vector2 position)
    {
        var currentState = GetState();
        if (currentState is AIState.AI_HALTED or AIState.AI_TAUNTED or AIState.AI_FEARED) 
            return false;


        if (currentState is AIState.AI_PET_HARDATTACK or AIState.AI_PET_HARDMOVE or AIState.AI_PET_HARDIDLE
            or AIState.AI_HARDIDLE_ATTACKING or AIState.AI_PET_HARDRETURN or AIState.AI_PET_HARDSTOP && !IsHardOrderType(orderType))
        {
            return true;
        }

        if (orderType is OrderType.AttackTo or OrderType.MoveTo or OrderType.AttackMove or OrderType.Stop)
        {
            return true;
        }

        var petOwner = GetOwner();

        if (petOwner == null)
        {
            Die(Me, DamageSource.DAMAGE_SOURCE_INTERNALRAW);
            return false;
        }

        switch (orderType)
        {
            case OrderType.PetHardStop:
                TurnOffAutoAttack(StopReason.TARGET_LOST);
                SetStateAndCloseToTarget(AIState.AI_PET_HARDSTOP, petOwner);
                AIScriptSpellBuffRemove(petOwner, "PetCommandParticle");
                return true;
            case OrderType.PetHardAttack when target == null:
                return false;
            case OrderType.PetHardAttack:
                //TurnOffAutoAttack(StopReason.TARGET_LOST);
                SetStateAndCloseToTarget(AIState.AI_HARDATTACK, target);
                AIScriptSpellBuffAdd(target, Me, "PetCommandParticle", 0, 45f);
                return true;
            case OrderType.PetHardMove:
                SetStateAndMoveInPos(AIState.AI_PET_HARDMOVE);
                AIScriptSpellBuffAdd(Me, Me, "PetCommandParticle", 0, 45f);
                return true;
            case OrderType.PetHardReturn:
                SetStateAndMoveInPos(AIState.AI_PET_HARDRETURN);
                AIScriptSpellBuffAdd(petOwner, Me, "PetCommandParticle", 0, 45f);
                return true;
            case OrderType.Hold:
                AIScriptSpellBuffRemove(Me, "PetCommandParticle");
                SetStateAndCloseToTarget(AIState.AI_PET_HOLDPOSITION, Me);
                return true;
        }


        return false;
    }


    public override void OnTargetLost(LostTargetEvent eventType, AttackableUnit lostTarget)
    {
        var currentState = GetState();

        if (currentState is AIState.AI_HALTED or AIState.AI_PET_MOVE or AIState.AI_PET_HARDMOVE
            or AIState.AI_PET_HARDRETURN or AIState.AI_FEARED or AIState.AI_PET_HARDSTOP)
        {
            return;
        }

        var newTarget = FindTargetInAcR();

        if (newTarget == null)
        {
            var petOwner = GetOwner();
            if (petOwner == null)
            {
                Die(Me, DamageSource.DAMAGE_SOURCE_INTERNALRAW);
                return;
            }

            switch (currentState)
            {
                case AIState.AI_PET_HARDIDLE_ATTACKING:
                    NetSetState(AIState.AI_HARDIDLE);
                    return;
                case AIState.AI_PET_HOLDPOSITION_ATTACKING:
                    NetSetState(AIState.AI_PET_HOLDPOSITION);
                    return;
                case AIState.AI_PET_RETURN_ATTACKING:
                    SetStateAndCloseToTarget(AIState.AI_PET_RETURN, petOwner);
                    return;
                case AIState.AI_PET_ATTACKMOVE_ATTACKING:
                    SetStateAndCloseToTarget(AIState.AI_PET_ATTACKMOVE, petOwner);
                    return;
            }
        }
        else
        {
            switch (currentState)
            {
                case AIState.AI_HARDATTACK or AIState.AI_PET_ATTACK or AIState.AI_TAUNTED:
                    SetStateAndCloseToTarget(AIState.AI_PET_ATTACK, newTarget);
                    return;
                case AIState.AI_HARDIDLE_ATTACKING:
                    NetSetState(AIState.AI_HARDIDLE_ATTACKING);
                    SetTarget(newTarget);
                    return;
                case AIState.AI_PET_HOLDPOSITION_ATTACKING:
                    NetSetState(AIState.AI_PET_HOLDPOSITION_ATTACKING);
                    SetTarget(newTarget);
                    return;
                case AIState.AI_PET_RETURN_ATTACKING:
                    SetStateAndCloseToTarget(AIState.AI_PET_RETURN_ATTACKING, newTarget);
                    return;
                case AIState.AI_ATTACKMOVE_ATTACKING:
                    SetStateAndCloseToTarget(AIState.AI_ATTACKMOVE_ATTACKING, newTarget);
                    return;
            }
        }
        NetSetState(AIState.AI_PET_IDLE);
    }


    public override void OnTauntBegin()
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        var tauntTarget = GetTauntTarget();
        if (tauntTarget != null) 
            SetStateAndCloseToTarget(AIState.AI_TAUNTED, tauntTarget);
    }

    public override void OnTauntEnd()
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

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
        NetSetState(AIState.AI_PET_IDLE);
        TimerFindEnemies();
    }

    protected virtual void TimerFeared()
    {
        var state = GetState();
        if (state == AIState.AI_HALTED) return;

        var wanderPoint = MakeWanderPoint(GetFearLeashPoint(), FEAR_WANDER_DISTANCE);
        SetStateAndMove(AIState.AI_FEARED, wanderPoint);
    }


    public override void OnCanMove()
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        NetSetState(AIState.AI_PET_IDLE);
        TimerFindEnemies();
    }

    public override void OnCanAttack()
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED) return;

        NetSetState(AIState.AI_PET_IDLE);
        TimerFindEnemies();
    }


    protected void TimerScanDistance()
    {
        var currentState = GetState();

        if (currentState == AIState.AI_HALTED)
        {
            return;
        }

        var petOwner = GetOwner();
        if (petOwner == null)
        {
            Die(Me, DamageSource.DAMAGE_SOURCE_INTERNALRAW);
            return;
        }

        var distanceToOwner = DistanceBetweenObjectBounds(Me, petOwner);

        if (distanceToOwner > TELEPORT_DISTANCE)
        {
            SetActorPositionFromObject(Me, petOwner);
            AIScriptSpellBuffRemove(Me, "PetCommandParticle");
            NetSetState(AIState.AI_PET_IDLE);
            return;
        }

        var bNoEnemiesNear = FindTargetInAcR() == null;

        if (currentState == AIState.AI_PET_IDLE)
        {
            if (distanceToOwner > GetPetReturnRadius(Me))
            {
                if (bNoEnemiesNear)
                {
                    SetStateAndCloseToTarget(AIState.AI_PET_RETURN, petOwner);
                    return;
                }
            }
        }
        
        /* This is in the original Lua script but broken in ours
        if (currentState != AIState.AI_PET_RETURN && currentState != AIState.AI_PET_HARDRETURN)
        {
            if (distanceToOwner <= GetPetReturnRadius(Me))
            {
                NetSetState(AIState.AI_PET_IDLE);
                return;
            }
        }

        if (!TargetIsMoving(petOwner))
        {
            if (currentState != AIState.AI_PET_MOVE && currentState != AIState.AI_PET_ATTACKMOVE)
            {
                NetSetState(AIState.AI_PET_IDLE);
                return;
            }

            if (!bNoEnemiesNear)
            {
                NetSetState(AIState.AI_PET_IDLE);
                return;
            }
        }

        if (!IsMoving())
        {
            if (currentState == AIState.AI_PET_HARDMOVE)
            {
                NetSetState(AIState.AI_PET_HARDIDLE);
                return;
            }
        }

        if (currentState == AIState.AI_PET_SPAWNING)
        {
            if (IsNetworkLocal())
            {
                NetSetState(AIState.AI_PET_IDLE);
            }
        }
        */
    }

    protected void TimerFindEnemies()
    {
        var currentState = GetState();

        if (currentState == AIState.AI_HALTED)
        {
            return;
        }

        if (GetOwner() == null)
        {
            Die(Me, DamageSource.DAMAGE_SOURCE_INTERNALRAW);
            return;
        }

        if (currentState is AIState.AI_PET_MOVE or AIState.AI_PET_HARDMOVE or AIState.AI_PET_HARDSTOP)
        {
            return;
        }

        var newTarget = FindTargetInAcR();

        if (newTarget == null)
        {
            TurnOffAutoAttack(StopReason.TARGET_LOST);
            return;
        }

        if (currentState != AIState.AI_PET_HARDATTACK && currentState != AIState.AI_PET_HARDIDLE_ATTACKING)
        {
            AIScriptSpellBuffRemove(Me, "PetCommandParticle");
        }

        switch (currentState)
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
                SetTarget(newTarget);
                break;
            case AIState.AI_PET_HOLDPOSITION:
                NetSetState(AIState.AI_PET_HOLDPOSITION_ATTACKING);
                SetTarget(newTarget);
                break;
            case AIState.AI_PET_ATTACK or AIState.AI_PET_HARDATTACK or AIState.AI_PET_RETURN_ATTACKING or AIState.AI_PET_ATTACKMOVE_ATTACKING or AIState.AI_PET_HARDIDLE_ATTACKING or AIState.AI_PET_HOLDPOSITION_ATTACKING or AIState.AI_TAUNTED when TargetInAttackRange():
                {
                    var e = GetTarget();
                    TurnOnAutoAttack(e);
                    break;
                }
            case AIState.AI_PET_ATTACK or AIState.AI_PET_HARDATTACK or AIState.AI_PET_RETURN_ATTACKING or AIState.AI_PET_ATTACKMOVE_ATTACKING or AIState.AI_PET_HARDIDLE_ATTACKING or AIState.AI_PET_HOLDPOSITION_ATTACKING or AIState.AI_TAUNTED:
                {
                    if (!TargetInCancelAttackRange())
                    {
                        TurnOffAutoAttack(StopReason.MOVING);
                    }
                    break;
                }
        }
    }

    public override void HaltAI()
    {
        StopTimer(TimerFeared);
        StopTimer(TimerScanDistance);
        StopTimer(TimerFindEnemies);
        TurnOffAutoAttack(StopReason.IMMEDIATELY);
        NetSetState(AIState.AI_HALTED);
    }
}