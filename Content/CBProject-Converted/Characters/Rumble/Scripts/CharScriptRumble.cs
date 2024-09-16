namespace CharScripts
{
    public class CharScriptRumble : CharScript
    {
        float punchdmg;
        int baseCDR; // UNUSED
        int[] effect0 = { 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105, 110 };
        public override void OnUpdateStats()
        {
            int level = GetLevel(owner);
            punchdmg = effect0[level - 1];
            SetBuffToolTipVar(1, punchdmg);
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.RumbleHeatSystem(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.RumbleHeatPunchTT(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            IncPAR(owner, -100, PrimaryAbilityResourceType.Other);
            charVars.DangerZone = 50;
            charVars.ShieldAmount = 0;
            baseCDR = 10;
        }
        public override void OnResurrect()
        {
            float temp1 = GetPAR(owner, PrimaryAbilityResourceType.Other);
            temp1 *= -1;
            IncPAR(owner, temp1, PrimaryAbilityResourceType.Other);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}