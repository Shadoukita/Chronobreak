namespace Spells
{
    public class VayneInquisition : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 75f, 75f, 75f, 18f, 14f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 25, 40, 55 };
        int[] effect1 = { 8, 10, 12 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_ADMod = effect0[level - 1];
            AddBuff(attacker, target, new Buffs.VayneInquisition(nextBuffVars_ADMod), 1, 1, effect1[level - 1], BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class VayneInquisition : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "C_BUFFBONE_GLB_CHEST_LOC", "C_BUFFBONE_GLB_CENTER_LOC", "", "", },
            AutoBuffActivateEffect = new[] { "vayne_ult_primary_buf.troy", "vayne_ult_primary_buf_02.troy", "vayne_ult_primary_buf_03.troy", "", },
            BuffName = "VayneInquisition",
            BuffTextureName = "Vayne_Inquisition.dds",
        };
        float aDMod;
        public VayneInquisition(float aDMod = default)
        {
            this.aDMod = aDMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.aDMod);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.VayneTumbleBonus)) > 0)
            {
                OverrideAnimation("Idle1", "Idle_TumbleUlt", owner);
                OverrideAnimation("Idle2", "Idle_TumbleUlt", owner);
                OverrideAnimation("Idle3", "Idle_TumbleUlt", owner);
                OverrideAnimation("Idle4", "Idle_TumbleUlt", owner);
                OverrideAnimation("Attack1", "Attack_TumbleUlt", owner);
                OverrideAnimation("Attack2", "Attack_TumbleUlt", owner);
                OverrideAnimation("Crit", "Attack_TumbleUlt", owner);
                OverrideAnimation("Spell3", "Attack_TumbleUlt", owner);
                OverrideAnimation("Run", "Run_TumbleUlt", owner);
                OverrideAutoAttack(5, SpellSlotType.ExtraSlots, owner, 1, false);
            }
            else
            {
                OverrideAnimation("Idle1", "Idle_Ult", owner);
                OverrideAnimation("Idle2", "Idle_Ult", owner);
                OverrideAnimation("Idle3", "Idle_Ult", owner);
                OverrideAnimation("Idle4", "Idle_Ult", owner);
                OverrideAnimation("Attack1", "Attack_Ult", owner);
                OverrideAnimation("Attack2", "Attack_Ult", owner);
                OverrideAnimation("Crit", "Attack_Ult", owner);
                OverrideAnimation("Spell3", "Attack_Ult", owner);
                OverrideAnimation("Run", "Run_Ult", owner);
                OverrideAutoAttack(4, SpellSlotType.ExtraSlots, owner, 1, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.VayneTumbleBonus)) > 0)
            {
                OverrideAnimation("Idle1", "Idle_Tumble", owner);
                OverrideAnimation("Idle2", "Idle_Tumble", owner);
                OverrideAnimation("Idle3", "Idle_Tumble", owner);
                OverrideAnimation("Idle4", "Idle_Tumble", owner);
                OverrideAnimation("Attack1", "Attack_Tumble", owner);
                OverrideAnimation("Attack2", "Attack_Tumble", owner);
                OverrideAnimation("Crit", "Attack_Tumble", owner);
                ClearOverrideAnimation("Spell3", owner);
                OverrideAnimation("Run", "Run_Tumble", owner);
                OverrideAutoAttack(2, SpellSlotType.ExtraSlots, owner, 1, false);
            }
            else
            {
                ClearOverrideAnimation("Idle1", owner);
                ClearOverrideAnimation("Idle2", owner);
                ClearOverrideAnimation("Idle3", owner);
                ClearOverrideAnimation("Idle4", owner);
                ClearOverrideAnimation("Attack1", owner);
                ClearOverrideAnimation("Attack2", owner);
                ClearOverrideAnimation("Crit", owner);
                ClearOverrideAnimation("Spell3", owner);
                ClearOverrideAnimation("Run", owner);
                RemoveOverrideAutoAttack(owner, false);
            }
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, aDMod);
        }
    }
}