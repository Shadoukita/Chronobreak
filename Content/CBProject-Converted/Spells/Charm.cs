namespace Buffs
{
    public class Charm : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Charm_tar.troy", },
            BuffName = "Charm",
            BuffTextureName = "48thSlave_Pacify.dds",
        };
        public override void OnActivate()
        {
            SetCharmed(owner, true);
            RedirectGold(owner, attacker);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCharmed(owner, false);
            RedirectGold(owner, null);
        }
        public override void OnUpdateStats()
        {
            SetCharmed(owner, true);
            IncPercentPhysicalDamageMod(owner, -0.3f);
        }
    }
}