namespace Buffs
{
    public class Hide : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Hide",
            BuffTextureName = "Twitch_AlterEgo.dds",
        };
        public override void OnActivate()
        {
            SetStealthed(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetStealthed(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetStealthed(owner, true);
        }
        public override void OnUpdateActions()
        {
            bool temp = IsMoving(owner);
            if (temp)
            {
                SpellBuffRemove(owner, nameof(Buffs.HideInShadows), attacker);
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            SpellBuffRemove(owner, nameof(Buffs.HideInShadows), (ObjAIBase)owner);
            SpellBuffRemoveCurrent(owner);
        }
    }
}