namespace Spells
{
    public class Rewind : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 38f, 34f, 30f, 26f, 22f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float spellCD1 = GetSlotSpellCooldownTime((ObjAIBase)target, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float spellCD1a = spellCD1 + -10;
            float spellCD1b = Math.Max(spellCD1a, 0);
            SetSlotSpellCooldownTimeVer2(spellCD1b, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)target, false);
            SpellEffectCreate(out _, out _, "ChronoRefresh_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, default, default, false);
            float spellCD3 = GetSlotSpellCooldownTime((ObjAIBase)target, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float spellCD3a = spellCD3 + -10;
            float spellCD3b = Math.Max(spellCD3a, 0);
            SetSlotSpellCooldownTimeVer2(spellCD3b, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)target, false);
            float spellCD4 = GetSlotSpellCooldownTime((ObjAIBase)target, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float spellCD4a = spellCD4 + -10;
            float spellCD4b = Math.Max(spellCD4a, 0);
            SetSlotSpellCooldownTimeVer2(spellCD4b, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)target, true);
        }
    }
}