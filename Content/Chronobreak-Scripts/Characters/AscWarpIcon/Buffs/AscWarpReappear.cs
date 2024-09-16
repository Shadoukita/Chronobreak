namespace Buffs
{
    internal class AscWarpReappear : BuffScript
    {
        public override BuffScriptMetaData BuffMetaData { get; } = new()
        {
            BuffType = BuffType.INTERNAL,
            BuffAddType = BuffAddType.REPLACE_EXISTING,
        };

        public override void OnActivate()
        {
            //AddParticleLink(target, "Global_Asc_Teleport_reappear", target, target, Buff.Duration);
        }
    }
}