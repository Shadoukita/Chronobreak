
using MoonSharp.Interpreter;
using static Chronobreak.GameServer.Scripting.Lua.BBCacheEntry;
using GameServerCore.Enums;
using Chronobreak.GameServer.Scripting.CSharp;

namespace Chronobreak.GameServer.Scripting.Lua.Scripts;

public class LuaBuffScriptMetadataUnmutable : BuffScriptMetadataUnmutable, IBBMetadata
{
    public void Parse(Table globals)
    {
        BuffName = globals.RawGet("BuffName")?.String ?? BuffName;
        BuffTextureName = globals.RawGet("BuffTextureName")?.String ?? BuffTextureName;

        MinimapIconTextureName = globals.RawGet("MinimapIconTextureName")?.String ?? MinimapIconTextureName;
        MinimapIconEnemyTextureName = globals.RawGet("MinimapIconEnemyTextureName")?.String ?? MinimapIconEnemyTextureName;

        AutoBuffActivateEvent = globals.RawGet("AutoBuffActivateEvent")?.String ?? AutoBuffActivateEvent;
        AutoBuffActivateEffect = ReadArray<string?>(globals, "AutoBuffActivateEffect", null) ?? AutoBuffActivateEffect;
        AutoBuffActivateEffectFlags = (EffCreate?)(globals.RawGet("AutoBuffActivateEffectFlags")?.Number) ?? AutoBuffActivateEffectFlags;
        AutoBuffActivateAttachBoneName = ReadArray<string?>(globals, "AutoBuffActivateAttachBoneName", null) ?? AutoBuffActivateAttachBoneName;
        SpellToggleSlot = (int)(globals.RawGet("SpellToggleSlot")?.Number ?? SpellToggleSlot);

        bool? Invert(bool? src) => (src == null) ? null : !src;
        NonDispellable = globals.RawGet("NonDispellable")?.Boolean ??
                        Invert(globals.RawGet("Nondispellable")?.Boolean) ??
                        NonDispellable;

        PersistsThroughDeath = globals.RawGet("PersistsThroughDeath")?.Boolean ??
                               globals.RawGet("PermeatesThroughDeath")?.Boolean ??
                               PersistsThroughDeath;

        IsPetDurationBuff = globals.RawGet("IsPetDurationBuff")?.Boolean ?? IsPetDurationBuff;

        PopupMessage = ReadArray<string?>(globals, "PopupMessage", "")!;
        IsDeathRecapSource = globals.RawGet("IsDeathRecapSource")?.Boolean ?? IsDeathRecapSource;
        OnPreDamagePriority = (int)(globals.RawGet("OnPreDamagePriority")?.Number ?? OnPreDamagePriority);
        DoOnPreDamageInExpirationOrder = globals.RawGet("DoOnPreDamageInExpirationOrder")?.Boolean ?? DoOnPreDamageInExpirationOrder;
    }
}