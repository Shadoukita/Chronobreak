namespace Spells
{
    public class SonaAriaofPerseverance : SpellScript
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
        int[] effect0 = { 40, 60, 80, 100, 120 };
        int[] effect1 = { 8, 11, 14, 17, 20 };
        public override void SelfExecute()
        {
            AddBuff(owner, owner, new Buffs.SonaAriaofPerseverance(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SonaPowerChord)) > 0)
            {
                AddBuff(owner, owner, new Buffs.SonaAriaofPerseveranceCheck(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            float cooldownPerc = GetPercentCooldownMod(owner);
            cooldownPerc++;
            cooldownPerc *= 2;
            float currentCD = GetSlotSpellCooldownTime(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (currentCD <= cooldownPerc)
            {
                SetSlotSpellCooldownTime(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldownPerc);
            }
            currentCD = GetSlotSpellCooldownTime(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (currentCD <= cooldownPerc)
            {
                SetSlotSpellCooldownTime(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldownPerc);
            }
            //TeamId casterID = GetTeamID(attacker); // UNUSED
            int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            AttackableUnit jumpTarget = null/*NoTargetYet*/;
            float jumpTargetHealth_ = 1;
            foreach (AttackableUnit unit in GetRandomUnitsInArea(owner, owner.Position3D, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, 999, default, true))
            {
                if (jumpTarget == null/*NoValidTarget*/)
                {
                    jumpTarget = unit;
                }
                float unitHealth_ = GetHealthPercent(unit, PrimaryAbilityResourceType.MANA);
                if (unitHealth_ < jumpTargetHealth_)
                {
                    jumpTarget = unit;
                    jumpTargetHealth_ = unitHealth_;
                }
            }
            if (jumpTarget != null/*NoValidTarget*/)
            {
                AttackableUnit other1 = jumpTarget;
                foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1200, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, default, true))
                {
                    if (unit == other1)
                    {
                        SpellCast(owner, unit, owner.Position3D, owner.Position3D, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                    }
                }
            }
            float aPMod = GetFlatMagicDamageMod(attacker);
            aPMod *= 0.25f;
            IncHealth(owner, aPMod + effect0[level - 1], attacker);
            SpellEffectCreate(out _, out _, "Global_Heal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            AddBuff(owner, owner, new Buffs.SonaAriaofPerseveranceAura(), 1, 1, 2.5f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            PlayAnimation("Spell2", 1, owner, false, true, true);
            float nextBuffVars_DefenseBonus = effect1[level - 1];
            AddBuff(attacker, attacker, new Buffs.SonaAriaShield(nextBuffVars_DefenseBonus), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class SonaAriaofPerseverance : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            BuffName = "",
            BuffTextureName = "Sona_AriaofPerseveranceGold.dds",
            PersistsThroughDeath = true,
            SpellToggleSlot = 4,
        };
        public override void OnActivate()
        {
            SpellBuffRemove(owner, nameof(Buffs.SonaHymnofValor), (ObjAIBase)owner, 0);
            SpellBuffRemove(owner, nameof(Buffs.SonaSongofDiscord), (ObjAIBase)owner, 0);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SonaPowerChord)) == 0)
            {
                OverrideAutoAttack(3, SpellSlotType.ExtraSlots, owner, 1, false);
            }
        }
    }
}