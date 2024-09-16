namespace Buffs
{
    public class ExaltedWithBaronNashor : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Exalted with Baron Nashor",
            BuffTextureName = "Averdrian_AstralBeam.dds",
            NonDispellable = true,
        };
        float bonusAttack;
        EffectEmitter buffParticle;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            IncPermanentFlatPARRegenMod(owner, 3, PrimaryAbilityResourceType.MANA);
            float gameTime = GetGameTime();
            float bonusAttack = gameTime / 30;
            bonusAttack -= 15;
            bonusAttack = Math.Min(bonusAttack, 40);
            bonusAttack = Math.Max(bonusAttack, 20);
            this.bonusAttack = bonusAttack;
            IncPermanentFlatMagicDamageMod(owner, bonusAttack);
            IncPermanentFlatPhysicalDamageMod(owner, bonusAttack);
            SpellEffectCreate(out buffParticle, out _, "nashor_rune_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            IncPermanentFlatPARRegenMod(owner, -3, PrimaryAbilityResourceType.MANA);
            float bonusAttack = -1 * this.bonusAttack;
            IncPermanentFlatPhysicalDamageMod(owner, bonusAttack);
            IncPermanentFlatMagicDamageMod(owner, bonusAttack);
            SpellEffectRemove(buffParticle);
        }
        public override void OnUpdateStats()
        {
            if (ExecutePeriodically(5, ref lastTimeExecuted, false))
            {
                float health = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                float healthInc = health * 0.03f;
                IncHealth(owner, healthInc, owner);
                SpellEffectCreate(out _, out _, "InnervatingLocket_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            }
        }
    }
}