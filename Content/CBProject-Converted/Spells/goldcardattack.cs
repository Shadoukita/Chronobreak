namespace Spells
{
    public class Goldcardattack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
        };
        float[] effect0 = { 15, 22.5f, 30, 37.5f, 45 };
        float[] effect1 = { 1, 1.25f, 1.5f, 1.75f, 2 };
        public override void SelfExecute()
        {
            SpellBuffRemove(owner, nameof(Buffs.GoldCardPreAttack), owner);
            SpellBuffRemove(owner, nameof(Buffs.PickACard), owner);
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(attacker);
            if (target is ObjAIBase)
            {
                BreakSpellShields(target);
                int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float totalDamage = GetTotalAttackDamage(owner);
                float bonusDamage = effect0[level - 1];
                float goldCardDamage = bonusDamage + totalDamage;
                ApplyDamage(attacker, target, 0, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
                ApplyDamage(attacker, target, goldCardDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.4f, 1, false, false, attacker);
                SpellEffectCreate(out _, out _, "PickaCard_yellow_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                if (target is not BaseTurret)
                {
                    ApplyStun(attacker, target, effect1[level - 1]);
                }
            }
            else
            {
                float baseDamage = GetBaseAttackDamage(attacker);
                ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
                Vector3 targetPosition = GetSpellTargetPos(spell);
                SpellEffectCreate(out _, out _, "PickaCard_yellow_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPosition, default, default, default, true);
            }
        }
    }
}