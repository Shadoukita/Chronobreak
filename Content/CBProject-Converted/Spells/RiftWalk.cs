namespace Spells
{
    public class RiftWalk : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 60, 90, 120 };
        public override void SelfExecute()
        {
            Vector3 castPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(ownerPos, castPos);
            FaceDirection(owner, castPos);
            if (distance >= 700)
            {
                castPos = GetPointByUnitFacingOffset(owner, 700, 0);
            }
            TeamId casterID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "Riftwalk_flashback.troy", default, casterID, 250, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
            TeleportToPosition(owner, castPos);
            int count = GetBuffCountFromAll(owner, nameof(Buffs.RiftWalk));
            float damage = effect0[level - 1];
            float count2 = 1 + count;
            float totalDamage = damage * count2;
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 270, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.5f, 1, false, false, attacker);
            }
            float extraCost = 100 * count2;
            SetPARCostInc(owner, 3, SpellSlotType.SpellSlots, extraCost, PrimaryAbilityResourceType.MANA);
            AddBuff(attacker, owner, new Buffs.RiftWalk(), 10, 1, 8, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            SpellEffectCreate(out _, out _, "Riftwalk_flash.troy", default, casterID, 250, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, castPos, target, default, default, true, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class RiftWalk : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            AutoBuffActivateEvent = "",
            BuffName = "RiftWalk",
            BuffTextureName = "Voidwalker_Riftwalk.dds",
        };
        public override void OnDeactivate(bool expired)
        {
            SetPARCostInc((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
        }
    }
}