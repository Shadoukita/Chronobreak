namespace Spells
{
    public class BlindMonkETwo : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        public override bool CanCast()
        {
            bool returnValue = true;
            returnValue = false;
            foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 750, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.BlindMonkEOne), true))
            {
                returnValue = true;
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            Vector3 ownerPos = GetUnitPosition(owner);
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 800, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.BlindMonkEOne), true))
            {
                SpellCast(owner, unit, default, default, 0, SpellSlotType.ExtraSlots, level, true, true, false, true, false, true, ownerPos);
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.BlindMonkEManager)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.BlindMonkEManager), owner);
            }
            AddBuff(attacker, attacker, new Buffs.BlindMonkETwo(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class BlindMonkETwo : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "L_Hand", "R_Hand", },
            AutoBuffActivateEffect = new[] { "pirate_attack_buf_01.troy", "pirate_attack_buf_01.troy", },
            BuffName = "RaiseMorale",
            BuffTextureName = "Pirate_RaiseMorale.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            PlayAnimation("Spell2b", 0, owner, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            UnlockAnimation(owner, true);
        }
    }
}