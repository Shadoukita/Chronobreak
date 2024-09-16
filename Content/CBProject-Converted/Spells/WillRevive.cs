namespace Buffs
{
    public class WillRevive : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "spine", },
            AutoBuffActivateEffect = new[] { "rebirthready.troy", },
            BuffName = "Guardian Angel",
            BuffTextureName = "3026_Guardian_Angel.dds",
            NonDispellable = true,
            OnPreDamagePriority = 6,
            PersistsThroughDeath = true,
        };

        //TODO: Check
        public override void OnUpdateStats()
        {
            string name = GetSlotSpellName((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name1 = GetSlotSpellName((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name2 = GetSlotSpellName((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name3 = GetSlotSpellName((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name4 = GetSlotSpellName((ObjAIBase)owner, 4, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name5 = GetSlotSpellName((ObjAIBase)owner, 5, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            float guardianAngelCount = 0;
            if (name == nameof(Buffs.GuardianAngel))
            {
                guardianAngelCount++;
            }
            if (name1 == nameof(Buffs.GuardianAngel))
            {
                guardianAngelCount++;
            }
            if (name2 == nameof(Buffs.GuardianAngel))
            {
                guardianAngelCount++;
            }
            if (name3 == nameof(Buffs.GuardianAngel))
            {
                guardianAngelCount++;
            }
            if (name4 == nameof(Buffs.GuardianAngel))
            {
                guardianAngelCount++;
            }
            if (name5 == nameof(Buffs.GuardianAngel))
            {
                guardianAngelCount++;
            }
            if (guardianAngelCount == 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.WillRevive), (ObjAIBase)owner, 0);
            }
        }

        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.HasBeenRevived)) == 0)
            {
                float curHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
                if (curHealth <= damageAmount)
                {
                    if (damageSource != DamageSource.DAMAGE_SOURCE_INTERNALRAW)
                    {
                        if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.GuardianAngel)) == 0)
                        {
                            if (owner is Champion)
                            {
                                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.YorickRAZombie)) == 0)
                                {
                                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.YorickRAZombieLich)) == 0)
                                    {
                                        if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.YorickRAZombieKogMaw)) == 0)
                                        {
                                            damageAmount = curHealth - 1;
                                            AddBuff((ObjAIBase)owner, owner, new Buffs.GuardianAngel(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}