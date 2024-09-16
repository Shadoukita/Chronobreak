namespace CharScripts
{
    public class Sheen : CharScript
    {
        public override void OnLaunchAttack(AttackableUnit target)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SheenDelay)) == 0)
            {
                AddBuff(owner, owner, new Buffs.SheenDelay(), 1, 1, 1.4f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
            }
        }
    }
}
namespace Buffs
{
    public class Sheen : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "l_hand", "r_hand", },
            AutoBuffActivateEffect = new[] { "enrage_buf.troy", "enrage_buf.troy", },
            BuffName = "Sheen",
            BuffTextureName = "3057_Sheen.dds",
        };
        float baseDamage;
        bool isSheen;
        public Sheen(float baseDamage = default, bool isSheen = default)
        {
            this.baseDamage = baseDamage;
            this.isSheen = isSheen;
        }
        public override void OnActivate()
        {
            //RequireVar(this.baseDamage);
            //RequireVar(this.isSheen);
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (hitResult != HitResult.HIT_Dodge && hitResult != HitResult.HIT_Miss)
            {
                float percentBase;
                if (!isSheen)
                {
                    percentBase = baseDamage * 1.5f;
                    damageAmount += percentBase;
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SheenDelay)) == 0)
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.SheenDelay(), 1, 1, 1.3f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                    }
                    SpellBuffClear(owner, nameof(Buffs.Sheen));
                    SpellBuffRemoveCurrent(owner);
                }
                if (isSheen)
                {
                    percentBase = baseDamage * 1;
                    damageAmount += percentBase;
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SheenDelay)) == 0)
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.SheenDelay(), 1, 1, 1.2f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                    }
                    SpellBuffClear(owner, nameof(Buffs.Sheen));
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}