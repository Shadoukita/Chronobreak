namespace Buffs
{
    public class JarvanIVDragonStrikeDebuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "JarvanIVDragonStrikeDebuff",
            BuffTextureName = "JarvanIV_DragonStrike.dds",
        };
        float armorDebuff;
        EffectEmitter particle1;
        EffectEmitter hitParticle;
        public JarvanIVDragonStrikeDebuff(float armorDebuff = default)
        {
            this.armorDebuff = armorDebuff;
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            //RequireVar(this.armorDebuff);
            IncPercentArmorMod(owner, armorDebuff);
            SpellEffectCreate(out particle1, out _, "JarvanDragonStrike_debuff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out hitParticle, out _, "JarvanDragonStrike_hit.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle1);
            SpellEffectRemove(hitParticle);
        }
        public override void OnUpdateStats()
        {
            IncPercentArmorMod(owner, armorDebuff);
        }
    }
}