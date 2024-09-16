namespace Buffs
{
    public class SkarnerVirulentSlashEnergy1 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "L_crystals", "R_crystals", "tail_t", },
            AutoBuffActivateEffect = new[] { "Skarner_Crystal_Slash_Buff.troy", "Skarner_Crystal_Slash_Buff.troy", "Skarner_Crystal_Slash_Buff.troy", },
        };
        EffectEmitter particle1;
        EffectEmitter particle2;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out particle1, out _, "Skarner_Crystal_Slash_Activate_L.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_hand", default, default, default, default, false, default, default, false, false);
            SpellEffectCreate(out particle2, out _, "Skarner_Crystal_Slash_Activate_R.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_hand", default, default, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle1);
            SpellEffectRemove(particle2);
        }
    }
}