using GameServerCore;
using GameServerLib.Packets;
using LeaguePackets.Game;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.GameObjects.SpellNS;

namespace Chronobreak.GameServer.Packets.PacketHandlers
{
    internal class HandleSpells : IPacketHandler<NPC_CastSpellReq>, IPacketHandler<C2S_SpellChargeUpdateReq>
    {
        public bool HandlePacket(int userId, NPC_CastSpellReq req)
        {
            AttackableUnit targetUnit = Game.ObjectManager.GetObjectById(req.TargetNetID) as AttackableUnit;
            Champion owner = Game.PlayerManager.GetPeerInfo(userId).Champion;
            if (owner is not null)
            {
                Spell spell = owner.Spells[req.Slot];
                if (spell is not null)
                {
                    return TryCast(spell, req, owner, targetUnit);
                }
            }
            return false;
        }

        private static bool TryCast(Spell s, NPC_CastSpellReq req, Champion owner, AttackableUnit targetUnit)
        {
            if (s.TryCast(targetUnit, req.Position.ToVector3(0), req.EndPosition.ToVector3(0)))
            {
                owner.ItemInventory.ClearUndoHistory();
                //TODO: Move to Spell.TryCast?
                if (s.IsItem)
                {
                    var item = s.Caster.ItemInventory.GetItem(s.SpellName);
                    if (item != null && item.ItemData.Consumed)
                    {
                        var inventory = owner.ItemInventory;
                        inventory.RemoveItem(inventory.GetItemSlot(item));
                    }
                }
                return true;
            }
            return false;
        }

        public bool HandlePacket(int userId, C2S_SpellChargeUpdateReq req)
        {
            //Game.PacketNotifier.NotifyS2C_SystemMessage($"X: {req.Position.X} Y: {req.Position.Y} Z: {req.Position.Z}");
            var channelSpell = Game.PlayerManager.GetPeerInfo(userId).Champion.ChannelSpell;
            if (channelSpell != null)
            {
                channelSpell.SpellChargePosition = req.Position;
            }
            return true;
        }
    }
}
