namespace Spells
{
    public class KennenLightningRush : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 85, 125, 165, 205, 245 };
        public override void SelfExecute()
        {
            int nextBuffVars_RushDamage = effect0[level - 1];
            AddBuff(owner, owner, new Buffs.KennenLightningRushDamage(nextBuffVars_RushDamage), 1, 1, 2.2f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0.1f, true, false);
            AddBuff(owner, owner, new Buffs.KennenLightningRush(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.HASTE, 0, true, false);
            SetSpell(owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.KennenLRCancel));
            SetSlotSpellCooldownTime(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0.5f);
        }
    }
}
namespace Buffs
{
    public class KennenLightningRush : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            SpellToggleSlot = 3,
        };
        EffectEmitter ar;
        float moveSpeedMod;
        int defenseBonus;
        Fade litRush;
        int[] effect0 = { 10, 20, 30, 40, 50 };
        int[] effect1 = { 10, 9, 8, 7, 6 };
        public override void OnActivate()
        {
            SpellEffectCreate(out ar, out _, "kennen_lr_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            moveSpeedMod = 1;
            defenseBonus = effect0[level - 1];
            SetGhosted(owner, true);
            SetForceRenderParticles(owner, true);
            SetCanAttack(owner, false);
            IncFlatAttackRangeMod(owner, -575);
            litRush = PushCharacterFade(owner, 0, 0.1f);
            float nextBuffVars_DefenseBonus = defenseBonus;
            AddBuff((ObjAIBase)owner, owner, new Buffs.KennenLightningRushBuff(nextBuffVars_DefenseBonus), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.KennenLightningRush));
            int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float spellCD = effect1[level - 1];
            float cDMod = GetPercentCooldownMod(owner);
            float superCDMod = 1 + cDMod;
            float realCD = spellCD * superCDMod;
            SpellEffectRemove(ar);
            SetGhosted(owner, false);
            SpellBuffRemove(owner, nameof(Buffs.KennenLightningRush), (ObjAIBase)owner);
            SetForceRenderParticles(owner, false);
            SpellEffectCreate(out _, out _, "kennen_lr_off.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            SetCanAttack(owner, true);
            PopCharacterFade(owner, litRush);
            IncFlatAttackRangeMod(owner, 575);
            SetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, realCD);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, moveSpeedMod);
            IncFlatAttackRangeMod(owner, -575);
        }
    }
}