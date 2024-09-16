namespace ItemPassives
{
    public class ItemID_3078 : ItemScript
    {
        int cooldownResevoir; // UNUSED
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            if (!spellVars.DoesntTriggerSpellCasts && GetBuffCountFromCaster(owner, owner, nameof(Buffs.SheenDelay)) == 0)
            {
                float baseDamage = GetBaseAttackDamage(owner);
                float nextBuffVars_BaseDamage = baseDamage;
                bool nextBuffVars_IsSheen = false;
                AddBuff(owner, owner, new Buffs.Sheen(nextBuffVars_BaseDamage, nextBuffVars_IsSheen), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (hitResult != HitResult.HIT_Dodge && hitResult != HitResult.HIT_Miss && target is ObjAIBase && RandomChance() < 0.25f && target is not BaseTurret)
            {
                AddBuff((ObjAIBase)target, target, new Buffs.Internal_35Slow(), 1, 1, 2.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
                AddBuff(owner, target, new Buffs.ItemSlow(), 1, 1, 2.5f, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false);
            }
        }
        public override void OnActivate()
        {
            cooldownResevoir = 0;
        }
    }
}
namespace Buffs
{
    public class _3078 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Global_Slow.troy", },
            BuffName = "Hamstring",
            BuffTextureName = "3078_Trinity_Force.dds",
        };
    }
}