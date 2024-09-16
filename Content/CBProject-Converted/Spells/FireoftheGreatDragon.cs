namespace Buffs
{
    public class FireoftheGreatDragon : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "FireoftheGreatDragon",
            BuffTextureName = "Annie_Incinerate.dds",
        };
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is ObjAIBase && target is not BaseTurret)
            {
                AddBuff(attacker, target, new Buffs.Burning(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.DAMAGE, 0);
            }
        }
    }
}