namespace Spells
{
    public class FlashFrost : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override void SelfExecute()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.FlashFrost)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.FlashFrost), owner);
            }
            else
            {
                int level = GetSpellLevelPlusOne(spell);
                Vector3 targetPos = GetSpellTargetPos(spell);
                SetSlotSpellCooldownTime(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0.285f);
                AddBuff(attacker, target, new Buffs.FlashFrost(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                SpellCast(owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class FlashFrost : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Flash Frost",
            BuffTextureName = "Cryophoenix_FrigidOrb.dds",
            SpellToggleSlot = 1,
        };
        int missileAlive;
        SpellMissile flashMissileId;
        int[] effect0 = { 60, 90, 120, 150, 180 };
        int[] effect1 = { 12, 11, 10, 9, 8 };
        public override void OnActivate()
        {
            SetTargetingType(0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, TargetingType.Self, owner);
            missileAlive = 0;
            flashMissileId = null;
        }
        public override void OnDeactivate(bool expired)
        {
            SetTargetingType(0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, TargetingType.Location, owner);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (missileAlive == 1)
            {
                Vector3 missileEndPosition = GetMissilePosFromID(flashMissileId);
                DestroyMissile(flashMissileId);
                TeamId teamID = GetTeamID_CS(attacker);
                SpellEffectCreate(out _, out _, "cryo_FlashFrost_tar.troy", default, teamID, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, missileEndPosition, target, default, default, true);
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, missileEndPosition, 210, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    BreakSpellShields(unit);
                    ApplyDamage(attacker, unit, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.5f, 1, false, false, attacker);
                    ApplyStun(attacker, unit, 0.75f);
                    float nextBuffVars_MovementSpeedMod = -0.2f;
                    float nextBuffVars_AttackSpeedMod = 0;
                    AddBuff(attacker, unit, new Buffs.Chilled(nextBuffVars_MovementSpeedMod, nextBuffVars_AttackSpeedMod), 1, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                }
                missileAlive = 0;
            }
            float cooldownPerLevel = effect1[level - 1];
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = multiplier * cooldownPerLevel;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
        }
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            if (spellName == nameof(Spells.FlashFrostSpell))
            {
                TeamId teamOfOwner = GetTeamID_CS(owner);
                int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                SpellEffectCreate(out _, out _, "cryo_FlashFrost_tar.troy", default, teamOfOwner, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, missileEndPosition, target, default, default, true);
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, missileEndPosition, 230, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    BreakSpellShields(unit);
                    ApplyDamage(attacker, unit, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.5f, 1, false, false, attacker);
                    ApplyStun(attacker, unit, 1);
                    float nextBuffVars_MovementSpeedMod = -0.2f;
                    float nextBuffVars_AttackSpeedMod = 0;
                    AddBuff(attacker, unit, new Buffs.Chilled(nextBuffVars_MovementSpeedMod, nextBuffVars_AttackSpeedMod), 1, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                }
                missileAlive = 0;
                SpellBuffRemove(owner, nameof(Buffs.FlashFrost), (ObjAIBase)owner);
            }
        }
        public override void OnLaunchMissile(SpellMissile missileId)
        {
            if (flashMissileId == null)
            {
                flashMissileId = missileId;
                missileAlive = 1;
            }
        }
    }
}