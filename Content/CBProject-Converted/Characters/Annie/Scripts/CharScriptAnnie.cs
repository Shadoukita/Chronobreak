namespace CharScripts
{
    public class CharScriptAnnie : CharScript
    {
        float[] effect0 = { 1.75f, 1.75f, 1.75f, 1.75f, 1.75f, 1.75f, 1.75f, 1.75f, 1.75f, 1.75f, 1.75f, 1.75f, 1.75f, 1.75f, 1.75f, 1.75f, 1.75f, 1.75f };
        public override void SetVarsByLevel()
        {
            charVars.StunDuration = effect0[level - 1];
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.Pyromania_marker(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}