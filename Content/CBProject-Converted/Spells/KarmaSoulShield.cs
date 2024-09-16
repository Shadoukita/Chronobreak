namespace Spells
{
    public class KarmaSoulShield : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 80, 120, 160, 200, 240, 280 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            AddBuff(owner, owner, new Buffs.KarmaSoulShieldAnim(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            float abilityPower = GetFlatMagicDamageMod(attacker);
            float armorAmount = effect0[level - 1];
            abilityPower *= 0.8f;
            float totalArmorAmount = abilityPower + armorAmount;
            float nextBuffVars_TotalArmorAmount = totalArmorAmount;
            AddBuff(attacker, target, new Buffs.KarmaSoulShield(nextBuffVars_TotalArmorAmount), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class KarmaSoulShield : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            AutoBuffActivateEvent = "DeathsCaress_buf.troy",
            BuffName = "KarmaSoulShield",
            BuffTextureName = "KarmaSoulShield.dds",
            OnPreDamagePriority = 3,
        };
        float totalArmorAmount;
        EffectEmitter particle;
        EffectEmitter soundParticle; // UNUSED
        float oldArmorAmount;
        public KarmaSoulShield(float totalArmorAmount = default)
        {
            this.totalArmorAmount = totalArmorAmount;
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            //RequireVar(this.totalArmorAmount);
            SpellEffectCreate(out particle, out _, "karma_soulShield_buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
            SpellEffectCreate(out soundParticle, out _, "KarmaSoulShieldSound.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true);
            SetBuffToolTipVar(1, totalArmorAmount);
            ApplyAssistMarker(attacker, owner, 10);
            IncreaseShield(owner, totalArmorAmount, true, true);
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