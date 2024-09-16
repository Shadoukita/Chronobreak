using GameServerCore.Enums;

namespace Chronobreak.GameServer.GameObjects.AttackableUnits;

public class DamageData
{
    /// <summary>
    /// Unit that inflicted the damage.
    /// </summary>
    public AttackableUnit Attacker { get; init; }
    /// <summary>
    /// The raw amount of damage to be inflicted (Pre-mitigated damage)
    /// </summary>
    public float Damage { get; set; }
    /// <summary>
    /// The result of this damage (Ex. Dodged, Missed, Invulnerable or Crit)
    /// </summary>
    public DamageResultType DamageResultType { get; set; } = DamageResultType.RESULT_NORMAL;
    /// <summary>
    /// Source of the damage.
    /// </summary>
    public DamageSource DamageSource { get; init; }
    /// <summary>
    /// Type of damage received.
    /// </summary>
    public DamageType DamageType { get; init; } = DamageType.DAMAGE_TYPE_PHYSICAL;
    /// <summary>
    /// Unit that will receive the damage.
    /// </summary>
    public AttackableUnit Target { get; init; }
    /// <summary>
    /// Whether to ignore crits from attacker
    /// </summary>
    public bool IgnoreDamageCrit { get; set; }
    /// <summary>
    /// Whether to prevent any damage increases
    /// </summary>
    public bool IgnoreDamageIncreaseMods { get; set; }
}
