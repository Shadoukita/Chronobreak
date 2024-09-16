namespace Spells
{
    public class GarenQAttack: GarenSlash2 {}
    public class GarenSlash2 : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { 2.5f, 2.5f, 2.5f, 2.5f, 2.5f };
        int[] effect1 = { 30, 45, 60, 75, 90 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float silenceDuration = effect0[level - 1];
            float bonusDamage = effect1[level - 1];
            float supremeDmg = GetTotalAttackDamage(owner);
            float scalingDamage = supremeDmg * 1.4f;
            float dealtDamage = scalingDamage + bonusDamage;
            hitResult = HitResult.HIT_Normal;
            if (target is ObjAIBase)
            {
                BreakSpellShields(target);
                ApplyDamage(attacker, target, dealtDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, true, attacker);
                if (target is not BaseTurret)
                {
                    ApplySilence(attacker, target, silenceDuration);
                }
            }
            else
            {
                ApplyDamage(attacker, target, dealtDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, true, attacker);
            }
        }
    }
}
namespace Buffs
{
    public class GarenSlash2 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "GarenSlash",
            BuffTextureName = "17.dds",
        };
    }
}