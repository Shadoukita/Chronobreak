namespace Buffs
{
    public class AkaliShadowDanceCounter : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        public override void OnUpdateActions()
        {
            if (!IsDead(owner) && GetBuffCountFromCaster(owner, owner, nameof(Buffs.AkaliShadowDanceTimer)) == 0)
            {
                int count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.AkaliShadowDance));
                if (count != 3)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.AkaliShadowDanceTimer(), 1, 1, charVars.DanceTimerCooldown, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
            }
        }
        public override void OnKill(AttackableUnit target)
        {
            if (target is Champion)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.AkaliShadowDance(), 3, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false);
                int count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.AkaliShadowDance));
                if (count == 3)
                {
                    SpellBuffClear(owner, nameof(Buffs.AkaliShadowDanceTimer));
                }
            }
        }
        public override void OnAssist(ObjAIBase attacker, AttackableUnit target)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.AkaliShadowDance(), 3, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false);
            int count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.AkaliShadowDance));
            if (count != 3)
            {
                SpellBuffClear(owner, nameof(Buffs.AkaliShadowDanceTimer));
            }
        }
    }
}