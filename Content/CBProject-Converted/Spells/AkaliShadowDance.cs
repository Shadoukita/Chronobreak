namespace Spells
{
    public class AkaliShadowDance : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
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
        int[] effect0 = { 100, 175, 250, 0, 0 };
        public override bool CanCast()
        {
            bool returnValue = true;
            bool canMove = GetCanMove(owner);
            bool canCast = GetCanCast(owner);
            int count = GetBuffCountFromAll(owner, nameof(Buffs.AkaliShadowDance));
            if (count <= 1)
            {
                returnValue = false;
            }
            else
            {
                if (!canMove)
                {
                    returnValue = false;
                }
                else if (!canCast)
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
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int count = GetBuffCountFromAll(owner, nameof(Buffs.AkaliShadowDance));
            if (count > 3)
            {
                SpellBuffRemove(owner, nameof(Buffs.AkaliShadowDance), owner, charVars.DanceTimerCooldown);
            }
            else
            {
                SpellBuffRemove(owner, nameof(Buffs.AkaliShadowDance), owner, 0);
            }
            SpellEffectCreate(out _, out _, "akali_shadowDance_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            Vector3 ownerPos = GetUnitPosition(owner);
            SpellEffectCreate(out _, out _, "akali_shadowDance_return_02.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out _, out _, "akali_shadowDance_return.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
            Vector3 targetPos = GetSpellTargetPos(spell);
            float moveSpeed = GetMovementSpeed(owner);
            float dashSpeed = moveSpeed + 1600;
            float distance = DistanceBetweenObjects(owner, target);
            Vector3 nextBuffVars_TargetPos = targetPos;
            float nextBuffVars_Distance = distance;
            float nextBuffVars_dashSpeed = dashSpeed;
            int nextBuffVars_DamageVar = effect0[level - 1];
            AddBuff((ObjAIBase)target, owner, new Buffs.AkaliShadowDanceKick(nextBuffVars_TargetPos, nextBuffVars_Distance, nextBuffVars_dashSpeed, nextBuffVars_DamageVar), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0.25f, true, false, false);
        }
    }
}
namespace Buffs
{
    public class AkaliShadowDance : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "AkaliShadowDance",
            BuffTextureName = "AkaliShadowDance.dds",
            PersistsThroughDeath = true,
        };
        public override void OnUpdateAmmo()
        {
            int count = GetBuffCountFromAll(owner, nameof(Buffs.AkaliShadowDance));
            if (count >= 3)
            {
                AddBuff(attacker, owner, new Buffs.AkaliShadowDance(), 4, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
            }
            else
            {
                AddBuff(attacker, owner, new Buffs.AkaliShadowDance(), 4, 1, charVars.DanceTimerCooldown, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
            }
        }
    }
}