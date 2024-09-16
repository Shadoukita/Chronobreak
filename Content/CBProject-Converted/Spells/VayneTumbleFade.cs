namespace Buffs
{
    public class VayneTumbleFade : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "VayneInquisitionStealth",
            BuffTextureName = "MasterYi_Vanish.dds",
        };
        public override void OnActivate()
        {
            PushCharacterFade(owner, 0.2f, 0);
            SetStealthed(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            PushCharacterFade(owner, 1, 0);
            SetStealthed(owner, false);
        }
        public override void OnUpdateStats()
        {
            PushCharacterFade(owner, 0.2f, 0);
            SetStealthed(owner, true);
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            if (spellVars.CastingBreaksStealth)
            {
                SpellBuffRemoveCurrent(owner);
            }
            else if (!spellVars.DoesntTriggerSpellCasts)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}