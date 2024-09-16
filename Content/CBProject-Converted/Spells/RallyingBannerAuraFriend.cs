namespace Buffs
{
    public class RallyingBannerAuraFriend : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Stark's Fervor Aura",
            BuffTextureName = "3050_Rallying_Banner.dds",
        };
        float lifeStealMod;
        float attackSpeedMod;
        float healthRegenMod;
        EffectEmitter starkAuraParticle;
        public RallyingBannerAuraFriend(float lifeStealMod = default, float attackSpeedMod = default, float healthRegenMod = default)
        {
            this.lifeStealMod = lifeStealMod;
            this.attackSpeedMod = attackSpeedMod;
            this.healthRegenMod = healthRegenMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.lifeStealMod);
            //RequireVar(this.attackSpeedMod);
            //RequireVar(this.healthRegenMod);
            SpellEffectCreate(out starkAuraParticle, out _, "RallyingBanner_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, true, target, default, default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(starkAuraParticle);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, attackSpeedMod);
            IncPercentLifeStealMod(owner, lifeStealMod);
            IncFlatHPRegenMod(owner, healthRegenMod);
        }
    }
}