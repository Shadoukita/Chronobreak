namespace Buffs
{
    public class LeonaZenithBladeBuffChaos : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "leBlanc_displace_AOE_tar.troy", },
            BuffName = "LeonaZenithBladeBuffChaos",
            PersistsThroughDeath = true,
        };
        EffectEmitter b;
        public override void OnActivate()
        {
            Vector3 ownerPos = GetUnitPosition(owner);
            SpellEffectCreate(out b, out _, "Leona_ZenithBlade_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, default, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(b);
        }
    }
}