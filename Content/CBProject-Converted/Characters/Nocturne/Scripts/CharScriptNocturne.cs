namespace CharScripts
{
    public class CharScriptNocturne : CharScript
    {
        float[] effect0 = { 0.2f, 0.05f, 0.05f, 0.05f, 0.05f };
        public override void OnUpdateActions()
        {
            float bonusAD = GetFlatPhysicalDamageMod(owner);
            float bonusAD2 = bonusAD * 1.2f;
            bonusAD *= 0.75f;
            SetSpellToolTipVar(bonusAD, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
            SetSpellToolTipVar(bonusAD2, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
            if (!IsDead(owner) && GetBuffCountFromCaster(owner, owner, nameof(Buffs.NocturneUmbraBlades)) == 0)
            {
                float curTime = GetGameTime();
                float timeSinceLastHit = curTime - charVars.LastHit;
                if (timeSinceLastHit >= 10)
                {
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.NocturneUmbraBlades)) == 0)
                    {
                        AddBuff(owner, owner, new Buffs.NocturneUmbraBlades(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                    }
                }
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (hitResult != HitResult.HIT_Miss && hitResult != HitResult.HIT_Dodge)
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.NocturneUmbraBlades)) > 0)
                {
                    charVars.LastHit = GetGameTime();
                    if (target is ObjAIBase)
                    {
                        SpellEffectCreate(out _, out _, "Globalhit_red.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, owner.Position3D, owner, default, default, true, default, default, false);
                    }
                }
                else
                {
                    charVars.LastHit--;
                    float curTime = GetGameTime();
                    float timeSinceLastHit = curTime - charVars.LastHit;
                    if (timeSinceLastHit >= 9)
                    {
                        if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.NocturneUmbraBlades)) == 0)
                        {
                            AddBuff(owner, owner, new Buffs.NocturneUmbraBlades(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                        }
                    }
                }
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            string slotName = GetSpellName(spell);
            float cooldown = GetSlotSpellCooldownTime(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (slotName == nameof(Spells.NocturneParanoia))
            {
                SetSpell(owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.NocturneParanoia2));
                SetSlotSpellCooldownTimeVer2(cooldown, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
                SealSpellSlot(3, SpellSlotType.SpellSlots, owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            charVars.LastHit = 0;
        }
        public override void OnLevelUpSpell(int slot)
        {
            int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float attackSpeedBoost = effect0[level - 1];
            if (slot == 1)
            {
                IncPermanentPercentAttackSpeedMod(owner, attackSpeedBoost);
            }
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class CharScriptNocturne : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffTextureName = "Wolfman_InnerHunger.dds",
        };
    }
}