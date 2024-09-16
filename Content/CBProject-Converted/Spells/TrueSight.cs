namespace Spells
{
    public class TrueSight : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            AddBuff((ObjAIBase)target, target, new Buffs.TrueSight(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
        }
    }
}
namespace Buffs
{
    public class TrueSight : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Magical Sight",
            BuffTextureName = "2026_Arcane_Protection_Potion.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        Region thisBubble;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            thisBubble = AddUnitPerceptionBubble(teamID, 700, owner, 25000, default, default, true);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(thisBubble);
        }
    }
}