namespace Buffs
{
    public class MonkeyKingDecoyStealth : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MonkeyKingDecoyStealth",
            BuffTextureName = "MonkeyKingDecoy.dds",
        };
        public override void OnActivate()
        {
            Fade iD = PushCharacterFade(owner, 0.2f, 0); // UNUSED
            SetStealthed(owner, true);
            SetGhosted(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetStealthed(owner, false);
            SetGhosted(owner, false);
            PushCharacterFade(owner, 1, 0);
        }
        public override void OnUpdateStats()
        {
            SetStealthed(owner, true);
            SetGhosted(owner, true);
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
            else if (!spellVars.DoesntTriggerSpellCasts)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnLaunchAttack(AttackableUnit target)
        {
            SpellBuffRemoveCurrent(owner);
        }
    }
}