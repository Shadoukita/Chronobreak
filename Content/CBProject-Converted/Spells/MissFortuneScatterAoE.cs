namespace Buffs
{
    public class MissFortuneScatterAoE : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
        };
        int rainCount; // UNUSED
        float damage;
        float totalDamage;
        float moveSpeedMod;
        int attackSpeedMod;
        float lastTimeExecuted;
        float[] effect0 = { -0.2f, -0.25f, -0.3f, -0.35f, -0.4f };
        int[] effect1 = { 0, 0, 0, 0, 0 };
        public MissFortuneScatterAoE(float damage = default)
        {
            this.damage = damage;
        }
        public override void OnActivate()
        {
            rainCount = 1;
            int level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            //RequireVar(this.damage);
            totalDamage = damage / 7;
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            TeamId teamOfOwner = GetTeamID_CS(owner); // UNUSED
            moveSpeedMod = effect0[level - 1];
            attackSpeedMod = effect1[level - 1];
            float nextBuffVars_MoveSpeedMod = moveSpeedMod;
            float nextBuffVars_AttackSpeedMod = moveSpeedMod;
            level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                AddBuff(attacker, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 1, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false);
                ApplyDamage(attacker, unit, totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.114f, 1, false, false, attacker);
                SpellEffectCreate(out _, out _, "missFortune_makeItRain_unit_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 1000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnUpdateActions()
        {
            int level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots); // UNUSED
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, false))
            {
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    float nextBuffVars_MoveSpeedMod = moveSpeedMod;
                    float nextBuffVars_AttackSpeedMod = attackSpeedMod;
                    AddBuff(attacker, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 1, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false);
                    ApplyDamage(attacker, unit, totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.114f, 1, false, false, attacker);
                    SpellEffectCreate(out _, out _, "missFortune_makeItRain_unit_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true);
                }
            }
        }
    }
}