namespace Spells
{
    public class RenektonPreExecute : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 13, 12, 11, 10, 9 };
        int[] effect1 = { 35, 70, 105, 140, 175 };
        public override void SelfExecute()
        {
            SetSlotSpellCooldownTimeVer2(0, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            int nextBuffVars_SpellCooldown = effect0[level - 1];
            int nextBuffVars_BonusDamage = effect1[level - 1]; // UNUSED
            float ragePercent = GetPAR(owner, PrimaryAbilityResourceType.Other); // UNUSED
            AddBuff(owner, owner, new Buffs.RenektonPreExecute(nextBuffVars_SpellCooldown), 1, 1, 6, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class RenektonPreExecute : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head", "", },
            AutoBuffActivateEffect = new[] { "Renekton_Weapon_Hot.troy", "", "", },
            BuffName = "RenektonExecuteReady",
            BuffTextureName = "Renekton_Execute.dds",
            SpellToggleSlot = 2,
        };
        float spellCooldown;
        bool swung;
        public RenektonPreExecute(float spellCooldown = default)
        {
            this.spellCooldown = spellCooldown;
        }
        public override void OnActivate()
        {
            //RequireVar(this.spellCooldown);
            //RequireVar(this.bonusDamage);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            CancelAutoAttack(owner, true);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots); // UNUSED
            swung = false;
        }
        public override void OnDeactivate(bool expired)
        {
            float spellCooldown = this.spellCooldown;
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = multiplier * spellCooldown;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            TeamId ownerVar = GetTeamID_CS(owner);
            if (!swung)
            {
                SpellEffectCreate(out _, out _, "Renekton_RuthlessPredator_obd-sound.troy", default, ownerVar, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, default, default, false, false);
            }
        }
        public override void OnPreAttack(AttackableUnit target)
        {
            if (target is ObjAIBase && target is not BaseTurret)
            {
                SetDodgePiercing(owner, true);
                int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                SkipNextAutoAttack((ObjAIBase)owner);
                float ragePercent = GetPARPercent(owner, PrimaryAbilityResourceType.Other);
                if (ragePercent >= 0.5f)
                {
                    SpellCast((ObjAIBase)owner, target, default, default, 1, SpellSlotType.ExtraSlots, level, false, false, false, false, true, false);
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonUnlockAnimation(), 1, 1, 0.76f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    swung = true;
                }
                else
                {
                    SpellCast((ObjAIBase)owner, target, default, default, 0, SpellSlotType.ExtraSlots, level, false, false, false, false, true, false);
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonUnlockAnimation(), 1, 1, 0.51f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    swung = true;
                }
                SpellBuffRemove(owner, default, (ObjAIBase)owner, 0);
            }
        }
    }
}