namespace Spells
{
    public class VladimirTidesofBlood : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            SpellFXOverrideSkins = new[] { "BloodkingVladimir", },
            SpellVOOverrideSkins = new[] { "BloodkingVladimir", },
        };
        int[] effect0 = { 30, 40, 50, 60, 70 };
        public override void SelfExecute()
        {
            int level = base.level;
            int count = GetBuffCountFromAll(owner, nameof(Buffs.VladimirTidesofBloodCost));
            charVars.NumTideStacks = count;
            float multiplier = count * 0.25f;
            multiplier++;
            float healthCost = effect0[level - 1];
            healthCost *= multiplier;
            float temp1 = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            if (healthCost >= temp1)
            {
                healthCost = temp1 - 1;
            }
            healthCost *= -1;
            IncHealth(owner, healthCost, owner);
            TeamId casterID = GetTeamID_CS(attacker); // UNUSED
            level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 620, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                bool canSee = CanSeeTarget(owner, target);
                if (canSee)
                {
                    SpellCast(owner, unit, owner.Position3D, owner.Position3D, 4, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                }
            }
            AddBuff(attacker, attacker, new Buffs.VladimirTidesofBloodCost(), 4, 1, 10, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff(attacker, attacker, new Buffs.VladimirTidesofBloodNuke(), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class VladimirTidesofBlood : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "",
            BuffTextureName = "Vladimir_TidesofBlood.dds",
        };
    }
}