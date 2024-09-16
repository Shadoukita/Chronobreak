namespace CharScripts
{
    public class CharScriptEzreal : CharScript
    {
        float lastTime2Executed;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTime2Executed, true))
            {
                int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float totalDamage = GetTotalAttackDamage(owner);
                float baseDamage = GetBaseAttackDamage(owner);
                float bonusDamage = totalDamage - baseDamage;
                float spell3Display = bonusDamage * 1;
                SetSpellToolTipVar(spell3Display, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
                float aP = GetFlatMagicDamageMod(owner);
                float finalAP = aP * 0.2f;
                //totalDamage = GetTotalAttackDamage(owner);
                float attackDamage = 1 * totalDamage;
                SetSpellToolTipVar(attackDamage, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
                SetSpellToolTipVar(finalAP, 2, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
            }
            float cURMoveSpeed = GetMovementSpeed(owner);
            if (cURMoveSpeed > 390)
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.EzrealFastRunAnim)) == 0)
                {
                    AddBuff(owner, owner, new Buffs.EzrealFastRunAnim(), 1, 1, 100000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
            else if (cURMoveSpeed < 390)
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.EzrealFastRunAnim)) > 0)
                {
                    SpellBuffRemove(owner, nameof(Buffs.EzrealFastRunAnim), owner, 0);
                }
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            spellName = GetSpellName(spell);
            if (spellName == nameof(Spells.EzrealTrueshotBarrage))
            {
                charVars.PercentOfAttack = 1;
                AddBuff(owner, owner, new Buffs.CantAttack(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            //charVars.CastPoint = 1;
            AddBuff(owner, owner, new Buffs.EzrealCyberSkinSound(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
        }
        public override void OnResurrect()
        {
            TeamId teamID = GetTeamID_CS(owner);
            int ezrealSkinID = GetSkinID(owner);
            if (ezrealSkinID == 5)
            {
                SpellEffectCreate(out _, out _, "Ezreal_cyberezreal_revive.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, true, owner, default, default, owner, default, default, true, false, false, false, false);
            }
        }
        public override void OnLevelUpSpell(int slot)
        {
            TeamId teamID = GetTeamID_CS(attacker);
            int ezrealSkinID = GetSkinID(attacker);
            if (ezrealSkinID == 5)
            {
                if (slot == 0)
                {
                    SpellEffectCreate(out _, out _, "Ezreal_cyberezreal_mysticshot.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, true, owner, default, default, owner, default, default, true, false, false, false, false);
                }
                else if (slot == 1)
                {
                    SpellEffectCreate(out _, out _, "Ezreal_cyberezreal_essenceflux.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, true, owner, default, default, owner, default, default, true, false, false, false, false);
                }
                else if (slot == 2)
                {
                    SpellEffectCreate(out _, out _, "Ezreal_cyberezreal_arcaneshift.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, true, owner, default, default, owner, default, default, true, false, false, false, false);
                }
                else if (slot == 3)
                {
                    SpellEffectCreate(out _, out _, "Ezreal_cyberezreal_trueshotbarrage.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, true, owner, default, default, owner, default, default, true, false, false, false, false);
                }
            }
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}