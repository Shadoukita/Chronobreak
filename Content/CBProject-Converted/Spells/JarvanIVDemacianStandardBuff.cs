namespace Buffs
{
    public class JarvanIVDemacianStandardBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "JarvanIVDemacianStandard",
            BuffTextureName = "JarvanIV_DemacianStandard.dds",
        };
        float attackSpeedMod;
        float armorMod;
        EffectEmitter asdf;
        public JarvanIVDemacianStandardBuff(float attackSpeedMod = default, float armorMod = default)
        {
            this.attackSpeedMod = attackSpeedMod;
            this.armorMod = armorMod;
        }
        public override void OnActivate()
        {
            TeamId teamID; // UNITIALIZED
            teamID = TeamId.TEAM_UNKNOWN; //TODO: Verify
            //RequireVar(this.attackSpeedMod);
            //RequireVar(this.armorMod);
            SpellEffectCreate(out asdf, out _, "JarvanDemacianStandard_shield.troy", default, teamID, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, owner, default, default, true);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(asdf);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, attackSpeedMod);
            IncFlatArmorMod(owner, armorMod);
        }
    }
}