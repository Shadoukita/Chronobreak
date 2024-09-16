namespace Spells
{
    public class OrianaDetonateCommand : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        EffectEmitter particle; // UNUSED
        int[] effect0 = { 150, 225, 300 };
        float[] effect1 = { -0.4f, -0.5f, -0.6f };
        int[] effect2 = { 415, 415, 415, 415, 415 };
        public override void SelfExecute()
        {
            Vector3 targetPos = Vector3.Zero;
            int currentType = 0;
            AttackableUnit other1 = null;

            AddBuff(owner, owner, new Buffs.OrianaGlobalCooldown(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            float damage = effect0[level - 1];
            bool deployed = false;
            float nextBuffVars_MoveSpeedMod = effect1[level - 1]; // UNUSED
            float rangeVar = effect2[level - 1];
            float selfAP = GetFlatMagicDamageMod(owner);
            float bonusDamage = selfAP * 0.7f;
            damage += bonusDamage;
            TeamId teamID = GetTeamID_CS(owner);
            foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, 1, nameof(Buffs.OrianaGhost), true))
            {
                deployed = true;
                targetPos = GetUnitPosition(unit);
                if (unit is Champion)
                {
                    currentType = 0;
                }
                else
                {
                    currentType = 1;
                }
                other1 = unit;
            }
            if (!deployed)
            {
                if (GetBuffCountFromCaster(owner, default, nameof(Buffs.OriannaBallTracker)) > 0)
                {
                    currentType = 5;
                    targetPos = charVars.BallPosition;
                }
                else
                {
                    targetPos = GetUnitPosition(owner);
                    currentType = 3;
                    targetPos = GetPointByUnitFacingOffset(owner, 0, 0);
                }
            }
            if (currentType != charVars.UltimateType)
            {
                currentType = 5;
                targetPos = charVars.UltimateTargetPos;
            }
            if (currentType == 0)
            {
                bool isStealthed = GetStealthed(other1);
                if (!isStealthed)
                {
                    SpellEffectCreate(out particle, out _, "Oriana_Shockwave_nova_ally.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, other1, "SpinnigTopRidge", targetPos, default, default, targetPos, true, false, false, false, false);
                }
                else
                {
                    SpellEffectCreate(out particle, out _, "Oriana_Shockwave_nova.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, default, default, targetPos, true, false, false, false, false);
                }
            }
            else if (currentType == 1)
            {
                SpellEffectCreate(out particle, out _, "Oriana_Shockwave_nova.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, other1, "SpinnigTopRidge", targetPos, default, default, targetPos, true, false, false, false, false);
            }
            else if (currentType == 2)
            {
                AttackableUnit unit = null; // UNITIALIZED
                SpellEffectCreate(out particle, out _, "Oriana_Shockwave_nova.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "SpinnigTopRidge", targetPos, default, default, targetPos, true, false, false, false, false);
            }
            else if (currentType == 3)
            {
                SpellEffectCreate(out particle, out _, "Oriana_Shockwave_nova.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "SpinnigTopRidge", targetPos, default, default, targetPos, true, false, false, false, false);
            }
            else if (currentType == 5)
            {
                SpellEffectCreate(out particle, out _, "Oriana_Shockwave_nova.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, default, default, targetPos, true, false, false, false, false);
            }
            foreach (AttackableUnit unit in GetUnitsInArea(owner, targetPos, rangeVar, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                bool canSee = CanSeeTarget(owner, unit);
                bool validTarget = true;
                if (unit is not Champion && !canSee)
                {
                    validTarget = false;
                }
                if (validTarget)
                {
                    BreakSpellShields(unit);
                    Vector3 oldPos = GetPointByUnitFacingOffset(unit, 425, 0);
                    FaceDirection(unit, targetPos);
                    SpellEffectCreate(out _, out _, "OrianaDetonate_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                    ApplyDamage(owner, unit, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, owner);
                    Vector3 newPos = GetPointByUnitFacingOffset(unit, 425, -180);
                    FaceDirection(unit, oldPos);
                    float nextBuffVars_Distance = 790;
                    float nextBuffVars_IdealDistance = 870;
                    float nextBuffVars_Gravity = 25;
                    float nextBuffVars_Speed = 775;
                    Vector3 nextBuffVars_Center = newPos;
                    AddBuff(owner, unit, new Buffs.OrianaStun(), 1, 1, 0.75f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                    AddBuff(owner, unit, new Buffs.MoveAwayCollision(nextBuffVars_Speed, nextBuffVars_Gravity, nextBuffVars_Center, nextBuffVars_Distance, nextBuffVars_IdealDistance), 1, 1, 0.75f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                }
            }
        }
    }
}
namespace Buffs
{
    public class OrianaDetonateCommand : BuffScript
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