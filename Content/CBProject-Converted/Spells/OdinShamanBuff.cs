namespace Spells
{
    public class OdinShamanBuff : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class OdinShamanBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "l_hand", "r_hand", },
            AutoBuffActivateEffect = new[] { "bloodboil_buf.troy", "bloodboil_buf.troy", },
            BuffName = "OdinShamanBuff",
            BuffTextureName = "Sona_SongofDiscordGold.dds",
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            SetScaleSkinCoef(1.15f, owner);
        }
        public override void OnUpdateStats()
        {
            SetScaleSkinCoef(1.15f, owner);
        }
        public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            string targetName = GetUnitSkinName(target);
            float damageMultiplier = 1.5f;
            if (targetName == "OdinChaosGuardian")
            {
                damageMultiplier = 1;
            }
            if (targetName == "OdinOrderGuardian")
            {
                damageMultiplier = 1;
            }
            if (targetName == "OdinNeutralGuardian")
            {
                damageMultiplier = 1;
            }
            damageAmount *= damageMultiplier;
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            damageAmount *= 0.5f;
        }
    }
}