namespace Spells
{
    public class MonkeyKingNimbus : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            ChainMissileParameters = new()
            {
                CanHitCaster = false,
                CanHitEnemies = true,
                CanHitFriends = false,
                CanHitSameTarget = false,
                CanHitSameTargetConsecutively = false,
                MaximumHits = 10,
            },
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = { 0.3f, 0.35f, 0.4f, 0.45f, 0.5f };
        int[] effect1 = { 60, 105, 150, 195, 240 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 ownerPos = GetUnitPosition(owner);
            SpellEffectCreate(out _, out _, "monkeyKing_Q_cas.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out _, out _, "monkeyKing_Q_cas.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
            Vector3 targetPos = GetSpellTargetPos(spell);
            float moveSpeed = GetMovementSpeed(owner);
            float dashSpeed = moveSpeed + 1050;
            float distance = DistanceBetweenObjects(owner, target);
            Vector3 nextBuffVars_TargetPos = targetPos;
            float nextBuffVars_Distance = distance; // UNUSED
            float nextBuffVars_dashSpeed = dashSpeed;
            float nextBuffVars_AttackSpeedVar = effect0[level - 1];
            float damageVar = effect1[level - 1];
            float bonusAD = GetFlatPhysicalDamageMod(owner);
            float bonusDamage = bonusAD * 0.8f;
            float nextBuffVars_DamageVar = bonusDamage + damageVar;
            AddBuff((ObjAIBase)target, owner, new Buffs.MonkeyKingNimbusKick(nextBuffVars_TargetPos, nextBuffVars_dashSpeed, nextBuffVars_AttackSpeedVar, nextBuffVars_DamageVar), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0.25f, true, false, true);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.MonkeyKingDecoyStealth)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.MonkeyKingDecoyStealth), owner, 0);
            }
            float unitsHit = 0;
            foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, target.Position3D, 320, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 10, default, true))
            {
                if (unitsHit < 2 && unit != target)
                {
                    Minion other1;
                    bool isStealthed = GetStealthed(unit);
                    if (isStealthed)
                    {
                        bool canSee = CanSeeTarget(owner, unit);
                        if (canSee)
                        {
                            targetPos = GetUnitPosition(unit);
                            nextBuffVars_TargetPos = targetPos;
                            other1 = SpawnMinion("MonkeyKingClone", "MonkeyKingFlying", "Aggro.lua", ownerPos, teamID, false, false, false, false, false, true, 0, false, false, (Champion)owner);
                            AddBuff((ObjAIBase)unit, other1, new Buffs.MonkeyKingNimbusKickClone(nextBuffVars_TargetPos, nextBuffVars_dashSpeed, nextBuffVars_DamageVar), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0.25f, true, false, true);
                            unitsHit++;
                        }
                    }
                    else
                    {
                        targetPos = GetUnitPosition(unit);
                        nextBuffVars_TargetPos = targetPos;
                        other1 = SpawnMinion("MonkeyKingClone", "MonkeyKingFlying", "Aggro.lua", ownerPos, teamID, false, false, false, false, false, true, 0, false, false, (Champion)owner);
                        AddBuff((ObjAIBase)unit, other1, new Buffs.MonkeyKingNimbusKickClone(nextBuffVars_TargetPos, nextBuffVars_dashSpeed, nextBuffVars_DamageVar), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0.25f, true, false, true);
                        unitsHit++;
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class MonkeyKingNimbus : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "AkaliShadowDance",
            BuffTextureName = "AkaliShadowDance.dds",
        };
    }
}