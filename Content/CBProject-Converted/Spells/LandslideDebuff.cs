namespace Buffs
{
    public class LandslideDebuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "LandslideDebuff",
            BuffTextureName = "Malphite_GroundSlam.dds",
        };
        int level;
        EffectEmitter landslideLHand;
        EffectEmitter landslideRHand;
        float[] effect0 = { -0.3f, -0.35f, -0.4f, -0.45f, -0.5f };
        public LandslideDebuff(int level = default)
        {
            this.level = level;
        }
        public override void OnActivate()
        {
            //RequireVar(this.level);
            SpellEffectCreate(out landslideLHand, out _, "Landslide_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_hand", default, target, default, default, false);
            SpellEffectCreate(out landslideRHand, out _, "Landslide_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_hand", default, target, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(landslideLHand);
            SpellEffectRemove(landslideRHand);
        }
        public override void OnUpdateStats()
        {
            int level = this.level;
            IncPercentMultiplicativeAttackSpeedMod(owner, effect0[level - 1]);
        }
    }
}