using GameServerCore.Enums;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;

namespace Chronobreak.GameServer.GameObjects.AttackableUnits;

public class Shield
{
    public ObjAIBase SourceUnit { get; }
    public AttackableUnit TargetUnit { get; }
    public bool Physical { get; }
    public bool Magical { get; }
    public float Amount { get; protected set; }

    public Shield(ObjAIBase sourceUnit, AttackableUnit targetUnit, bool physical, bool magical, float amount)
    {
        SourceUnit = sourceUnit;
        TargetUnit = targetUnit;
        Physical = physical;
        Magical = magical;
        Amount = amount;
    }

    public float Consume(DamageData damageData)
    {
        float consumed = 0;
        if ((damageData.DamageType == DamageType.DAMAGE_TYPE_PHYSICAL && Physical) ||
            (damageData.DamageType == DamageType.DAMAGE_TYPE_MAGICAL && Magical) ||
            (damageData.DamageType == DamageType.DAMAGE_TYPE_MIXED))
        {
            if (Amount > damageData.Damage)
            {
                Amount -= damageData.Damage;
                consumed = damageData.Damage;
            }
            else
            {
                damageData.Damage -= Amount;
                consumed = Amount;
                Amount = 0;
            }
        }
        return consumed;
    }

    public bool IsConsumed()
    {
        return Amount <= 0;
    }
}