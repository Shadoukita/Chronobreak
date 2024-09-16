using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GameServerCore.Enums;
using Chronobreak.GameServer.Content;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.Logging;
using Chronobreak.GameServer.Scripting.CSharp;
using Chronobreak.GameServer.Scripting.Lua;
using log4net;

namespace Chronobreak.GameServer.Inventory
{
    public class ItemInventory
    {
        private enum UndoItemMatchResult
        {
            DEFAULT,
            NONE,
            DIFFERENT_QUANTITY,
            MATCHES
        }

        private record UndoItemTemplate(int ItemId, int StackCount);
        private record InventoryState(float AmountSpent, UndoItemTemplate?[] PreviousItems);
        private readonly Stack<InventoryState> _previousStates = new();

        private const byte TRINKET_SLOT = 6;
        private const byte BASE_INVENTORY_SIZE = 7; // Includes trinket
        private const byte EXTRA_INVENTORY_SIZE = 7;
        private const byte RUNE_INVENTORY_SIZE = 30;
        private static ILog _logger = LoggerProvider.GetLogger();
        internal Dictionary<int, IItemScript> ItemScripts = [];
        public Item[] Items { get; }
        public ObjAIBase Owner { get; set; }

        public ItemInventory(ObjAIBase owner)
        {
            Items = new Item[BASE_INVENTORY_SIZE + EXTRA_INVENTORY_SIZE + RUNE_INVENTORY_SIZE];
            Owner = owner;
        }

        public Item[] GetBaseItems()
        {
            return Items.Take(BASE_INVENTORY_SIZE).ToArray();
        }

        /// <summary>
        /// Adds an item to the unit's inventory from it's ItemData using the next available slot (if it's an trinket, it adds it to the extra slot instead)
        /// </summary>
        /// <param name="item">Target ItemData</param>
        /// <returns>The added item instance</returns>
        public bool AddItem(ItemData? item)
        {
            if (item is null)
            {
                return false;
            }

            Item? itemToAdd;

            if (item.ItemGroup.ToLower().Equals("relicbase"))
            {
                itemToAdd = AddTrinketItem(item);
            }
            else if (item.MaxStacks > 1)
            {
                itemToAdd = AddStackingItem(item);
            }
            else
            {
                itemToAdd = AddNewItem(item);
            }

            if (itemToAdd == null)
            {
                return false;
            }

            LoadOwnerStats(item);

            //This packet seems to break when buying more than 3 of one of the 250Gold elixirs
            Game.PacketNotifier.NotifyBuyItem(Owner, itemToAdd);
            return true;
        }

        /// <summary>
        /// Creates an item from ItemData and sets it to the desired slot with 1 stacks
        /// </summary>
        /// <param name="item">Target ItemData</param>
        /// <param name="slot">Target slot</param>
        /// <returns>The added item instance</returns>
        public Item SetItemToSlot(ItemData item, byte slot)
        {
            if (item.ItemGroup.ToLower().Equals("relicbase"))
            {
                return AddTrinketItem(item);
            }

            var result = SetItem(slot, item);
            LoadOwnerStats(item);
            return result;
        }

        /// <summary>
        /// Loads the stat modifiers, active spells and passives of the item (NOTE: You must add the item first to the unit's inventory)
        /// </summary>
        /// <param name="item">Requested item</param>
        private void LoadOwnerStats(ItemData item)
        {
            Owner.AddStatModifier(item);

            if (!string.IsNullOrEmpty(item.SpellName))
            {
                Owner.SetSpell
                (
                    item.SpellName,
                    (byte)(
                        SpellSlotType.InventorySlots +
                        GetItemSlot(GetItem(item.SpellName))
                    ),
                    true
                );

            }

            //Checks if the item's script was already loaded before
            if (!ItemScripts.ContainsKey(item.Id))
            {
                IItemScript script = Game.ScriptEngine.CreateObject<IItemScript>("ItemPassives", $"ItemID_{item.Id}", Game.Config.SupressScriptNotFound);
                if (script is null)
                {
                    string scriptName = item.Id.ToString();
                    if (LuaScriptEngine.HasBBScript(scriptName))
                    {
                        script = new BBItemScript
                        (
                            new BBScriptCtrReqArgs
                            (
                                scriptName, Owner, (Owner as Minion)?.Owner as Champion
                            )
                        );
                    }
                    else
                    {
                        script = new ItemScript();
                        //HasEmptyScript = true;
                    }
                }
                //Loads the Script
                ItemScripts.Add(item.Id, script);
                try
                {
                    ItemScripts[item.Id].OnActivate(Owner);
                }
                catch (Exception e)
                {
                    _logger.Error(null, e);
                }
            }
        }

