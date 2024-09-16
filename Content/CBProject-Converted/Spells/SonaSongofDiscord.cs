namespace Spells
{
    public class SonaSongofDiscord : SpellScript
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
        float[] effect0 = { 0.08f, 0.1f, 0.12f, 0.14f, 0.16f };
        public override void SelfExecute()
        {
            AddBuff(owner, owner, new Buffs.SonaSongofDiscord(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SonaPowerChord)) > 0)
            {
                AddBuff(owner, owner, new Buffs.SonaSongofDiscordCheck(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            float cooldownPerc = GetPercentCooldownMod(owner);
            cooldownPerc++;
            cooldownPerc *= 2;
            float currentCD = GetSlotSpellCooldownTime(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (currentCD <= cooldownPerc)
            {
                SetSlotSpellCooldownTime(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldownPerc);
            }
            currentCD = GetSlotSpellCooldownTime(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (currentCD <= cooldownPerc)
            {
                SetSlotSpellCooldownTime(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldownPerc);
            }
            float nextBuffVars_MoveSpeedMod = effect0[level - 1];
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
            {
                SpellCast(owner, unit, owner.Position3D, owner.Position3D, 6, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
            }
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions, default, true))
            {
                ApplyAssistMarker(attacker, unit, 10);
                AddBuff(owner, unit, new Buffs.SonaSongofDiscordHaste(nextBuffVars_MoveSpeedMod), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.HASTE, 0, true, false, false);
            }
            AddBuff(owner, owner, new Buffs.SonaSongofDiscordAura(), 1, 1, 2.5f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            PlayAnimation("Spell3", 1, owner, false, true, true);
        }
    }
}
namespace Buffs
{
    public class SonaSongofDiscord : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            BuffName = "",
            BuffTextureName = "Sona_SongofDiscordGold.dds",
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            SpellBuffRemove(owner, nameof(Buffs.SonaHymnofValor), (ObjAIBase)owner, 0);
            SpellBuffRemove(owner, nameof(Buffs.SonaAriaofPerseverance), (ObjAIBase)owner, 0);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SonaPowerChord)) == 0)
            {
                OverrideAutoAttack(5, SpellSlotType.ExtraSlots, owner, 1, false);
            }
        }
    }
}