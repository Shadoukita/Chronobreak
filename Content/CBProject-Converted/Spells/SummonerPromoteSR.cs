namespace Spells
{
    public class SummonerPromoteSR : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override bool CanCast()
        {
            bool returnValue = true;
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinPlayerBuff)) > 0)
            {
                if (ExecutePeriodically(0.25f, ref avatarVars.LastTimeExecutedPromote, true))
                {
                    avatarVars.CanCastPromote = false;
                    foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.NotAffectSelf, nameof(Buffs.OdinSuperMinion), true))
                    {
                        if (GetBuffCountFromCaster(unit, default, nameof(Buffs.SummonerOdinPromote)) == 0)
                        {
                            if (GetBuffCountFromCaster(unit, default, nameof(Buffs.OdinSuperMinion)) > 0)
                            {
                                avatarVars.CanCastPromote = true;
                            }
                        }
                    }
                }
            }
            else
            {
                if (ExecutePeriodically(0.25f, ref avatarVars.LastTimeExecutedPromote, true))
                {
                    avatarVars.CanCastPromote = false;
                    foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.NotAffectSelf, nameof(Buffs.PromoteMeBuff), true))
                    {
                        if (GetBuffCountFromCaster(unit, default, nameof(Buffs.SummonerPromoteSR)) == 0)
                        {
                            if (GetBuffCountFromCaster(unit, default, nameof(Buffs.PromoteMeBuff)) > 0)
                            {
                                avatarVars.CanCastPromote = true;
                            }
                        }
                    }
                }
            }
            returnValue = avatarVars.CanCastPromote;
            return returnValue;
        }
        public override float AdjustCooldown()
        {
            float baseCooldown = 180;
            if (avatarVars.SummonerCooldownBonus != 0)
            {
                float cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            if (avatarVars.PromoteCooldownBonus != 0)
            {
                baseCooldown -= avatarVars.PromoteCooldownBonus;
            }
            return baseCooldown;
        }
        public override void SelfExecute()
        {
            float nextBuffVars_BonusHealth = 0;
            float nextBuffVars_BonusArmor = 0;
            float nextBuffVars_TotalMR = 0;
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinPlayerBuff)) > 0)
            {
                float count = 0;
                int ownerLevel = GetLevel(owner);
                float bonusHealth = ownerLevel * 75;
                bonusHealth += 200;
                nextBuffVars_BonusHealth = bonusHealth;
                foreach (AttackableUnit unit1 in GetClosestUnitsInArea(owner, owner.Position3D, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.NotAffectSelf, 1, nameof(Buffs.OdinSuperMinion), true))
                {
                    string skinName;
                    if (GetBuffCountFromCaster(unit1, default, nameof(Buffs.SummonerOdinPromote)) > 0)
                    {
                        foreach (AttackableUnit unit2 in GetClosestUnitsInArea(owner, owner.Position3D, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.NotAffectSelf, 2, nameof(Buffs.OdinSuperMinion), true))
                        {
                            if (GetBuffCountFromCaster(unit2, default, nameof(Buffs.SummonerOdinPromote)) > 0)
                            {
                                count++;
                                if (count >= 2)
                                {
                                    foreach (AttackableUnit unit3 in GetClosestUnitsInArea(owner, owner.Position3D, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.NotAffectSelf, 3, nameof(Buffs.OdinSuperMinion), true))
                                    {
                                        if (GetBuffCountFromCaster(unit3, default, nameof(Buffs.SummonerOdinPromote)) > 0)
                                        {
                                            count = Math.Max(count, 0);
                                            count++;
                                            if (count >= 3)
                                            {
                                                foreach (AttackableUnit unit4 in GetClosestUnitsInArea(owner, owner.Position3D, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.NotAffectSelf, 4, nameof(Buffs.OdinSuperMinion), true))
                                                {
                                                    if (GetBuffCountFromCaster(unit4, default, nameof(Buffs.SummonerOdinPromote)) == 0)
                                                    {
                                                        skinName = GetUnitSkinName(unit4);
                                                        if (skinName == "OdinBlueSuperminion")
                                                        {
                                                            SpellEffectCreate(out _, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, unit4, default, default, false, false, false, false, false);
                                                            AddBuff(attacker, unit4, new Buffs.SummonerPromoteSR(nextBuffVars_TotalMR, nextBuffVars_BonusArmor, nextBuffVars_BonusHealth), 1, 1, 3600, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                                                            IncHealth(unit4, 10000, unit4);
                                                        }
                                                        else if (skinName == "OdinRedSuperminion")
                                                        {
                                                            SpellEffectCreate(out _, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, unit4, default, default, false, false, false, false, false);
                                                            AddBuff(attacker, unit4, new Buffs.SummonerPromoteSR(nextBuffVars_TotalMR, nextBuffVars_BonusArmor, nextBuffVars_BonusHealth), 1, 1, 3600, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                                                            IncHealth(unit4, 10000, unit4);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        if (GetBuffCountFromCaster(unit3, default, nameof(Buffs.SummonerOdinPromote)) == 0)
                                        {
                                            skinName = GetUnitSkinName(unit3);
                                            if (skinName == "OdinBlueSuperminion")
                                            {
                                                SpellEffectCreate(out _, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, unit3, default, default, false, false, false, false, false);
                                                AddBuff(attacker, unit3, new Buffs.SummonerPromoteSR(nextBuffVars_TotalMR, nextBuffVars_BonusArmor, nextBuffVars_BonusHealth), 1, 1, 3600, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                                                IncHealth(unit3, 10000, unit3);
                                            }
                                            else if (skinName == "OdinRedSuperminion")
                                            {
                                                SpellEffectCreate(out _, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, unit3, default, default, false, false, false, false, false);
                                                AddBuff(attacker, unit3, new Buffs.SummonerPromoteSR(nextBuffVars_TotalMR, nextBuffVars_BonusArmor, nextBuffVars_BonusHealth), 1, 1, 3600, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                                                IncHealth(unit3, 10000, unit3);
                                            }
                                        }
                                    }
                                }
                            }
                            if (GetBuffCountFromCaster(unit2, default, nameof(Buffs.SummonerOdinPromote)) == 0)
                            {
                                skinName = GetUnitSkinName(unit2);
                                if (skinName == "OdinBlueSuperminion")
                                {
                                    SpellEffectCreate(out _, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, unit2, default, default, false, false, false, false, false);
                                    AddBuff(attacker, unit2, new Buffs.SummonerPromoteSR(nextBuffVars_TotalMR, nextBuffVars_BonusArmor, nextBuffVars_BonusHealth), 1, 1, 3600, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                                    IncHealth(unit2, 10000, unit2);
                                }
                                else if (skinName == "OdinRedSuperminion")
                                {
                                    SpellEffectCreate(out _, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, unit2, default, default, false, false, false, false, false);
                                    AddBuff(attacker, unit2, new Buffs.SummonerPromoteSR(nextBuffVars_TotalMR, nextBuffVars_BonusArmor, nextBuffVars_BonusHealth), 1, 1, 3600, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                                    IncHealth(unit2, 10000, unit2);
                                }
                            }
                        }
                    }
                    if (GetBuffCountFromCaster(unit1, default, nameof(Buffs.SummonerOdinPromote)) == 0)
                    {
                        skinName = GetUnitSkinName(unit1);
                        if (skinName == "OdinBlueSuperminion")
                        {
                            SpellEffectCreate(out _, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, unit1, default, default, false, false, false, false, false);
                            AddBuff(attacker, unit1, new Buffs.SummonerPromoteSR(nextBuffVars_TotalMR, nextBuffVars_BonusArmor, nextBuffVars_BonusHealth), 1, 1, 3600, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                            IncHealth(unit1, 10000, unit1);
                        }
                        else if (skinName == "OdinRedSuperminion")
                        {
                            SpellEffectCreate(out _, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, unit1, default, default, false, false, false, false, false);
                            AddBuff(attacker, unit1, new Buffs.SummonerPromoteSR(nextBuffVars_TotalMR, nextBuffVars_BonusArmor, nextBuffVars_BonusHealth), 1, 1, 3600, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                            IncHealth(unit1, 10000, unit1);
                        }
                    }
                }
            }
            else
            {
                int ownerLevel = GetLevel(owner);
                float bonusHealth = ownerLevel * 100;
                bonusHealth += 100;
                nextBuffVars_BonusHealth = bonusHealth;
                float bonusArmor = 5 * ownerLevel;
                bonusArmor += 20;
                nextBuffVars_BonusArmor = bonusArmor;
                float totalMR = 0.75f * ownerLevel;
                totalMR += 10;
                nextBuffVars_TotalMR = totalMR;
                foreach (AttackableUnit unit in GetClosestUnitsInArea(attacker, attacker.Position3D, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AlwaysSelf, 1, nameof(Buffs.PromoteMeBuff), true))
                {
                    AddBuff(attacker, unit, new Buffs.SummonerPromoteSR(nextBuffVars_TotalMR, nextBuffVars_BonusArmor, nextBuffVars_BonusHealth), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    SpellBuffRemove(unit, nameof(Buffs.PromoteBuff), owner, 0);
                }
            }
        }
        public override void UpdateTooltip(int spellSlot)
        {
            float baseCooldown = 180;
            if (avatarVars.SummonerCooldownBonus != 0)
            {
                float cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            SetSpellToolTipVar(baseCooldown, 1, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, attacker);
        }
    }
}
namespace Buffs
{
    public class SummonerPromoteSR : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "l_hand", "r_hand", "", "", },
            AutoBuffActivateEffect = new[] { "bloodboil_buf.troy", "bloodboil_buf.troy", },
            BuffName = "SummonerPromoteSR",
            BuffTextureName = "Summoner_PromoteSR.dds",
        };
        float totalMR;
        float bonusArmor;
        float bonusHealth;
        float lastTimeExecuted;
        public SummonerPromoteSR(float totalMR = default, float bonusArmor = default, float bonusHealth = default)
        {
            this.totalMR = totalMR;
            this.bonusArmor = bonusArmor;
            this.bonusHealth = bonusHealth;
        }
        public override void OnActivate()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.PromoteMeBuff)) > 0)
            {
                //RequireVar(this.totalMR);
                //RequireVar(this.bonusArmor);
                RedirectGold(owner, attacker);
                SpellEffectCreate(out _, out _, "Summoner_Flash.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position3D, target, default, default, false, false, false, false, false);
                SpellEffectCreate(out _, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
                //RequireVar(this.bonusHealth);
                IncPermanentPercentAttackSpeedMod(owner, 0.5f);
                IncPermanentFlatAttackRangeMod(owner, 75);
                IncPermanentFlatSpellBlockMod(owner, totalMR);
                IncPermanentFlatArmorMod(owner, bonusArmor);
                IncScaleSkinCoef(0.4f, owner);
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinSuperMinion)) > 0)
            {
                RedirectGold(owner, attacker);
                SpellEffectCreate(out _, out _, "Summoner_Flash.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position3D, target, default, default, false, false, false, false, false);
                SpellEffectCreate(out _, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
                //RequireVar(this.bonusHealth);
                TeamId ownerTeamID = GetTeamID_CS(attacker); // UNUSED
                IncPermanentPercentAttackSpeedMod(owner, 0.8f);
                IncPermanentFlatAttackRangeMod(owner, 75);
                IncPermanentPercentCooldownMod(owner, -1);
                IncPermanentFlatSpellBlockMod(owner, -10);
                IncScaleSkinCoef(0.7f, owner);
            }
        }
        public override void OnUpdateStats()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.PromoteMeBuff)) > 0)
            {
                IncFlatHPPoolMod(owner, bonusHealth);
                IncScaleSkinCoef(0.4f, owner);
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinSuperMinion)) > 0)
            {
                IncFlatHPPoolMod(owner, bonusHealth);
                IncScaleSkinCoef(0.7f, owner);
            }
        }
        public override void OnUpdateActions()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.PromoteMeBuff)) > 0 && ExecutePeriodically(5, ref lastTimeExecuted, false))
            {
                foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 850, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectTurrets, 1, nameof(Buffs.Taunt), false))
                {
                    ApplyTaunt(unit, owner, 10);
                }
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.PromoteMeBuff)) > 0)
            {
                SpellEffectCreate(out _, out _, "GemKnightBasicAttack_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, false, false, false, false);
            }
        }
    }
}