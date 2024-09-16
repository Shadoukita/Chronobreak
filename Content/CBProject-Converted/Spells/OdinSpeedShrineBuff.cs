namespace Buffs
{
    public class OdinSpeedShrineBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "OdinSpeedShrineBuff",
            BuffTextureName = "Odin_SpeedShrine.dds",
            NonDispellable = true,
        };
        float speedMod;
        float massiveBoostOverseer;
        float massiveSpeedMod;
        EffectEmitter buffParticle;
        EffectEmitter buffParticle2;
        public OdinSpeedShrineBuff(float speedMod = default)
        {
            this.speedMod = speedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.speedMod);
            massiveBoostOverseer = 1;
            massiveSpeedMod = speedMod * 2;
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out buffParticle, out _, "invis_runes_01.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out buffParticle2, out _, "Odin_Speed_Shrine_buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(buffParticle);
            SpellEffectRemove(buffParticle2);
        }
        public override void OnUpdateStats()
        {
            if (massiveBoostOverseer < 4)
            {
                IncPercentMovementSpeedMod(owner, massiveSpeedMod);
                massiveBoostOverseer++;
            }
            else
            {
                IncPercentMovementSpeedMod(owner, speedMod);
            }
        }
    }
}