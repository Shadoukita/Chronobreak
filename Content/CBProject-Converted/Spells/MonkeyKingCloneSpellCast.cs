namespace Buffs
{
    public class MonkeyKingCloneSpellCast : BuffScript
    {
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            AddBuff((ObjAIBase)owner, attacker, new Buffs.MonkeyKingCloneSweep(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}