namespace Spells
{
    public class BlueCardAttack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
        };
        int[] effect0 = { 40, 60, 80, 100, 120 };
        public override void SelfExecute()
        {
            SpellBuffRemove(owner, nameof(Buffs.PickACard), owner);
            SpellBuffRemove(owner, nameof(Buffs.BlueCardPreattack), owner);
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(attacker);
            int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (target is ObjAIBase)
            {
                if (!IsDead(target))
                {
                    BreakSpellShields(target);
                    SpellEffectCreate(out _, out _, "PickaCard_blue_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                }
                float totalDamage = GetTotalAttackDamage(owner);
                float bonusDamage = effect0[level - 1];
                float damageToDeal = totalDamage + bonusDamage;
                AddBuff((ObjAIBase)target, owner, new Buffs.CardmasterBlueCardMana(), 1, 1, 0.1f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.4f, 1, false, false, attacker);
                ApplyDamage(attacker, target, 0, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
            }
            else
            {
                float baseDamage = GetBaseAttackDamage(attacker);
                ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
                SpellEffectCreate(out _, out _, "soraka_infuse_ally_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true);
            }
        }
    }
}