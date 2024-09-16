namespace CharScripts
{
    public class LichBane : CharScript
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
    public class LichBane : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "l_hand", "r_hand", },
            AutoBuffActivateEffect = new[] { "bluehands_buf.troy", "bluehands_buf.troy", },
            BuffName = "LichBane",
            BuffTextureName = "126_Zeal_and_Sheen.dds",
        };
        float abilityPower;
        public LichBane(float abilityPower = default)
        {
            this.abilityPower = abilityPower;
        }
        public override void OnActivate()
        {
            //RequireVar(this.abilityPower);
        }
        public override void OnUpdateStats()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Sheen)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.Sheen), (ObjAIBase)owner);
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (hitResult != HitResult.HIT_Dodge && hitResult != HitResult.HIT_Miss)
            {
                damageAmount += abilityPower;
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SheenDelay)) == 0)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.SheenDelay(), 1, 1, 1.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
                SpellBuffClear(owner, nameof(Buffs.LichBane));
            }
        }
    }
}