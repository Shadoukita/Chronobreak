
using GameServerCore.Enums;
using GameServerLib.Scripting.Lua.Scripts;
using Chronobreak.GameServer.GameObjects.AttackableUnits;

namespace Chronobreak.GameServer.Scripting.Lua;

public static partial class Functions
{
    private static void SetStatus(AttackableUnit target, StatusFlags flags, bool src)
    {
        //TODO: Items?
        var buff = (BBScript.CurrentlyExecuted?.WrapperScript as BBBuffScript)?.Buff;
        if (buff != null && buff.TargetUnit == target)
        {
            buff.SetStatusEffect(flags, src);
        }
        else
        {
            target?.SetStatus(flags, src);
        }
    }
    private static bool GetStatus(AttackableUnit target, StatusFlags flags)
    {
        return target.Status.HasFlag(flags);
    }

    public static void SetCallForHelpSuppresser(AttackableUnit target, bool src)
    {
        SetStatus(target, StatusFlags.CallForHelpSuppressor, src);
    }
    public static void SetCanAttack(AttackableUnit target, bool src)
    {
        SetStatus(target, StatusFlags.CanAttack, src);
    }
    public static void SetCanCast(AttackableUnit target, bool src)
    {
        SetStatus(target, StatusFlags.CanCast, src);
    }
    public static void SetCanMove(AttackableUnit target, bool src)
    {
        SetStatus(target, StatusFlags.CanMove, src);
    }
    public static void SetCanMoveEver(AttackableUnit target, bool src)
    {
        SetStatus(target, StatusFlags.CanMoveEver, src);
    }
    public static void SetCharmed(AttackableUnit target, bool src)
    {
        SetStatus(target, StatusFlags.Charmed, src);
    }
    public static void SetDisableAmbientGold(AttackableUnit target, bool src)
    {
        SetStatus(target, StatusFlags.DisableAmbientGold, src);
    }
    public static void SetDisarmed(AttackableUnit target, bool src)
    {
        SetStatus(target, StatusFlags.Disarmed, src);
    }
    public static void SetFeared(AttackableUnit target, bool src)
    {
        SetStatus(target, StatusFlags.Feared, src);
    }
    public static void SetForceRenderParticles(AttackableUnit target, bool src)
    {
        SetStatus(target, StatusFlags.ForceRenderParticles, src);
    }
    public static void SetGhosted(AttackableUnit target, bool src)
    {
        SetStatus(target, StatusFlags.Ghosted, src);
    }
    public static void SetGhostProof(AttackableUnit target, bool src)
    {
        SetStatus(target, StatusFlags.GhostProof, src);
    }
    public static void SetIgnoreCallForHelp(AttackableUnit target, bool src)
    {
        SetStatus(target, StatusFlags.IgnoreCallForHelp, src);
    }
    public static void SetImmovable(AttackableUnit target, bool src)
    {
        SetStatus(target, StatusFlags.Immovable, src);
    }
    public static void SetInvulnerable(AttackableUnit target, bool src)
    {
        SetStatus(target, StatusFlags.Invulnerable, src);
    }
    public static void SetLifestealImmune(AttackableUnit target, bool src)
    {
        target.IsLifestealImmune = src;
    }
    public static void SetMagicImmune(AttackableUnit target, bool src)
    {
        SetStatus(target, StatusFlags.MagicImmune, src);
    }
    public static void SetNearSight(AttackableUnit target, bool src)
    {
        SetStatus(target, StatusFlags.NearSighted, src);
    }
    public static void SetNetted(AttackableUnit target, bool src)
    {
        SetStatus(target, StatusFlags.Netted, src);
    }
    public static void SetNoRender(AttackableUnit target, bool src)
    {
        SetStatus(target, StatusFlags.NoRender, src);
    }
    public static void SetPacified(AttackableUnit target, bool src)
    {
        SetStatus(target, StatusFlags.Pacified, src);
    }
    public static void SetPhysicalImmune(AttackableUnit target, bool src)
    {
        SetStatus(target, StatusFlags.PhysicalImmune, src);
    }
    public static void SetRevealSpecificUnit(AttackableUnit target, bool src)
    {
        SetStatus(target, StatusFlags.RevealSpecificUnit, src);
    }
    public static void SetRooted(AttackableUnit target, bool src)
    {
        SetStatus(target, StatusFlags.Rooted, src);
    }
    public static void SetSilenced(AttackableUnit target, bool src)
    {
        SetStatus(target, StatusFlags.Silenced, src);
    }
    public static void SetSleep(AttackableUnit target, bool src)
    {
        SetStatus(target, StatusFlags.Sleep, src);
    }
    public static void SetStealthed(AttackableUnit target, bool src)
    {
        SetStatus(target, StatusFlags.Stealthed, src);
    }
    public static void SetStunned(AttackableUnit target, bool src)
    {
        SetStatus(target, StatusFlags.Stunned, src);
    }
    public static void SetSuppressCallForHelp(AttackableUnit target, bool src)
    {
        SetStatus(target, StatusFlags.SuppressCallForHelp, src);
    }
    public static void SetSuppressed(AttackableUnit target, bool src)
    {
        SetStatus(target, StatusFlags.Suppressed, src);
    }
    public static void SetTargetable(AttackableUnit target, bool src)
    {
        SetStatus(target, StatusFlags.Targetable, src);
        //Check
        if (target is not null && src)
        {
            target.Stats.IsTargetableToTeam = SpellDataFlags.TargetableToAll;
        }
    }
    public static void SetTaunted(AttackableUnit target, bool src)
    {
        SetStatus(target, StatusFlags.Taunted, src);
    }

