namespace Buffs
{
    public class UrgotHeatseekingManager : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        float reloadRate;
        public UrgotHeatseekingManager(float reloadRate = default)
        {
            this.reloadRate = reloadRate;
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.UrgotHeatseekingAmmo(), 4, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0, ref lastTimeExecuted, false, reloadRate))
            {
                if (!IsDead(owner))
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.UrgotHeatseekingAmmo(), 4, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false);
                }
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            spellName = GetSpellName(spell);
            if (spellName == nameof(Spells.UrgotHeatseekingMissile))
            {
                int count = GetBuffCountFromAll(owner, nameof(Buffs.UrgotHeatseekingAmmo));
                if (count == 4)
                {
                    ExecutePeriodicallyReset(out lastTimeExecuted);
                }
            }
        }
    }
}