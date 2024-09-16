namespace Spells
{
    public class SonaAriaofPerseveranceMissile : SpellScript
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
        int[] effect0 = { 40, 60, 80, 100, 120 };
        int[] effect1 = { 8, 11, 14, 17, 20 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float aPMod = GetFlatMagicDamageMod(attacker);
            aPMod *= 0.25f;
            IncHealth(target, aPMod + effect0[level - 1], attacker);
            SpellEffectCreate(out _, out _, "Global_Heal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, target, false, target, default, default, target, default, default, false, false, false, false, false);
            ApplyAssistMarker(attacker, target, 10);
            float nextBuffVars_DefenseBonus = effect1[level - 1];
            AddBuff(attacker, target, new Buffs.SonaAriaShield(nextBuffVars_DefenseBonus), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}