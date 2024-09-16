namespace ItemPassives
{
    public class ItemID_3124 : ItemScript
    {
        float lastTimeExecuted;
        float cooldownResevoir;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(2.85f, ref lastTimeExecuted, false))
            {
                if (cooldownResevoir < 2)
                {
                    cooldownResevoir++;
                }
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            if (!spellVars.DoesntTriggerSpellCasts && cooldownResevoir > 0)
            {
                if (cooldownResevoir == 2)
                {
                    lastTimeExecuted = GetTime();
                }
                AddBuff(owner, owner, new Buffs.Rageblade(), 8, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0);
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            AddBuff(owner, owner, new Buffs.Rageblade(), 8, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0);
        }
        public override void OnActivate()
        {
            cooldownResevoir = 2;
        }
        public override void OnDeactivate()
        {
            IncPermanentPercentAttackSpeedMod(owner, -0.04f);
            IncPermanentFlatMagicDamageMod(owner, -7);
        }
        public override void OnBeingDodged(ObjAIBase target)
        {
            AddBuff(owner, owner, new Buffs.Rageblade(), 8, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0);
        }
        public override void OnMiss(AttackableUnit target)
        {
            AddBuff(owner, owner, new Buffs.Rageblade(), 8, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0);
        }
    }
}
namespace Buffs
{
    public class _3124 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Rageblade",
            BuffTextureName = "139_Strygwyrs_Reaver.dds",
        };
        public override void OnActivate()
        {
            IncPermanentPercentAttackSpeedMod(owner, 0.04f);
            IncPermanentFlatMagicDamageMod(owner, 7);
        }
    }
}