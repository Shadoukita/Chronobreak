namespace Spells
{
    public class UdyrTurtleAttack : SpellScript
    {
        float[] effect0 = { 0.1f, 0.12f, 0.14f, 0.16f, 0.18f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (target is ObjAIBase && target is not BaseTurret)
            {
                int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float nextBuffVars_DrainPercent = effect0[level - 1];
                float nextBuffVars_ManaDrainPercent = 0.5f * nextBuffVars_DrainPercent;
                SpellEffectCreate(out _, out _, "ItemLifesteal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                SpellEffectCreate(out _, out _, "globalhit_physical.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, false, false, false, false);
                AddBuff(attacker, attacker, new Buffs.GlobalDrainMana(nextBuffVars_DrainPercent, nextBuffVars_ManaDrainPercent), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            float baseDamage = GetBaseAttackDamage(owner);
            ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 1, 1, false, false, attacker);
        }
    }
}