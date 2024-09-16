namespace Buffs
{
    public class SeismicShardBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SeismicShard",
            BuffTextureName = "Malphite_SeismicShard.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        int level;
        EffectEmitter sSSlow;
        float value;
        float[] effect0 = { 0.14f, 0.17f, 0.2f, 0.23f, 0.26f };
        public SeismicShardBuff(int level = default)
        {
            this.level = level;
        }
        public override void OnActivate()
        {
            //RequireVar(this.level);
            SpellEffectCreate(out sSSlow, out _, "Global_Slow.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
            value = GetMovementSpeed(owner);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(sSSlow);
        }
        public override void OnUpdateStats()
        {
            int level = this.level;
            float modifier = effect0[level - 1];
            float result = value * modifier;
            IncFlatMovementSpeedMod(attacker, result);
            result *= -1;
            IncFlatMovementSpeedMod(owner, result);
        }
    }
}