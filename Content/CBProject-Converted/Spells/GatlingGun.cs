namespace Spells
{
    public class GatlingGun : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 20f, 16f, 12f, 8f, 4f, },
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { 4, 4.5f, 5, 5.5f, 6 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            AddBuff(attacker, target, new Buffs.GatlingGun(), 1, 1, effect0[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
        }
    }
}
namespace Buffs
{
    public class GatlingGun : BuffScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, true))
            {
                Vector3 pos = GetPointByUnitFacingOffset(owner, 300, -15);
                SpellCast((ObjAIBase)owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, 1, true, true, false);
                pos = GetPointByUnitFacingOffset(owner, 300, 0);
                SpellCast((ObjAIBase)owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, 1, true, true, false);
                pos = GetPointByUnitFacingOffset(owner, 300, 5);
                SpellCast((ObjAIBase)owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, 1, true, true, false);
                pos = GetPointByUnitFacingOffset(owner, 300, -5);
                SpellCast((ObjAIBase)owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, 1, true, true, false);
                pos = GetPointByUnitFacingOffset(owner, 300, 10);
                SpellCast((ObjAIBase)owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, 1, true, true, false);
                pos = GetPointByUnitFacingOffset(owner, 300, -10);
                SpellCast((ObjAIBase)owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, 1, true, true, false);
                pos = GetPointByUnitFacingOffset(owner, 300, 15);
                SpellCast((ObjAIBase)owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, 1, true, true, false);
            }
        }
    }
}