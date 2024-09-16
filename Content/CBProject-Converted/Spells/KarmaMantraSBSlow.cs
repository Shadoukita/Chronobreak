namespace Buffs
{
    public class KarmaMantraSBSlow : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "KarmaMantraSBSlow",
            BuffTextureName = "Chronokeeper_Timestop.dds",
        };
        float moveSpeedMod;
        EffectEmitter karmaSlow;
        public KarmaMantraSBSlow(float moveSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
            IncPercentMultiplicativeMovementSpeedMod(owner, moveSpeedMod);
            ApplyAssistMarker(attacker, target, 10);
            SpellEffectCreate(out karmaSlow, out _, "karma_spiritBond_slow_trigger.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(karmaSlow);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, moveSpeedMod);
        }
    }
}