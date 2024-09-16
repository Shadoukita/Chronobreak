namespace Spells
{
    public class AhriFoxFire : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        public override void SelfExecute()
        {
            AddBuff(owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            PlayAnimation("Spell2", 0.71f, owner, false, true, true);
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "Ahri_FoxFire_cas.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out _, out _, "Ahri_FoxFire_weapon_cas.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "BUFFBONE_GLB_WEAPON_1", default, target, "BUFFBONE_GLB_WEAPON_1", default, true, false, false, false, false);
            Vector3 point1 = GetPointByUnitFacingOffset(owner, 150, 45);
            Vector3 point2 = GetPointByUnitFacingOffset(owner, 150, 165);
            Vector3 point3 = GetPointByUnitFacingOffset(owner, 150, 285);
            int level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SpellCast(attacker, attacker, default, default, 2, SpellSlotType.ExtraSlots, level, true, true, false, true, false, true, point1);
            SpellCast(attacker, attacker, default, default, 2, SpellSlotType.ExtraSlots, level, true, true, false, true, false, true, point2);
            SpellCast(attacker, attacker, default, default, 2, SpellSlotType.ExtraSlots, level, true, true, false, true, false, true, point3);
            AddBuff(attacker, target, new Buffs.AhriFoxFire(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff(attacker, target, new Buffs.AhriFoxFireMissile(), 3, 3, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.AhriFoxFireMissileTwo)) > 0)
            {
                SpellBuffClear(owner, nameof(Buffs.AhriFoxFireMissileTwo));
            }
        }
    }
}
namespace Buffs
{
    public class AhriFoxFire : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "AhriFoxFire",
            BuffTextureName = "Ahri_FoxFire.dds",
        };
        EffectEmitter particle1; // UNUSED
        int[] effect0 = { 9, 8, 7, 6, 5 };
        public override void OnActivate()
        {
            SetSlotSpellCooldownTimeVer2(0, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnDeactivate(bool expired)
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float cooldown = effect0[level - 1];
            float cooldownMod = GetPercentCooldownMod(owner);
            cooldownMod++;
            float finalCooldown = cooldown * cooldownMod;
            SetSlotSpellCooldownTimeVer2(finalCooldown, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            float duration = GetBuffRemainingDuration(owner, nameof(Buffs.AhriFoxFire));
            if (duration < -0.001f)
            {
                SpellEffectCreate(out particle1, out _, "Ahri_foxfire_obd-sound.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_GLB_WEAPON_1", default, owner, default, default, false, false, false, false, false);
            }
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
        }
    }
}