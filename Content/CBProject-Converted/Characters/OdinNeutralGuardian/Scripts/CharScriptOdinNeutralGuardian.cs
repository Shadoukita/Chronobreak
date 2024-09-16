namespace CharScripts
{
    public class CharScriptOdinNeutralGuardian : CharScript
    {
        public override void OnActivate()
        {
            SetImmovable(owner, true);
            SetDodgePiercing(owner, true);
            AddBuff(owner, owner, new Buffs.OdinGuardianBuff(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.OdinGuardianUI(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.TurretDamageManager(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 1, true, false, false);
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 25000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, false))
            {
                AddBuff((ObjAIBase)unit, unit, new Buffs.OdinPlayerBuff(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            }
            int nextBuffVars_HPPerLevel = 75; // UNUSED
            int nextBuffVars_DmgPerLevel = 15;
            int nextBuffVars_ArmorPerLevel = 4; // UNUSED
            int nextBuffVars_MR_per_level = 2; // UNUSED
            AddBuff(owner, owner, new Buffs.OdinGuardianStatsByLevel(nextBuffVars_DmgPerLevel), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}