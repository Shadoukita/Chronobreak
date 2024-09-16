namespace Spells
{
    public class IreliaHitenStyleCharged : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class IreliaHitenStyleCharged : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "BUFFBONE_GLB_WEAPON_1", "BUFFBONE_GLB_WEAPON_1", "", "", },
            AutoBuffActivateEffect = new[] { "irelia_hitenStyle_activate.troy", "irelia_hitenStlye_active_glow.troy", "", "", },
            BuffName = "IreliaHitenStyleCharged",
            BuffTextureName = "Irelia_HitenStyle.dds",
        };
        int[] effect0 = { 15, 30, 45, 60, 75 };
        int[] effect1 = { 10, 14, 18, 22, 26 };
        public override void OnActivate()
        {
            OverrideAnimation("Attack1", "Attack1c", owner);
            OverrideAnimation("Attack2", "Attack2c", owner);
            OverrideAnimation("Crit", "Critc", owner);
            OverrideAnimation("Idle1", "Idle1c", owner);
            OverrideAnimation("Run", "Runc", owner);
        }
        public override void OnDeactivate(bool expired)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.IreliaHitenStyle(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float trueDamage = effect0[level - 1];
            float healthRestoration = effect1[level - 1];
            IncHealth(owner, healthRestoration, owner);
            if (target is ObjAIBase && target is not BaseTurret)
            {
                ApplyDamage(attacker, target, trueDamage, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 1, false, false, attacker);
            }
        }
    }
}