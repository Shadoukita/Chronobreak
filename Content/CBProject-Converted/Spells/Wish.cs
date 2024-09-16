namespace Spells
{
    public class Wish : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 200, 320, 440 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(attacker);
            float spellPower = GetFlatMagicDamageMod(owner);
            float baseHealthToHeal = effect0[level - 1];
            float spellPowerBonus = spellPower * 0.7f;
            float healthToHeal = baseHealthToHeal + spellPowerBonus;
            IncHealth(target, healthToHeal, owner);
            float temp1 = GetHealthPercent(target, PrimaryAbilityResourceType.MANA);
            if (temp1 < 1)
            {
                SpellEffectCreate(out _, out _, "Wish_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                ApplyAssistMarker(owner, target, 10);
            }
        }
    }
}