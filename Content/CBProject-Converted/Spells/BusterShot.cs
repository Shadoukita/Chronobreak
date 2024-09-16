namespace Spells
{
    public class BusterShot : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
            SpellFXOverrideSkins = new[] { "RocketTristana", },
        };
        int[] effect0 = { 600, 800, 1000 };
        int[] effect1 = { 300, 400, 500 };
        public override void AdjustCastInfo()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.DrawABead)) > 0)
            {
                int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float bonusCastRange = level * 90;
                float newCastRange = bonusCastRange + 600;
                SetSpellCastRange(newCastRange);
            }
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_KnockBackDistance = effect0[level - 1];
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, target.Position3D, 200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                SpellEffectCreate(out _, out _, "tristana_bustershot_unit_impact.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                AddBuff(attacker, unit, new Buffs.BusterShot(nextBuffVars_KnockBackDistance), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
            }
            ApplyDamage(attacker, target, effect1[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 1.5f, 1, false, false, attacker);
        }
    }
}
namespace Buffs
{
    public class BusterShot : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Buster Shot",
            BuffTextureName = "Tristana_BusterShot.dds",
            PopupMessage = new[] { "game_floatingtext_Knockup", },
        };
        float knockBackDistance;
        public BusterShot(float knockBackDistance = default)
        {
            this.knockBackDistance = knockBackDistance;
        }
        public override void OnActivate()
        {
            //RequireVar(this.knockBackDistance);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            SetCanCast(owner, false);
            float distance = DistanceBetweenObjects(owner, attacker);
            distance += knockBackDistance;
            MoveAway(owner, attacker.Position3D, 1500, 5, 5 + distance, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 0, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            ApplyAssistMarker(attacker, owner, 10);
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
            SetCanMove(owner, false);
            SetCanCast(owner, false);
        }
    }
}