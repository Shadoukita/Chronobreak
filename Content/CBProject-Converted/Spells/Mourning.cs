namespace Spells
{
    public class Mourning : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            SpellEffectCreate(out _, out _, "executionersCalling_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, target, default, default, false);
            SpellEffectCreate(out _, out _, "executionersCalling_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "head", default, target, default, default, false);
            AddBuff((ObjAIBase)target, target, new Buffs.Internal_50MS(), 1, 1, 8, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(attacker, target, new Buffs.GrievousWound(), 1, 1, 8, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false);
            string name = GetSlotSpellName(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name1 = GetSlotSpellName(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name2 = GetSlotSpellName(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name3 = GetSlotSpellName(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name4 = GetSlotSpellName(owner, 4, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name5 = GetSlotSpellName(owner, 5, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            if (name == nameof(Spells.Mourning))
            {
                SetSlotSpellCooldownTimeVer2(20, 0, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            }
            if (name1 == nameof(Spells.Mourning))
            {
                SetSlotSpellCooldownTimeVer2(20, 1, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            }
            if (name2 == nameof(Spells.Mourning))
            {
                SetSlotSpellCooldownTimeVer2(20, 2, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            }
            if (name3 == nameof(Spells.Mourning))
            {
                SetSlotSpellCooldownTimeVer2(20, 3, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            }
            if (name4 == nameof(Spells.Mourning))
            {
                SetSlotSpellCooldownTimeVer2(20, 4, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            }
            if (name5 == nameof(Spells.Mourning))
            {
                SetSlotSpellCooldownTimeVer2(20, 5, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            }
        }
    }
}
namespace Buffs
{
    public class Mourning : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "bleeding_GLB2_tar.troy", },
            BuffName = "Mourning",
            BuffTextureName = "3069_Sword_of_Light_and_Shadow.dds",
        };
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, true))
            {
                ApplyDamage(attacker, owner, 4, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PERIODIC, 1, 0, 1, false, false, attacker);
            }
        }
    }
}