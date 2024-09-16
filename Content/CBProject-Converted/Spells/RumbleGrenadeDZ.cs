﻿namespace Buffs
{
    public class RumbleGrenadeDZ : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", "", },
            AutoBuffActivateEffect = new[] { "", "", "", },
            BuffName = "RumbleDangerZone",
            BuffTextureName = "Annie_GhastlyShield.dds",
        };
    }
}