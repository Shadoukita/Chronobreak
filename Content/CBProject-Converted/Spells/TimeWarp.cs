namespace Spells
{
    public class TimeWarp : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = { 2.5f, 3.25f, 4, 4.75f, 5.5f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_SpeedMod;
            if (target.Team == owner.Team)
            {
                nextBuffVars_SpeedMod = 0.55f;
                AddBuff(attacker, target, new Buffs.TimeWarp(nextBuffVars_SpeedMod), 1, 1, effect0[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.HASTE, 0, true);
            }
            else
            {
                int nextBuffVars_AttackSpeedMod = 0;
                nextBuffVars_SpeedMod = -0.55f;
                AddBuff(attacker, target, new Buffs.TimeWarpSlow(nextBuffVars_SpeedMod, nextBuffVars_AttackSpeedMod), 1, 1, effect0[level - 1], BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true);
            }
        }
    }
}
namespace Buffs
{
    public class TimeWarp : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "ChronoClockFast_tar.troy", "Global_Haste.troy", },
            BuffName = "Time Warp",
            BuffTextureName = "Chronokeeper_Haste.dds",
        };
        float speedMod;
        public TimeWarp(float speedMod = default)
        {
            this.speedMod = speedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.speedMod);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, speedMod);
        }
    }
}