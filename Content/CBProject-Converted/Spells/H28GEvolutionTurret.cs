namespace Spells
{
    public class HeimerdingerQ: H28GEvolutionTurret {}
    public class H28GEvolutionTurret : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 1, 1, 2, 2, 2 };
        int[] effect1 = { 0, 0, 0, 125, 125 };
        int[] effect2 = { 30, 38, 46, 54, 62 };
        int[] effect3 = { 0, 0, 0, 0, 0 };
        public override bool CanCast()
        {
            int count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.HeimerdingerTurretReady));
            return count > 0;
        }
        public override void SelfExecute()
        {
            Minion other3;
            SpellBuffRemove(owner, nameof(Buffs.HeimerdingerTurretReady), owner, 0);
            int maxStacks = effect0[level - 1];
            float level4BonusHP = effect1[level - 1];
            float numFound = 0;
            float minDuration = 25000;
            AttackableUnit other2 = owner;
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions, nameof(Buffs.H28GEvolutionTurret), true))
            {
                numFound++;
                float durationRemaining = GetBuffRemainingDuration(unit, nameof(Buffs.H28GEvolutionTurret));
                if (durationRemaining < minDuration)
                {
                    minDuration = durationRemaining;
                    InvalidateUnit(other2);
                    other2 = unit;
                }
            }
            if (numFound >= maxStacks && owner != other2)
            {
                ApplyDamage((ObjAIBase)other2, other2, 10000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 1, false, false, (ObjAIBase)other2);
            }
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 targetPos = GetSpellTargetPos(spell);
            float abilityPower = GetFlatMagicDamageMod(owner);
            float abilityPowerBonus = abilityPower * 0.2f;
            float baseDamage = effect2[level - 1];
            float bonusDamage = baseDamage + abilityPowerBonus;
            float nextBuffVars_BonusDamage = bonusDamage;
            int ownerLevel = GetLevel(owner);
            float nextBuffVars_BonusHealthPreLevel4 = ownerLevel * 15;
            float nextBuffVars_BonusHealth = nextBuffVars_BonusHealthPreLevel4 + level4BonusHP;
            float nextBuffVars_BonusStats = ownerLevel * 1;
            float nextBuffVars_BonusArmor = effect3[level - 1];
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.UpgradeBuff)) > 0)
            {
                other3 = SpawnMinion("H-28G Evolution Turret", "HeimerTBlue", "Minion.lua", targetPos, teamID, false, false, true, false, false, false, 0, false, false, (Champion)owner);
                float remainingDuration = GetBuffRemainingDuration(owner, nameof(Buffs.UpgradeBuff));
                AddBuff(attacker, other3, new Buffs.UpgradeSlow(), 1, 1, remainingDuration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            else if (level == 5)
            {
                other3 = SpawnMinion("H-28G Evolution Turret", "HeimerTRed", "Minion.lua", targetPos, teamID, false, false, true, false, false, false, 0, false, false, (Champion)owner);
                AddBuff(owner, other3, new Buffs.ExplosiveCartridges(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            else if (level >= 2)
            {
                other3 = SpawnMinion("H-28G Evolution Turret", "HeimerTGreen", "Minion.lua", targetPos, teamID, false, false, true, false, false, false, 0, false, false, (Champion)owner);
                AddBuff(owner, other3, new Buffs.UrAniumRounds(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            else
            {
                other3 = SpawnMinion("H-28G Evolution Turret", "HeimerTYellow", "Minion.lua", targetPos, teamID, false, false, true, false, false, false, 0, false, false, (Champion)owner);
            }
            AddBuff(owner, other3, new Buffs.UPGRADE___Proof(), 1, 1, 6, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff(owner, other3, new Buffs.H28GEvolutionTurret(nextBuffVars_BonusDamage, nextBuffVars_BonusHealth, nextBuffVars_BonusArmor, nextBuffVars_BonusStats), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class H28GEvolutionTurret : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "H28GEvolutionTurret",
            BuffTextureName = "Heimerdinger_H28GEvolutionTurret.dds",
        };
        float bonusDamage;
        float bonusHealth;
        float bonusArmor;
        float bonusStats;
        float lastTimeExecuted;
        public H28GEvolutionTurret(float bonusDamage = default, float bonusHealth = default, float bonusArmor = default, float bonusStats = default)
        {
            this.bonusDamage = bonusDamage;
            this.bonusHealth = bonusHealth;
            this.bonusArmor = bonusArmor;
            this.bonusStats = bonusStats;
        }
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            if (owner.Team != attacker.Team)
            {
                return scriptName != nameof(Buffs.GlobalWallPush) && type != BuffType.STUN;
            }
            return maxStack != 76;
        }
        public override void OnActivate()
        {
            CancelAutoAttack(owner, true);
            TeamId teamID = GetTeamID_CS(attacker);
            //RequireVar(this.bonusDamage);
            //RequireVar(this.bonusHealth);
            //RequireVar(this.bonusArmor);
            //RequireVar(this.bonusStats);
            SetCanMove(owner, false);
            SpellEffectCreate(out _, out _, "heimerdinger_turret_birth.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            foreach (AttackableUnit unit in GetClosestUnitsInArea(attacker, owner.Position3D, 425, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectTurrets, 1, default, true))
            {
                if (unit is Champion)
                {
                    AddBuff((ObjAIBase)unit, owner, new Buffs.H28GEvolutionTurretSpell2(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else
                {
                    AddBuff((ObjAIBase)unit, owner, new Buffs.H28GEvolutionTurretSpell3(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            if (GetBuffCountFromCaster(attacker, owner, nameof(Buffs.HeimerdingerTurretMaximum)) > 0)
            {
                SpellBuffRemove(attacker, nameof(Buffs.HeimerdingerTurretMaximum), (ObjAIBase)owner, 0);
            }
        }
        public override void OnUpdateStats()
        {
            IncFlatHPPoolMod(owner, bonusHealth);
            IncFlatPhysicalDamageMod(owner, bonusDamage);
            SetCanMove(owner, false);
            IncFlatArmorMod(owner, bonusStats);
            IncFlatSpellBlockMod(owner, bonusStats);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, false))
            {
                if (GetBuffCountFromCaster(owner, default, nameof(Buffs.H28GEvolutionTurretSpell1)) == 0)
                {
                    if (GetBuffCountFromCaster(owner, default, nameof(Buffs.H28GEvolutionTurretSpell2)) == 0)
                    {
                        if (GetBuffCountFromCaster(owner, default, nameof(Buffs.H28GEvolutionTurretSpell3)) == 0)
                        {
                            int unitFound = 0;
                            if (GetBuffCountFromCaster(owner, default, nameof(Buffs.UpgradeSlow)) > 0)
                            {
                                foreach (AttackableUnit unit in GetClosestUnitsInArea(attacker, owner.Position3D, 425, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
                                {
                                    unitFound = 1;
                                    AddBuff((ObjAIBase)unit, owner, new Buffs.H28GEvolutionTurretSpell2(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                                }
                            }
                            if (unitFound == 0)
                            {
                                foreach (AttackableUnit unit in GetClosestUnitsInArea(attacker, owner.Position3D, 425, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectTurrets, 1, default, true))
                                {
                                    if (unit is Champion)
                                    {
                                        AddBuff((ObjAIBase)unit, owner, new Buffs.H28GEvolutionTurretSpell2(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                                    }
                                    else
                                    {
                                        AddBuff((ObjAIBase)unit, owner, new Buffs.H28GEvolutionTurretSpell3(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (GetBuffCountFromCaster(owner, default, nameof(Buffs.UpgradeSlow)) > 0)
                            {
                                foreach (AttackableUnit unit in GetClosestUnitsInArea(attacker, owner.Position3D, 425, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
                                {
                                    SpellBuffClear(owner, nameof(Buffs.H28GEvolutionTurretSpell3));
                                    AddBuff((ObjAIBase)unit, owner, new Buffs.H28GEvolutionTurretSpell2(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                                }
                            }
                        }
                    }
                }
            }
        }
        public override void OnSpellHit(AttackableUnit target)
        {
            bonusArmor += 0.143f;
        }
    }
}