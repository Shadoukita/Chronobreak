namespace Buffs
{
    public class OrianaGhost : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "OrianaGhost",
            BuffTextureName = "OriannaPassive.dds",
            PersistsThroughDeath = true,
        };
        ObjAIBase caster;
        bool ghostSpawned;
        bool minionBall;
        EffectEmitter temp;
        EffectEmitter orianaPointer;
        int previousState;
        int currentState;
        int[] effect0 = { 10, 15, 20, 25, 30 };
        public override void OnActivate()
        {
            Vector3 currentPos = GetUnitPosition(owner);
            ObjAIBase caster = GetBuffCasterUnit();
            this.caster = GetBuffCasterUnit();
            ghostSpawned = false;
            minionBall = false;
            string skinName = GetUnitSkinName(owner);
            if (skinName == "OriannaBall")
            {
                minionBall = true;
            }
            if (!minionBall && caster != owner)
            {
                int skinID = GetSkinID(caster);
                if (skinID == 1)
                {
                    SpellEffectCreate(out temp, out _, "Oriana_ghost_bind_goth.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, currentPos, owner, default, currentPos, false, false, false, false, false);
                }
                else if (skinID == 2)
                {
                    SpellEffectCreate(out temp, out _, "Oriana_ghost_bind_doll.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, currentPos, owner, default, currentPos, false, false, false, false, false);
                }
                else
                {
                    SpellEffectCreate(out temp, out _, "Oriana_Ghost_bind.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, currentPos, owner, default, currentPos, false, false, false, false, false);
                }
            }
            Vector3 attackerPos = GetUnitPosition(attacker); // UNUSED
            caster = GetBuffCasterUnit();
            float distance = DistanceBetweenObjects(caster, owner);
            if (distance >= 1000)
            {
                SpellEffectCreate(out orianaPointer, out _, "OrianaBallIndicatorFar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, true, attacker, default, default, owner, default, default, false, false, false, false, true, owner);
                previousState = 0;
            }
            else if (distance >= 800)
            {
                SpellEffectCreate(out orianaPointer, out _, "OrianaBallIndicatorMedium.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, true, attacker, default, default, owner, default, default, false, false, false, false, true, owner);
                previousState = 1;
            }
            else if (distance >= 0)
            {
                SpellEffectCreate(out orianaPointer, out _, "OrianaBallIndicatorNear.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, true, attacker, default, default, owner, default, default, false, false, false, false, true, owner);
                previousState = 2;
            }
            SetSpellOffsetTarget(1, SpellSlotType.SpellSlots, nameof(Spells.JunkName), SpellbookType.SPELLBOOK_CHAMPION, attacker, owner);
            SetSpellOffsetTarget(3, SpellSlotType.SpellSlots, nameof(Spells.JunkName), SpellbookType.SPELLBOOK_CHAMPION, attacker, owner);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(orianaPointer);
            SpellBuffClear(owner, nameof(Buffs.OrianaGhostMinion));
            AttackableUnit caster = this.caster;
            string skinName = GetUnitSkinName(owner);
            if (skinName != "OriannaBall")
            {
                if (caster != owner)
                {
                    SpellEffectRemove(temp);
                }
            }
            else
            {
                SpellBuffClear(attacker, nameof(Buffs.OriannaBallTracker));
            }
        }
        public override void OnUpdateStats()
        {
            ObjAIBase caster = GetBuffCasterUnit();
            int level = GetSlotSpellLevel(caster, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level > 0)
            {
                float defenseBonus = effect0[level - 1];
                IncFlatArmorMod(owner, defenseBonus);
                IncFlatSpellBlockMod(owner, defenseBonus);
            }
            caster = GetBuffCasterUnit();
            float distance = DistanceBetweenObjects(caster, owner);
            if (distance >= 1000)
            {
                currentState = 0;
            }
            else if (distance >= 800)
            {
                currentState = 1;
            }
            else if (distance >= 0)
            {
                currentState = 2;
            }
            if (currentState != previousState)
            {
                SpellEffectRemove(orianaPointer);
                if (currentState == 0)
                {
                    SpellEffectCreate(out orianaPointer, out _, "OrianaBallIndicatorFar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, true, attacker, default, default, owner, default, default, false, false, false, false, true, owner);
                }
                else if (currentState == 1)
                {
                    SpellEffectCreate(out orianaPointer, out _, "OrianaBallIndicatorMedium.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, true, attacker, default, default, owner, default, default, false, false, false, false, true, owner);
                }
                else
                {
                    SpellEffectCreate(out orianaPointer, out _, "OrianaBallIndicatorNear.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, true, attacker, default, default, owner, default, default, false, false, false, false, true, owner);
                }
            }
            previousState = currentState;
        }
        public override void OnUpdateActions()
        {
            Vector3 castPos = default;           //TODO: Verify
            TeamId teamID = TeamId.TEAM_UNKNOWN; //TODO: Verify

            ObjAIBase caster = GetBuffCasterUnit();
            float distance = DistanceBetweenObjects(caster, owner);
            if (distance > 1125)
            {
                SealSpellSlot(0, SpellSlotType.SpellSlots, caster, true, SpellbookType.SPELLBOOK_CHAMPION);
                SealSpellSlot(1, SpellSlotType.SpellSlots, caster, true, SpellbookType.SPELLBOOK_CHAMPION);
                SealSpellSlot(2, SpellSlotType.SpellSlots, caster, true, SpellbookType.SPELLBOOK_CHAMPION);
                SealSpellSlot(3, SpellSlotType.SpellSlots, caster, true, SpellbookType.SPELLBOOK_CHAMPION);
                SpellBuffClear(owner, nameof(Buffs.OrianaGhost));
                castPos = GetUnitPosition(owner);
                teamID = GetTeamID_CS(owner);
                SpellEffectCreate(out _, out _, "Orianna_Ball_Flash.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, "root", castPos, owner, default, default, true, false, false, false, false);
                AddBuff(caster, caster, new Buffs.OrianaGhostSelf(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                SpellEffectCreate(out _, out _, "Orianna_Ball_Flash_Reverse.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, caster, false, caster, "SpinnigBottomRidge", castPos, caster, default, default, true, false, false, false, false);
                SealSpellSlot(0, SpellSlotType.SpellSlots, caster, false, SpellbookType.SPELLBOOK_CHAMPION);
                SealSpellSlot(1, SpellSlotType.SpellSlots, caster, false, SpellbookType.SPELLBOOK_CHAMPION);
                SealSpellSlot(2, SpellSlotType.SpellSlots, caster, false, SpellbookType.SPELLBOOK_CHAMPION);
                SealSpellSlot(3, SpellSlotType.SpellSlots, caster, false, SpellbookType.SPELLBOOK_CHAMPION);
            }
            else if (distance <= 25)
            {
                if (GetBuffCountFromCaster(caster, default, nameof(Buffs.OrianaDissonanceCountdown)) == 0 && owner is not Champion)
                {
                    caster = GetBuffCasterUnit();
                    AddBuff(caster, caster, new Buffs.OrianaGhostSelf(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                    SpellBuffClear(owner, nameof(Buffs.OrianaGhost));
                    SpellEffectCreate(out _, out _, "Orianna_Ball_Flash_Reverse.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, caster, false, caster, "SpinnigBottomRidge", castPos, caster, default, default, true, false, false, false, false);
                }
            }
            else
            {
                bool noRender = GetNoRender(owner);
                if (owner is Champion && noRender)
                {
                    SealSpellSlot(0, SpellSlotType.SpellSlots, caster, true, SpellbookType.SPELLBOOK_CHAMPION);
                    SealSpellSlot(1, SpellSlotType.SpellSlots, caster, true, SpellbookType.SPELLBOOK_CHAMPION);
                    SealSpellSlot(2, SpellSlotType.SpellSlots, caster, true, SpellbookType.SPELLBOOK_CHAMPION);
                    SealSpellSlot(3, SpellSlotType.SpellSlots, caster, true, SpellbookType.SPELLBOOK_CHAMPION);
                    SpellBuffClear(owner, nameof(Buffs.OrianaGhost));
                    castPos = GetUnitPosition(owner);
                    teamID = GetTeamID_CS(owner);
                    SpellEffectCreate(out _, out _, "Orianna_Ball_Flash.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, "root", castPos, owner, default, default, true, false, false, false, false);
                    AddBuff(caster, caster, new Buffs.OrianaGhostSelf(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                    SpellEffectCreate(out _, out _, "Orianna_Ball_Flash_Reverse.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, caster, false, caster, "SpinnigBottomRidge", castPos, caster, default, default, true, false, false, false, false);
                    SealSpellSlot(0, SpellSlotType.SpellSlots, caster, false, SpellbookType.SPELLBOOK_CHAMPION);
                    SealSpellSlot(1, SpellSlotType.SpellSlots, caster, false, SpellbookType.SPELLBOOK_CHAMPION);
                    SealSpellSlot(2, SpellSlotType.SpellSlots, caster, false, SpellbookType.SPELLBOOK_CHAMPION);
                    SealSpellSlot(3, SpellSlotType.SpellSlots, caster, false, SpellbookType.SPELLBOOK_CHAMPION);
                }
            }
            if (owner is Champion && !ghostSpawned && IsDead(owner))
            {
                caster = GetBuffCasterUnit();
                Vector3 missileEndPosition = GetUnitPosition(owner);
                teamID = GetTeamID_CS(attacker);
                ghostSpawned = true;
                Minion other3 = SpawnMinion("TheDoomBall", "OriannaBall", "idle.lua", missileEndPosition, teamID, false, true, false, true, true, true, 0, false, true, (Champion)caster);
                AddBuff(attacker, other3, new Buffs.OrianaGhost(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff(attacker, other3, new Buffs.OrianaGhostMinion(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                SpellBuffClear(owner, nameof(Buffs.OrianaGhost));
            }
            if (IsDead(caster))
            {
                SpellBuffClear(owner, nameof(Buffs.OrianaGhost));
            }
            else if (caster == owner)
            {
                AddBuff(caster, caster, new Buffs.OrianaGhostSelf(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                SpellBuffClear(owner, nameof(Buffs.OrianaGhost));
            }
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            SpellEffectRemove(orianaPointer);
            if (!ghostSpawned && !minionBall)
            {
                ObjAIBase caster = GetBuffCasterUnit();
                Vector3 missileEndPosition = GetUnitPosition(owner);
                TeamId teamID = GetTeamID_CS(caster);
                ghostSpawned = true;
                Minion other3 = SpawnMinion("TheDoomBall", "OriannaBall", "idle.lua", missileEndPosition, teamID, false, true, false, true, true, true, 0, false, true, (Champion)caster);
                AddBuff(caster, other3, new Buffs.OrianaGhost(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff(caster, other3, new Buffs.OrianaGhostMinion(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                SpellBuffClear(owner, nameof(Buffs.OrianaGhost));
            }
        }
    }
}