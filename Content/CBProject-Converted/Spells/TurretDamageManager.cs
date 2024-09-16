namespace Buffs
{
    public class TurretDamageManager : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
        };
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is Champion)
            {
                int turretBuffCount = GetBuffCountFromCaster(owner, owner, nameof(Buffs.TurretDamageMarker));
                int targetBuffCount = GetBuffCountFromCaster(target, owner, nameof(Buffs.TurretDamageMarker));
                float buffCount = turretBuffCount + targetBuffCount;
                float damageBonus = 0.2f * buffCount;
                damageBonus++;
                damageAmount *= damageBonus;
                if (turretBuffCount >= 3)
                {
                    AddBuff((ObjAIBase)owner, target, new Buffs.TurretDamageMarker(), 3, 1, 3, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false);
                }
                AddBuff((ObjAIBase)owner, owner, new Buffs.TurretDamageMarker(), 3, 1, 3, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false);
            }
        }
    }
}