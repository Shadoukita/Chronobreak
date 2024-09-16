namespace CharScripts
{
    public class CharScriptNidalee_Cougar : CharScript
    {
        float lastTimeExecuted;
        int[] effect0 = { 0, 0, 0 };
        int[] effect1 = { 300, 550, 800 };
        int[] effect2 = { 40, 42, 45, 47, 50, 52, 55, 57, 60, 62, 65, 67, 70, 72, 75, 77, 80, 82 };
        public override void OnUpdateActions()
        {
            int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level >= 1)
            {
                AddBuff(owner, owner, new Buffs.VorpalSpikes(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0);
            }
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                int count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.Feast));
                level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level >= 1)
                {
                    float cooldown = GetSlotSpellCooldownTime(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if (cooldown <= 0)
                    {
                        foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes))
                        {
                            count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.Feast));
                            float healthPerStack = effect0[level - 1];
                            float feastBase = effect1[level - 1];
                            float bonusFeastHealth = healthPerStack * count;
                            float feastHealth = bonusFeastHealth + feastBase;
                            float targetHealth = GetHealth(unit, PrimaryAbilityResourceType.MANA);
                            if (feastHealth >= targetHealth)
                            {
                                AddBuff(owner, unit, new Buffs.FeastMarker(), 1, 1, 1.1f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
                            }
                        }
                    }
                }
            }
        }
        public override void SetVarsByLevel()
        {
            charVars.HealAmount = effect2[level - 1];
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level >= 1)
            {
                Vector3 missileEndPosition = GetPointByUnitFacingOffset(owner, 550, 0);
                SpellCast(owner, target, missileEndPosition, default, 0, SpellSlotType.ExtraSlots, level, true, true, false);
            }
        }
        public override void OnKill(AttackableUnit target)
        {
            IncHealth(owner, charVars.HealAmount, owner);
            SpellEffectCreate(out _, out _, "EternalThirst_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.Carnivore(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
            SealSpellSlot(2, SpellSlotType.SpellSlots, owner, true);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false);
            TeamId teamID = GetTeamID_CS(owner);
            foreach (Champion unit in GetChampions(teamID))
            {
                if (owner != unit)
                {
                    IncPermanentFlatGoldPer10Mod(unit, 4);
                    IncPermanentPercentEXPBonus(unit, 0.04f);
                }
            }
            SetDisableAmbientGold(owner, true);
        }
        public override void OnReconnect()
        {
            TeamId teamID = GetTeamID_CS(owner);
            foreach (Champion unit in GetChampions(teamID))
            {
                if (owner != unit)
                {
                    IncPermanentFlatGoldPer10Mod(unit, -4);
                    IncPermanentPercentEXPBonus(unit, -0.04f);
                }
            }
            SetDisableAmbientGold(owner, false);
        }
    }
}