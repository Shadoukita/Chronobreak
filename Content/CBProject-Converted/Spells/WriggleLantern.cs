namespace Spells
{
    public class WriggleLantern : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 targetPos = GetSpellTargetPos(spell);
            Minion other3 = SpawnMinion("WriggleLantern", "WriggleLantern", "idle.lua", targetPos, teamID, true, true, false, false, false, false, 0, true, false, (Champion)owner);
            AddBuff(attacker, other3, new Buffs.SharedWardBuff(), 1, 1, 180, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, other3, new Buffs.WriggleLanternWard(), 1, 1, 180, BuffAddType.REPLACE_EXISTING, BuffType.INVISIBILITY, 0, true, false, false);
            AddBuff(attacker, other3, new Buffs.ItemPlacementMissile(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            if (avatarVars.Scout)
            {
                AddBuff(attacker, other3, new Buffs.MasteryScoutBuff(), 1, 1, 180, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            SetSpell(owner, 7, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.ItemPlacementMissile));
            FaceDirection(owner, targetPos);
            SpellCast(owner, default, targetPos, targetPos, 7, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
            string name = GetSlotSpellName(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name1 = GetSlotSpellName(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name2 = GetSlotSpellName(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name3 = GetSlotSpellName(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name4 = GetSlotSpellName(owner, 4, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name5 = GetSlotSpellName(owner, 5, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            float newCooldown = 180;
            if (name == nameof(Spells.WriggleLantern))
            {
                SetSlotSpellCooldownTimeVer2(newCooldown, 0, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name1 == nameof(Spells.WriggleLantern))
            {
                SetSlotSpellCooldownTimeVer2(newCooldown, 1, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name2 == nameof(Spells.WriggleLantern))
            {
                SetSlotSpellCooldownTimeVer2(newCooldown, 2, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name3 == nameof(Spells.WriggleLantern))
            {
                SetSlotSpellCooldownTimeVer2(newCooldown, 3, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name4 == nameof(Spells.WriggleLantern))
            {
                SetSlotSpellCooldownTimeVer2(newCooldown, 4, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name5 == nameof(Spells.WriggleLantern))
            {
                SetSlotSpellCooldownTimeVer2(newCooldown, 5, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
        }
    }
}
namespace Buffs
{
    public class WriggleLantern : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "WriggleLantern",
            BuffTextureName = "3154_WriggleLantern.dds",
        };
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is ObjAIBase && RandomChance() < 0.2f && target is not BaseTurret && target is not Champion && GetBuffCountFromCaster(target, default, nameof(Buffs.OdinGolemBombBuff)) == 0)
            {
                ApplyDamage(attacker, target, 425, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
            }
        }
    }
}