namespace Buffs
{
    public class GravesSmokeGrenadeBoomSlow : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Nearsight_glb.troy", },
            BuffName = "GravesSmokeCloud",
            BuffTextureName = "GravesSmokeGrenade.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        float movementSpeedMod;
        public GravesSmokeGrenadeBoomSlow(float movementSpeedMod = default)
        {
            this.movementSpeedMod = movementSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.movementSpeedMod);
        }
        public override void OnDeactivate(bool expired)
        {
            AddBuff(attacker, owner, new Buffs.GravesSmokeGrenadeDelay(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, movementSpeedMod);
        }
    }
}