namespace Buffs
{
    public class OdinCenterRelicBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "PotionofBrilliance_itm.troy", "", },
            BuffName = "OdinCenterRelic",
            BuffTextureName = "StormShield.dds",
            NonDispellable = true,
        };
        float totalArmorAmount;
        EffectEmitter buffParticle2;
        float lastTimeExecuted;
        float oldArmorAmount;
        public override void OnActivate()
        {
            int level = GetLevel(owner);
            float bonusShieldHP = level * 25;
            totalArmorAmount = bonusShieldHP + 100;
            IncreaseShield(owner, totalArmorAmount, true, true);
            SetBuffToolTipVar(1, totalArmorAmount);
            SpellEffectCreate(out buffParticle2, out _, "odin_center_relic.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinCenterRelicShieldCheck2(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            RemoveShield(owner, totalArmorAmount, true, true);
            SpellEffectRemove(buffParticle2);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinCenterRelicShieldCheck)) == 0)
                {
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinCenterRelicShieldCheck2)) == 0)
                    {
                        RemoveShield(owner, totalArmorAmount, true, true);
                        int level = GetLevel(owner);
                        float bonusShieldHP = level * 25;
                        totalArmorAmount = bonusShieldHP + 100;
                        IncreaseShield(owner, totalArmorAmount, true, true);
                        SetBuffToolTipVar(1, totalArmorAmount);
                        SpellEffectRemove(buffParticle2);
                        SpellEffectCreate(out buffParticle2, out _, "odin_center_relic.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                        AddBuff((ObjAIBase)owner, owner, new Buffs.OdinCenterRelicShieldCheck2(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
            }
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinCenterRelicShieldCheck(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            oldArmorAmount = totalArmorAmount;
            if (totalArmorAmount >= damageAmount)
            {
                totalArmorAmount -= damageAmount;
                damageAmount = 0;
                oldArmorAmount -= totalArmorAmount;
                ReduceShield(owner, oldArmorAmount, true, true);
            }
            else
            {
                damageAmount -= totalArmorAmount;
                totalArmorAmount = 0;
                ReduceShield(owner, oldArmorAmount, true, true);
                RemoveShield(owner, totalArmorAmount, true, true);
                SpellEffectRemove(buffParticle2);
            }
            SetBuffToolTipVar(1, totalArmorAmount);
        }
    }
}