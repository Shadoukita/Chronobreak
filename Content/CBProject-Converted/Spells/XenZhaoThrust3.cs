﻿namespace Spells
{
    public class XenZhaoThrust3 : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = { 15, 30, 45, 60, 75 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (hitResult == HitResult.HIT_Dodge)
            {
                hitResult = HitResult.HIT_Normal;
            }
            int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float attackDmg = GetTotalAttackDamage(owner);
            float combo1DamageLeet = effect0[level - 1];
            float combo1Damage = combo1DamageLeet + attackDmg;
            int count = GetBuffCountFromAll(owner, nameof(Buffs.XenZhaoComboCounter)); // UNUSED
            SpellEffectCreate(out _, out _, "xenZiou_ChainAttack_03.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false);
            if (hitResult == HitResult.HIT_Critical)
            {
                float comboDamageCrit = attackDmg * 2;
                combo1Damage = combo1DamageLeet + comboDamageCrit;
            }
            ApplyDamage(attacker, target, combo1Damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, true, attacker);
            SpellBuffRemove(attacker, nameof(Buffs.XenZhaoComboAutoFinish), attacker);
            BreakSpellShields(target);
            AddBuff(owner, target, new Buffs.XenZhaoKnockup(), 1, 1, 0.5f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.STUN, 0, true, false);
        }
    }
}