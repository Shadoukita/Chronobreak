namespace CharScripts
{
    public class CharScriptSejuani : CharScript
    {
        float lastTimeExecuted;
        float mS; // UNUSED
        int[] effect0 = { 12, 20, 28, 36, 44 };
        float[] effect1 = { 0.01f, 0.0125f, 0.015f, 0.0175f, 0.02f };
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(5, ref lastTimeExecuted, true))
            {
                int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                level = Math.Max(level, 1);
                float damagePerTick = effect0[level - 1];
                float maxHPPercent = effect1[level - 1];
                float frostBonus = 1.5f;
                float temp1 = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                float percentDamage = temp1 * maxHPPercent;
                damagePerTick += percentDamage;
                float abilityPowerMod = GetFlatMagicDamageMod(owner);
                float abilityPowerBonus = abilityPowerMod * 0.1f;
                damagePerTick += abilityPowerBonus;
                float damagePerTickFrost = frostBonus * damagePerTick;
                SetSpellToolTipVar(percentDamage, 1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
                SetSpellToolTipVar(damagePerTickFrost, 2, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (hitResult != HitResult.HIT_Dodge && hitResult != HitResult.HIT_Miss && target is ObjAIBase)
            {
                SpellCast(owner, target, default, default, 1, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            if (!spellVars.DoesntTriggerSpellCasts)
            {
                int slot = GetSpellSlot(spell);
                if (slot == 2)
                {
                    float duration;
                    TeamId teamID = GetTeamID_CS(owner);
                    if (teamID == TeamId.TEAM_ORDER)
                    {
                        foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 900, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                        {
                            if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.SejuaniFrost)) > 0)
                            {
                                duration = GetBuffRemainingDuration(unit, nameof(Buffs.SejuaniFrost));
                                duration += 0.5f;
                                SpellBuffRenew(unit, nameof(Buffs.SejuaniFrost), duration);
                            }
                            if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.SejuaniFrostResist)) > 0)
                            {
                                duration = GetBuffRemainingDuration(unit, nameof(Buffs.SejuaniFrostResist));
                                duration += 0.5f;
                                SpellBuffRenew(unit, nameof(Buffs.SejuaniFrostResist), duration);
                            }
                        }
                    }
                    else
                    {
                        foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 900, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                        {
                            if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.SejuaniFrostChaos)) > 0)
                            {
                                duration = GetBuffRemainingDuration(unit, nameof(Buffs.SejuaniFrostChaos));
                                duration += 0.5f;
                                SpellBuffRenew(unit, nameof(Buffs.SejuaniFrostChaos), duration);
                            }
                            if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.SejuaniFrostResistChaos)) > 0)
                            {
                                duration = GetBuffRemainingDuration(unit, nameof(Buffs.SejuaniFrostResistChaos));
                                duration += 0.5f;
                                SpellBuffRenew(unit, nameof(Buffs.SejuaniFrostResistChaos), duration);
                            }
                        }
                    }
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            charVars.FrostDuration = 3;
            mS = GetMovementSpeed(owner);
            AddBuff(owner, owner, new Buffs.SejuaniRunSpeed(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnResurrect()
        {
            SealSpellSlot(2, SpellSlotType.SpellSlots, owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            UnlockAnimation(owner, true);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}