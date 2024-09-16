namespace Spells
{
    public class AhriTumble : SpellScript
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
            int count = GetBuffCountFromAll(owner, nameof(Buffs.AhriTumble));
            if (count == 0)
            {
                AddBuff(owner, owner, new Buffs.AhriTumble(), 2, 2, 10, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0.25f, true, false, false);
                SetSlotSpellCooldownTimeVer2(0.75f, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, true);
                SetPARCostInc(owner, 3, SpellSlotType.SpellSlots, -150, PrimaryAbilityResourceType.MANA);
            }
            else if (count == 1)
            {
                SpellBuffRemoveStacks(owner, owner, nameof(Buffs.AhriTumble), 1);
            }
            else if (count == 2)
            {
                SpellBuffRemoveStacks(owner, owner, nameof(Buffs.AhriTumble), 1);
                SetSlotSpellCooldownTimeVer2(0.75f, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, true);
            }
            SpellEffectCreate(out _, out _, "Ahri_SpiritRush_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_GLB_GROUND_LOC", default, owner, "BUFFBONE_GLB_GROUND_LOC", default, false, false, false, false, false);
            SpellEffectCreate(out _, out _, "Ahri_Orb_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_GLB_WEAPON_1", default, owner, "BUFFBONE_GLB_WEAPON_1", default, false, false, false, false, false);
            Vector3 ownerPos = GetUnitPosition(owner); // UNUSED
            Vector3 targetPos = GetSpellTargetPos(spell);
            float moveSpeed = GetMovementSpeed(owner);
            float dashSpeed = moveSpeed + 1200;
            float distance = DistanceBetweenObjectAndPoint(owner, targetPos);
            if (distance > 500)
            {
                FaceDirection(owner, targetPos);
                targetPos = GetPointByUnitFacingOffset(owner, 500, 0);
                distance = 500;
                Vector3 nearestAvailablePos = GetNearestPassablePosition(owner, targetPos);
                float distance2 = DistanceBetweenPoints(nearestAvailablePos, targetPos);
                if (distance2 > 25)
                {
                    targetPos = GetPointByUnitFacingOffset(owner, 600, 0);
                    distance = 600;
                }
            }
            Vector3 nextBuffVars_TargetPos = targetPos;
            float nextBuffVars_Distance = distance;
            float nextBuffVars_dashSpeed = dashSpeed;
            AddBuff(owner, owner, new Buffs.AhriTumbleKick(nextBuffVars_TargetPos, nextBuffVars_Distance, nextBuffVars_dashSpeed), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0.25f, true, false, false);
        }
    }
}
namespace Buffs
{
    public class AhriTumble : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "AhriTumble",
            BuffTextureName = "Ahri_SpiritRush.dds",
            PersistsThroughDeath = true,
        };
        float newCd;
        int[] effect0 = { 90, 80, 70, 0, 0 };
        public override void OnDeactivate(bool expired)
        {
            int count = GetBuffCountFromAll(owner, nameof(Buffs.AhriTumble));
            if (count == 0)
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                newCd = effect0[level - 1];
                float cooldownStat = GetPercentCooldownMod(owner);
                float multiplier = 1 + cooldownStat;
                float newCooldown = multiplier * newCd;
                SetSlotSpellCooldownTimeVer2(newCooldown, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, true);
                SetPARCostInc((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
            }
        }
    }
}