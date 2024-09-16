
using GameServerLib.Packets;
using LeaguePackets.Game;

namespace Chronobreak.GameServer.Packets.PacketHandlers
{
    public class HandleUpgradeSpellReq : IPacketHandler<NPC_UpgradeSpellReq>
    {
        public bool HandlePacket(int userId, NPC_UpgradeSpellReq req)
        {
            // TODO: Check if can up skill

            var champion = Game.PlayerManager.GetPeerInfo(userId).Champion;

            // Normal level-up:
            if (req.IsEvolve == false)
            {
                var s = champion.LevelUpSpell(req.Slot);
                if (s == null)
                {
                    return false;
                }
                Game.PacketNotifier.NotifyNPC_UpgradeSpellAns(userId, champion.NetId, req.Slot, s.Level, champion.Experience.SpellTrainingPoints.TrainingPoints);

                return true;
            }
            // Evolve Request:
            else
            {
                champion.EvolveSpell(req.Slot, champion.Spells[req.Slot].Script.MetaData.SpellEvolveDesc);
                return true;
            }
        }
    }
}
