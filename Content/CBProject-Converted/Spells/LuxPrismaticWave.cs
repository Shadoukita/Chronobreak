namespace Spells
{
    public class LuxPrismaticWave : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = { 80, 105, 130, 155, 180 };
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            FaceDirection(owner, targetPos);
            if (distance > 1000)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 950, 0);
            }
            float baseDamageBlock = effect0[level - 1];
            float abilityPower = GetFlatMagicDamageMod(owner);
            float bonusHealth = abilityPower * 0.35f;
            float damageBlock = baseDamageBlock + bonusHealth;
            float nextBuffVars_DamageBlock = damageBlock;
            AddBuff(attacker, target, new Buffs.LuxPrismaticWaveShieldSelf(nextBuffVars_DamageBlock), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            SpellCast(owner, default, targetPos, targetPos, 3, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
        }
    }
}