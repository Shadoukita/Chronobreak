namespace Buffs
{
    public class ArmsmasterRelentlessMR : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "JaxRelentlessAssault_buf.troy", },
            BuffName = "RelentlessBarrier",
            BuffTextureName = "Armsmaster_RelentlessMR.dds",
        };
        public override void OnUpdateStats()
        {
            IncFlatSpellBlockMod(owner, charVars.BonusMR);
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (damageSource != DamageSource.DAMAGE_SOURCE_PERIODIC && damageType == DamageType.DAMAGE_TYPE_MAGICAL)
            {
                SpellEffectCreate(out _, out _, "JaxRelentlessAssaultShield_hit.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
            }
        }
    }
}