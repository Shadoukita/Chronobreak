namespace Spells
{
    public class OrianaSpellSword : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class OrianaSpellSword : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "OrianaSpellSword",
            BuffTextureName = "OriannaPowerDagger.dds",
            IsDeathRecapSource = true,
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        EffectEmitter particle; // UNUSED
        int[] effect0 = { 5, 5, 5, 10, 10, 10, 15, 15, 15, 20, 20, 20, 25, 25, 25, 30, 30, 30, 35, 35, 35 };
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(10, ref lastTimeExecuted, true))
            {
                int level = GetLevel(owner);
                float baseDamage = effect0[level - 1];
                SetBuffToolTipVar(2, baseDamage);
                baseDamage = effect0[level - 1];
                float aPBonus = GetFlatMagicDamageMod(owner);
                float damage = aPBonus * 0.2f;
                damage += baseDamage;
                SetBuffToolTipVar(3, damage);
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            int castSlot = GetSpellSlot(spell);
            if (castSlot == 3)
            {
                Vector3 targetPos;
                TeamId teamID = GetTeamID_CS(owner);
                bool deployed = false;
                foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf | SpellDataFlags.AffectUntargetable, 1, nameof(Buffs.OrianaGhost), true))
                {
                    deployed = true;
                    targetPos = GetUnitPosition(unit);
                    if (unit is Champion)
                    {
                        bool isStealth = GetStealthed(owner);
                        if (!isStealth)
                        {
                            SpellEffectCreate(out particle, out _, "OrianaVacuumIndicator_ally.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "spinnigtopridge", targetPos, default, default, targetPos, true, false, false, false, false);
                            charVars.UltimateType = 0;
                            charVars.UltimateTargetPos = targetPos;
                        }
                        else
                        {
                            SpellEffectCreate(out particle, out _, "OrianaVacuumIndicatorSelfNoRing.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, "root", targetPos, default, default, targetPos, true, false, false, false, false);
                            SpellEffectCreate(out particle, out _, "OrianaVacuumIndicatorSelfRing.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, "root", targetPos, default, default, targetPos, true, false, false, false, false);
                            charVars.UltimateType = 1;
                            charVars.UltimateTargetPos = targetPos;
                        }
                    }
                    else
                    {
                        SpellEffectCreate(out particle, out _, "OrianaVacuumIndicator.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "spinnigtopridge", targetPos, default, default, targetPos, true, false, false, false, false);
                        charVars.UltimateType = 1;
                        charVars.UltimateTargetPos = targetPos;
                    }
                }
                if (!deployed)
                {
                    if (GetBuffCountFromCaster(owner, default, nameof(Buffs.OriannaBallTracker)) > 0)
                    {
                        targetPos = charVars.BallPosition;
                        SpellEffectCreate(out particle, out _, "OrianaVacuumIndicatorSelfNoRing.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, "root", targetPos, default, default, targetPos, true, false, false, false, false);
                        SpellEffectCreate(out particle, out _, "OrianaVacuumIndicatorSelfRing.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, "root", targetPos, default, default, targetPos, true, false, false, false, false);
                        charVars.UltimateType = 5;
                        charVars.UltimateTargetPos = targetPos;
                    }
                    else
                    {
                        targetPos = GetPointByUnitFacingOffset(owner, 0, 0);
                        SpellEffectCreate(out particle, out _, "OrianaVacuumIndicatorSelfNoRing.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "spinnigtopridge", targetPos, default, default, targetPos, true, false, false, false, false);
                        SpellEffectCreate(out particle, out _, "OrianaVacuumIndicatorSelfRing.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, "root", targetPos, default, default, targetPos, true, false, false, false, false);
                        charVars.UltimateType = 3;
                        charVars.UltimateTargetPos = targetPos;
                    }
                }
            }
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if (GetBuffCountFromCaster(unit, default, nameof(Buffs.OrianaGhost)) > 0)
                {
                    SpellBuffClear(unit, nameof(Buffs.OrianaGhost));
                }
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            float aPBonus = GetFlatMagicDamageMod(owner);
            int level = GetLevel(owner);
            float baseDamage = effect0[level - 1];
            float damage = aPBonus * 0.2f;
            damage += baseDamage;
            int count = GetBuffCountFromCaster(target, owner, nameof(Buffs.OrianaPowerDagger));
            float multiplier = 0.15f * count;
            multiplier++;
            damage *= multiplier;
            if (target is ObjAIBase && target is not BaseTurret)
            {
                if (GetBuffCountFromCaster(target, owner, nameof(Buffs.OrianaPowerDagger)) == 0)
                {
                    SpellBuffClear(owner, nameof(Buffs.OrianaPowerDaggerDisplay));
                }
                AddBuff((ObjAIBase)owner, target, new Buffs.OrianaPowerDagger(), 3, 1, 4, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position3D, 25000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.OrianaPowerDagger), true))
                {
                    if (unit != target)
                    {
                        SpellBuffClear(unit, nameof(Buffs.OrianaPowerDagger));
                    }
                }
                AddBuff((ObjAIBase)owner, owner, new Buffs.OrianaPowerDaggerDisplay(), 3, 1, 4, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            if (target is ObjAIBase && target is not BaseTurret && target is ObjAIBase)
            {
                ApplyDamage(attacker, target, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
            }
        }
    }
}