namespace Spells
{
    public class GravesChargeShotShot : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 250, 350, 450 };
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            int _1; // UNITIALIZED
            _1 = 1; //TODO: Verify
            TeamId teamIDAttacker = GetTeamID_CS(attacker);
            TeamId teamIDTarget = GetTeamID_CS(target); // UNUSED
            Minion other1 = SpawnMinion("SpellBook1", "SpellBook1", "idle.lua", charVars.BriggsCastPos, teamIDAttacker, false, true, false, true, true, true, 0, false, false, (Champion)owner);
            FaceDirection(other1, missileEndPosition);
            AddBuff(attacker, other1, new Buffs.ExpirationTimer(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            float distance = DistanceBetweenObjectAndPoint(other1, missileEndPosition);
            distance -= 150;
            Vector3 spawnPos = GetPointByUnitFacingOffset(other1, distance, 0);
            Minion other3 = SpawnMinion("SpellBook1", "SpellBook1", "idle.lua", spawnPos, teamIDAttacker, false, true, false, true, true, true, 0, false, true, (Champion)owner);
            FaceDirection(other3, missileEndPosition);
            Vector3 point1 = GetPointByUnitFacingOffset(other3, 400, 30);
            Vector3 point2 = GetPointByUnitFacingOffset(other3, 400, -30);
            Vector3 point3 = GetPointByUnitFacingOffset(other3, 400, 0);
            Minion other2 = SpawnMinion("ParticleTarget", "SpellBook1", "idle.lua", point3, teamIDAttacker, false, true, false, true, true, true, 0, false, true, (Champion)owner);
            FaceDirection(other2, other1.Position3D);
            Vector3 shockwaveTarget = GetPointByUnitFacingOffset(other3, 700, 0); // UNUSED
            AddBuff(attacker, other2, new Buffs.ExpirationTimer(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellCast(attacker, default, point1, point1, 5, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, true, spawnPos);
            SpellCast(attacker, default, point2, point2, 5, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, true, spawnPos);
            SpellCast(attacker, default, point3, point3, 6, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, true, spawnPos);
            AddBuff(attacker, other3, new Buffs.ExpirationTimer(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellCast(attacker, target, missileEndPosition, missileEndPosition, 2, SpellSlotType.ExtraSlots, _1, false, true, false, true, false, true, other3.Position3D);
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float spellBaseDamage = effect0[level - 1];
            AddBuff(attacker, target, new Buffs.GravesChargeShotShot(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            if (target is Champion)
            {
                DestroyMissile(missileNetworkID);
                BreakSpellShields(target);
            }
            float totalAD = GetTotalAttackDamage(owner);
            float baseAD = GetBaseAttackDamage(owner);
            float bonusAD = totalAD - baseAD;
            bonusAD *= 1.4f;
            spellBaseDamage += bonusAD;
            ApplyDamage(attacker, target, spellBaseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, false, attacker);
        }
    }
}
namespace Buffs
{
    public class GravesChargeShotShot : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "RapidFire",
            BuffTextureName = "FallenAngel_DarkBinding.dds",
            PopupMessage = new[] { "game_floatingtext_Snared", },
        };
    }
}