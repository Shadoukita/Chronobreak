namespace Buffs
{
    public class AbsoluteZeroBonusDamage : BuffScript
    {
        public override void OnUpdateActions()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.AbsoluteZeroBonusDamage2)) > 0)
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float baseDamage = level * 250;
                float secondDamage = baseDamage + 250;
                float totalTime = 0.25f * lifeTime;
                SpellEffectCreate(out _, out _, "AbsoluteZero_nova.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 650, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes))
                {
                    ApplyDamage(attacker, unit, secondDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, totalTime, 3);
                }
                SpellBuffRemove(owner, nameof(Buffs.AbsoluteZeroBonusDamage), (ObjAIBase)owner);
            }
        }
    }
}