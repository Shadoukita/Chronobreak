namespace Spells
{
    public class TalonShadowAssaultMisOne : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = { 120, 190, 260, 85, 100 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId ownerTeam = GetTeamID_CS(owner);
            int count = GetBuffCountFromCaster(target, target, nameof(Buffs.TalonShadowAssaultMisBuff));
            if (count == 0 && (!GetStealthed(target) || target is Champion || CanSeeTarget(owner, target)))
            {
                SpellEffectCreate(out _, out _, "talon_ult_tar.troy", default, ownerTeam, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, target.Position3D, target, default, default, true, false, false, false, false);
                AddBuff((ObjAIBase)target, target, new Buffs.TalonShadowAssaultMisBuff(), 9, 1, 0.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                BreakSpellShields(target);
                float baseDamage = GetBaseAttackDamage(owner);
                float totalAD = GetTotalAttackDamage(owner);
                baseDamage = totalAD - baseDamage;
                baseDamage *= 0.9f;
                int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float bonusDamage = effect0[level - 1];
                baseDamage += bonusDamage;
                ApplyDamage(owner, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                //level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            }
        }
    }
}
namespace Buffs
{
    public class TalonShadowAssaultMisOne : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "BladeRogue_AOE_Knives",
        };
        int[] effect0 = { 75, 65, 55, 13, 12 };
        public override void OnDeactivate(bool expired)
        {
            SetSpell((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.TalonShadowAssault));
            int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            Vector3 ownerPos = GetUnitPosition(owner);
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, ownerPos, 3000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.NotAffectSelf | SpellDataFlags.AffectUntargetable, default, true))
            {
                if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.TalonShadowAssaultMarker)) > 0)
                {
                    Vector3 unitPos = GetUnitPosition(unit);
                    float newDistance = DistanceBetweenObjects(owner, unit);
                    if (newDistance > 100)
                    {
                        SpellCast((ObjAIBase)owner, owner, ownerPos, default, 4, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, unitPos);
                    }
                    SpellBuffRemove(unit, nameof(Buffs.TalonShadowAssaultMarker), (ObjAIBase)owner, 0);
                    SetInvulnerable(unit, false);
                    SetTargetable(unit, true);
                    ApplyDamage((ObjAIBase)unit, unit, 50000, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 0, false, false, attacker);
                }
            }
            //level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float cooldownVal = effect0[level - 1];
            float flatCDVal = 0;
            float flatCD = GetPercentCooldownMod(owner);
            flatCDVal = cooldownVal * flatCD;
            cooldownVal += flatCDVal;
            SetSlotSpellCooldownTimeVer2(cooldownVal, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            charVars.HasCastR = false;
            SetCanCast(owner, true);
        }
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            if (spellName == nameof(Spells.TalonShadowAssaultMisOne))
            {
                Vector3 ownerPos = GetUnitPosition(owner);
                TeamId teamOfOwner = GetTeamID_CS(owner);
                Minion other3 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", missileEndPosition, teamOfOwner, false, true, false, false, false, true, 0, false, true, (Champion)owner);
                FaceDirection(other3, ownerPos);
                SetInvulnerable(other3, true);
                SetTargetable(other3, false);
                charVars.SAMissileNumber++;
                if (charVars.SAMissileNumber > 8)
                {
                    charVars.SAMissileNumber = 1;
                }
                AddBuff((ObjAIBase)owner, other3, new Buffs.TalonShadowAssaultMarker(), 1, 1, 100000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                DestroyMissile(charVars.MISSILEID);
                SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
                SetCanCast(owner, true);
            }
        }
        public override void OnLaunchMissile(SpellMissile missileId)
        {
            charVars.MISSILEID = missileId;
        }
    }
}