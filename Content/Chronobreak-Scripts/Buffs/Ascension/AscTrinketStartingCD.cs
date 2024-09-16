namespace Buffs
{
    internal class AscTrinketStartingCD : BuffScript
    {
        public override BuffScriptMetaData BuffMetaData { get; } = new()
        {
            BuffType = BuffType.INTERNAL,
            BuffAddType = BuffAddType.REPLACE_EXISTING
        };

        public override void OnDeactivate(bool expired)
        {
            if (target is ObjAIBase obj && obj.ItemInventory != null)
            {
                obj.Spells[6 + (byte)SpellSlotType.InventorySlots].SetCooldown(45.0f);
            }
        }
    }
}