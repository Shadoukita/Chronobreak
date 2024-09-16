namespace Spells
{
    public class JackInTheBox : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 0, 100, 200, 300, 400 };
        float[] effect1 = { 0.5f, 0.75f, 1, 1.25f, 1.5f };
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 nextBuffVars_TargetPos = targetPos;
            int nextBuffVars_BonusHealth = effect0[level - 1];
            float nextBuffVars_FearDuration = effect1[level - 1];
            AddBuff(attacker, owner, new Buffs.JackInTheBoxInternal(nextBuffVars_TargetPos, nextBuffVars_BonusHealth, nextBuffVars_FearDuration), 1, 1, 0.1f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class JackInTheBox : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Jack In The Box",
            BuffTextureName = "Jester_DeathWard.dds",
        };
        Fade iD; // UNUSED
        float fearDuration;
        float lastTimeExecuted;
        public JackInTheBox(float fearDuration = default)
        {
            this.fearDuration = fearDuration;
        }
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            bool returnValue = true;
            if (owner.Team != attacker.Team)
            {
                if (scriptName == nameof(Buffs.GlobalWallPush))
                {
                    returnValue = false;
                }
                else if (type == BuffType.FEAR)
                {
                    returnValue = false;
                }
                else if (type == BuffType.CHARM)
                {
                    returnValue = false;
                }
                else if (type == BuffType.SILENCE)
                {
                    returnValue = false;
                }
                else if (type == BuffType.SLEEP)
                {
                    returnValue = false;
                }
                else if (type == BuffType.SLOW)
                {
                    returnValue = false;
                }
                else if (type == BuffType.SNARE)
                {
                    returnValue = false;
                }
                else if (type == BuffType.STUN)
                {
                    returnValue = false;
                }
                else if (type == BuffType.TAUNT)
                {
                    returnValue = false;
                }
                else if (type == BuffType.BLIND)
                {
                    returnValue = false;
                }
                else if (type == BuffType.SUPPRESSION)
                {
                    returnValue = false;
                }
                else if (type == BuffType.COMBAT_DEHANCER)
                {
                    returnValue = false;
                }
                else
                {
                    returnValue = true;
                }
            }
            else
            {
                if (maxStack == 76)
                {
                    returnValue = false;
                }
                else
                {
                    returnValue = true;
                }
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            Vector3 targetPos; // UNITIALIZED
            targetPos = default; //TODO: Verify
            iD = PushCharacterFade(owner, 0.2f, 2);
            //RequireVar(this.fearDuration);
            SetCanMove(owner, false);
            SetCanAttack(owner, false);
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "jackintheboxpoof.troy", default, teamID, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, targetPos, target, default, default, true, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            ApplyDamage((ObjAIBase)owner, owner, 1000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            SetCanMove(owner, false);
            SetCanAttack(owner, false);
        }
        public override void OnUpdateActions()
        {
            if (lifeTime >= 2 && GetBuffCountFromCaster(owner, owner, nameof(Buffs.EndKill)) == 0)
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Stealth)) == 0)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.Stealth(), 1, 1, 90, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                foreach (AttackableUnit unit1 in GetClosestVisibleUnitsInArea(attacker, owner.Position3D, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectBuildings | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectTurrets, 1, default, true))
                {
                    ObjAIBase caster = GetBuffCasterUnit();
                    iD = PushCharacterFade(owner, 1, 0.1f);
                    AddBuff(attacker, attacker, new Buffs.JackInTheBoxDamageSensor(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, false, false, false);
                    AddBuff((ObjAIBase)owner, owner, new Buffs.EndKill(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    SpellBuffRemove(owner, nameof(Buffs.Stealth), (ObjAIBase)owner, 0);
                    foreach (AttackableUnit unit2 in GetUnitsInArea(attacker, owner.Position3D, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                    {
                        BreakSpellShields(unit2);
                        ApplyAssistMarker(caster, unit2, 10);
                        ApplyFear(caster, unit2, fearDuration);
                    }
                }
            }
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, false))
            {
                IncPAR(owner, -0.5f, PrimaryAbilityResourceType.Shield);
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.EndKill)) > 0)
                {
                    int unitFound = 0;
                    foreach (AttackableUnit unit in GetClosestVisibleUnitsInArea(attacker, owner.Position3D, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.JackInTheBoxHardLock), true))
                    {
                        SpellCast((ObjAIBase)owner, unit, default, default, 0, SpellSlotType.SpellSlots, 1, true, false, false, true, false, false);
                        unitFound = 1;
                        SpellBuffRemove(owner, nameof(Buffs.Stealth), (ObjAIBase)owner, 0);
                    }
                    if (unitFound == 0)
                    {
                        foreach (AttackableUnit unit in GetClosestVisibleUnitsInArea(attacker, owner.Position3D, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.JackInTheBoxSoftLock), true))
                        {
                            if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.JackInTheBoxSoftLock)) > 0)
                            {
                                SpellCast((ObjAIBase)owner, unit, default, default, 0, SpellSlotType.SpellSlots, 1, true, false, false, true, false, false);
                                unitFound = 1;
                                SpellBuffRemove(owner, nameof(Buffs.Stealth), (ObjAIBase)owner, 0);
                            }
                        }
                    }
                    if (unitFound == 0)
                    {
                        foreach (AttackableUnit unit in GetClosestVisibleUnitsInArea(attacker, owner.Position3D, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, default, true))
                        {
                            SpellCast((ObjAIBase)owner, unit, default, default, 0, SpellSlotType.SpellSlots, 1, true, false, false, true, false, false);
                            unitFound = 1;
                            SpellBuffRemove(owner, nameof(Buffs.Stealth), (ObjAIBase)owner, 0);
                            if (unit is Champion)
                            {
                                AddBuff((ObjAIBase)owner, unit, new Buffs.JackInTheBoxSoftLock(), 20, 1, 5, BuffAddType.STACKS_AND_OVERLAPS, BuffType.INTERNAL, 0, false, false, false);
                            }
                        }
                    }
                    if (unitFound == 0)
                    {
                        foreach (AttackableUnit unit in GetClosestVisibleUnitsInArea(attacker, owner.Position3D, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectBuildings | SpellDataFlags.AffectTurrets, 1, default, true))
                        {
                            SpellCast((ObjAIBase)owner, unit, default, default, 0, SpellSlotType.SpellSlots, 1, true, false, false, true, false, false);
                            unitFound = 1;
                            SpellBuffRemove(owner, nameof(Buffs.Stealth), (ObjAIBase)owner, 0);
                        }
                    }
                }
            }
        }
        public override void OnPreMitigationDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (attacker is not BaseTurret && GetBuffCountFromCaster(owner, owner, nameof(Buffs.Stealth)) > 0)
            {
                bool canSee = CanSeeTarget(attacker, owner);
                if (!canSee)
                {
                    damageAmount = 0;
                }
            }
        }
    }
}