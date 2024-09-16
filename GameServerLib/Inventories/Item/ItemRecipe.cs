
using Chronobreak.GameServer.Content;
using System.Collections.Generic;

namespace Chronobreak.GameServer.Inventory
{
    public class ItemRecipe
    {
        private readonly ItemData _itemData;
        private ItemData[]? _items = null;
        public ItemData[] Items
        {
            get
            {
                if (_items == null)
                {
                    var items = new List<ItemData>();
                    foreach (var itemID in _itemData.RecipeItem)
                    {
                        ItemData? data;
                        if (itemID > 0 && (data = ContentManager.GetItemData(itemID)) != null)
                        {
                            items.Add(data);
                        }
                    }
                    _items = items.ToArray();
                }
                return _items;
            }
        }
        private int _totalPrice = -1;
        public int TotalPrice
        {
            get
            {
                if (_totalPrice <= -1)
                {
                    _totalPrice = 0;
                    foreach (var item in Items)
                    {
                        _totalPrice += item.TotalPrice;
                    }
                    _totalPrice += _itemData.Price;
                }

                return _totalPrice;
            }
        }

        public ItemRecipe(ItemData itemData)
        {
            _itemData = itemData;
        }
    }
}