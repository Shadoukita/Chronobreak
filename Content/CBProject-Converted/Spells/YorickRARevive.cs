namespace Spells
{
    public class YorickRARevive : SpellScript
    {
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            Vector3 pos = GetUnitPosition(target);
            Pet other1 = CloneUnitPet(target, nameof(Buffs.YorickRARevive), 0, pos, 0, 0, false);
            float temp1 = GetMaxHealth(other1, PrimaryAbilityResourceType.MANA);
            IncHealth(other1, temp1, other1);
            AddBuff(owner, other1, new Buffs.YorickRAPetBuff2(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff(other1, owner, new Buffs.YorickRARemovePet(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class YorickRARevive : BuffScript
    {
    }
}