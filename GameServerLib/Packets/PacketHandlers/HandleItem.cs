using GameServerLib.Packets;
using LeaguePackets.Game;
using Chronobreak.GameServer.Inventory;

namespace Chronobreak.GameServer.Packets.PacketHandlers
{
    internal class HandleItem : IPacketHandler<BuyItemReq>, IPacketHandler<C2S_UndoItemReq>, IPacketHandler<SwapItemReq>, IPacketHandler<RemoveItemReq>
    {
        private const byte ITEM_ACTIVE_OFFSET = 6;

        public bool HandlePacket(int userId, BuyItemReq req)
        {
            var champion = Game.PlayerManager.GetPeerInfo(userId).Champion;
            return champion.ShopEnabled
                   && !Game.Map.MapData.UnpurchasableItemList.Contains((int)req.ItemID)
                   && champion.ItemInventory.BuyItem(req.ItemID);
        }

        public bool HandlePacket(int userId, RemoveItemReq req)
        {
            var champion = Game.PlayerManager.GetPeerInfo(userId).Champion;
            var inventory = champion.ItemInventory;
            Item item;
            return champion.ShopEnabled
                && (item = inventory.GetItem(req.Slot)) != null
                && inventory.SellItem(item);
        }


        public bool HandlePacket(int userId, SwapItemReq req)
        {
            if (req.Source > 6 || req.Destination > 6)
            {
                return false;
            }

            var champion = Game.PlayerManager.GetPeerInfo(userId).Champion;

            // "Holy shit this needs refactoring" - Mythic, April 13th 2016
            champion.ItemInventory.SwapItems(req.Source, req.Destination);
            Game.PacketNotifier.NotifySwapItemAns(champion, req.Source, req.Destination);
            champion.SwapSpells((byte)(req.Source + ITEM_ACTIVE_OFFSET), (byte)(req.Destination + ITEM_ACTIVE_OFFSET));
            return true;
        }

        public bool HandlePacket(int userId, C2S_UndoItemReq req)
        {
            var champion = Game.PlayerManager.GetPeerInfo(userId).Champion;
            return champion.ShopEnabled && champion.ItemInventory.UndoLastAction();
        }
    }
}