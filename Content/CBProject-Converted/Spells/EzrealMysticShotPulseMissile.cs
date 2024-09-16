namespace Spells
{
    public class EzrealMysticShotPulseMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 35, 55, 75, 95, 115 };
        int[] effect1 = { 0, 0, 0, 0, 0 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float newCooldown;
            float newCooldown1;
            float newCooldown2;
            float newCooldown3;
            TeamId teamID = GetTeamID_CS(attacker);
            float cooldown = GetSlotSpellCooldownTime(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float cooldown1 = GetSlotSpellCooldownTime(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float cooldown2 = GetSlotSpellCooldownTime(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float cooldown3 = GetSlotSpellCooldownTime(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float spellBaseDamage = effect0[level - 1];
            float baseDamage = GetTotalAttackDamage(owner);
            float attackDamage = 1 * baseDamage;
            float damageVar = spellBaseDamage + attackDamage;
            float aP = GetFlatMagicDamageMod(owner);
            float finalAP = aP * 0.2f;
            float finalDamage = damageVar + finalAP;
            bool isStealthed = GetStealthed(target);
            if (!isStealthed)
            {
                if (cooldown > 0)
                {
                    newCooldown = cooldown - 1;
                    SetSlotSpellCooldownTimeVer2(newCooldown, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker, false);
                }
                if (cooldown1 > 0)
                {
                    newCooldown1 = cooldown1 - 1;
                    SetSlotSpellCooldownTimeVer2(newCooldown1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker, false);
                }
                if (cooldown2 > 0)
                {
                    newCooldown2 = cooldown2 - 1;
                    SetSlotSpellCooldownTimeVer2(newCooldown2, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker, false);
                }
                if (cooldown3 > 0)
                {
                    newCooldown3 = cooldown3 - 1;
                    SetSlotSpellCooldownTimeVer2(newCooldown3, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker, false);
                }
                BreakSpellShields(target);
                ApplyDamage(attacker, target, finalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
                SpellEffectCreate(out _, out _, "Ezreal_mysticshot_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                DestroyMissile(missileNetworkID);
                AddBuff(attacker, attacker, new Buffs.EzrealRisingSpellForce(), 5, 1, 6 + effect1[level - 1], BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            else
            {
                if (target is Champion)
                {
                    if (cooldown > 0)
                    {
                        newCooldown = cooldown - 1;
                        SetSlotSpellCooldownTimeVer2(newCooldown, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker, false);
                    }
                    if (cooldown1 > 0)
                    {
                        newCooldown1 = cooldown1 - 1;
                        SetSlotSpellCooldownTimeVer2(newCooldown1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker, false);
                    }
                    if (cooldown2 > 0)
                    {
                        newCooldown2 = cooldown2 - 1;
                        SetSlotSpellCooldownTimeVer2(newCooldown2, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker, false);
                    }
                    if (cooldown3 > 0)
                    {
                        newCooldown3 = cooldown3 - 1;
                        SetSlotSpellCooldownTimeVer2(newCooldown3, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker, false);
                    }
                    BreakSpellShields(target);
                    ApplyDamage(attacker, target, finalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
                    SpellEffectCreate(out _, out _, "Ezreal_mysticshot_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                    DestroyMissile(missileNetworkID);
                    AddBuff(attacker, attacker, new Buffs.EzrealRisingSpellForce(), 5, 1, 6 + effect1[level - 1], BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
                else
                {
                    bool canSee = CanSeeTarget(owner, target);
                    if (canSee)
                    {
                        if (cooldown > 0)
                        {
                            newCooldown = cooldown - 1;
                            SetSlotSpellCooldownTimeVer2(newCooldown, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker, false);
                        }
                        if (cooldown1 > 0)
                        {
                            newCooldown1 = cooldown1 - 1;
                            SetSlotSpellCooldownTimeVer2(newCooldown1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker, false);
                        }
                        if (cooldown2 > 0)
                        {
                            newCooldown2 = cooldown2 - 1;
                            SetSlotSpellCooldownTimeVer2(newCooldown2, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker, false);
                        }
                        if (cooldown3 > 0)
                        {
                            newCooldown3 = cooldown3 - 1;
                            SetSlotSpellCooldownTimeVer2(newCooldown3, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker, false);
                        }
                        BreakSpellShields(target);
                        ApplyDamage(attacker, target, finalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
                        SpellEffectCreate(out _, out _, "Ezreal_mysticshot_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                        DestroyMissile(missileNetworkID);
                        AddBuff(attacker, attacker, new Buffs.EzrealRisingSpellForce(), 5, 1, 6 + effect1[level - 1], BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class EzrealMysticShotPulseMissile : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "DarkBinding_tar.troy", "", },
            BuffName = "Dark Binding",
            BuffTextureName = "FallenAngel_DarkBinding.dds",
            PopupMessage = new[] { "game_floatingtext_Snared", },
        };
    }
}