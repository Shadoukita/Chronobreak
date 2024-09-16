namespace Spells
{
    public class MissileBarrage : SpellScript
    {
        public override bool CanCast()
        {
            int count = GetBuffCountFromAll(owner, nameof(Buffs.MissileBarrage));
            return count > 1;
        }
        public override void SelfExecute()
        {
            Vector3 pos = GetSpellTargetPos(spell);
            int count = GetBuffCountFromAll(owner, nameof(Buffs.MissileBarrage));
            if (count >= 8)
            {
                SpellBuffRemove(owner, nameof(Buffs.MissileBarrage), owner, charVars.ChargeCooldown);
            }
            else
            {
                SpellBuffRemove(owner, default, owner, 0);
            }
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(ownerPos, pos);
            FaceDirection(owner, pos);
            if (distance > 1200)
            {
                pos = GetPointByUnitFacingOffset(owner, 1150, 0);
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.MBCheck2)) > 0)
            {
                SpellCast(owner, default, pos, pos, 2, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
                SpellBuffRemove(owner, nameof(Buffs.MBCheck2), owner, 0);
            }
            else
            {
                SpellCast(owner, default, pos, pos, 1, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
            }
            int barrageCount = GetBuffCountFromAll(owner, nameof(Buffs.CorkiMissileBarrageNC));
            if (barrageCount == 3)
            {
                AddBuff(owner, owner, new Buffs.MBCheck2(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                SpellBuffRemoveStacks(owner, owner, nameof(Buffs.CorkiMissileBarrageNC), 3);
            }
            else
            {
                AddBuff(owner, owner, new Buffs.CorkiMissileBarrageNC(), 3, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class MissileBarrage : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MissileBarrageCheck",
            BuffTextureName = "Corki_MissileBarrage.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnUpdateAmmo()
        {
            int count = GetBuffCountFromAll(owner, nameof(Buffs.MissileBarrage));
            if (count == 7)
            {
                AddBuff(attacker, owner, new Buffs.MissileBarrage(), 8, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
            }
            else
            {
                AddBuff(attacker, owner, new Buffs.MissileBarrage(), 8, 1, charVars.ChargeCooldown, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
            }
        }
    }
}