namespace Buffs
{
    public class EzrealRisingSpellForce : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "EzrealRisingSpellForce",
            BuffTextureName = "Ezreal_RisingSpellForce.dds",
        };
        EffectEmitter particle;
        int lastCount;
        public override void OnActivate()
        {
            int count = GetBuffCountFromAll(owner, nameof(Buffs.EzrealRisingSpellForce));
            float totalAttackSpeed = count * 10;
            SetBuffToolTipVar(1, totalAttackSpeed);
            if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.EzrealRisingSpellForce)) > 0)
            {
                if (count == 1)
                {
                    SpellEffectCreate(out particle, out _, "Ezreal_glow1.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "l_hand", default, owner, default, default, false);
                }
                if (count == 2)
                {
                    SpellEffectCreate(out particle, out _, "Ezreal_glow2.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "l_hand", default, owner, default, default, false);
                }
                if (count == 3)
                {
                    SpellEffectCreate(out particle, out _, "Ezreal_glow3.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "l_hand", default, owner, default, default, false);
                }
                if (count == 4)
                {
                    SpellEffectCreate(out particle, out _, "Ezreal_glow4.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "l_hand", default, owner, default, default, false);
                }
                if (count == 5)
                {
                    if (lastCount != 5)
                    {
                        SpellEffectCreate(out particle, out _, "Ezreal_glow5.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "l_hand", default, owner, default, default, false);
                    }
                }
                lastCount = count;
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, 0.1f);
        }
    }
}