namespace Spells
{
    public class OrianaDissonanceCountdown : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            SpellVOOverrideSkins = new[] { "BroOlaf", },
        };
    }
}
namespace Buffs
{
    public class OrianaDissonanceCountdown : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Malady",
            BuffTextureName = "3114_Malady.dds",
        };
        Vector3 targetPos;
        bool hit; // UNUSED
        EffectEmitter temp; // UNUSED
        public override void OnActivate()
        {
            //RequireVar(this.targetPos);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            hit = false;
            TeamId teamID = GetTeamID_CS(owner);
            Minion other3 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", targetPos, teamID, false, true, false, true, true, true, 0, default, true, (Champion)owner);
            AddBuff((ObjAIBase)owner, other3, new Buffs.OrianaGhost(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, other3, new Buffs.OrianaGhostMinion(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellEffectCreate(out temp, out _, "UrgotHeatseekingIndicator.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, target, default, default, false, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            bool found = false;
            foreach (AttackableUnit other2 in GetClosestUnitsInArea(owner, targetPos, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.OrianaGhost), true))
            {
                targetPos = GetUnitPosition(other2);
            }
            foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, targetPos, 375, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
            {
                foreach (AttackableUnit other2 in GetClosestUnitsInArea(owner, targetPos, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.OrianaGhost), true))
                {
                    SpellBuffClear(other2, nameof(Buffs.OrianaGhost));
                }
                found = true;
                Vector3 enemyPos = GetUnitPosition(unit);
                AddBuff((ObjAIBase)owner, owner, new Buffs.OrianaDissonance(), 1, 1, 1.25f, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                SpellCast(attacker, unit, enemyPos, enemyPos, 5, SpellSlotType.ExtraSlots, level, false, true, false, false, false, true, targetPos);
            }
            if (!found)
            {
                SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
                SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
                SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            }
        }
        public override void OnUpdateActions()
        {
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
    }
}