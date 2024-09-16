namespace Spells
{
    public class KarmaSpiritBondC : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = { 7.5f, 7, 6.5f, 6, 5.5f, 5 };
        float[] effect1 = { 35, 37.5f, 40, 42.5f, 45, 47.5f };
        int[] effect2 = { 80, 125, 170, 215, 260, 305 };
        float[] effect3 = { 0.2f, 0.24f, 0.28f, 0.32f, 0.36f, 0.4f };
        int[] effect4 = { 5, 5, 5, 5, 5, 5 };
        float[] effect6 = { -0.2f, -0.24f, -0.28f, -0.32f, -0.36f, -0.4f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_CooldownToRestore;
            bool isStealthed = GetStealthed(target);
            if (isStealthed)
            {
                TeamId teamID = GetTeamID_CS(owner);
                SpellEffectCreate(out _, out _, "karma_spiritBond_break_overhead.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
                nextBuffVars_CooldownToRestore = effect0[level - 1];
                float manaToRestore = effect1[level - 1];
                IncPAR(owner, manaToRestore, PrimaryAbilityResourceType.MANA);
                AddBuff(owner, owner, new Buffs.KarmaSBStealthBreak(nextBuffVars_CooldownToRestore), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            else
            {
                float nextBuffVars_MoveSpeedMod;
                int nextBuffVars_MantraBoolean = 1;
                float nextBuffVars_DamageToDeal = effect2[level - 1];
                if (target.Team == attacker.Team)
                {
                    nextBuffVars_MoveSpeedMod = effect3[level - 1];
                    AddBuff(attacker, target, new Buffs.KarmaSpiritBond(nextBuffVars_MantraBoolean, nextBuffVars_MoveSpeedMod, nextBuffVars_DamageToDeal), 1, 1, effect4[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    AddBuff(attacker, attacker, new Buffs.KarmaSpiritBondAllySelfTooltip(), 1, 1, effect4[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
                else
                {
                    nextBuffVars_MoveSpeedMod = effect6[level - 1];
                    BreakSpellShields(target);
                    AddBuff(owner, target, new Buffs.KarmaSpiritBondC(nextBuffVars_MantraBoolean, nextBuffVars_DamageToDeal, nextBuffVars_MoveSpeedMod), 1, 1, effect4[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                    AddBuff((ObjAIBase)target, owner, new Buffs.KarmaSpiritBondEnemyTooltip(), 1, 1, effect4[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    AddBuff(owner, target, new Buffs.KarmaMantraSBSlow(nextBuffVars_MoveSpeedMod), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                }
            }
        }
    }
}
namespace Buffs
{
    public class KarmaSpiritBondC : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "KarmaSpiritBondEnemySelf",
            BuffTextureName = "KarmaSpiritBond.dds",
        };
        int mantraBoolean;
        float damageToDeal;
        float moveSpeedMod;
        float negMoveSpeed;
        EffectEmitter moveSpeedPart1;
        EffectEmitter sBIdle1;
        EffectEmitter sBIdle2;
        EffectEmitter soulShackleTarget_blood;
        EffectEmitter soulShackleTarget;
        EffectEmitter soulShackleTarget2;
        EffectEmitter particleID;
        EffectEmitter soulShackleIdle;
        EffectEmitter soundOne;
        EffectEmitter soundTwo;
        public KarmaSpiritBondC(int mantraBoolean = default, float damageToDeal = default, float moveSpeedMod = default)
        {
            this.mantraBoolean = mantraBoolean;
            this.damageToDeal = damageToDeal;
            this.moveSpeedMod = moveSpeedMod;
        }
        /*
        //TODO: Uncomment and fix
        public override void OnActivate()
        {
            TeamId teamOfAttacker; // UNITIALIZED
            float nextBuffVars_MoveSpeedMod;
            //RequireVar(this.mantraBoolean);
            //RequireVar(this.moveSpeedMod);
            //RequireVar(this.damageToDeal);
            TeamId teamOfOwner = GetTeamID(attacker);
            this.negMoveSpeed = this.moveSpeedMod * -1;
            IncPercentMovementSpeedMod(attacker, this.negMoveSpeed);
            SpellEffectCreate(out this.moveSpeedPart1, out _, "karma_spiritBond_speed_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "root", default, attacker, default, default, false, default, default, false, false);
            SpellEffectCreate(out this.sBIdle1, out _, "leBlanc_shackle_self_idle.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, false, default, default, false, false);
            SpellEffectCreate(out this.sBIdle2, out _, "leBlanc_shackle_self_idle.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            SpellEffectCreate(out this.soulShackleTarget_blood, out _, "karma_spiritBond_indicator_target_blank.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, false, default, default, false, false);
            if(this.mantraBoolean == 1)
            {
                SpellEffectCreate(out this.soulShackleTarget, out _, "karma_spiritBond_indicator_target_enemy.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
                SpellEffectCreate(out this.soulShackleTarget2, out _, "karma_spiritBond_indicator_impact_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
                SpellEffectCreate(out this.particleID, out this.soulShackleIdle, "karma_spiritBond_ult_beam_teamID_ally_green.troy", "karma_spiritBond_ult_beam_teamID_enemy_red.troy", teamOfOwner, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "root", default, owner, "root", default, false, default, default, false, false);
                SpellEffectCreate(out this.soundOne, out this.soundTwo, "KarmaSpiritBondSoundGreen.troy", "KarmaSpiritBondSoundRed.troy", teamOfAttacker, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            }
            else
            {
                SpellEffectCreate(out this.soulShackleTarget, out _, "karma_spiritBond_indicator_impact.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
                SpellEffectCreate(out this.particleID, out this.soulShackleIdle, "karma_spiritBond_ult_beam_teamID_ally_green.troy", "karma_spiritBond_ult_beam_teamID_enemy_red.troy", teamOfOwner, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "root", default, owner, "root", default, false, default, default, false, false);
                SpellEffectCreate(out this.soundOne, out this.soundTwo, "KarmaSpiritBondSoundGreen.troy", "KarmaSpiritBondSoundRed.troy", teamOfAttacker, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            }
            float distance = DistanceBetweenObjects(owner, attacker);
            float offsetAngle = GetOffsetAngle(attacker, owner.Position3D);
            float halfDistance = distance / 2;
            Vector3 centerPoint = GetPointByUnitFacingOffset(attacker, halfDistance, offsetAngle);
            TeamId teamID = GetTeamID(attacker);
            if(teamID == TeamId.TEAM_BLUE)
            {
                foreach(AttackableUnit unit in GetUnitsInRectangle(attacker, centerPoint, 25, halfDistance, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.KarmaLinkDmgCDOrder), false))
                {
                    if(unit != attacker)
                    {
                        if(unit != owner)
                        {
                            AddBuff(attacker, unit, new Buffs.KarmaLinkDmgCDOrder(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            if(this.mantraBoolean == 1)
                            {
                                if(unit.Team == attacker.Team)
                                {
                                    if(unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.negMoveSpeed;
                                        AddBuff(attacker, unit, new Buffs.KarmaMantraSBHaste(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                                    }
                                }
                                else
                                {
                                    SpellEffectCreate(out hit, out _, "karma_spiritBond_damage_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                                    BreakSpellShields(unit);
                                    ApplyDamage(attacker, unit, this.damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 0, false, false, attacker);
                                    if(unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
                                        AddBuff(attacker, unit, new Buffs.KarmaMantraSBSlow(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                                    }
                                }
                            }
                            else
                            {
                                if(unit.Team == attacker.Team)
                                {
                                    if(unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.negMoveSpeed;
                                        AddBuff(attacker, unit, new Buffs.KarmaMantraSBHaste(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                                    }
                                }
                                else
                                {
                                    SpellEffectCreate(out hit, out _, "karma_spiritBond_damage_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                                    BreakSpellShields(unit);
                                    ApplyDamage(attacker, unit, this.damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 0, false, false, attacker);
                                    if(unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
                                        AddBuff(attacker, unit, new Buffs.KarmaMantraSBSlow(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                foreach(AttackableUnit unit in GetUnitsInRectangle(attacker, centerPoint, 25, halfDistance, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.KarmaLinkDmgCDChaos), false))
                {
                    if(unit != owner && unit != attacker)
                    {
                        AddBuff(attacker, unit, new Buffs.KarmaLinkDmgCDChaos(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        if(this.mantraBoolean == 1)
                        {
                            if(unit.Team == attacker.Team)
                            {
                                if(unit is Champion)
                                {
                                    nextBuffVars_MoveSpeedMod = this.negMoveSpeed;
                                    AddBuff(attacker, unit, new Buffs.KarmaMantraSBHaste(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                                }
                            }
                            else
                            {
                                SpellEffectCreate(out hit, out _, "karma_spiritBond_damage_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                                BreakSpellShields(unit);
                                ApplyDamage(attacker, unit, this.damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 0, false, false, attacker);
                                if(unit is Champion)
                                {
                                    nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
                                    AddBuff(attacker, unit, new Buffs.KarmaMantraSBSlow(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                                }
                            }
                        }
                        else
                        {
                            if(unit.Team == attacker.Team)
                            {
                                if(unit is Champion)
                                {
                                    nextBuffVars_MoveSpeedMod = this.negMoveSpeed;
                                    AddBuff(attacker, unit, new Buffs.KarmaMantraSBHaste(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                                }
                            }
                            else
                            {
                                SpellEffectCreate(out hit, out _, "karma_spiritBond_damage_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                                BreakSpellShields(unit);
                                ApplyDamage(attacker, unit, this.damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 0, false, false, attacker);
                                if(unit is Champion)
                                {
                                    nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
                                    AddBuff(attacker, unit, new Buffs.KarmaMantraSBSlow(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                                }
                            }
                        }
                    }
                }
            }
        }
        */
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(soundOne);
            SpellEffectRemove(soundTwo);
            SpellEffectRemove(moveSpeedPart1);
            SpellEffectRemove(sBIdle1);
            SpellEffectRemove(sBIdle2);
            SpellEffectRemove(particleID);
            if (GetBuffCountFromCaster(owner, attacker, nameof(Buffs.KarmaMantraSBSlow)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.KarmaMantraSBSlow), attacker, 0);
            }
            if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.KarmaSpiritBondEnemyTooltip)) > 0)
            {
                SpellBuffRemove(attacker, nameof(Buffs.KarmaSpiritBondEnemyTooltip), attacker, 0);
            }
            SpellEffectRemove(soulShackleIdle);
            SpellEffectRemove(soulShackleTarget);
            SpellEffectRemove(soulShackleTarget_blood);
            if (mantraBoolean == 1)
            {
                SpellEffectRemove(soulShackleTarget2);
            }
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(attacker, negMoveSpeed);
        }
        public override void OnUpdateActions()
        {
            float nextBuffVars_MoveSpeedMod;
            float distance = DistanceBetweenObjects(owner, attacker);
            float offsetAngle = GetOffsetAngle(attacker, owner.Position3D);
            float halfDistance = distance / 2;
            Vector3 centerPoint = GetPointByUnitFacingOffset(attacker, halfDistance, offsetAngle);
            TeamId teamID = GetTeamID_CS(attacker);
            if (IsDead(owner))
            {
                SpellBuffRemoveCurrent(owner);
            }
            else
            {
                if (IsDead(attacker))
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else
                {
                    distance = DistanceBetweenObjects(owner, attacker);
                    if (distance > 900)
                    {
                        SpellEffectCreate(out _, out _, "karma_spiritBond_break_overhead.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, true, default, default, false, false);
                        SpellEffectCreate(out _, out _, "karma_spiritBond_break_overhead.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
                        SpellBuffRemoveCurrent(owner);
                    }
                }
            }
            if (teamID == TeamId.TEAM_ORDER)
            {
                foreach (AttackableUnit unit in GetUnitsInRectangle(attacker, centerPoint, 25, halfDistance, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.KarmaLinkDmgCDOrder), false))
                {
                    if (unit != owner)
                    {
                        if (unit != attacker)
                        {
                            AddBuff(attacker, unit, new Buffs.KarmaLinkDmgCDOrder(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            if (mantraBoolean == 1)
                            {
                                if (unit.Team == attacker.Team)
                                {
                                    if (unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = negMoveSpeed;
                                        AddBuff(attacker, unit, new Buffs.KarmaMantraSBHaste(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                                    }
                                }
                                else
                                {
                                    SpellEffectCreate(out _, out _, "karma_spiritBond_damage_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                                    BreakSpellShields(unit);
                                    ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 0, false, false, attacker);
                                    if (unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = moveSpeedMod;
                                        AddBuff(attacker, unit, new Buffs.KarmaMantraSBSlow(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                                    }
                                }
                            }
                            else
                            {
                                if (unit.Team == attacker.Team)
                                {
                                    if (unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = negMoveSpeed;
                                        AddBuff(attacker, unit, new Buffs.KarmaMantraSBHaste(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                                    }
                                }
                                else
                                {
                                    SpellEffectCreate(out _, out _, "karma_spiritBond_damage_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                                    BreakSpellShields(unit);
                                    ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 0, false, false, attacker);
                                    if (unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = moveSpeedMod;
                                        AddBuff(attacker, unit, new Buffs.KarmaMantraSBSlow(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (AttackableUnit unit in GetUnitsInRectangle(attacker, centerPoint, 25, halfDistance, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.KarmaLinkDmgCDChaos), false))
                {
                    if (unit != attacker && unit != owner)
                    {
                        AddBuff(attacker, unit, new Buffs.KarmaLinkDmgCDChaos(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        if (mantraBoolean == 1)
                        {
                            if (unit.Team == attacker.Team)
                            {
                                if (unit is Champion)
                                {
                                    nextBuffVars_MoveSpeedMod = negMoveSpeed;
                                    AddBuff(attacker, unit, new Buffs.KarmaMantraSBHaste(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                                }
                            }
                            else
                            {
                                SpellEffectCreate(out _, out _, "karma_spiritBond_damage_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                                BreakSpellShields(unit);
                                ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 0, false, false, attacker);
                                if (unit is Champion)
                                {
                                    nextBuffVars_MoveSpeedMod = moveSpeedMod;
                                    AddBuff(attacker, unit, new Buffs.KarmaMantraSBSlow(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                                }
                            }
                        }
                        else
                        {
                            if (unit.Team == attacker.Team)
                            {
                                if (unit is Champion)
                                {
                                    nextBuffVars_MoveSpeedMod = negMoveSpeed;
                                    AddBuff(attacker, unit, new Buffs.KarmaMantraSBHaste(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                                }
                            }
                            else
                            {
                                SpellEffectCreate(out _, out _, "karma_spiritBond_damage_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                                BreakSpellShields(unit);
                                ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 0, false, false, attacker);
                                if (unit is Champion)
                                {
                                    nextBuffVars_MoveSpeedMod = moveSpeedMod;
                                    AddBuff(attacker, unit, new Buffs.KarmaMantraSBSlow(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                                }
                            }
                        }
                    }
                }
            }
            bool isStealthed = GetStealthed(owner);
            if (isStealthed)
            {
                SpellEffectCreate(out _, out _, "karma_spiritBond_break_overhead.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}