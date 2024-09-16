namespace CharScripts
{
    public class CharScriptKogMawDead : CharScript
    {
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.KogMawIcathianSurpriseReady(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, default, false);
        }
        public override void OnResurrect()
        {
            AddBuff(owner, owner, new Buffs.KogMawIcathianSurpriseReady(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true);
        }
    }
}