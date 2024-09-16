namespace Spells
{
    public class VolleyAttack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = { 40, 50, 60, 70, 80 };
        float[] effect1 = { -0.15f, -0.2f, -0.25f, -0.3f, -0.35f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int count = GetBuffCountFromCaster(target, target, nameof(Buffs.VolleyAttack));
            if (count == 0)
            {
                float nextBuffVars_MovementSpeedMod;
                float baseDamage;
                float bonusDamage;
                bool isStealthed = GetStealthed(target);
                if (!isStealthed)
                {
                    SpellEffectCreate(out _, out _, "bowmaster_BasicAttack_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, target.Position3D, target, default, default, false);
                    AddBuff((ObjAIBase)target, target, new Buffs.VolleyAttack(), 9, 1, 0.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false);
                    BreakSpellShields(target);
                    baseDamage = GetBaseAttackDamage(owner);
                    int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    bonusDamage = effect0[level - 1];
                    baseDamage += bonusDamage;
                    ApplyDamage(owner, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false);
                    level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if (level >= 1)
                    {
                        nextBuffVars_MovementSpeedMod = effect1[level - 1];
                        AddBuff(owner, target, new Buffs.FrostArrow(nextBuffVars_MovementSpeedMod), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false);
                    }
                    DestroyMissile(missileNetworkID);
                }
                else
                {
                    if (target is Champion)
                    {
                        SpellEffectCreate(out _, out _, "bowmaster_BasicAttack_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, target.Position3D, target, default, default, false);
                        AddBuff((ObjAIBase)target, target, new Buffs.VolleyAttack(), 9, 1, 0.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false);
                        BreakSpellShields(target);
                        baseDamage = GetBaseAttackDamage(owner);
                        int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        bonusDamage = effect0[level - 1];
                        baseDamage += bonusDamage;
                        ApplyDamage(owner, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false);
                        level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        if (level >= 1)
                        {
                            nextBuffVars_MovementSpeedMod = effect1[level - 1];
                            AddBuff(owner, target, new Buffs.FrostArrow(nextBuffVars_MovementSpeedMod), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false);
                        }
                        DestroyMissile(missileNetworkID);
                    }
                    else
                    {
                        bool canSee = CanSeeTarget(owner, target);
                        if (canSee)
                        {
                            SpellEffectCreate(out _, out _, "bowmaster_BasicAttack_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, target.Position3D, target, default, default, false);
                            AddBuff((ObjAIBase)target, target, new Buffs.VolleyAttack(), 9, 1, 0.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false);
                            BreakSpellShields(target);
                            baseDamage = GetBaseAttackDamage(owner);
                            int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                            bonusDamage = effect0[level - 1];
                            baseDamage += bonusDamage;
                            ApplyDamage(owner, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false);
                            level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                            if (level >= 1)
                            {
                                nextBuffVars_MovementSpeedMod = effect1[level - 1];
                                AddBuff(owner, target, new Buffs.FrostArrow(nextBuffVars_MovementSpeedMod), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false);
                            }
                            DestroyMissile(missileNetworkID);
                        }
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class VolleyAttack : BuffScript
    {
    }
}