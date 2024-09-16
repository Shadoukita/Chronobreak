namespace Buffs
{
    public class RedCard : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "",
        };
        bool willRemove;
        EffectEmitter effectID;
        int[] effect0 = { 30, 45, 60, 75, 90 };
        float[] effect1 = { -0.7f, -0.7f, -0.7f, -0.7f, -0.7f };
        int[] effect2 = { 0, 0, 0, 0, 0 };
        public override void OnActivate()
        {
            //RequireVar(this.willRemove);
            SpellEffectCreate(out effectID, out _, "Card_Red_Tag.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position3D, target, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(effectID);
            float baseCooldown = 4;
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = multiplier * baseCooldown;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
        }
        public override void OnUpdateActions()
        {
            if (willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float attackDamage = GetTotalAttackDamage(owner);
            float bonusDamage = effect0[level - 1];
            float redCardDamage = attackDamage + bonusDamage;
            if (target is ObjAIBase)
            {
                SpellEffectCreate(out _, out _, "Pulverize_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false);
            }
            float nextBuffVars_MoveSpeedMod = effect1[level - 1];
            float nextBuffVars_AttackSpeedMod = effect2[level - 1];
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position3D, 275, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                AddBuff((ObjAIBase)owner, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 2.5f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true);
                if (unit != target)
                {
                    DebugSay(owner, "YO!2");
                    ApplyDamage(attacker, unit, redCardDamage + effect2[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0.4f, 1, false, false);
                }
                else
                {
                    ApplyDamage(attacker, unit, bonusDamage + effect2[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0.4f, 1, false, false);
                    DebugSay(owner, "YO!");
                }
            }
            willRemove = true;
            damageAmount *= 0;
        }
    }
}