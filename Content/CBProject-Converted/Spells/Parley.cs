namespace Spells
{
    public class Parley : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = { 4, 5, 6, 7, 8 };
        int[] effect1 = { 20, 45, 70, 95, 120 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float critChance = GetFlatCritChanceMod(attacker);
            if (RandomChance() < critChance)
            {
                hitResult = HitResult.HIT_Critical;
            }
            else
            {
                hitResult = HitResult.HIT_Normal;
            }
            BreakSpellShields(target);
            int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float nextBuffVars_GoldGain = effect0[level - 1];
            AddBuff(attacker, target, new Buffs.Parley(nextBuffVars_GoldGain), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            float parBaseDamage = effect1[level - 1];
            float baseDamage = GetBaseAttackDamage(owner);
            baseDamage *= 1;
            float damageVar = parBaseDamage + baseDamage;
            ApplyDamage(attacker, target, damageVar, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
        }
    }
}
namespace Buffs
{
    public class Parley : BuffScript
    {
        float goldGain;
        public Parley(float goldGain = default)
        {
            this.goldGain = goldGain;
        }
        public override void OnActivate()
        {
            //RequireVar(this.goldGain);
        }
        public override void OnUpdateActions()
        {
            SpellBuffRemoveCurrent(owner);
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            ObjAIBase caster = GetBuffCasterUnit();
            if (!IsDead(attacker) && attacker == caster)
            {
                IncGold(attacker, goldGain);
            }
        }
    }
}