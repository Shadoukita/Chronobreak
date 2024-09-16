namespace Spells
{
    public class VladimirTransfusion : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = false,
            SpellFXOverrideSkins = new[] { "BloodkingVladimir", },
            SpellVOOverrideSkins = new[] { "BloodkingVladimir", },
        };
        int[] effect0 = { 0, 0, 0, 0, 0 };
        int[] effect1 = { 90, 125, 160, 195, 230 };
        public override void SelfExecute()
        {
            float healthCost = effect0[level - 1];
            float temp1 = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            if (healthCost >= temp1)
            {
                healthCost = temp1 - 1;
            }
            healthCost *= -1;
            IncHealth(owner, healthCost, owner);
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            int level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            ApplyDamage(attacker, target, effect1[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.6f, 1, false, false, attacker);
            SpellCast(attacker, owner, attacker.Position3D, owner.Position3D, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, targetPos);
        }
    }
}
namespace Buffs
{
    public class VladimirTransfusion : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "",
            BuffTextureName = "",
        };
    }
}