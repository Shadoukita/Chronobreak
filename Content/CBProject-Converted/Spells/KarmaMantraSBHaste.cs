namespace Buffs
{
    public class KarmaMantraSBHaste : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "KarmaMantraSBHaste",
            BuffTextureName = "KarmaSpiritBond.dds",
        };
        float moveSpeedMod;
        EffectEmitter moveSpeedPart1;
        public KarmaMantraSBHaste(float moveSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
            IncPercentMovementSpeedMod(owner, moveSpeedMod);
            SpellEffectCreate(out moveSpeedPart1, out _, "karma_spiritBond_speed_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, owner, default, default, false);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(moveSpeedPart1);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, moveSpeedMod);
        }
    }
}