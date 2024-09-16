﻿namespace Buffs
{
    public class CassiopeiaMiasmaPoison : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head", },
            AutoBuffActivateEffect = new[] { "Global_Poison.troy", },
            BuffName = "CassiopeiaMiasma",
            BuffTextureName = "Cassiopeia_Miasma.dds",
        };
        float damagePerTick;
        float lastTimeExecuted;
        public CassiopeiaMiasmaPoison(float damagePerTick = default)
        {
            this.damagePerTick = damagePerTick;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damagePerTick);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, true))
            {
                ApplyDamage(attacker, target, damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.15f, 1, false, false, attacker);
            }
        }
    }
}