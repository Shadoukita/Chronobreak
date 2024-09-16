namespace Buffs
{
    public class OrianaGhostSelf : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "OrianaGhostSelf",
            BuffTextureName = "OriannaPassive.dds",
        };
        int[] effect0 = { 10, 15, 20, 25, 30 };
        public override void OnActivate()
        {
            //this.selfParticle = selfParticle;
            SetSpellOffsetTarget(1, SpellSlotType.SpellSlots, nameof(Spells.JunkName), SpellbookType.SPELLBOOK_CHAMPION, owner, owner);
            SetSpellOffsetTarget(1, SpellSlotType.SpellSlots, nameof(Spells.JunkName), SpellbookType.SPELLBOOK_CHAMPION, owner, owner);
            string myName = GetUnitSkinName(owner);
            if (myName == "Orianna" && !IsDead(owner) && charVars.GhostInitialized)
            {
                PopCharacterData(owner, charVars.TempSkin);
            }
            SpellBuffClear(owner, nameof(Buffs.OriannaBallTracker));
        }
        public override void OnDeactivate(bool expired)
        {
            string myName = GetUnitSkinName(owner);
            charVars.GhostInitialized = true;
            if (!IsDead(owner))
            {
                if (myName == "Orianna")
                {
                    charVars.TempSkin = PushCharacterData("OriannaNoBall", owner, false);
                }
                if (myName == "orianna")
                {
                    charVars.TempSkin = PushCharacterData("OriannaNoBall", owner, false);
                }
            }
        }
        public override void OnUpdateStats()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level > 0)
            {
                float defenseBonus = effect0[level - 1];
                IncFlatArmorMod(owner, defenseBonus);
                IncFlatSpellBlockMod(owner, defenseBonus);
            }
        }
    }
}