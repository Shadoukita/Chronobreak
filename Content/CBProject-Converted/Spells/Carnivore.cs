namespace Buffs
{
    public class Carnivore : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Carnivore",
            BuffTextureName = "GreenTerror_TailSpike.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastHeal;
        int lastMana; // UNUSED
        float lastTimeExecuted;
        int[] effect0 = { 34, 36, 38, 40, 42, 44, 46, 48, 50, 52, 54, 56, 58, 60, 62, 64, 66, 68, 70 };
        float[] effect1 = { 3.5f, 3.75f, 4, 4.25f, 4.5f, 4.75f, 5, 5.25f, 5.5f, 5.75f, 6, 6.25f, 6.5f, 6.75f, 7, 7.25f, 7.5f, 7.75f, 8, 8.25f };
        public override void OnActivate()
        {
            lastHeal = 0;
            lastMana = 0;
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(10, ref lastTimeExecuted, true))
            {
                int level = GetLevel(owner);
                float currentHeal = effect0[level - 1];
                float manaAmount = effect1[level - 1];
                if (currentHeal > lastHeal)
                {
                    lastHeal = currentHeal;
                    SetBuffToolTipVar(1, currentHeal);
                    SetBuffToolTipVar(2, manaAmount);
                }
            }
        }
        public override void OnKill(AttackableUnit target)
        {
            int level = GetLevel(owner);
            float manaAmount = effect1[level - 1];
            IncPAR(owner, manaAmount, PrimaryAbilityResourceType.MANA);
            float healAmount = effect0[level - 1];
            IncHealth(owner, healAmount, owner);
            SpellEffectCreate(out _, out _, "EternalThirst_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            float count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.Feast));
            count *= 0.5f;
            if (count < 1.5f)
            {
                SpellBuffRemoveStacks(owner, (ObjAIBase)owner, nameof(Buffs.Feast), 1);
            }
            else if (count < 2.5f)
            {
                SpellBuffRemoveStacks(owner, (ObjAIBase)owner, nameof(Buffs.Feast), 2);
            }
            else
            {
                SpellBuffRemoveStacks(owner, (ObjAIBase)owner, nameof(Buffs.Feast), 3);
            }
        }
    }
}