namespace Buffs
{
    public class KarmaTwoMantraParticle : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        EffectEmitter twoChargeSound; // UNUSED
        EffectEmitter twoCharge;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out twoChargeSound, out _, "KarmaTwoMantraSound.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true);
            SpellEffectCreate(out twoCharge, out _, "karma_mantraCharge_indicator_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(twoCharge);
        }
    }
}