namespace Buffs
{
    public class LeblancPassive : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "R_hand", null, "", },
            AutoBuffActivateEffect = new[] { "", "akali_twinDisciplines_AP_buf.troy", },
            BuffName = "LeblancNoxianCruelty",
            BuffTextureName = "LeblancMirrorImage.dds",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        public override void OnTakeDamage(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.LeblancPassiveCooldown)) == 0)
            {
                float healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
                if (healthPercent <= 0.4f)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.LeblancMIApplicator(), 1, 1, 0.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    AddBuff((ObjAIBase)owner, owner, new Buffs.LeblancPassiveCooldown(), 1, 1, 60, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                }
            }
        }
    }
}