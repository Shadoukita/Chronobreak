namespace Buffs
{
    public class RumbleGrenadeZapEffect : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { null, "", "", },
            AutoBuffActivateEffect = new[] { "Aegis_buf.troy", "", "", },
            BuffName = "RumbleGrenadeAmmo",
            BuffTextureName = "Heimerdinger_HextechMicroRockets.dds",
        };
        EffectEmitter bParticle;
        EffectEmitter cParticle;
        public override void OnActivate()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RumbleGrenadeDZ)) > 0)
            {
                SpellEffectCreate(out bParticle, out _, "rumble_taze_tar_dangerZone_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "spike", default, owner, "spine", default, false, default, default, false);
                SpellEffectCreate(out cParticle, out _, "rumble_taze_tar_dangerZone.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "spike", default, owner, "Bird_head", default, false, default, default, false);
            }
            else
            {
                SpellEffectCreate(out bParticle, out _, "rumble_taze_beam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "spike", default, owner, "spine", default, false, default, default, false);
                SpellEffectCreate(out cParticle, out _, "rumble_taze_beam_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "spike", default, owner, "Bird_head", default, false, default, default, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(cParticle);
            SpellEffectRemove(bParticle);
        }
    }
}