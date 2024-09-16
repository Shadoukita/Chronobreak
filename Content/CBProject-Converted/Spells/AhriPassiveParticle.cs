namespace Buffs
{
    public class AhriPassiveParticle : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "AhriSoulCrusher",
            BuffTextureName = "Ahri_SoulEater.dds",
            PersistsThroughDeath = true,
        };
        bool particleAlive; // UNUSED
        EffectEmitter particle1;
        public override void OnActivate()
        {
            particleAlive = false;
            if (!IsDead(owner))
            {
                TeamId teamID = GetTeamID_CS(owner);
                SpellEffectCreate(out particle1, out _, "Ahri_Passive.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_GLB_WEAPON_1", default, owner, default, default, false, false, false, false, false);
                particleAlive = true;
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle1);
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            SpellEffectRemove(particle1);
            particleAlive = false;
        }
        public override void OnResurrect()
        {
            if (particleAlive)
            {
                SpellEffectRemove(particle1);
                particleAlive = false;
            }
            SpellEffectCreate(out particle1, out _, "Ahri_Passive.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_GLB_WEAPON_1", default, owner, default, default, false, false, false, false, false);
        }
    }
}