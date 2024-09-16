namespace Spells
{
    public class BlindMonkR : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            ChainMissileParameters = new()
            {
                CanHitCaster = false,
                CanHitSameTarget = false,
                CanHitSameTargetConsecutively = false,
                MaximumHits = 4,
            },
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override bool CanCast()
        {
            bool returnValue = true;
            returnValue = false;
            foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.BlindMonkRMarker), true))
            {
                returnValue = true;
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.BlindMonkRMarker), true))
            {
                Vector3 targetPos = GetSpellTargetPos(spell);
                TeamId teamID = GetTeamID_CS(owner);
                Minion other2 = SpawnMinion("TestMinion", "TestCubeRender", "idle.lua", targetPos, teamID, false, true, false, false, false, true, 0, default, true);
                AddBuff(other2, other2, new Buffs.BlindMonkRNewMinion(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                FaceDirection(unit, targetPos);
                Vector3 teleportPos = GetPointByUnitFacingOffset(unit, 100, 180);
                TeleportToPosition(owner, teleportPos);
                Vector3 ownerPos = GetUnitPosition(owner);
                SpellCast(owner, unit, targetPos, targetPos, 1, SpellSlotType.ExtraSlots, level, true, false, false, true, false, true, ownerPos);
            }
        }
    }
}
namespace Buffs
{
    public class BlindMonkR : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "UnstoppableForce_cas.troy", "", },
            BuffName = "PoppyDevastatingBlow",
            BuffTextureName = "PoppyDevastatingBlow.dds",
            PersistsThroughDeath = true,
        };
    }
}