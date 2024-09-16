namespace Spells
{
    public class AspectOfTheCougar : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 10, 20, 30 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.AspectOfTheCougar)) > 0)
            {
                SpellBuffRemove(owner, default, owner, 0);
            }
            else
            {
                float nextBuffVars_armorMod = effect0[level - 1];
                AddBuff(attacker, owner, new Buffs.AspectOfTheCougar(nextBuffVars_armorMod), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class AspectOfTheCougar : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "AspectOfTheCougar",
            BuffTextureName = "Nidalee_AspectOfTheCougar.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
            SpellToggleSlot = 4,
        };
        float armorMod;
        float cD0;
        float cD1;
        float cD2;
        int cougarID;
        int[] effect0 = { 10, 20, 30 };
        public AspectOfTheCougar(float armorMod = default)
        {
            this.armorMod = armorMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.armorMod);
            cD0 = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            cD1 = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            cD2 = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            cougarID = PushCharacterData("Nidalee_Cougar", owner, true);
            SpellEffectCreate(out _, out _, "nidalee_transform.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = multiplier * 4;
            SetSlotSpellCooldownTimeVer2(newCooldown, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Takedown)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.Takedown), (ObjAIBase)owner, 0);
            }
            PopCharacterData(owner, cougarID);
            SpellEffectCreate(out _, out _, "nidalee_transform.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = multiplier * 4;
            SetSlotSpellCooldownTimeVer2(newCooldown, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            float cD0 = this.cD0 - lifeTime;
            float cD1 = this.cD1 - lifeTime;
            float cD2 = this.cD2 - lifeTime;
            SetSlotSpellCooldownTimeVer2(cD0, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            SetSlotSpellCooldownTimeVer2(cD1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            SetSlotSpellCooldownTimeVer2(cD2, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, armorMod);
            IncFlatSpellBlockMod(owner, armorMod);
        }
        public override void OnLevelUpSpell(int slot)
        {
            if (slot == 3)
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                armorMod = effect0[level - 1];
            }
        }
    }
}