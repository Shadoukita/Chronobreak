namespace Buffs
{
    public class MalphiteShieldRemoval : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        EffectEmitter sEPar;
        public override void OnActivate()
        {
            SpellEffectCreate(out sEPar, out _, "Obduracy_off.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, target, "root", default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(sEPar);
        }
    }
}