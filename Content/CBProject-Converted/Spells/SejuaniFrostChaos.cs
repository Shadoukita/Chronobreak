namespace Buffs
{
    public class SejuaniFrostChaos : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Sejuani_Frost.troy", },
            BuffName = "SejuaniFrost",
            BuffTextureName = "Sejuani_Frost.dds",
        };
        float movementSpeedMod;
        bool indicator;
        EffectEmitter overhead;
        public SejuaniFrostChaos(float movementSpeedMod = default)
        {
            this.movementSpeedMod = movementSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.movementSpeedMod);
            IncPercentMultiplicativeMovementSpeedMod(owner, movementSpeedMod);
            ApplyAssistMarker(attacker, owner, 10);
            indicator = false;
            if (owner is Champion)
            {
                indicator = true;
            }
            else if (owner is Pet p && p.IsClone) //TODO: Clone class?
            {
                indicator = true;
            }
            if (indicator)
            {
                SpellEffectCreate(out overhead, out _, "Sejuani_Frost_Overhead.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, true, owner, "spine", default, attacker, "Bird_head", default, false, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            if (indicator)
            {
                SpellEffectRemove(overhead);
            }
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, movementSpeedMod);
        }
    }
}