        /// <summary>
        /// Sets an extra item to the specified slot
        /// </summary>
        /// <param name="slot">Target slot</param>
        /// <param name="item">Target item</param>
        /// <returns>The item instance</returns>
        public Item SetExtraItem(byte slot, ItemData item)
        {
            if (slot < BASE_INVENTORY_SIZE)
            {
                throw new Exception("Invalid extra item slot—must be greater than base inventory size!");
            }

            return SetItem(slot, item);
        }

        private Item SetItem(byte slot, ItemData item)
        {
            Items[slot] = new Item(item);
            return Items[slot];
        }

        /// <summary>
        /// Returns an item in the inventory slot.
        /// </summary>
        /// <param name="slot"></param>
        /// <returns>Item owned by the unit.</returns>
        public Item GetItem(byte slot)
        {
            return Items[slot];
        }

        /// <summary>
        /// Returns an item the unit owns by name.
        /// </summary>
        /// <param name="itemSpellName">The item spell name to match</param>
        /// <param name="isItemName">Whether to use the item name instead of the item spell name</param>
        /// <returns>Item owned by the unit.</returns>
        public Item GetItem(string itemSpellName, bool isItemName = false)
        {
            if (itemSpellName != null)
            {
                for (byte i = 0; i < Items.Length; i++)
                {
                    if (Items[i] != null)
                    {
                        if (isItemName)
                        {
                            if (itemSpellName == Items[i].ItemData.Name)
                            {
                                return Items[i];
                            };
                        }
                        else if (itemSpellName == Items[i].ItemData.SpellName)
                        {
                            return Items[i];
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Remove a specific item from a unit.
        /// </summary>
        /// <param name="slot">Which item slot</param>
        /// <param name="stacksToRemove">How many stacks to remove</param>
        /// <param name="force">Whether to forcefully remove the item (remove all stacks)</param>
        /// <returns>If the item was removed.</returns>
        public void RemoveItem(byte slot, int stacksToRemove = 1, bool force = false)
        {
            var item = Items[slot];
            if (item == null)
            {
                return;
            }

            if (stacksToRemove < 0)
            {
                throw new Exception("Stacks to be Removed can't be a negative number!");
            }

            var itemID = Items[slot].ItemData.Id;
            int finalStacks = Items[slot].StackCount - stacksToRemove;

            if (finalStacks <= 0 || force)
            {
                if (Items[slot] == null)
                {
                    return;
                }

                Owner.RemoveStatModifier(Items[slot].ItemData);

                if (!string.IsNullOrEmpty(Items[slot].ItemData.SpellName))
                {
                    Owner.SetSpell(string.Empty, slot + (byte)(SpellSlotType.InventorySlots), false);
                }

                Items[slot] = null;

                if (!HasItem(itemID) && ItemScripts.ContainsKey(itemID))
                {
                    try
                    {
                        ItemScripts[itemID].OnDeactivate(Owner);
                    }
                    catch (Exception e)
                    {
                        _logger.Error(null, e);
                    }

                    ItemScripts.Remove(itemID);
                }

            }
            else
            {
                Items[slot].SetStacks(finalStacks);
            }

            item = Items[slot];
            byte stacks = 0;
            if (item != null)
            {
                stacks = (byte)item.StackCount;
            }

            if (stacks > 0 && (item?.ItemData.Consumed ?? false))
            {
                Game.PacketNotifier.NotifyUseItemAns(Owner, slot,
                    stacks);
            }
            else
            {
                Game.PacketNotifier.NotifyRemoveItem(Owner, slot, stacks);
            }
        }

        /// <summary>
        /// Remove a specific item from a unit.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="owner"></param>
        /// <param name="stacksToRemove"></param>
        /// <returns>If the item was removed.</returns>
        public void RemoveItem(Item item, int stacksToRemove = 1)
        {
            var slot = GetItemSlot(item);

            if (Items[slot] == null)
            {
                return;
            }

            RemoveItem(slot, stacksToRemove);
        }

        /// <summary>
        /// Returns if the unit owns an item with the provided item id.
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns>If the unit has the item.</returns>
        public bool HasItem(int itemId)
        {
            foreach (var item in Items)
            {
                if (item != null && itemId == item.ItemData.Id)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns the slot of an item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Item slot</returns>
        public byte GetItemSlot(Item item)
        {
            for (byte i = 0; i < Items.Length; i++)
            {
                if (Items[i] != item)
                {
                    continue;
                }

                return i;
            }

            throw new Exception("Specified item doesn't exist in the inventory!");
        }

        /// <summary>
        /// Swaps an item(s) slot.
        /// </summary>
        /// <param name="slot1"></param>
        /// <param name="slot2"></param>
        public void SwapItems(byte slot1, byte slot2)
        {
            if (slot1 == TRINKET_SLOT || slot2 == TRINKET_SLOT)
            {
                throw new Exception("Can't swap to or from the trinket slot");
            }

            (Items[slot1], Items[slot2]) = (Items[slot2], Items[slot1]);
        }

        /// <summary>
        /// Adds an item from the target ItemData to the unit's trinket slot
        /// </summary>
        /// <param name="item"></param>
        /// <returns>The item instance</returns>
        private Item? AddTrinketItem(ItemData item)
        {
            if (Items[TRINKET_SLOT] != null)
            {
                return null;
            }

            var itemResult = SetItem(TRINKET_SLOT, item);
            if (!string.IsNullOrEmpty(item.SpellName))
            {
                Owner.SetSpell(item.SpellName, TRINKET_SLOT + (byte)SpellSlotType.InventorySlots, true);
            }
            //Checks if the item's script was already loaded before
            if (!ItemScripts.ContainsKey(item.Id))
            {
                //Loads the Script
                ItemScripts.Add(item.Id, Game.ScriptEngine.CreateObject<IItemScript>("ItemPassives", $"ItemID_{item.Id}") ?? new ItemScript());
                try
                {
                    ItemScripts[item.Id].OnActivate(Owner);
                }
                catch (Exception e)
                {
                    _logger.Error(null, e);
                }
            }
            return itemResult;
        }

        /// <summary>
        /// Adds (or increases the stack count) of the target ItemData to the unit's inventory
        /// </summary>
        /// <param name="item"></param>
        /// <returns>The item instance</returns>
        private Item? AddStackingItem(ItemData item)
        {
            for (var i = 0; i < BASE_INVENTORY_SIZE; i++)
            {
                if (Items[i] == null)
                {
                    continue;
                }

                if (item.Id != Items[i].ItemData.Id)
                {
                    continue;
                }

                if (Items[i].IncrementStackCount())
                {
                    return Items[i];
                }

                return null;
            }
            return AddNewItem(item);
        }

        /// <summary>
        /// Adds the target ItemData to the unit's inventory
        /// </summary>
        /// <param name="item"></param>
        /// <returns>The item instance</returns>
        private Item? AddNewItem(ItemData item)
        {
            for (var i = 0; i < BASE_INVENTORY_SIZE; i++)
            {
                if (i == TRINKET_SLOT)
                {
                    continue;
                }

                if (Items[i] != null)
                {
                    continue;
                }

                return SetItem((byte)i, item);
            }

            return null;
        }

        /// <summary>
        /// Returns a list of items owned by the unit.
        /// </summary>
        /// <param name="includeRunes">Whether to include items or not</param>
        /// <param name="includeRecallItem">Whether to include the recall item (blue pill) or not</param>
        /// <returns>List of items the unit owns</returns>
        public List<Item> GetItems(bool includeRunes = false, bool includeRecallItem = false)
        {
            List<Item> toReturn = new List<Item>(Items.ToList());
            if (!includeRecallItem)
            {
                toReturn.RemoveAt(7);
            }
            toReturn.RemoveAll(x => x == null);
            if (!includeRunes)
            {
                toReturn.RemoveAll(x => x.ItemData.Id >= 5000);
            }
            return toReturn;
        }

        public List<Item> GetAvailableItems(IEnumerable<ItemData> items)
        {
            var tempInv = new List<Item>(GetBaseItems());
            return GetAvailableItemsRecursive(ref tempInv, items);
        }

        private static List<Item> GetAvailableItemsRecursive(ref List<Item> inventoryState, IEnumerable<ItemData> items)
        {
            var result = new List<Item>();
            foreach (var component in items)
            {
                if (component == null)
                {
                    continue;
                }
                var idx = inventoryState.FindIndex(i => i != null && i.ItemData == component);
                if (idx == -1)
                {
                    result = result.Concat(GetAvailableItemsRecursive(ref inventoryState, component.Recipe.Items)).ToList();
                }
                else
                {
                    result.Add(inventoryState[idx]);
                    // remove entry in case that the recipe has the same item more than once in it
                    inventoryState[idx] = null;
                }
            }
            return result;
        }

        public IEnumerator GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public void UpdateStats()
        {
            foreach (var item in ItemScripts)
            {
                try
                {
                    item.Value.OnUpdateStats();
                }
                catch (Exception e)
                {
                    _logger.Error(null, e);
                }
            }
        }
        public void OnUpdate()
        {
            foreach (var item in ItemScripts)
            {
                try
                {
                    item.Value.OnUpdate();
                }
                catch (Exception e)
                {
                    _logger.Error(null, e);
                }
            }
        }

        /// <summary>
        /// Clears the available undo actions for the target unit
        /// </summary>
        public void ClearUndoHistory()
        {
            if (_previousStates.Count > 0)
            {
                _previousStates.Clear();
                Game.PacketNotifier.NotifyS2C_SetUndoEnabled(Owner, _previousStates.Count);
            }
        }

        /// <summary>
        /// Attempts to buy an item for an unit (deducts the gold)
        /// </summary>
        /// <param name="reqItemId">The requested itemId to buy</param>
        /// <returns>Whether the purchase was successful</returns>
        public bool BuyItem(uint reqItemId)
        {
            var itemTemplate = ContentManager.GetItemData((int)reqItemId);
            if (itemTemplate == null)
            {
                return false;
            }
            var price = itemTemplate.TotalPrice;

            var previousState = GenerateItemState();

            var ownedItems = GetAvailableItems(itemTemplate.Recipe.Items);
            if (ownedItems.Count != 0)
            {
                price -= ownedItems.Sum(item => item.ItemData.TotalPrice);
                if (Owner.GoldOwner.Gold < price)
                {
                    return false;
                }

                foreach (var items in ownedItems)
                {
                    RemoveItem(GetItemSlot(items));
                }

                var itemBought = AddItem(itemTemplate);
                if (!itemBought)
                {
                    return false;
                }
            }
            else if (Owner.GoldOwner.Gold < price || !AddItem(itemTemplate))
            {
                return false;
            }
            if (ContentManager.GameFeatures.HasFlag(GameFeatures.ItemUndo))
            {
                // It may be worth moving to OnActivate, as the name suggests,
                // but for this you will have to change when and how it is called.
                if (itemTemplate.ClearUndoHistoryOnActivate)
                {
                    ClearUndoHistory();
                }
                else
                {
                    _previousStates.Push(new InventoryState(price, previousState));
                    Game.PacketNotifier.NotifyS2C_SetUndoEnabled(Owner, _previousStates.Count);
                }
            }
            Owner.GoldOwner.SpendGold(price);
            return true;
        }

        /// <summary>
        /// Tries to undo the last action of the unit (buy/sell) and restores the previous inventory state
        /// </summary>
        /// <returns>Whether the undo was successful</returns>
        public bool UndoLastAction()
        {
            if (_previousStates.TryPop(out var lastState))
            {
                for (int i = 0; i < BASE_INVENTORY_SIZE; i++)
                {
                    var itemMatchResult = UndoItemMatchResult.DEFAULT;
                    var currentItem = Items[i];
                    var previousItem = lastState.PreviousItems[i];

                    if (previousItem == null)
                    {
                        if (currentItem != null)
                        {
                            RemoveItem((byte)i, 1, true);
                        }
                    }
                    else if (currentItem == null || !ItemMatchesTemplate(currentItem, previousItem, out itemMatchResult))
                    {
                        Debug.Assert(currentItem == null ? itemMatchResult == UndoItemMatchResult.DEFAULT : itemMatchResult != UndoItemMatchResult.DEFAULT);
                        if (currentItem == null || itemMatchResult == UndoItemMatchResult.NONE)
                        {
                            if (currentItem != null)
                            {
                                RemoveItem((byte)i, 1, true);
                            }
                            var itemTemplate = ContentManager.GetItemData(previousItem.ItemId);
                            var newItem = SetItemToSlot(itemTemplate, (byte)i);
                            newItem.SetStacks(previousItem.StackCount);
                            Game.PacketNotifier.NotifyBuyItem(Owner, newItem);
                        }
                        else if (itemMatchResult == UndoItemMatchResult.DIFFERENT_QUANTITY)
                        {
                            currentItem.SetStacks(previousItem.StackCount);
                            if (currentItem.StackCount > previousItem.StackCount)
                            {
                                Game.PacketNotifier.NotifyRemoveItem(Owner, i, previousItem.StackCount);
                            }
                            else
                            {
                                Game.PacketNotifier.NotifyBuyItem(Owner, currentItem);
                            }
                        }
                    }
                }
                Owner.GoldOwner.AddGold(lastState.AmountSpent, false);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Sells an item from the provided unit (for stackable it only removes one stack) and restores gold
        /// </summary>
        /// <param name="item">The item requested to be sold</param>
        public bool SellItem(Item item)
        {
            var previousState = GenerateItemState();
            var sellPrice = item.ItemData.TotalPrice * item.ItemData.SellBackModifier;
            Owner.GoldOwner.AddGold(sellPrice, false);
            RemoveItem(GetItemSlot(item));
            if (ContentManager.GameFeatures.HasFlag(GameFeatures.ItemUndo))
            {
                _previousStates.Push(new InventoryState(-sellPrice, previousState));
                Game.PacketNotifier.NotifyS2C_SetUndoEnabled(Owner, _previousStates.Count);
            }
            return true;
        }

        /// <summary>
        /// Generates a copy of the current inventory state from 0 to BASE_INVENTORY_SIZE
        /// </summary>
        /// <returns>Copy of the current inventory state</returns>
        private UndoItemTemplate[] GenerateItemState()
        {
            var previousState = new UndoItemTemplate[BASE_INVENTORY_SIZE];
            for (int i = 0; i < BASE_INVENTORY_SIZE; i++)
            {
                if (Items[i] != null)
                {
                    previousState[i] = new UndoItemTemplate(Items[i].ItemData.Id, Items[i].StackCount);
                }
            }
            return previousState;
        }

        /// <summary>
        /// Compares an if an Item instance equals the provided UndoItemTemplate (ItemId and StackCount) and provides a match result
        /// </summary>
        /// <param name="item">Target item</param>
        /// <param name="itemTemplate">Target undo item template</param>
        /// <param name="result">Where the comparison result is stored</param>
        /// <returns>If the item fully matches the undo item template (ItemId and StackCount)</returns>
        private static bool ItemMatchesTemplate(Item item, UndoItemTemplate itemTemplate, out UndoItemMatchResult result)
        {
            if (item.ItemData.Id != itemTemplate.ItemId)
            {
                result = UndoItemMatchResult.NONE;
                return false;
            }
            if (item.StackCount != itemTemplate.StackCount)
            {
                result = UndoItemMatchResult.DIFFERENT_QUANTITY;
                return false;
            }
            result = UndoItemMatchResult.MATCHES;
            return true;
        }
    }
}
