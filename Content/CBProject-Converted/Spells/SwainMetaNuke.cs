namespace Spells
{
    public class SwainMetaNuke : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 0f, 0f, 0f, 0f, 0f, },
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 50, 70, 90 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_DrainPercent;
            int level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            bool nextBuffVars_DrainedBool = false;
            SpellEffectCreate(out _, out _, "swain_heal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, target, default, default, false, default, default, false, false);
            Vector3 targetPos = GetSpellTargetPos(spell);
            SpellCast(attacker, owner, attacker.Position3D, owner.Position3D, 2, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, targetPos);
            bool isTargetable = GetTargetable(attacker);
            if (target is Champion)
            {
                nextBuffVars_DrainPercent = 0.75f;
            }
            else
            {
                nextBuffVars_DrainPercent = 0.25f;
            }
            if (!isTargetable)
            {
                SpellEffectCreate(out _, out _, "swain_heal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, target, default, default, false, default, default, false, false);
            }
            AddBuff(owner, owner, new Buffs.GlobalDrain(nextBuffVars_DrainPercent, nextBuffVars_DrainedBool), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            ApplyDamage(attacker, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.2f, 1, false, false, attacker);
        }
    }
}