namespace Spells
{
    public class SonaSongofDiscordAttackUpgrade : SpellScript
    {
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_MoveSpeedMod;
            int levelDamage = GetLevel(owner);
            float bonusDamage = levelDamage * 9;
            float totalDamage = bonusDamage + 14;
            float nextBuffVars_TotalDamage = totalDamage;
            float attackDamage = GetTotalAttackDamage(owner);
            ApplyDamage(owner, target, attackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
            AddBuff(attacker, attacker, new Buffs.IfHasBuffCheck(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            if (target is ObjAIBase && target is not BaseTurret)
            {
                BreakSpellShields(target);
                nextBuffVars_MoveSpeedMod = -0.4f;
                AddBuff(owner, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 1, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
            }
            AddBuff(attacker, target, new Buffs.SonaSongPCDeathRecapFix(nextBuffVars_TotalDamage), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellBuffRemove(owner, nameof(Buffs.SonaPowerChord), owner, 0);
        }
    }
}
namespace Buffs
{
    public class SonaSongofDiscordAttackUpgrade : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            IsDeathRecapSource = true,
        };
    }
}