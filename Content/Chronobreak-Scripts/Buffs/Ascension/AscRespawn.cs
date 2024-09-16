namespace Buffs
{
    internal class AscRespawn : BuffScript
    {
        public override BuffScriptMetaData BuffMetaData { get; } = new()
        {
            BuffType = BuffType.INTERNAL,
            BuffAddType = BuffAddType.REPLACE_EXISTING
        };

        public override void OnActivate()
        {
            if (target is ObjAIBase obj && obj.ItemInventory != null)
            {
                OldAPI.AddBuff("AscTrinketStartingCD", 0.3f, 1, null, target, obj);
                ApiEventManager.OnResurrect.AddListener(this, obj, OnRespawn, false);
            }
        }

        public void OnRespawn(ObjAIBase owner)
        {
            owner.Spells[6 + (byte)SpellSlotType.InventorySlots].SetCooldown(0);
        }
    }
}