namespace Spells
{
    public class TalonRakeMissileOne : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = { 30, 55, 80, 105, 130 };
        float[] effect1 = { -0.2f, -0.25f, -0.3f, -0.35f, -0.4f };
        float[] effect2 = { -0.15f, -0.2f, -0.25f, -0.3f, -0.35f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId ownerTeamID = GetTeamID_CS(owner);
            int count = GetBuffCountFromCaster(target, target, nameof(Buffs.TalonRakeMissileOneMarker));
            if (count == 0 && (!GetStealthed(target) || target is Champion || CanSeeTarget(owner, target)))
            {
                SpellEffectCreate(out _, out _, "talon_w_tar.troy", default, ownerTeamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, target.Position3D, target, default, default, true, false, false, false, false);
                AddBuff((ObjAIBase)target, target, new Buffs.TalonRakeMissileOneMarker(), 9, 1, 0.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                BreakSpellShields(target);
                float baseDamage = GetBaseAttackDamage(owner);
                float totalAD = GetTotalAttackDamage(owner);
                baseDamage = totalAD - baseDamage;
                baseDamage *= 0.6f;
                int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float bonusDamage = effect0[level - 1];
                baseDamage += bonusDamage;
                ApplyDamage(owner, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                //level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float nextBuffVars_MoveSpeedMod = effect1[level - 1];
                AddBuff(attacker, target, new Buffs.TalonSlow(nextBuffVars_MoveSpeedMod), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                //if (level >= 1)
                //{
                //   float nextBuffVars_MovementSpeedMod = effect2[level - 1]; // UNUSED
                //}
            }
        }
    }
}
namespace Buffs
{
    public class TalonRakeMissileOne : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "TalonRakeMissileOne",
            IsDeathRecapSource = true,
        };
        int[] effect0 = { 14, 13, 12, 11, 10 };
        public override void OnDeactivate(bool expired)
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            Vector3 ownerPos = GetUnitPosition(owner);
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, ownerPos, 3000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.NotAffectSelf | SpellDataFlags.AffectUntargetable, default, true))
            {
                if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.TalonRakeMarker)) > 0)
                {
                    Vector3 unitPos = GetUnitPosition(unit);
                    float newDistance = DistanceBetweenObjects(owner, unit);
                    if (newDistance > 100)
                    {
                        SpellCast((ObjAIBase)owner, owner, ownerPos, default, 2, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, unitPos);
                    }
                    SpellBuffRemove(unit, nameof(Buffs.TalonRakeMarker), (ObjAIBase)owner, 0);
                    SetInvulnerable(unit, false);
                    SetTargetable(unit, true);
                    ApplyDamage((ObjAIBase)unit, unit, 50000, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 0, false, false, attacker);
                }
            }
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float cooldownVal = effect0[level - 1];
            float flatCDVal = 0;
            float flatCD = GetPercentCooldownMod(owner);
            flatCDVal = cooldownVal * flatCD;
            cooldownVal += flatCDVal;
        }
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            if (spellName == nameof(Spells.TalonRakeMissileOne))
            {
                Vector3 ownerPos = GetUnitPosition(owner);
                TeamId teamOfOwner = GetTeamID_CS(owner);
                Minion other3 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", missileEndPosition, teamOfOwner, false, true, false, false, false, true, 0, false, true, (Champion)owner);
                FaceDirection(other3, ownerPos);
                SetInvulnerable(other3, true);
                SetTargetable(other3, false);
                charVars.MissileNumber++;
                Vector3 unitPos = GetUnitPosition(other3); // UNUSED
                int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots); // UNUSED
                if (charVars.MissileNumber > 2)
                {
                    charVars.MissileNumber = 0;
                    SpellBuffRemove(owner, nameof(Buffs.TalonRakeMissileOne), (ObjAIBase)owner, 0);
                }
                AddBuff((ObjAIBase)owner, other3, new Buffs.TalonRakeMarker(), 1, 1, 100000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                DestroyMissile(charVars.MISSILEID2);
            }
        }
        public override void OnLaunchMissile(SpellMissile missileId)
        {
            charVars.MISSILEID2 = missileId;
        }
    }
}