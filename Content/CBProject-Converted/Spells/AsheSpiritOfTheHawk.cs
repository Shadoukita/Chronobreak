namespace Spells
{
    public class AsheSpiritOfTheHawk : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            Vector3 nextBuffVars_TargetPos = missileEndPosition;
            AddBuff(owner, owner, new Buffs.AsheSpiritOfTheHawkBubble(nextBuffVars_TargetPos), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class AsheSpiritOfTheHawk : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}