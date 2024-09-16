namespace Spells
{
    public class KennenShurikenStorm : SpellScript
    {
        int[] effect0 = { 80, 145, 210 };
        int[] effect1 = { 3, 4, 5 };
        public override void SelfExecute()
        {
            float nextBuffVars_BonusDamage = effect0[level - 1];
            AddBuff(owner, owner, new Buffs.KennenShurikenStorm(nextBuffVars_BonusDamage), 1, 1, effect1[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class KennenShurikenStorm : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", "", },
            AutoBuffActivateEffect = new[] { "", "", "", "", },
        };
        float bonusDamage;
        EffectEmitter particle;
        EffectEmitter particle2;
        int level;
        float lastTimeExecuted;
        public KennenShurikenStorm(float bonusDamage = default)
        {
            this.bonusDamage = bonusDamage;
        }
        public override void OnActivate()
        {
            //RequireVar(this.bonusDamage);
            TeamId teamOfOwner = GetTeamID_CS(owner);
            SpellEffectCreate(out particle, out particle2, "kennen_ss_aoe_green.troy", "kennen_ss_aoe_red.troy", teamOfOwner, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, owner.Position3D, owner, default, owner.Position3D, false, default, default, false, false);
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SpellEffectRemove(particle2);
        }
        public override void OnUpdateActions()
        {
            int count;
            if (level == 1)
            {
                if (ExecutePeriodically(0.5f, ref lastTimeExecuted, true))
                {
                    foreach (AttackableUnit unit in GetRandomUnitsInArea((ObjAIBase)owner, owner.Position3D, 550, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.KennenShurikenNOCAST), false))
                    {
                        count = GetBuffCountFromAll(unit, nameof(Buffs.KennenShurikenStormHolder));
                        if (count >= 2)
                        {
                            AddBuff(attacker, unit, new Buffs.KennenShurikenNOCAST(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        }
                        BreakSpellShields(unit);
                        AddBuff(attacker, unit, new Buffs.KennenMarkofStorm(), 3, 1, 8, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                        AddBuff((ObjAIBase)owner, unit, new Buffs.KennenShurikenStormMOS(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        ApplyDamage((ObjAIBase)owner, unit, bonusDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, attacker);
                        AddBuff(attacker, unit, new Buffs.KennenShurikenStormHolder(), 4, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                        AddBuff(attacker, unit, new Buffs.KennenShurikenNOCAST(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        SpellEffectCreate(out _, out _, "kennen_ss_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, unit, false, unit, "spine", default, unit, "spine", default, true, default, default, false, false);
                    }
                }
            }
            else if (level == 2)
            {
                if (ExecutePeriodically(0.4f, ref lastTimeExecuted, true))
                {
                    foreach (AttackableUnit unit in GetRandomUnitsInArea((ObjAIBase)owner, owner.Position3D, 550, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.KennenShurikenNOCAST), false))
                    {
                        count = GetBuffCountFromAll(unit, nameof(Buffs.KennenShurikenStormHolder));
                        if (count >= 2)
                        {
                            AddBuff(attacker, unit, new Buffs.KennenShurikenNOCAST(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        }
                        BreakSpellShields(unit);
                        AddBuff(attacker, unit, new Buffs.KennenMarkofStorm(), 3, 1, 8, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                        AddBuff((ObjAIBase)owner, unit, new Buffs.KennenShurikenStormMOS(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        ApplyDamage((ObjAIBase)owner, unit, bonusDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, attacker);
                        AddBuff(attacker, unit, new Buffs.KennenShurikenStormHolder(), 4, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                        AddBuff(attacker, unit, new Buffs.KennenShurikenNOCAST(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        SpellEffectCreate(out _, out _, "kennen_ss_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, unit, false, unit, "spine", default, unit, "spine", default, true, default, default, false, false);
                    }
                }
            }
            else if (level == 3)
            {
                if (ExecutePeriodically(0.33f, ref lastTimeExecuted, true))
                {
                    foreach (AttackableUnit unit in GetRandomUnitsInArea((ObjAIBase)owner, owner.Position3D, 550, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.KennenShurikenNOCAST), false))
                    {
                        count = GetBuffCountFromAll(unit, nameof(Buffs.KennenShurikenStormHolder));
                        if (count >= 2)
                        {
                            AddBuff(attacker, unit, new Buffs.KennenShurikenNOCAST(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        }
                        BreakSpellShields(unit);
                        AddBuff(attacker, unit, new Buffs.KennenMarkofStorm(), 3, 1, 8, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                        AddBuff((ObjAIBase)owner, unit, new Buffs.KennenShurikenStormMOS(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        ApplyDamage((ObjAIBase)owner, unit, bonusDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, attacker);
                        AddBuff(attacker, unit, new Buffs.KennenShurikenStormHolder(), 4, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                        AddBuff(attacker, unit, new Buffs.KennenShurikenNOCAST(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        SpellEffectCreate(out _, out _, "kennen_ss_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, unit, false, unit, "spine", default, unit, "spine", default, true, default, default, false, false);
                    }
                }
            }
        }
    }
}