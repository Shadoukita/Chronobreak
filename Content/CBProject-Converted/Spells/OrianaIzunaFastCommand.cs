namespace Spells
{
    public class OrianaIzunaFastCommand : SpellScript
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
            SpellBuffClear(owner, nameof(Buffs._0));
            SpellBuffClear(owner, nameof(Buffs.OrianaGhostSelf));
            Vector3 targetPos = GetSpellTargetPos(spell);
            FaceDirection(owner, targetPos);
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            if (distance > 775)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 800, 0);
            }
            bool nextBuffVars_GhostAlive = charVars.GhostAlive; // UNUSED
            bool deployed = false;
            foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.OrianaGhost), true))
            {
                deployed = true;
                targetPos = GetSpellTargetPos(spell);
                distance = DistanceBetweenObjectAndPoint(owner, targetPos);
                if (distance > 775)
                {
                    targetPos = GetPointByUnitFacingOffset(owner, 750, 0);
                }
                castPos = GetUnitPosition(unit);
                nextBuffVars_CastPos = castPos;
                nextBuffVars_TargetPos = targetPos;
                AddBuff(owner, owner, new Buffs.OrianaIzuna(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                SpellBuffClear(unit, nameof(Buffs.OrianaGhost));
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.OrianaDesperatePower)) > 0)
                {
                    SpellCast(owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, castPos);
                }
                else
                {
                    SpellCast(owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, castPos);
                }
            }
            if (!deployed && !charVars.GhostAlive)
            {
                castPos = GetUnitPosition(owner);
                nextBuffVars_CastPos = castPos;
                nextBuffVars_TargetPos = targetPos;
                AddBuff(owner, owner, new Buffs.OrianaIzuna(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.OrianaDesperatePower)) > 0)
                {
                    SpellCast(owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, castPos);
                }
                else
                {
                    SpellCast(owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, castPos);
                }
            }
            PlayAnimation("Spell2", 1.25f, owner, true, false, true);
            AddBuff(owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.OrianaGlobalCooldown(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class OrianaIzunaFastCommand : BuffScript
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