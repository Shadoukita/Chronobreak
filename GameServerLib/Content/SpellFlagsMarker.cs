using System;
using System.Collections.Generic;
using GameServerCore.Enums;
using Chronobreak.GameServer.Content;

namespace GameServerLib.Content;

public static class SpellFlagsMarker
{
    private static readonly Dictionary<string, string> SpellFlagConsistencyConversionMap = new()
    {
        { "Autocast", "AutoCast" },
        { "Instacast", "InstantCast" },
        { "InstaCast", "InstantCast" },
        { "NotAffectZombies", "NotAffectZombie" },
        { "AffectFriendly", "AffectFriends" },
        { "AffectNeutrals", "AffectNeutral" },
        { "AlwaysAffectSelf", "AlwaysSelf" },
        { "AffectBarracksOnly", "AffectBarrackOnly" },
        { "AffectsWards", "AffectWards" }
    };

    private static SpellDataFlags ParseSpellDataFlagsFromString(string input)
    {
        if (string.IsNullOrEmpty(input)) return default;
        var flags = input.Split('|');
        var result = 0;
        foreach (var flag in flags)
        {
            var flagName = flag.Trim();
            if (SpellFlagConsistencyConversionMap.ContainsKey(flagName))
                flagName = SpellFlagConsistencyConversionMap[flagName];

            if (Enum.TryParse(typeof(SpellDataFlags), flagName, out var res)) result |= (int)res;
        }

        return (SpellDataFlags)Enum.ToObject(typeof(SpellDataFlags), result);
    }

    /// <summary>
    /// Switches any flags from TextFlags to Flags SpellData
    /// </summary>
    /// <param name="data">Spell data to process</param>
    public static void SwitchFlagsIfNeeded(SpellData? data)
    {
        if (data != null) data.Flags = ParseSpellDataFlagsFromString(data.TextFlags);
    }
}