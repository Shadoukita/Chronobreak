namespace Spells
{
    public class VladimirTidesofBloodHeal : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        float[] effect0 = { 30, 57.5f, 85, 112.5f, 140 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float aPMod = GetFlatMagicDamageMod(attacker);
            aPMod *= 0.325f;
            IncHealth(target, aPMod + effect0[level - 1], attacker);
            SpellEffectCreate(out _, out _, "BriefHeal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, target, false, target, default, default, target, default, default, false);
            ApplyAssistMarker(attacker, target, 10);
        }
    }
}