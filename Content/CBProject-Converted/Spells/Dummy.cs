namespace Spells
{
    public class Dummy : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 1f, 1f, 1f, 1f, 1f, },
            AutoTargetDamageByLevel = new[] { 1f, 1f, 1f, 1f, 1f, },
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
    }
}