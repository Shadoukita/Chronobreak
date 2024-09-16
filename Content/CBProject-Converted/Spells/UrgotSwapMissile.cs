namespace Spells
{
    public class UrgotSwapMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int count = GetBuffCountFromAll(target, nameof(Buffs.UrgotSwapMarker));
            if (count != 0)
            {
                DestroyMissile(missileNetworkID);
            }
        }
    }
}
namespace Buffs
{
    public class UrgotSwapMissile : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "SwapArrow_green.troy", "", },
            BuffName = "Dark Binding",
            BuffTextureName = "FallenAngel_DarkBinding.dds",
            PopupMessage = new[] { "game_floatingtext_Snared", },
        };
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.1f, ref lastTimeExecuted, true))
            {
                Vector3 ownerPos = GetUnitPosition(owner);
                Vector3 targetPos = GetUnitPosition(attacker);
                TeamId teamID = GetTeamID_CS(attacker);
                Minion other2 = SpawnMinion("enemy", "TestCubeRender", "idle.lua", targetPos, teamID, true, true, false, true, true, true, 0, default, true);
                AddBuff((ObjAIBase)owner, other2, new Buffs.UrgotSwapMarker(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                FaceDirection(other2, ownerPos);
                Vector3 targetOffsetPos = GetPointByUnitFacingOffset(other2, 80, 90);
                Minion other3 = SpawnMinion("ownerMinion", "TestCubeRender", "idle.lua", ownerPos, teamID, true, true, false, true, true, true, 0, default, true);
                AddBuff((ObjAIBase)owner, other3, new Buffs.UrgotSwapMarker(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                FaceDirection(other3, targetPos);
                Vector3 ownerOffset = GetPointByUnitFacingOffset(other3, 80, 90);
                SetSpell((ObjAIBase)owner, 7, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.UrgotSwapMissile));
                SpellCast((ObjAIBase)owner, attacker, targetPos, targetPos, 7, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, true, ownerOffset);
                SetSpell(attacker, 7, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.UrgotSwapMissile2));
                SpellCast(attacker, owner, ownerPos, ownerPos, 7, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, true, targetOffsetPos);
                AddBuff(attacker, other2, new Buffs.ExpirationTimer(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                AddBuff(attacker, other3, new Buffs.ExpirationTimer(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            }
        }
    }
}