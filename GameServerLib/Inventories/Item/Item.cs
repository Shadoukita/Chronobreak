using System;

namespace Chronobreak.GameServer.Inventory
{
    public class Item
    {
        public int StackCount { get; protected set; }

        public ItemData ItemData { get; }

        public Item(ItemData data)
        {
            ItemData = data;
            StackCount = 1;
        }

        public bool IncrementStackCount()
        {
            if (StackCount >= ItemData.MaxStacks)
            {
                return false;
            }

            StackCount++;
            return true;
        }

        public bool DecrementStackCount()
        {
            if (StackCount < 1)
            {
                return false;
            }

            StackCount--;
            return true;
        }

        public void SetStacks(int newStacks)
        {
            if (StackCount == newStacks)
            {
                return;
            }

            if (newStacks < 1 || newStacks > ItemData.MaxStacks)
            {
                throw new Exception($"Cannot set stack size out of bounds (max is {ItemData.MaxStacks})");
            }

            StackCount = newStacks;
        }
    }
}