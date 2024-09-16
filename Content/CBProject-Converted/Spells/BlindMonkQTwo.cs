namespace Spells
{
    public class BlindMonkQTwo : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 22f, 18f, 14f, 10f, 6f, },
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 50, 80, 110, 140, 170 };
        public override bool CanCast()
        {
            bool returnValue = true;
            returnValue = false;
            TeamId teamID = GetTeamID_CS(owner);
            if (teamID == TeamId.TEAM_ORDER)
            {
                foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1250, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.BlindMonkQOne), true))
                {
                    if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.BlindMonkQOne)) > 0)
                    {
                        returnValue = true;
                    }
                }
            }
            else
            {
                foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1250, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.BlindMonkQOneChaos), true))
                {
                    if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.BlindMonkQOneChaos)) > 0)
                    {
                        returnValue = true;
                    }
                }
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            Vector3 ownerPos;
            Vector3 targetPos;
            float moveSpeed;
            float dashSpeed;
            float distance;
            Vector3 nextBuffVars_TargetPos;
            float baseDamage;
            float bonusAD;
            float damageVar;
            float nextBuffVars_Distance; // UNUSED
            float nextBuffVars_dashSpeed;
            float nextBuffVars_DamageVar;
            TeamId teamID = GetTeamID_CS(owner);
            if (teamID == TeamId.TEAM_ORDER)
            {
                foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 2000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 2, nameof(Buffs.BlindMonkQOne), true))
                {
                    if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.BlindMonkQOne)) > 0)
                    {
                        SpellBuffRemove(unit, nameof(Buffs.BlindMonkQOne), owner, 0);
                        ownerPos = GetUnitPosition(owner);
                        SpellEffectCreate(out _, out _, "blindMonk_Q_resonatingStrike_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
                        targetPos = GetUnitPosition(unit);
                        moveSpeed = GetMovementSpeed(owner);
                        dashSpeed = moveSpeed + 1350;
                        distance = DistanceBetweenObjects(owner, unit);
                        nextBuffVars_TargetPos = targetPos;
                        nextBuffVars_Distance = distance;
                        nextBuffVars_dashSpeed = dashSpeed;
                        baseDamage = effect0[level - 1];
                        bonusAD = GetFlatPhysicalDamageMod(owner);
                        bonusAD *= 0.9f;
                        damageVar = baseDamage + bonusAD;
                        nextBuffVars_DamageVar = damageVar;
                        AddBuff((ObjAIBase)unit, owner, new Buffs.BlindMonkQTwoDash(nextBuffVars_TargetPos, nextBuffVars_dashSpeed, nextBuffVars_DamageVar), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, true);
                        SpellBuffClear(owner, nameof(Buffs.BlindMonkQManager));
                    }
                }
            }
            else
            {
                foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 2000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 2, nameof(Buffs.BlindMonkQOneChaos), true))
                {
                    if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.BlindMonkQOneChaos)) > 0)
                    {
                        SpellBuffRemove(unit, nameof(Buffs.BlindMonkQOneChaos), owner, 0);
                        ownerPos = GetUnitPosition(owner);
                        SpellEffectCreate(out _, out _, "blindMonk_Q_resonatingStrike_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
                        targetPos = GetUnitPosition(unit);
                        moveSpeed = GetMovementSpeed(owner);
                        dashSpeed = moveSpeed + 1350;
                        distance = DistanceBetweenObjects(owner, unit);
                        nextBuffVars_TargetPos = targetPos;
                        nextBuffVars_Distance = distance;
                        nextBuffVars_dashSpeed = dashSpeed;
                        baseDamage = effect0[level - 1];
                        bonusAD = GetFlatPhysicalDamageMod(owner);
                        bonusAD *= 0.9f;
                        damageVar = baseDamage + bonusAD;
                        nextBuffVars_DamageVar = damageVar;
                        AddBuff((ObjAIBase)unit, owner, new Buffs.BlindMonkQTwoDash(nextBuffVars_TargetPos, nextBuffVars_dashSpeed, nextBuffVars_DamageVar), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, true);
                        SpellBuffClear(owner, nameof(Buffs.BlindMonkQManager));
                    }
                }
            }
        }
    }
}