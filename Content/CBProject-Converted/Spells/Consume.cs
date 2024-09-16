namespace Spells
{
    public class Consume : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 35f, 30f, 25f, 20f, 15f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
            SpellVOOverrideSkins = new[] { "NunuBot", },
        };
        int[] effect0 = { 125, 180, 235, 290, 345 };
        float[] effect1 = { 200, 262.5f, 325, 387.5f, 450 };
        int[] effect2 = { 400, 525, 650, 775, 900 };
        public override void SelfExecute()
        {
            SpellEffectCreate(out _, out _, "Meditate_eff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false, false);
            float healthToInc = effect0[level - 1];
            float abilityPower = GetFlatMagicDamageMod(owner);
            abilityPower *= 1;
            healthToInc += abilityPower;
            IncHealth(owner, healthToInc, owner);
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (GetBuffCountFromCaster(target, target, nameof(Buffs.ResistantSkin)) > 0)
            {
                ApplyDamage(attacker, target, effect1[level - 1], DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 1, false, false, attacker);
            }
            else
            {
                ApplyDamage(attacker, target, effect2[level - 1], DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 1, false, false, attacker);
            }
        }
    }
}
namespace Buffs
{
    public class Consume : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Consume_buf.troy", },
            BuffName = "",
            BuffTextureName = "Yeti_Consume.dds",
        };
        float armorIncrease;
        public Consume(float armorIncrease = default)
        {
            this.armorIncrease = armorIncrease;
        }
        public override void UpdateBuffs()
        {
            IncFlatArmorMod(owner, armorIncrease);
        }
    }
}