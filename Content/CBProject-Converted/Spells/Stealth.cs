namespace Buffs
{
    public class Stealth : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Stealth",
            BuffTextureName = "Evelynn_ReadyToBetray.dds",
        };
        Fade iD;
        public override void OnActivate()
        {
            SetStealthed(owner, true);
            iD = PushCharacterFade(owner, 0.2f, 3);
        }
        public override void OnDeactivate(bool expired)
        {
            SetStealthed(owner, false);
            PopCharacterFade(owner, iD);
        }
        public override void OnUpdateStats()
        {
            SetStealthed(owner, true);
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            if (spellVars.CastingBreaksStealth)
            {
                SpellBuffRemoveCurrent(owner);
            }
            else if (!spellVars.CastingBreaksStealth)
            {
            }
            else
            {
                if (!spellVars.DoesntTriggerSpellCasts)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}