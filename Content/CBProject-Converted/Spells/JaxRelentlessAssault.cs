namespace Spells
{
    public class JaxRelentlessAssault : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { 0.3f, 0.35f, 0.4f };
        int[] effect1 = { 6, 6, 6 };
        public override void SelfExecute()
        {
            float nextBuffVars_SpeedBoost = effect0[level - 1]; // UNUSED
            float duration = effect1[level - 1];
            AddBuff(attacker, owner, new Buffs.JaxRelentlessAssaultSpeed(), 1, 1, duration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class JaxRelentlessAssault : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "JaxRelentlessAssault",
            BuffTextureName = "Armsmaster_CoupDeGrace.dds",
            PersistsThroughDeath = true,
        };
        float[] effect0 = { 0.03f, 0.03f, 0.03f, 0.04f, 0.04f, 0.04f, 0.05f, 0.05f, 0.05f, 0.06f, 0.06f, 0.06f, 0.07f, 0.07f, 0.07f, 0.08f, 0.08f, 0.08f };
        int[] effect2 = { 20, 20, 20, 30, 30, 30, 40, 40, 40, 50, 50, 50, 60, 60, 60, 70, 70, 70 };
        public override void OnUpdateStats()
        {
            int count = GetBuffCountFromAll(owner, nameof(Buffs.JaxRelentlessAssaultAS));
            if (count > 0)
            {
                int level = GetLevel(owner);
                float aS = effect0[level - 1];
                aS *= count;
                IncPercentAttackSpeedMod(owner, aS);
            }
        }
        public override void OnUpdateActions()
        {
            int level = GetLevel(owner);
            float aS = effect0[level - 1];
            SetBuffToolTipVar(1, aS);
            float dmg = effect2[level - 1];
            SetBuffToolTipVar(2, dmg);
            float aP = GetFlatMagicDamageMod(owner);
            SetBuffToolTipVar(3, aP);
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (hitResult != HitResult.HIT_Dodge && hitResult != HitResult.HIT_Miss)
            {
                DebugSay(owner, "2");
                AddBuff((ObjAIBase)owner, owner, new Buffs.JaxRelentlessAssaultAS(), charVars.UltStacks, 1, 2.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                int ult = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (ult > 0)
                {
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.JaxRelentlessAssaultAS)) == 0)
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.JaxRelentlessAssaultDebuff(), 2, 2, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                        DebugSay(owner, "1");
                    }
                    int count = GetBuffCountFromAll(owner, nameof(Buffs.JaxRelentlessAssaultDebuff));
                    if (count < 2)
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.JaxRelentlessAssaultDebuff(), 2, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                        DebugSay(owner, "2");
                    }
                    else
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.JaxRelentlessAttack(), 1, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
                        SpellBuffClear(owner, nameof(Buffs.JaxRelentlessAssaultDebuff));
                        DebugSay(owner, "3");
                    }
                }
            }
        }
    }
}