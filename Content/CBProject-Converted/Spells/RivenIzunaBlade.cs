namespace Spells
{
    public class RivenIzunaBlade : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        public override void SelfExecute()
        {
            SpellBuffClear(owner, nameof(Buffs.RivenWindSlashReady));
            Vector3 targetPos = GetSpellTargetPos(spell);
            FaceDirection(owner, targetPos);
            Vector3 pos = GetPointByUnitFacingOffset(owner, 150, 0);
            SpellCast(owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
            pos = GetPointByUnitFacingOffset(owner, 150, 9);
            SpellCast(owner, default, pos, pos, 3, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
            pos = GetPointByUnitFacingOffset(owner, 150, -9);
            SpellCast(owner, default, pos, pos, 3, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RivenFengShuiEngine)) > 0)
            {
                SetSlotSpellCooldownTimeVer2(0, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
                SealSpellSlot(3, SpellSlotType.SpellSlots, owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            }
        }
    }
}
namespace Buffs
{
    public class RivenIzunaBlade : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "LuxPrismaticWave_shield.troy", },
            BuffName = "Shen Feint Buff",
            BuffTextureName = "Shen_Feint.dds",
            OnPreDamagePriority = 3,
        };
    }
}