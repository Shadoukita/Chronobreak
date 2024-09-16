namespace Spells
{
    public class OrianaIzunaCommand : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 60, 100, 140, 180, 220 };
        public override void SelfExecute()
        {
            Vector3 castPos = Vector3.Zero;

            Vector3 nextBuffVars_TargetPos; // UNUSED
            Vector3 nextBuffVars_CastPos; // UNUSED
            float nextBuffVars_TotalDamage;
            SpellBuffClear(owner, nameof(Buffs.OrianaGhostSelf));
            SetSpellOffsetTarget(3, SpellSlotType.SpellSlots, nameof(Spells.JunkName), SpellbookType.SPELLBOOK_CHAMPION, owner, owner);
            SetSpellOffsetTarget(1, SpellSlotType.SpellSlots, nameof(Spells.JunkName), SpellbookType.SPELLBOOK_CHAMPION, owner, owner);
            SpellBuffClear(owner, nameof(Buffs.OrianaBlendDelay));
            Vector3 targetPos = GetSpellTargetPos(spell);
            FaceDirection(owner, targetPos);
            Vector3 ownerPos = GetUnitPosition(owner);
            charVars.IzunaPercent = 1;
            float castRange = 885;
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            if (distance > castRange)
            {
                FaceDirection(owner, targetPos);
                targetPos = GetPointByUnitFacingOffset(owner, castRange, 0);
            }
            if (distance <= 150)
            {
                FaceDirection(owner, targetPos);
                targetPos = GetPointByUnitFacingOffset(owner, 150, 0);
            }
            bool nextBuffVars_GhostAlive = charVars.GhostAlive; // UNUSED
            bool deployed = false;
            bool shiftWithoutMissile = false;
            foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf | SpellDataFlags.AffectUntargetable, 1, nameof(Buffs.OrianaGhost), true))
            {
                deployed = true;
                targetPos = GetSpellTargetPos(spell);
                distance = DistanceBetweenObjectAndPoint(owner, targetPos);
                if (distance > castRange)
                {
                    targetPos = GetPointByUnitFacingOffset(owner, castRange, 0);
                }
                castPos = GetUnitPosition(unit);
                SpellBuffClear(unit, nameof(Buffs.OrianaGhost));
                distance = DistanceBetweenPoints(castPos, targetPos);
                if (distance >= 75)
                {
                    nextBuffVars_TargetPos = targetPos;
                    nextBuffVars_CastPos = castPos;
                    AddBuff(owner, owner, new Buffs.OrianaIzuna(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    SpellCast(owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, castPos);
                }
                else
                {
                    shiftWithoutMissile = true;
                }
            }
            if (!deployed)
            {
                if (GetBuffCountFromCaster(owner, default, nameof(Buffs.OriannaBallTracker)) > 0)
                {
                    castPos = charVars.BallPosition;
                    SpellBuffClear(owner, nameof(Buffs.OriannaBallTracker));
                    targetPos = GetSpellTargetPos(spell);
                    distance = DistanceBetweenObjectAndPoint(owner, targetPos);
                    if (distance > castRange)
                    {
                        targetPos = GetPointByUnitFacingOffset(owner, castRange, 0);
                    }
                    distance = DistanceBetweenPoints(charVars.BallPosition, targetPos);
                    if (distance >= 75)
                    {
                        nextBuffVars_TargetPos = targetPos;
                        nextBuffVars_CastPos = charVars.BallPosition;
                        AddBuff(owner, owner, new Buffs.OrianaIzuna(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        SpellCast(owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, charVars.BallPosition);
                    }
                    else
                    {
                        castPos = charVars.BallPosition;
                        shiftWithoutMissile = true;
                    }
                }
                else
                {
                    castPos = GetUnitPosition(owner);
                    distance = DistanceBetweenPoints(castPos, targetPos);
                    if (distance >= 75)
                    {
                        nextBuffVars_TargetPos = targetPos;
                        nextBuffVars_CastPos = castPos;
                        AddBuff(owner, owner, new Buffs.OrianaIzuna(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        SpellCast(owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, castPos);
                    }
                    else
                    {
                        shiftWithoutMissile = true;
                    }
                }
            }
            if (shiftWithoutMissile)
            {
                TeamId teamID = GetTeamID_CS(owner);
                Minion other3 = SpawnMinion("TheDoomBall", "OriannaBall", "idle.lua", targetPos, teamID, false, true, false, true, true, true, 0, default, true, (Champion)owner);
                int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                SpellEffectCreate(out _, out _, "Oriana_Izuna_nova.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, default, default, targetPos, true, default, default, false, false);
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, other3.Position3D, 175, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, false))
                {
                    float baseDamage = effect0[level - 1];
                    float aP = GetFlatMagicDamageMod(owner);
                    float bonusDamage = aP * 0.6f;
                    float totalDamage = bonusDamage + baseDamage;
                    totalDamage *= charVars.IzunaPercent;
                    charVars.IzunaPercent *= 0.9f;
                    charVars.IzunaPercent = Math.Max(0.4f, charVars.IzunaPercent);
                    nextBuffVars_TotalDamage = totalDamage;
                    if (GetBuffCountFromCaster(unit, default, nameof(Buffs.OrianaIzunaDamage)) == 0)
                    {
                        BreakSpellShields(unit);
                        AddBuff(owner, unit, new Buffs.OrianaIzunaDamage(nextBuffVars_TotalDamage), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
            }
            AddBuff(owner, owner, new Buffs.OrianaGlobalCooldown(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            Vector3 leftPoint = GetPointByUnitFacingOffset(owner, 500, 90);
            Vector3 rightPoint = GetPointByUnitFacingOffset(owner, 500, -90);
            float leftDistance = DistanceBetweenPoints(castPos, leftPoint);
            float rightDistance = DistanceBetweenPoints(castPos, rightPoint);
            if (leftDistance >= rightDistance)
            {
                PlayAnimation("Spell1b", 1, owner, true, false, false);
            }
            else
            {
                PlayAnimation("Spell1", 1, owner, true, false, false);
            }
            AddBuff(owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class OrianaIzunaCommand : BuffScript
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