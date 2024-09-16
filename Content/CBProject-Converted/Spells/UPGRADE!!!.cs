namespace Spells
{
    public class HeimerdingerR: UPGRADE___ {}
    public class UPGRADE___ : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        public override void SelfExecute()
        {
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions, nameof(Buffs.H28GEvolutionTurret), true))
            {
                if (GetBuffCountFromCaster(unit, attacker, nameof(Buffs.H28GEvolutionTurret)) > 0)
                {
                    AddBuff(attacker, unit, new Buffs.UpgradeSlow(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    float maxHP = GetMaxHealth(unit, PrimaryAbilityResourceType.MANA);
                    IncHealth(unit, maxHP, attacker);
                }
            }
            AddBuff(attacker, attacker, new Buffs.UpgradeBuff(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class UPGRADE___ : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "UPGRADE!!!",
            BuffTextureName = "Heimerdinger_UPGRADE.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float cooldownBonus;
        float[] effect0 = { -0.1f, -0.15f, -0.2f };
        public override void OnActivate()
        {
            int level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            cooldownBonus = effect0[level - 1];
        }
        public override void OnUpdateStats()
        {
            IncPercentCooldownMod(owner, cooldownBonus);
        }
        public override void OnLevelUpSpell(int slot)
        {
            if (slot == 3)
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                cooldownBonus = effect0[level - 1];
            }
        }
    }
}