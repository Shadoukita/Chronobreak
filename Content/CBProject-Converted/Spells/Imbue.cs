namespace Spells
{
    public class Imbue : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = { 60, 100, 140, 180, 220 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float baseHealAmount = effect0[level - 1];
            float aP = GetFlatMagicDamageMod(owner);
            float aPMod = 0.6f * aP;
            float healAmount = baseHealAmount + aPMod;
            if (target == owner)
            {
                healAmount *= 1.4f;
                IncHealth(owner, healAmount, owner);
            }
            else
            {
                IncHealth(owner, healAmount, owner);
                float temp1 = GetHealthPercent(target, PrimaryAbilityResourceType.MANA);
                if (temp1 < 1)
                {
                    IncHealth(target, healAmount, owner);
                    ApplyAssistMarker(attacker, target, 10);
                }
                SpellEffectCreate(out _, out _, "Global_Heal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
        }
    }
}