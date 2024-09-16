namespace Spells
{
    public class JaxRelentlessAttack : SpellScript
    {
        int[] effect0 = { 100, 160, 220 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (hitResult != HitResult.HIT_Dodge && hitResult != HitResult.HIT_Miss)
            {
                float baseAttackDamage = GetBaseAttackDamage(owner);
                ApplyDamage(attacker, target, baseAttackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
                if (target is not BaseTurret)
                {
                    if (target is ObjAIBase)
                    {
                        int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        ApplyDamage(owner, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.7f, 1, false, false, attacker);
                        SpellBuffRemove(owner, nameof(Buffs.JaxRelentlessAttack), owner, 0);
                        SpellEffectCreate(out _, out _, "RelentlessAssault_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class JaxRelentlessAttack : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "JaxRelentlessAttack",
            BuffTextureName = "BlindMonk_BlindingStrike.dds",
            IsDeathRecapSource = true,
        };
        public override void OnActivate()
        {
            OverrideAutoAttack(2, SpellSlotType.ExtraSlots, owner, 1, true);
        }
        public override void OnDeactivate(bool expired)
        {
            RemoveOverrideAutoAttack(owner, true);
        }
    }
}