namespace Spells
{
    public class EyeOfTheStorm : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 80, 120, 160, 200, 240 };
        int[] effect1 = { 14, 23, 32, 41, 50 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            PlayAnimation("Spell3", 0, owner, false, false, false);
            float abilityPower = GetFlatMagicDamageMod(attacker);
            float armorAmount = effect0[level - 1];
            abilityPower *= 0.9f;
            float totalArmorAmount = abilityPower + armorAmount;
            float nextBuffVars_TotalArmorAmount = totalArmorAmount;
            int nextBuffVars_DamageBonus = effect1[level - 1];
            AddBuff(attacker, target, new Buffs.EyeOfTheStorm(nextBuffVars_TotalArmorAmount), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff(attacker, target, new Buffs.JannaEoTSBuff(nextBuffVars_DamageBonus), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class EyeOfTheStorm : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            AutoBuffActivateEvent = "DeathsCaress_buf.troy",
            BuffName = "Eye Of The Storm",
            BuffTextureName = "Janna_EyeOfTheStorm.dds",
            OnPreDamagePriority = 3,
            DoOnPreDamageInExpirationOrder = true,
        };
        float totalArmorAmount;
        EffectEmitter particle;
        float oldArmorAmount;
        public EyeOfTheStorm(float totalArmorAmount = default)
        {
            this.totalArmorAmount = totalArmorAmount;
        }
        public override void OnActivate()
        {
            //RequireVar(this.totalArmorAmount);
            SetBuffToolTipVar(1, totalArmorAmount);
            ApplyAssistMarker(attacker, owner, 10);
            IncreaseShield(owner, totalArmorAmount, true, true);
            int attackerSkinID = GetSkinID(attacker);
            if (attackerSkinID == 3)
            {
                SpellEffectCreate(out particle, out _, "EyeoftheStorm_Frost_Ally_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out particle, out _, "EyeoftheStorm_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            if (totalArmorAmount > 0)
            {
                RemoveShield(owner, totalArmorAmount, true, true);
            }
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            oldArmorAmount = totalArmorAmount;
            if (totalArmorAmount >= damageAmount)
            {
                totalArmorAmount -= damageAmount;
                damageAmount = 0;
                oldArmorAmount -= totalArmorAmount;
                ReduceShield(owner, oldArmorAmount, true, true);
            }
            else
            {
                damageAmount -= totalArmorAmount;
                totalArmorAmount = 0;
                ReduceShield(owner, oldArmorAmount, true, true);
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}