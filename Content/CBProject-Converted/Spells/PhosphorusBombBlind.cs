namespace Spells
{
    public class PhosphorusBombBlind : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            IsDamagingSpell = false,
        };
    }
}
namespace Buffs
{
    public class PhosphorusBombBlind : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head", },
            AutoBuffActivateEffect = new[] { "global_Watched.troy", },
            BuffName = "PhosphorusBomb",
            BuffTextureName = "Corki_PhosphorusBomb.dds",
        };
        Region bubbleID;
        public override void OnActivate()
        {
            TeamId team = GetTeamID_CS(attacker);
            bubbleID = AddUnitPerceptionBubble(team, 400, owner, 6, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(bubbleID);
        }
    }
}