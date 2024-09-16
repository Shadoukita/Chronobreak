namespace Spells
{
    public class ForcePulse : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 80, 130, 180, 230, 280 };
        float[] effect1 = { -0.3f, -0.35f, -0.4f, -0.45f, -0.5f };
        public override void SelfExecute()
        {
            SpellBuffRemove(owner, nameof(Buffs.ForcePulseCanCast), owner, 0);
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            ApplyDamage(attacker, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 1, false, false, attacker);
            float nextBuffVars_MoveSpeedMod = effect1[level - 1];
            float nextBuffVars_AttackSpeedMod = 0;
            AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class ForcePulse : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "ForcePulse",
            BuffTextureName = "Kassadin_ForcePulse.dds",
        };
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            if (!spellVars.DoesntTriggerSpellCasts)
            {
                ObjAIBase attacker = GetBuffCasterUnit();
                if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.ForcePulseCanCast)) == 0)
                {
                    AddBuff(attacker, attacker, new Buffs.ForcePulseCounter(), 6, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
                }
            }
        }
    }
}