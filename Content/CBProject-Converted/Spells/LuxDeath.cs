namespace Buffs
{
    public class LuxDeath : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.LuxDeathParticleTimer(), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
    }
}