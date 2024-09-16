using GameServerCore.Enums;
using GameServerLib.Packets;
using LeaguePackets.Game;
using Chronobreak.GameServer.GameObjects.AttackableUnits;

namespace Chronobreak.GameServer.Packets.PacketHandlers
{
    public class HandleUseObject : IPacketHandler<UseObjectC2S>
    {
        public bool HandlePacket(int userId, UseObjectC2S req)
        {
            var champion = Game.PlayerManager.GetPeerInfo(userId).Champion;
            var target = Game.ObjectManager.GetObjectById(req.TargetNetID) as AttackableUnit;

            champion.SetSpell(target.CharData.HeroUseSpell, (byte)SpellSlotType.UseSpellSlot, true);

            var s = champion.Spells[(short)SpellSlotType.UseSpellSlot];
            var ownerCastingSpell = champion.CastSpell;

            // Instant cast spells can be cast during other spell casts.
            /*
            if (s != null && champion.CanCast(s)
                && champion.ChannelSpell == null
                && (ownerCastingSpell == null
                || (ownerCastingSpell != null
                    && s.SpellData.Flags.HasFlag(SpellDataFlags.InstantCast))
                    && !ownerCastingSpell.SpellData.CantCancelWhileWindingUp))
            */
            {
                s.TryCast(target, target.Position3D, target.Position3D);
                return true;
            }

            //return false;
        }
    }
}
