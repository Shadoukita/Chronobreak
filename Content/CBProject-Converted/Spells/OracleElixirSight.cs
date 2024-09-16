namespace Spells
{
    public class OracleElixirSight : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            AddBuff((ObjAIBase)target, target, new Buffs.OracleElixirSight(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class OracleElixirSight : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head", },
            AutoBuffActivateEffect = new[] { "ElixirSight_aura_02.troy", },
            BuffName = "OracleElixir",
            BuffTextureName = "2026_Arcane_Protection_Potion.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        Region thisBubble;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            thisBubble = AddUnitPerceptionBubble(teamID, 750, owner, 25000, default, default, true);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(thisBubble);
        }
    }
}