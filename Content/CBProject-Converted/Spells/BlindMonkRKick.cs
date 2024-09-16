namespace Spells
{
    public class BlindMonkRKick : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            ChainMissileParameters = new()
            {
                CanHitCaster = false,
                CanHitEnemies = true,
                CanHitFriends = false,
                CanHitSameTarget = false,
                CanHitSameTargetConsecutively = false,
                MaximumHits = 4,
            },
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 200, 400, 600, 600, 600 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float baseDamage = effect0[level - 1];
            float bonusAD = GetFlatPhysicalDamageMod(owner);
            bonusAD *= 2;
            float primaryDamage = baseDamage + bonusAD;
            float secondaryDamage = primaryDamage / 1;
            float nextBuffVars_SecondaryDamage = secondaryDamage;
            float distanceToAdd = DistanceBetweenObjects(owner, target);
            float distanceToKick = distanceToAdd + 800;
            FaceDirection(owner, target.Position3D);
            Vector3 tarPos = GetPointByUnitFacingOffset(owner, distanceToKick, 0);
            Vector3 nextBuffVars_TarPos = tarPos;
            AddBuff(owner, target, new Buffs.BlindMonkRKick(nextBuffVars_SecondaryDamage, nextBuffVars_TarPos), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.STUN, 0, true, false, false);
            ApplyDamage(attacker, target, primaryDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, true, attacker);
        }
    }
}
namespace Buffs
{
    public class BlindMonkRKick : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "blindMonk_R_self_mis.troy", "", },
            BuffName = "BlindMonkDragonsRage",
            BuffTextureName = "BlindMonkR.dds",
        };
        float secondaryDamage;
        Vector3 tarPos;
        public BlindMonkRKick(float secondaryDamage = default, Vector3 tarPos = default)
        {
            this.secondaryDamage = secondaryDamage;
            this.tarPos = tarPos;
        }
        public override void OnActivate()
        {
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
            //RequireVar(this.secondaryDamage);
            //RequireVar(this.tarPos);
            Vector3 tarPos = this.tarPos;
            Move(target, tarPos, 1000, 5, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 800, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanAttack(owner, true);
            SetCanCast(owner, true);
            SetCanMove(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
        }
        public override void OnUpdateActions()
        {
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 160, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if (unit != owner && GetBuffCountFromCaster(unit, unit, nameof(Buffs.BlindMonkRMarker)) == 0)
                {
                    AddBuff((ObjAIBase)unit, unit, new Buffs.BlindMonkRMarker(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    BreakSpellShields(unit);
                    TeamId teamID = GetTeamID_CS(owner);
                    SpellEffectCreate(out _, out _, "blind_monk_ult_unit_impact.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                    ApplyDamage(attacker, unit, secondaryDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, true, attacker);
                    AddBuff(attacker, unit, new Buffs.BlindMonkRDamage(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.STUN, 0, true, false, true);
                }
            }
        }
        public override void OnMoveEnd()
        {
            SpellBuffRemoveCurrent(owner);
        }
    }
}