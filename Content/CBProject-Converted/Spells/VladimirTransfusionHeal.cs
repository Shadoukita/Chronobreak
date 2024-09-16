namespace Spells
{
    public class VladimirTransfusionHeal : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 0f, 0f, 0f, 0f, 0f, },
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            SpellFXOverrideSkins = new[] { "BloodkingVladimir", },
        };
        int[] effect0 = { 15, 25, 35, 45, 55 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float baseHeal = effect0[level - 1];
            float abilityPower = GetFlatMagicDamageMod(attacker);
            float abilityPowerMod = abilityPower * 0.25f;
            float totalHeal = abilityPowerMod + baseHeal;
            IncHealth(target, totalHeal, attacker);
            SpellEffectCreate(out _, out _, "VampHeal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, target, default, default, false, false, false, false, false);
        }
    }
}