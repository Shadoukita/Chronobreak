namespace Spells
{
    public class OrianaDissonance : SpellScript
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
        bool hit; // UNUSED
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            bool correctSpell = false;
            float duration = GetBuffRemainingDuration(owner, nameof(Buffs.OrianaDissonance));
            if (spellName == nameof(Spells.OrianaDissonance))
            {
                correctSpell = true;
            }
            /*
            // Non-existent spell
            else if(spellName == nameof(Spells.Yomudissonance))
            {
                correctSpell = true;
            }
            */
            if (duration >= 0.01f && correctSpell)
            {
                hit = true;
                charVars.GhostAlive = false;
                DestroyMissile(charVars.MissileID);
                SpellBuffClear(owner, nameof(Buffs.OrianaDissonance));
            }
        }
        public override void OnMissileUpdate(SpellMissile missileNetworkID, Vector3 missilePosition)
        {
            foreach (AttackableUnit unit in GetUnitsInArea(owner, missilePosition, 275, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.OrianaShock)) == 0)
                {
                    AddBuff(owner, unit, new Buffs.OrianaShock(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                }
            }
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            AddBuff(attacker, target, new Buffs.OrianaGhostEnemy(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            DestroyMissile(missileNetworkID);
        }
    }
}
namespace Buffs
{
    public class OrianaDissonance : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Malady",
            BuffTextureName = "3114_Malady.dds",
        };
        bool hit; // UNUSED
        public override void OnActivate()
        {
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            hit = false;
        }
        public override void OnDeactivate(bool expired)
        {
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnUpdateActions()
        {
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnLaunchMissile(SpellMissile missileId)
        {
            charVars.MissileID = missileId;
            charVars.GhostAlive = true;
        }
    }
}