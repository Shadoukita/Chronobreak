namespace Spells
{
    public class LuxLightstrikeToggle : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
        };
        public override void SelfExecute()
        {
            SpellBuffRemove(owner, nameof(Buffs.LuxLightStrikeKugel), owner);
        }
    }
}