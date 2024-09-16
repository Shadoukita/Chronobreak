namespace Spells
{
    public class IreliaGatotsu : SpellScript
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
        int[] effect0 = { 20, 50, 80, 110, 140 };
        public override bool CanCast()
        {
            bool returnValue = true;
            bool canMove = GetCanMove(owner);
            bool canCast = GetCanCast(owner);
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
            return returnValue;
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            SpellEffectCreate(out _, out _, "irelia_gotasu_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            Vector3 ownerPos = GetUnitPosition(owner);
            SpellEffectCreate(out _, out _, "irelia_gotasu_cast_01.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out _, out _, "irelia_gotasu_cast_02.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
            Vector3 targetPos = GetSpellTargetPos(spell);
            float moveSpeed = GetMovementSpeed(owner);
            float dashSpeed = moveSpeed + 1400;
            float distance = DistanceBetweenObjects(owner, target);
            Vector3 nextBuffVars_TargetPos = targetPos;
            float nextBuffVars_Distance = distance;
            float nextBuffVars_dashSpeed = dashSpeed;
            float damageVar = effect0[level - 1];
            float baseDamage = GetBaseAttackDamage(owner);
            float nextBuffVars_DamageVar = damageVar + baseDamage;
            AddBuff((ObjAIBase)target, owner, new Buffs.IreliaGatotsuDash(nextBuffVars_TargetPos, nextBuffVars_Distance, nextBuffVars_dashSpeed, nextBuffVars_DamageVar), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0.25f, true, false, false);
        }
    }
}
namespace Buffs
{
    public class IreliaGatotsu : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "IreliaGatotsu",
            BuffTextureName = "Irelia_Bladesurge.dds",
        };
        public override void OnDeactivate(bool expired)
        {
            if (IsDead(attacker))
            {
                SpellEffectCreate(out _, out _, "irelia_gotasu_ability_indicator.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                SpellEffectCreate(out _, out _, "irelia_gotasu_mana_refresh.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                IncPAR(owner, 35, PrimaryAbilityResourceType.MANA);
            }
        }
        public override void OnUpdateActions()
        {
            SpellBuffRemoveCurrent(owner);
        }
    }
}