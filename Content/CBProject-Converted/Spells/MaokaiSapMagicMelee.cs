namespace Buffs
{
    public class MaokaiSapMagicMelee : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "BUFFBONE_CSTM_SHIELDEYE_L", "BUFFBONE_CSTM_SHIELDEYE_R", },
            AutoBuffActivateEffect = new[] { "maokai_passive_indicator_left_eye.troy", "maokai_passive_indicator_right_eye.troy", },
            BuffName = "MaokaiSapMagicMelee",
            BuffTextureName = "Maokai_SapMagicReady.dds",
        };
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is ObjAIBase && target is not BaseTurret)
            {
                float healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
                if (healthPercent < 1)
                {
                    TeamId teamID = GetTeamID_CS(owner);
                    float maxHealth = GetMaxHealth(attacker, PrimaryAbilityResourceType.MANA);
                    int level = GetLevel(owner); // UNUSED
                    float regenPercent = 0.07f;
                    float healthToInc = maxHealth * regenPercent;
                    IncHealth(owner, healthToInc, owner);
                    SpellEffectCreate(out _, out _, "Maokai_Heal.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
                    SpellBuffClear(owner, nameof(Buffs.MaokaiSapMagicMelee));
                }
            }
        }
        public override void OnActivate()
        {
            OverrideAnimation("Attack", "Passive", owner);
            OverrideAnimation("Attack2", "Passive", owner);
            OverrideAnimation("Crit", "Passive", owner);
        }
        public override void OnDeactivate(bool expired)
        {
            ClearOverrideAnimation("Attack", owner);
            ClearOverrideAnimation("Attack2", owner);
            ClearOverrideAnimation("Crit", owner);
        }
    }
}