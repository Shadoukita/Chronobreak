namespace Spells
{
    public class Terrify : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
            SpellFXOverrideSkins = new[] { "SurprisePartyFiddlesticks", },
        };
        float[] effect0 = { 1, 1.5f, 2, 2.5f, 3 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            ApplyAssistMarker(attacker, target, 10);
            ApplyFear(attacker, target, effect0[level - 1]);
        }
    }
}