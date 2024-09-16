using GameServerCore;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.GameObjects.SpellNS;
using GameServerLib.Services;

namespace Katarina;

public class KatarinaPackage
{
    public static void CastE(ObjAIBase katarina, Vector3 targetPos)
    {
        katarina.Spells.Extra[0].TryCast(
            default,
            targetPos,
            targetPos,
            0,
            false,
            true,
            false,
            false,
            false,
            true,
            default
        );

        var indicator = new EffectEmitter(
            "Katarina",
            "Katarina_Base_E_EndIndicator.troy",
            katarina,
            default,
            default,
            "",
            "",
            targetPos.ToVector2(),
            katarina.Direction,
            targetPos.ToVector2(),
            flags: (FXFlags)48
        );
        SpecialEffectService.SpawnFx([indicator], katarina.NetId);
    }
}

