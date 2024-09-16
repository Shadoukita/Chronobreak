namespace Spells
{
    public class Headbutt : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = { 85, 130, 175, 220, 265 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            FaceDirection(owner, target.Position3D);
            float nextBuffVars_Damage = effect0[level - 1];
            float distance = DistanceBetweenObjects(attacker, target);
            float factor = distance / 650;
            factor = Math.Max(factor, 0.25f);
            factor = Math.Min(factor, 0.9f);
            PlayAnimation("Spell2", factor, owner, false, false, false);
            Vector3 targetPos = GetUnitPosition(target);
            Move(owner, targetPos, 1500, 2, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, distance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            TeamId teamID = GetTeamID_CS(owner);
            Minion other1 = SpawnMinion("placeholder", "TestCube", "idle.lua", owner.Position3D, teamID, false, true, false, true, false, true, 0, false, false, (Champion)owner);
            FaceDirection(other1, target.Position3D);
            AddBuff(owner, target, new Buffs.AlistarHeadbuttMarker(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(other1, other1, new Buffs.ExpirationTimer(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(other1, owner, new Buffs.Headbutt(nextBuffVars_Damage), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0.25f, true, false, false);
            AddBuff(owner, owner, new Buffs.AlistarTrample(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, false, false, false);
        }
    }
}
namespace Buffs
{
    public class Headbutt : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Charging",
            BuffTextureName = "Minotaur_Headbutt.dds",
        };
        float damage;
        public Headbutt(float damage = default)
        {
            this.damage = damage;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damage);
        }
        public override void OnMoveEnd()
        {
            SpellBuffRemove(owner, nameof(Buffs.Headbutt), attacker, 0);
        }
        public override void OnMoveSuccess()
        {
            ObjAIBase attacker = GetBuffCasterUnit();
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.AlistarHeadbuttMarker), true))
            {
                if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.AlistarHeadbuttMarker)) > 0)
                {
                    BreakSpellShields(unit);
                    ApplyDamage((ObjAIBase)owner, unit, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.7f, 1, false, false, (ObjAIBase)owner);
                    ApplyAssistMarker((ObjAIBase)owner, unit, 10);
                    AddBuff(attacker, unit, new Buffs.HeadbuttTarget(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                    SpellBuffRemove(owner, nameof(Buffs.Headbutt), attacker, 0);
                }
            }
        }
    }
}