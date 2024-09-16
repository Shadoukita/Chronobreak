namespace Spells
{
    public class RocketJump : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 20f, 18f, 16f, 14f, 12f, },
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
            SpellFXOverrideSkins = new[] { "RocketTristana", },
        };
        int[] effect0 = { 70, 115, 160, 205, 250 };
        public override bool CanCast()
        {
            bool returnValue = true;
            bool canMove = GetCanMove(owner);
            bool canCast = GetCanCast(owner);
            if (!canMove)
            {
                returnValue = false;
            }
            if (!canCast)
            {
                returnValue = false;
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            float gravityVar;
            float speedVar;
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            if (distance >= 900)
            {
                gravityVar = 50;
                speedVar = 1200;
                FaceDirection(owner, targetPos);
                targetPos = GetPointByUnitFacingOffset(owner, 900, 0);
                distance = 900;
            }
            else if (distance >= 600)
            {
                gravityVar = 50;
                speedVar = 1200;
            }
            else if (distance >= 500)
            {
                gravityVar = 80;
                speedVar = 1200;
            }
            else if (distance >= 400)
            {
                gravityVar = 100;
                speedVar = 1100;
            }
            else if (distance >= 300)
            {
                gravityVar = 120;
                speedVar = 1025;
            }
            else if (distance >= 200)
            {
                gravityVar = 150;
                speedVar = 975;
            }
            else if (distance >= 100)
            {
                gravityVar = 300;
                speedVar = 800;
            }
            else //if(distance >= 0)
            {
                gravityVar = 1000;
                speedVar = 800;
            }
            Move(owner, targetPos, speedVar, gravityVar, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, distance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            float nextBuffVars_Damage = effect0[level - 1];
            AddBuff(attacker, attacker, new Buffs.RocketJump(nextBuffVars_Damage), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class RocketJump : BuffScript
    {
        float damage;
        EffectEmitter a;
        public RocketJump(float damage = default)
        {
            this.damage = damage;
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            //RequireVar(this.damage);
            SpellEffectCreate(out a, out _, "tristana_rocketJump_cas.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, owner, default, default, true, false, false, false, false);
            int ownerSkinID = GetSkinID(owner); // UNUSED
            PlayAnimation("Spell2", 0, owner, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(a);
        }
        public override void OnMoveEnd()
        {
            SpellBuffRemoveCurrent(owner);
        }
        public override void OnMoveSuccess()
        {
            TeamId teamID = GetTeamID_CS(owner);
            int ownerSkinID = GetSkinID(owner);
            if (ownerSkinID == 6)
            {
                SpellEffectCreate(out _, out _, "tristana_rocket_rocketJump_land.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out _, out _, "tristana_rocketJump_land.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            }
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                ApplyDamage((ObjAIBase)owner, unit, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.8f, 1, false, false, attacker);
                SpellEffectCreate(out _, out _, "tristana_rocketJump_unit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                AddBuff((ObjAIBase)owner, unit, new Buffs.RocketJumpSlow(), 1, 1, 2.5f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
            }
        }
    }
}