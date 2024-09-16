namespace Spells
{
    public class ViktorDeathRay : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 70, 115, 160, 205, 250 };
        public override void SelfExecute()
        {
            PlayAnimation("Spell3", 0, owner, false, false, false);
            TeamId teamOfOwner = GetTeamID_CS(attacker);
            Vector3 targetPosStart = GetSpellTargetPos(spell);
            Vector3 targetPosEnd = GetSpellDragEndPos(spell);
            Minion other1 = SpawnMinion("MaokaiSproutling", "MaokaiSproutling", "idle.lua", targetPosStart, teamOfOwner, false, false, false, false, true, true, 0, false, true, (Champion)owner);
            AddBuff(attacker, other1, new Buffs.ViktorExpirationTimer(), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            FaceDirection(other1, targetPosEnd);
            targetPosEnd = GetPointByUnitFacingOffset(other1, 700, 0);
            TeamId teamID = GetTeamID_CS(owner);
            AddBuff(attacker, other1, new Buffs.ViktorDeathRay(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            Move(other1, targetPosEnd, 550, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 500, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            float baseDamage = effect0[level - 1];
            float aPVAL = GetFlatMagicDamageMod(owner);
            float aPBONUS = aPVAL * 0.7f;
            float totalDamage = aPBONUS + baseDamage;
            float damageForDot = totalDamage * 0.075f;
            foreach (AttackableUnit unit in GetUnitsInArea(owner, targetPosStart, 140, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.ViktorDeathRayBuff)) == 0)
                {
                    BreakSpellShields(unit);
                    ApplyDamage(attacker, unit, totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                    SpellEffectCreate(out _, out _, "ViktorEntropicBeam_hit.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                    AddBuff(owner, unit, new Buffs.ViktorDeathRayBuff(), 1, 1, 1.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.ViktorAugmentE)) > 0)
                    {
                        float nextBuffVars_DamageForDot = damageForDot;
                        AddBuff(owner, unit, new Buffs.ViktorDeathRayDOT(nextBuffVars_DamageForDot), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class ViktorDeathRay : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "GalioRighteousGust",
            BuffTextureName = "",
        };
        EffectEmitter particleID;
        EffectEmitter a;
        EffectEmitter hit;
        float lastTimeExecuted;
        int[] effect0 = { 70, 115, 160, 205, 250 };
        /*
        //TODO: Uncomment and fix
        public override void OnActivate()
        {
            Vector3 laserPos; // UNITIALIZED
            SpellEffectCreate(out this.particleID, out _, "ViktorEntropicBeam_beam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "Up_Hand", default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.a, out _, "ViktorEntropicBeam_tar_beam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.hit, out _, "ViktorEntropicBeam_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, default, default, laserPos, false, false, false, false, false);
        }
        */
        public override void OnDeactivate(bool expired)
        {
            ApplyDamage((ObjAIBase)owner, owner, 9999, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 0, false, false, attacker);
            SpellEffectRemove(particleID);
            SpellEffectRemove(a);
            SpellEffectRemove(hit);
        }
        public override void OnUpdateActions()
        {
            ObjAIBase caster = GetBuffCasterUnit();
            TeamId ownerTeam = GetTeamID_CS(owner);
            Vector3 laserPos = GetUnitPosition(owner);
            int level = GetSlotSpellLevel(caster, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float baseDamage = effect0[level - 1];
            float aPVAL = GetFlatMagicDamageMod(caster);
            float aPBONUS = aPVAL * 0.7f;
            float totalDamage = aPBONUS + baseDamage;
            float damageForDot = totalDamage * 0.075f;
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, true))
            {
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, laserPos, 140, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    if (GetBuffCountFromCaster(unit, caster, nameof(Buffs.ViktorDeathRayBuff)) == 0)
                    {
                        BreakSpellShields(unit);
                        ApplyDamage(attacker, unit, totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                        SpellEffectCreate(out a, out _, "ViktorEntropicBeam_hit.troy", default, ownerTeam, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                        AddBuff(caster, unit, new Buffs.ViktorDeathRayBuff(), 1, 1, 1.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        if (GetBuffCountFromCaster(caster, caster, nameof(Buffs.ViktorAugmentE)) > 0)
                        {
                            float nextBuffVars_DamageForDot = damageForDot;
                            AddBuff(caster, unit, new Buffs.ViktorDeathRayDOT(nextBuffVars_DamageForDot), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                        }
                    }
                }
            }
        }
        public override void OnMoveEnd()
        {
            SpellBuffRemoveCurrent(owner);
        }
    }
}