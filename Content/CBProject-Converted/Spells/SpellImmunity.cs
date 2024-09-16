namespace Spells
{
    public class SpellImmunity : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 0f, 0f, 0f, 0f, 0f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class SpellImmunity : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Global_Spellimmunity.troy", },
            BuffName = "Spell Immunity",
            BuffTextureName = "FallenAngel_BlackShield.dds",
        };
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            bool returnValue = true;
            if (owner.Team != attacker.Team)
            {
                Say(owner, "game_lua_SpellImmunity");
                returnValue = false;
            }
            else
            {
                returnValue = true;
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            SetMagicImmune(owner, true);
            SetCanCast(owner, true);
            SetCanMove(owner, true);
            SetCanAttack(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetMagicImmune(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetMagicImmune(owner, true);
            SetCanMove(owner, true);
            SetCanAttack(owner, true);
            SetCanCast(owner, true);
        }
    }
}