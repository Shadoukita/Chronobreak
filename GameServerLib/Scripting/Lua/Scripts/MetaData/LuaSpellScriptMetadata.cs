using System.Linq;
using Chronobreak.GameServer.Scripting.CSharp;
using MoonSharp.Interpreter;

namespace Chronobreak.GameServer.Scripting.Lua.Scripts;

public class LuaSpellScriptMetadata : SpellScriptMetadata, IBBMetadata
{
    public void Parse(Table globals)
    {
        //AutoItemActivateEffect = globals.RawGet("AutoItemActivateEffect")?.String ?? AutoItemActivateEffect;
        //AutoAuraBuffName = globals.RawGet("AutoAuraBuffName")?.String ?? AutoAuraBuffName;
        //IsDebugMode = globals.RawGet("IsDebugMode")?.Boolean ?? IsDebugMode;

        PhysicalDamageRatio = (float)(globals.RawGet("SpellDamageRatio")?.Number ?? PhysicalDamageRatio);
        SpellDamageRatio = (float)(
            (globals.RawGet("SpellDamageRatio")?.Number) ??
            (globals.RawGet("SetSpellDamageRatio")?.Number) ??
            SpellDamageRatio
        );

        //TODO:
        //ChainMissileParameters = (globals.RawGet("ChainMissileParameters").As<JObject>())?.ToObject<ChainMissileParameters>();

        ChannelDuration = (float)(globals.RawGet("ChannelDuration")?.Number ?? ChannelDuration);
        DoesntBreakShields = globals.RawGet("DoesntBreakShields")?.Boolean ?? DoesntBreakShields;

        bool? Invert(bool? src) => (src == null) ? null : !src;
        TriggersSpellCasts =
            globals.RawGet("TriggersSpellCasts")?.Boolean ??
            Invert(globals.RawGet("DoesntTriggerSpellCasts")?.Boolean) ??
            TriggersSpellCasts;

        IsDamagingSpell = globals.RawGet("IsDamagingSpell")?.Boolean ?? IsDamagingSpell;
        NotSingleTargetSpell = globals.RawGet("NotSingleTargetSpell")?.Boolean ?? NotSingleTargetSpell;

        float[]? ReadFloatArray(string name) =>
            globals.RawGet(name)?.Table.Values.Select(v => (float)(v?.Number ?? 0)).ToArray();

        AutoCooldownByLevel = ReadFloatArray("AutoCooldownByLevel") ?? AutoCooldownByLevel;
        AutoTargetDamageByLevel = ReadFloatArray("AutoTargetDamageByLevel") ?? AutoTargetDamageByLevel;

        CastingBreaksStealth = globals.RawGet("CastingBreaksStealth")?.Boolean ?? CastingBreaksStealth;
        CastTime = (float)(globals.RawGet("CastTime")?.Number ?? CastTime);

        string[]? ReadStringArray(string name) =>
            globals.RawGet(name)?.Table.Values.Select(v => v?.String ?? "").ToArray();

        SpellFXOverrideSkins = ReadStringArray("SpellFXOverrideSkins") ?? SpellFXOverrideSkins;
        SpellVOOverrideSkins = ReadStringArray("SpellVOOverrideSkins") ?? SpellVOOverrideSkins;
    }
}