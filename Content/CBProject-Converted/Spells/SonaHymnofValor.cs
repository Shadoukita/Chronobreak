namespace Spells
{
    public class SonaHymnofValor : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            SpellFXOverrideSkins = new[] { "GuqinSona", },
        };
        Region bubbleID; // UNUSED
        public override void SelfExecute()
        {
            bool result;
            AddBuff(owner, owner, new Buffs.SonaHymnofValor(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SonaPowerChord)) > 0)
            {
                AddBuff(owner, owner, new Buffs.SonaHymnofValorCheck(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            float cooldownPerc = GetPercentCooldownMod(owner);
            cooldownPerc++;
            cooldownPerc *= 2;
            float currentCD = GetSlotSpellCooldownTime(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (currentCD <= cooldownPerc)
            {
                SetSlotSpellCooldownTime(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldownPerc);
            }
            currentCD = GetSlotSpellCooldownTime(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (currentCD <= cooldownPerc)
            {
                SetSlotSpellCooldownTime(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldownPerc);
            }
            TeamId casterID = GetTeamID_CS(attacker);
            int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float availChamps = 0;
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 650, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, default, true))
            {
                availChamps++;
            }
            if (availChamps == 1)
            {
                foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 650, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
                {
                    result = CanSeeTarget(owner, unit);
                    if (result)
                    {
                        bubbleID = AddUnitPerceptionBubble(casterID, 300, unit, 1, default, default, false);
                        SpellCast(owner, unit, owner.Position3D, owner.Position3D, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                    }
                }
                foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 850, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, 1, default, true))
                {
                    result = CanSeeTarget(owner, unit);
                    if (result)
                    {
                        bubbleID = AddUnitPerceptionBubble(casterID, 300, unit, 1, default, default, false);
                        SpellCast(owner, unit, owner.Position3D, owner.Position3D, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                    }
                }
            }
            if (availChamps >= 2)
            {
                foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 650, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 2, default, true))
                {
                    result = CanSeeTarget(owner, unit);
                    if (result)
                    {
                        bubbleID = AddUnitPerceptionBubble(casterID, 300, unit, 1, default, default, false);
                        SpellCast(owner, unit, owner.Position3D, owner.Position3D, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                    }
                }
            }
            if (availChamps == 0)
            {
                foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 850, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 2, default, true))
                {
                    result = CanSeeTarget(owner, unit);
                    if (result)
                    {
                        bubbleID = AddUnitPerceptionBubble(casterID, 300, unit, 1, default, default, false);
                        SpellCast(owner, unit, owner.Position3D, owner.Position3D, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                    }
                }
            }
            AddBuff(owner, owner, new Buffs.SonaHymnofValorAura(), 1, 1, 2.5f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            PlayAnimation("Spell1", 1, owner, false, true, true);
        }
    }
}
namespace Buffs
{
    public class SonaHymnofValor : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            BuffName = "",
            BuffTextureName = "Sona_HymnofValorGold.dds",
            PersistsThroughDeath = true,
            SpellToggleSlot = 4,
        };
        public override void OnActivate()
        {
            SpellBuffRemove(owner, nameof(Buffs.SonaAriaofPerseverance), (ObjAIBase)owner, 0);
            SpellBuffRemove(owner, nameof(Buffs.SonaSongofDiscord), (ObjAIBase)owner, 0);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SonaPowerChord)) == 0)
            {
                OverrideAutoAttack(4, SpellSlotType.ExtraSlots, owner, 1, false);
            }
        }
    }
}