namespace Spells
{
    public class GatlingGunAttack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            int nextBuffVars_SpellLevel = level; // UNUSED
            float baseDamage = GetBaseAttackDamage(owner);
            baseDamage *= 0.4f;
            AddBuff(attacker, target, new Buffs.GatlingDebuff(), 10, 1, 1, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_DEHANCER, 0);
            if (GetBuffCountFromCaster(target, owner, nameof(Buffs.GatlingDebuffCheck)) == 0)
            {
                ApplyDamage(owner, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0);
                AddBuff(attacker, target, new Buffs.GatlingDebuffCheck(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                DestroyMissile(missileNetworkID);
            }
        }
    }
}