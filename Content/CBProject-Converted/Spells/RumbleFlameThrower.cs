namespace Spells
{
    public class RumbleFlameThrower : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 20f, 16f, 12f, 8f, 4f, },
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 20, 20, 20, 20, 20 };
        public override void SelfExecute()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.BLARGH)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.RumbleFlameThrower), owner, 0);
            }
            else
            {
                float par = GetPAR(target, PrimaryAbilityResourceType.Other);
                if (par >= 80)
                {
                    AddBuff(attacker, attacker, new Buffs.RumbleOverheat(), 1, 1, 5.25f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                    SetPARColorOverride(owner, 255, 0, 0, 255, 175, 0, 0, 255);
                }
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RumbleDangerZone)) > 0)
                {
                    AddBuff(attacker, target, new Buffs.RumbleFlameThrowerBuff(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                AddBuff(attacker, owner, new Buffs.RumbleFlameThrower(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                float initialHeatCost = effect0[level - 1];
                IncPAR(owner, initialHeatCost, PrimaryAbilityResourceType.Other);
                AddBuff(attacker, target, new Buffs.RumbleHeatDelay(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class RumbleFlameThrower : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "RumbleFlameThrower",
            BuffTextureName = "Rumble_Flamespitter.dds",
        };
        object dangerZone;
        float counter;
        float lastTimeExecuted;
        public RumbleFlameThrower(object dangerZone = default)
        {
            this.dangerZone = dangerZone;
        }
        public override void OnActivate()
        {
            //RequireVar(this.dangerZone);
            OverrideAnimation("Run", "Run2", owner);
            OverrideAnimation("Attack1", "Attack3", owner);
            OverrideAnimation("Attack2", "Attack3", owner);
            counter = 0;
        }
        public override void OnDeactivate(bool expired)
        {
            ClearOverrideAnimation("Run", owner);
            ClearOverrideAnimation("Attack1", owner);
            ClearOverrideAnimation("Attack2", owner);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, true))
            {
                Vector3 pos = GetPointByUnitFacingOffset(owner, 300, 0);
                object nextBuffVars_DangerZone = dangerZone; // UNUSED
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RumbleFlameThrowerBuff)) > 0)
                {
                    SpellCast((ObjAIBase)owner, default, pos, pos, 4, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
                }
                else
                {
                    SpellCast((ObjAIBase)owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
                }
                counter++;
                if (counter != 6)
                {
                    AddBuff(attacker, target, new Buffs.RumbleFlameThrowerEffect(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
    }
}