namespace Buffs
{
    public class LeviathanCheck : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "",
            BuffTextureName = "",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnKill(AttackableUnit target)
        {
            if (target is Champion)
            {
                AddBuff(attacker, attacker, new Buffs.LeviathanStats(), 20, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false);
                AddBuff(attacker, attacker, new Buffs.LeviathanStats(), 20, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false);
            }
        }
        public override void OnAssist(ObjAIBase attacker, AttackableUnit target)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.LeviathanStats(), 20, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false);
        }
    }
}