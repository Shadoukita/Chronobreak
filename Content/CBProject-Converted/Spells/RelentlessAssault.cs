namespace Spells
{
    public class RelentlessAssault : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { 5, 6.5f, 8 };
        public override void SelfExecute()
        {
            float duration = effect0[level - 1];
            AddBuff(attacker, owner, new Buffs.ArmsmasterRelentlessMR(), 1, 1, duration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class RelentlessAssault : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Relentless Assault",
            BuffTextureName = "Armsmaster_CoupDeGrace.dds",
        };
        float[] effect0 = { 0.06f, 0.1f, 0.14f };
        public override void OnUpdateStats()
        {
            if (charVars.NumSwings > 0)
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float attackSpeedPerLevel = effect0[level - 1];
                float attackSpeedMod = charVars.NumSwings * attackSpeedPerLevel;
                IncPercentAttackSpeedMod(owner, attackSpeedMod);
            }
        }
        public override void OnUpdateActions()
        {
            float tempTime = GetTime();
            float timeSinceLastHit = tempTime - charVars.LastHitTime;
            if (timeSinceLastHit > 2.5f)
            {
                charVars.NumSwings = 0;
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            charVars.LastHitTime = GetTime();
            if (target is ObjAIBase && target is not BaseTurret)
            {
            }
            AddBuff((ObjAIBase)owner, owner, new Buffs.ArmsmasterRelentlessCounter(), 10, 1, 2.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false);
            if (charVars.NumSwings <= 9)
            {
                charVars.NumSwings++;
            }
            if (hitResult != HitResult.HIT_Dodge && hitResult != HitResult.HIT_Miss && target is not BaseTurret)
            {
            }
            if (target is ObjAIBase)
            {
                AddBuff(attacker, attacker, new Buffs.RelentlessAssaultDebuff(), 8, 1, 2.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false);
                int count = GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.RelentlessAssaultDebuff));
                if (count >= 2)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RelentlessAssaultMarker(), 1, 1, 2.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
            }
        }
    }
}