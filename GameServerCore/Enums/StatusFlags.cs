using System;

namespace GameServerCore.Enums
{
    /// <summary>
    /// Enum of all statuses that can be applied to a unit.
    /// </summary>
    [Flags]
    public enum StatusFlags
    {
        None,
        CallForHelpSuppressor = 1 << 0,
        CanAttack = 1 << 1,
        CanCast = 1 << 2,
        CanMove = 1 << 3,
        CanMoveEver = 1 << 4, // Deprecated before 4.20
        Charmed = 1 << 5,
        DisableAmbientGold = 1 << 6,
        Disarmed = 1 << 7,
        Feared = 1 << 8,
        ForceRenderParticles = 1 << 9,
        GhostProof = 1 << 10,
        Ghosted = 1 << 11,
        IgnoreCallForHelp = 1 << 12,
        Immovable = 1 << 13, // Not mentioned in either 131 or 4.20
        Invulnerable = 1 << 14,
        //IsAutoCastOn      // Mentioned in 4.20
        //IsMoving          // Mentioned in 4.20 and 131
        //LifestealImmune   // Mentioned in 4.20
        MagicImmune = 1 << 15,
        NearSighted = 1 << 16,
        Netted = 1 << 17,
        NoRender = 1 << 18,
        Pacified = 1 << 19,
        PhysicalImmune = 1 << 20,
        RevealSpecificUnit = 1 << 21,
        Rooted = 1 << 22,
        Silenced = 1 << 23,
        Sleep = 1 << 24,
        Stealthed = 1 << 25,
        Stunned = 1 << 26,
        SuppressCallForHelp = 1 << 27,
        Suppressed = 1 << 28, // Deprecated before 4.20
        Targetable = 1 << 29,
        Taunted = 1 << 30
    }
}