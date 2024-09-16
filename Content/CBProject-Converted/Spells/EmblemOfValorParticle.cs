namespace Buffs
{
    public class EmblemOfValorParticle : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Emblem of Valour Aura",
            BuffTextureName = "3052_Reverb_Coil.dds",
        };
        EffectEmitter emblemOfValorParticle;
        public override void OnActivate()
        {
            SpellEffectCreate(out emblemOfValorParticle, out _, "RallyingBanner_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, true, owner, default, default, target, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(emblemOfValorParticle);
        }
        public override void OnUpdateStats()
        {
            IncFlatHPRegenMod(owner, 2);
        }
    }
}