namespace Spells
{
    public class NoRespawn : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class NoRespawn : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "NoRespawn",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            IncPercentRespawnTimeMod(owner, -3000);
        }
        public override void OnDeactivate(bool expired)
        {
            Alert("Should not be here");
        }
        public override void OnUpdateStats()
        {
            IncPercentRespawnTimeMod(owner, -3000);
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            float var = GetPercentRespawnTimeMod(owner);
            Alert("YO!", var);
        }
    }
}