namespace Spells
{
    public class UdyrBearAttack : SpellScript
    {
        int[] effect0 = { 5, 5, 5, 5, 5 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = base.level;
            float baseDamage = GetBaseAttackDamage(owner);
            ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 1, 1, false, false);
            if (target is ObjAIBase && target is not BaseTurret && GetBuffCountFromCaster(target, attacker, nameof(Buffs.UdyrBearStunCheck)) == 0)
            {
                AddBuff(attacker, target, new Buffs.UdyrBearStunCheck(), 1, 1, effect0[level - 1], BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
                BreakSpellShields(target);
                ApplyStun(attacker, target, 1);
                level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                SpellEffectCreate(out _, out _, "udyr_bear_slam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false);
            }
        }
    }
}