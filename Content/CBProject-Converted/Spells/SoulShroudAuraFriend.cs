namespace Buffs
{
    public class SoulShroudAuraFriend : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Soul Shroud Aura Friend",
            BuffTextureName = "3063_Soul_Shroud.dds",
        };
        float manaRegenMod;
        float cooldownReduction;
        EffectEmitter apocalypseParticle;
        public SoulShroudAuraFriend(float manaRegenMod = default, float cooldownReduction = default)
        {
            this.manaRegenMod = manaRegenMod;
            this.cooldownReduction = cooldownReduction;
        }
        public override void OnActivate()
        {
            //RequireVar(this.cooldownReduction);
            //RequireVar(this.manaRegenMod);
            SpellEffectCreate(out apocalypseParticle, out _, "ZettasManaManipulator_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, true, owner, default, default, target, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(apocalypseParticle);
        }
        public override void OnUpdateStats()
        {
            IncPercentCooldownMod(owner, cooldownReduction);
            IncFlatPARRegenMod(owner, manaRegenMod, PrimaryAbilityResourceType.MANA);
        }
    }
}