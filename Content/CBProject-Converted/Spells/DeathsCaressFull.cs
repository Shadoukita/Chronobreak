namespace Spells
{
    public class DeathsCaressFull : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 100, 150, 200, 250, 300 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float abilityPower = GetFlatMagicDamageMod(target);
            float armorAmount = effect0[level - 1];
            float bonusHealth = abilityPower * 0.9f;
            float totalArmorAmount = bonusHealth + armorAmount;
            float nextBuffVars_TotalArmorAmount = totalArmorAmount;
            float nextBuffVars_FinalArmorAmount = totalArmorAmount;
            float nextBuffVars_Ticktimer = 10;
            AddBuff(attacker, target, new Buffs.DeathsCaress(nextBuffVars_TotalArmorAmount, nextBuffVars_FinalArmorAmount, nextBuffVars_Ticktimer), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class DeathsCaressFull : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "DeathsCaress_buf.troy", },
            AutoBuffActivateEvent = "DeathsCaress_buf.prt",
            BuffName = "Death's Caress",
            BuffTextureName = "Sion_DeathsCaress.dds",
        };
    }
}