namespace AIScripts;

using GameServerCore.Enums;

using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.Scripting.CSharp.Converted;
using Chronobreak.GameServer.Scripting.Lua;

//Status: 100% Identical to Lua script
public class AzirTurretAI : CAIScript
{
    private const float UPDATE_INTERVAL = 0.15f;

    private bool Active;
    private bool SearchForTargets;
    private bool SearchIgnoresAzirMovement;

    public override bool OnInit()
    {
        SetState(AIState.AI_IDLE);
        InitTimer(Update, UPDATE_INTERVAL, true);
        Active = true;
        SearchForTargets = true;
        SearchIgnoresAzirMovement = false;
        return false;
    }

    public override bool OnOrder(OrderType order, AttackableUnit target, Vector2 position)
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED)
        {
            return true;
        }

        switch (order)
        {
            case OrderType.PetHardAttack:
                if (!Active)
                {
                    Active = true;
                    var redirectTarget = GetGoldRedirectTarget();
                    IssueOrder(redirectTarget, OrderType.PetHardAttack, GetPos(redirectTarget), redirectTarget);
                }
                break;

            case OrderType.PetHardReturn:
                Active = false;
                break;

            case OrderType.MoveTo:
                SearchForTargets = true;
                SearchIgnoresAzirMovement = false;
                TurnOffAutoAttack(StopReason.MOVING);
                break;

            case OrderType.AttackMove:
                SearchForTargets = true;
                SearchIgnoresAzirMovement = true;
                Update();
                break;

            case OrderType.Hold:
            case OrderType.Stop:
                SearchForTargets = false;
                SearchIgnoresAzirMovement = false;
                TurnOffAutoAttack(StopReason.TARGET_LOST);
                break;

            case OrderType.CastSpell:
                SearchForTargets = false;
                SearchIgnoresAzirMovement = false;
                if (Active)
                {
                    TurnOnAutoAttack(target);
                }
                break;
        }

        return true;
    }

    public void Update()
    {
        var redirectTarget = GetGoldRedirectTarget();
        if (redirectTarget == null)
        {
            ApplyDamage(Me, Me, 9999f, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW);
            return;
        }

        if (!Active)
        {
            return;
        }

        if (SearchForTargets)
        {
            if (!SearchIgnoresAzirMovement && TargetIsMoving(redirectTarget))
            {
                return;
            }

            if (!Functions.IsAutoAcquireTargetEnabled(redirectTarget))
            {
                return;
            }

            var target = FindTargetInAcR();
            if (target != null)
            {
                IssueOrder(redirectTarget, OrderType.PetHardMove, GetPos(redirectTarget));
            }
        }
    }

    public override void HaltAI()
    {
        StopTimer(Update);
        TurnOffAutoAttack(StopReason.IMMEDIATELY);
        NetSetState(AIState.AI_HALTED);
    }
}