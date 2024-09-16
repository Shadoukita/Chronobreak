namespace Spells
{
    public class XenZhaoParryCast : SpellScript
    {
        int[] effect0 = { 150, 200, 250 };
        public override void SelfExecute()
        {
            float dtD = effect0[level - 1];
            //SpellEffectCreate(out a, out _, default, default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, unit, default, default, false, default, default, false);
            float weaponDmg = GetTotalAttackDamage(owner);
            float weaponDmgBonus = weaponDmg * 0.4f;
            float dtDReal = dtD + weaponDmgBonus;
            float nextBuffVars_Count = 0;
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 450, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                ApplyDamage(attacker, unit, dtDReal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
                if (unit is Champion)
                {
                    nextBuffVars_Count++;
                }
            }
        }
    }
}