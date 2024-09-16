namespace Spells
{
    public class LeonaZenithBladeMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 60, 100, 140, 180, 220 };
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            SetCanAttack(owner, true);
            SetCanMove(owner, true);
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 ownerPos = GetUnitPosition(owner); // UNUSED
            int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 3000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectDead, 1, nameof(Buffs.LeonaZenithBladeBuffOrder), true))
            {
                FaceDirection(owner, unit.Position3D);
                float distance = DistanceBetweenObjects(owner, unit);
                float finalDistance = distance + 225;
                Vector3 targetPos = GetPointByUnitFacingOffset(owner, finalDistance, 0);
                AddBuff(owner, unit, new Buffs.LeonaZenithBladeRoot(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.CHARM, 0, true, false, false);
                Vector3 nextBuffVars_Destination = targetPos;
                float nextBuffVars_Distance = distance;
                AddBuff((ObjAIBase)unit, owner, new Buffs.LeonaZenithBladeMissile(nextBuffVars_Destination, nextBuffVars_Distance), 1, 1, 1.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                SpellEffectCreate(out _, out _, "Leona_ZenithBlade_trail.troy", default, teamID, 225, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, default, default, false, false);
            }
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(attacker);
            if (target is Champion)
            {
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, attacker.Position3D, 3000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf | SpellDataFlags.AffectDead, nameof(Buffs.LeonaZenithBladeBuffOrder), true))
                {
                    SpellBuffClear(unit, nameof(Buffs.LeonaZenithBladeBuffOrder));
                }
                AddBuff(attacker, target, new Buffs.LeonaZenithBladeBuffOrder(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            BreakSpellShields(target);
            AddBuff(attacker, target, new Buffs.LeonaSunlight(), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            float damageToDeal = effect0[level - 1];
            ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, attacker);
            if (target is not Champion)
            {
                SpellEffectCreate(out _, out _, "Leona_ZenithBlade_sound.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class LeonaZenithBladeMissile : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Blind",
            BuffTextureName = "Teemo_TranquilizingShot.dds",
            SpellToggleSlot = 1,
        };
        Vector3 destination;
        float distance;
        public LeonaZenithBladeMissile(Vector3 destination = default, float distance = default)
        {
            this.destination = destination;
            this.distance = distance;
        }
        public override void OnActivate()
        {
            //RequireVar(this.destination);
            //RequireVar(this.distance);
            Move(owner, destination, 1900, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, distance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
        }
        public override void OnMoveEnd()
        {
            SpellEffectCreate(out _, out _, "Leona_ZenithBlade_arrive.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
        }
        public override void OnMoveSuccess()
        {
            if (attacker is Champion && attacker.Team != owner.Team)
            {
                IssueOrder(owner, OrderType.AttackTo, default, attacker);
            }
        }
    }
}