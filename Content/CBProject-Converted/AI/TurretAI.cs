namespace AIScripts;

using GameServerCore.Enums;

using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.Scripting.CSharp.Converted;
using System.Collections.Generic;

//Status: 100% Identical to Lua script
public class TurretAI : CAIScript
{
    private AttackableUnit _tauntTarget;
    private List<AttackableUnit> _turretTargetList = new List<AttackableUnit>();

    public override bool OnInit()
    {
        SetState(AIState.AI_HARDIDLE);
        InitTimer(TimerFindEnemies, 0.15f, true);
        return false;
    }

    public override void OnTargetLost(LostTargetEvent eventType, AttackableUnit target)
    {
        var state = GetState();
        if (state == AIState.AI_HALTED)
        {
            return;
        }

        AttackableUnit newTarget = _turretTargetList.Count > 0 ? _turretTargetList[0] : FindTargetInAcR();

        if (newTarget == null)
        {
            if (state != AIState.AI_HARDIDLE_ATTACKING && state != AIState.AI_TAUNTED)
            {
                NetSetState(AIState.AI_HARDIDLE);
            }
        }
        else
        {
            if (state is AIState.AI_HARDIDLE_ATTACKING or AIState.AI_TAUNTED)
            {
                NetSetState(AIState.AI_HARDIDLE_ATTACKING);
                SetTarget(newTarget);
            }
        }
    }

    public override void OnCallForHelp(ObjAIBase attacker, ObjAIBase victim)
    {
        var state = GetState();
        if (state == AIState.AI_HALTED)
        {
            return;
        }

        if (attacker != null)
        {
            if (state != AIState.AI_HARDIDLE && state != AIState.AI_HARDIDLE_ATTACKING)
            {
                return;
            }

            NetSetState(AIState.AI_HARDIDLE_ATTACKING);
            SetTarget(attacker);
        }
    }

    public override void OnTauntBegin()
    {
        if (GetState() == AIState.AI_HALTED)
        {
            return;
        }

        _tauntTarget = GetTauntTarget();
        if (_tauntTarget != null)
        {
            NetSetState(AIState.AI_TAUNTED);
            SetTarget(_tauntTarget);
        }
    }

    public override void OnTauntEnd()
    {
        if (GetState() == AIState.AI_HALTED)
        {
            return;
        }

        _tauntTarget = GetTauntTarget();
        if (_tauntTarget != null)
        {
            NetSetState(AIState.AI_HARDIDLE_ATTACKING);
            SetTarget(_tauntTarget);
        }
        else
        {
            NetSetState(AIState.AI_HARDIDLE);
            TimerFindEnemies();
        }
    }

    public override void OnCanMove()
    {
        if (GetState() == AIState.AI_HALTED)
        {
            return;
        }

        NetSetState(AIState.AI_HARDIDLE);
        TimerFindEnemies();
    }

    public override void OnCanAttack()
    {
        if (GetState() == AIState.AI_HALTED)
        {
            return;
        }

        NetSetState(AIState.AI_HARDIDLE);
        TimerFindEnemies();
    }

    private void TimerFindEnemies()
    {
        var state = GetState();
        if (state == AIState.AI_HALTED)
        {
            return;
        }

        UpdateTargetList();

        if (state == AIState.AI_HARDIDLE)
        {
            AttackableUnit newTarget = _turretTargetList.Count > 0 ? _turretTargetList[0] : FindTargetInAcR();

            if (newTarget != null)
            {
                NetSetState(AIState.AI_HARDIDLE_ATTACKING);
                SetTarget(newTarget);
            }
            else
            {
                TurnOffAutoAttack(StopReason.TARGET_LOST);
                return;
            }
        }

        if (state != AIState.AI_HARDIDLE_ATTACKING && state != AIState.AI_TAUNTED)
        {
            return;
        }

        if (TargetInAttackRange())
        {
            TurnOnAutoAttack(GetTarget());
        }
        else
        {
            NetSetState(AIState.AI_HARDIDLE);
            TurnOffAutoAttack(StopReason.MOVING);
        }
    }

    private void UpdateTargetList()
    {
        _turretTargetList.RemoveAll(target => !ObjectInAttackRange(target));
    }

    public override void HaltAI()
    {
        StopTimer(TimerFindEnemies);
        TurnOffAutoAttack(StopReason.IMMEDIATELY);
        NetSetState(AIState.AI_HALTED);
    }
}