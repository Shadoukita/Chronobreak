namespace Spells
{
    public class MissFortuneBulletTime : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            ChannelDuration = 2.2f,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        //object castPosition; // UNITIALIZED
        float counter;
        /*
        //TODO: Uncomment and fix
        public override void ChannelingStart()
        {
            Vector3 castPosition; // UNITIALIZED
            FaceDirection(owner, castPosition);
        }
        */
        public override void ChannelingUpdateActions()
        {
            int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            //object castPosition = this.castPosition; // UNUSED
            counter++;
            SpellEffectCreate(out _, out _, "missFortune_ult_cas_muzzle_flash.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_WEAPON_3", default, owner, default, default, false);
            SpellEffectCreate(out _, out _, "missFortune_ult_cas_muzzle_flash.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_WEAPON_1", default, owner, default, default, false);
            SpellEffectCreate(out _, out _, "missFortune_left_ult_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_weapon", default, owner, default, default, false);
            SpellEffectCreate(out _, out _, "missFortune_ult_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_weapon", default, owner, default, default, false);
            Vector3 point1 = GetPointByUnitFacingOffset(owner, 500, 15);
            Vector3 point2 = GetPointByUnitFacingOffset(owner, 500, 9);
            Vector3 point3 = GetPointByUnitFacingOffset(owner, 500, 3);
            Vector3 point4 = GetPointByUnitFacingOffset(owner, 500, 357);
            Vector3 point5 = GetPointByUnitFacingOffset(owner, 500, 351);
            Vector3 point6 = GetPointByUnitFacingOffset(owner, 500, 345);
            Vector3 point7 = GetPointByUnitFacingOffset(owner, 500, 350); // UNUSED
            Vector3 point8 = GetPointByUnitFacingOffset(owner, 500, 345);
            Vector3 point9 = GetPointByUnitFacingOffset(owner, 500, 340); // UNUSED
            Vector3 point0 = GetPointByUnitFacingOffset(owner, 500, 0);
            float count = GetBuffCountFromAll(owner, nameof(Buffs.MissFortuneWaves));
            float modValue = count % 2;
            if (modValue == 0)
            {
                SpellCast(owner, default, point1, point1, 2, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                SpellCast(owner, default, point2, point2, 2, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                SpellCast(owner, default, point3, point3, 2, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                SpellCast(owner, default, point4, point4, 2, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                SpellCast(owner, default, point5, point5, 2, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                SpellCast(owner, default, point6, point6, 2, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                SpellCast(owner, default, point0, point8, 3, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
            }
            if (modValue > 0)
            {
                SpellCast(owner, default, point1, point1, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                SpellCast(owner, default, point2, point2, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                SpellCast(owner, default, point3, point3, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                SpellCast(owner, default, point4, point4, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                SpellCast(owner, default, point5, point5, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                SpellCast(owner, default, point6, point6, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                SpellCast(owner, default, point0, point8, 3, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
            }
            AddBuff(owner, owner, new Buffs.MissFortuneWaves(), 8, 1, 4, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
            count++;
            if (count >= 8)
            {
                StopChanneling(owner, ChannelingStopCondition.Success, ChannelingStopSource.TimeCompleted);
            }
        }
        public override void ChannelingSuccessStop()
        {
            SpellBuffRemove(owner, nameof(Buffs.MissFortuneBulletSound), owner);
            SpellBuffClear(owner, nameof(Buffs.MissFortuneWaves));
        }
        public override void ChannelingCancelStop()
        {
            SpellBuffRemove(owner, nameof(Buffs.MissFortuneBulletSound), owner);
            SpellBuffClear(owner, nameof(Buffs.MissFortuneWaves));
        }
    }
}
namespace Buffs
{
    public class MissFortuneBulletTime : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "",
        };
    }
}