    public static bool GetCallForHelpSuppresser(AttackableUnit target)
    {
        return GetStatus(target, StatusFlags.CallForHelpSuppressor);
    }
    public static bool GetCanAttack(AttackableUnit target)
    {
        return GetStatus(target, StatusFlags.CanAttack);
    }
    public static bool GetCanCast(AttackableUnit target)
    {
        return GetStatus(target, StatusFlags.CanCast);
    }
    public static bool GetCanMove(AttackableUnit target)
    {
        //TODO: Verify
        return GetStatus(target, StatusFlags.CanMove)
            && GetStatus(target, StatusFlags.CanMoveEver)
            && !(
                   GetStatus(target, StatusFlags.Rooted)
                || GetStatus(target, StatusFlags.Stunned)
                || GetStatus(target, StatusFlags.Suppressed)
                || GetStatus(target, StatusFlags.Netted)
                || GetStatus(target, StatusFlags.Sleep)
            );
    }
    public static bool GetCanMoveEver(AttackableUnit target)
    {
        return GetStatus(target, StatusFlags.CanMoveEver);
    }
    public static bool GetCharmed(AttackableUnit target)
    {
        return GetStatus(target, StatusFlags.Charmed);
    }
    public static bool GetDisableAmbientGold(AttackableUnit target)
    {
        return GetStatus(target, StatusFlags.DisableAmbientGold);
    }
    public static bool GetDisarmed(AttackableUnit target)
    {
        return GetStatus(target, StatusFlags.Disarmed);
    }
    public static bool GetFeared(AttackableUnit target)
    {
        return GetStatus(target, StatusFlags.Feared);
    }
    public static bool GetForceRenderParticles(AttackableUnit target)
    {
        return GetStatus(target, StatusFlags.ForceRenderParticles);
    }
    public static bool GetGhosted(AttackableUnit target)
    {
        return GetStatus(target, StatusFlags.Ghosted);
    }
    public static bool GetGhostProof(AttackableUnit target)
    {
        return GetStatus(target, StatusFlags.GhostProof);
    }
    public static bool GetIgnoreCallForHelp(AttackableUnit target)
    {
        return GetStatus(target, StatusFlags.IgnoreCallForHelp);
    }
    public static bool GetInvulnerable(AttackableUnit target)
    {
        return GetStatus(target, StatusFlags.Invulnerable);
    }
    public static bool IsAutoCastOn(AttackableUnit target)
    {
        //TODO: Implement.
        //return GetStatus(target, StatusFlags.IsAutoCastOn);
        return false;
    }
    public static bool IsMoving(AttackableUnit target)
    {
        //TODO: Implement.
        //return GetStatus(target, StatusFlags.IsMoving);
        return false;
    }
    public static bool GetLifestealImmune(AttackableUnit target)
    {
        //TODO: Implement.
        //return GetStatus(target, StatusFlags.LifestealImmune);
        return false;
    }
    public static bool GetMagicImmune(AttackableUnit target)
    {
        return GetStatus(target, StatusFlags.MagicImmune);
    }
    public static bool GetNearSight(AttackableUnit target)
    {
        return GetStatus(target, StatusFlags.NearSighted);
    }
    public static bool GetNetted(AttackableUnit target)
    {
        return GetStatus(target, StatusFlags.Netted);
    }
    public static bool GetNoRender(AttackableUnit target)
    {
        return GetStatus(target, StatusFlags.NoRender);
    }
    public static bool GetPacified(AttackableUnit target)
    {
        return GetStatus(target, StatusFlags.Pacified);
    }
    public static bool GetPhysicalImmune(AttackableUnit target)
    {
        return GetStatus(target, StatusFlags.PhysicalImmune);
    }
    public static bool GetRevealSpecificUnit(AttackableUnit target)
    {
        return GetStatus(target, StatusFlags.RevealSpecificUnit);
    }
    public static bool GetRooted(AttackableUnit target)
    {
        return GetStatus(target, StatusFlags.Rooted);
    }
    public static bool GetSilenced(AttackableUnit target)
    {
        return GetStatus(target, StatusFlags.Silenced);
    }
    public static bool GetSleep(AttackableUnit target)
    {
        return GetStatus(target, StatusFlags.Sleep);
    }
    public static bool GetStealthed(AttackableUnit target)
    {
        return GetStatus(target, StatusFlags.Stealthed);
    }
    public static bool GetStunned(AttackableUnit target)
    {
        return GetStatus(target, StatusFlags.Stunned);
    }
    public static bool GetSuppressCallForHelp(AttackableUnit target)
    {
        return GetStatus(target, StatusFlags.SuppressCallForHelp);
    }
    public static bool GetSuppressed(AttackableUnit target)
    {
        return GetStatus(target, StatusFlags.Suppressed);
    }
    public static bool GetTargetable(AttackableUnit target)
    {
        return GetStatus(target, StatusFlags.Targetable);
    }
    public static bool GetTaunted(AttackableUnit target)
    {
        return GetStatus(target, StatusFlags.Taunted);
    }
}