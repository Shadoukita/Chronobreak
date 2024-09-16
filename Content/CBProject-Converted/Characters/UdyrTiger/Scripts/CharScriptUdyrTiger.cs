namespace CharScripts
{
    public class CharScriptUdyrTiger : CharScript
    {
        float[] effect0 = { -0.024f, -0.028f, -0.032f, -0.036f, -0.04f, -0.044f, -0.048f, -0.052f, -0.056f, -0.06f, -0.064f, -0.068f, -0.072f, -0.076f, -0.08f, -0.084f, -0.088f, -0.092f };
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
            AddBuff(owner, owner, new Buffs.UdyrMonkeyAgility(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false);
        }
        public override void SetVarsByLevel()
        {
            charVars.BaseCritChance = effect0[level - 1];
        }
    }
}