namespace Spells
{
    public class OrianaDissonanceBackup : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override void SelfExecute()
        {
            Vector3 castPos;
            Vector3 nextBuffVars_CastPos; // UNUSED
            Vector3 nextBuffVars_TargetPos; // UNUSED
            Vector3 targetPos = GetSpellTargetPos(spell);
            FaceDirection(owner, targetPos);
            Vector3 ownerPos = GetUnitPosition(owner);
            float castRange = 1640;
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            if (distance > castRange)
            {
                targetPos = GetPointByUnitFacingOffset(owner, castRange, 0);
            }
            bool nextBuffVars_GhostAlive = charVars.GhostAlive; // UNUSED
            bool deployed = false;
            foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.OrianaGhost), true))
            {
                deployed = true;
                targetPos = GetSpellTargetPos(spell);
                distance = DistanceBetweenObjectAndPoint(owner, targetPos);
                if (distance > castRange)
                {
                    targetPos = GetPointByUnitFacingOffset(owner, castRange, 0);
                }
                castPos = GetUnitPosition(unit);
                nextBuffVars_CastPos = castPos;
                nextBuffVars_TargetPos = targetPos;
                SpellCast(owner, default, targetPos, targetPos, 5, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, castPos);
            }
            if (!deployed && !charVars.GhostAlive)
            {
                castPos = GetUnitPosition(owner);
                nextBuffVars_CastPos = castPos;
                nextBuffVars_TargetPos = targetPos;
                SpellCast(owner, default, targetPos, targetPos, 5, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, castPos);
            }
            PlayAnimation("Spell2", 1.25f, owner, true, false, true);
            AddBuff(owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.OrianaGlobalCooldown(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class OrianaDissonanceBackup : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "",
            BuffTextureName = "",
            PopupMessage = new[] { "game_floatingtext_Snared", },
        };
    }
}