namespace Buffs
{
    public class AlZaharVoidlingPhase3 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "BUFFBONE_GLB_HAND_R", "BUFFBONE_GLB_HAND_L", },
            AutoBuffActivateEffect = new[] { "AlzaharVoidling_speed.troy", "AlzaharVoidling_speed.troy", },
            BuffName = "AlZaharVoidlingPhase3",
            BuffTextureName = "AlZahar_SummonVoidling.dds",
        };
        public override void OnActivate()
        {
            SpellEffectCreate(out _, out _, "alzaharvoidling_evo.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, 1);
        }
    }
}