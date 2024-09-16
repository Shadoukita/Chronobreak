namespace CharScripts
{
    public class CharScriptHeimerdinger : CharScript
    {
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.HeimerdingerTurretDetonation(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.TechmaturgicalIcon(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            charVars.Time1 = 25000;
            charVars.Time2 = 25000;
            charVars.Time3 = 25000;
            charVars.Time4 = 25000;
            charVars.Time5 = 25000;
            charVars.Time6 = 25000;
            charVars.Level1 = 4;
            charVars.Level2 = 4;
            charVars.Level3 = 4;
            charVars.Level4 = 4;
            charVars.Level5 = 4;
            charVars.Level6 = 4;
        }
        public override void OnResurrect()
        {
            int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level >= 1)
            {
                AddBuff(owner, owner, new Buffs.HeimerdingerTurretReady(), 2, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
            }
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}