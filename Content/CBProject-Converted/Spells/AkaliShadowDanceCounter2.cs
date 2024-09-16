namespace Buffs
{
    public class AkaliShadowDanceCounter2 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(20, ref lastTimeExecuted, false))
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level > 0)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.AkaliShadowDance(), 3, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false);
                }
                if (level == 3)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.AkaliShadowDanceCounter3(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                    SpellBuffRemove(owner, nameof(Buffs.AkaliShadowDanceCounter2), (ObjAIBase)owner);
                }
            }
        }
        public override void OnKill(AttackableUnit target)
        {
            if (target is Champion)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.AkaliShadowDance(), 3, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false);
            }
        }
        public override void OnAssist(ObjAIBase attacker, AttackableUnit target)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.AkaliShadowDance(), 3, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false);
        }
    }
}