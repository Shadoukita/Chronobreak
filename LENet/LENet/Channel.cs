using System;

namespace LENet
{
    public sealed class Channel
    {
        public ushort OutgoingReliableSequenceNumber { get; set; }
        public ushort OutgoingUnreliableSequenceNumber { get; set; }
        public ushort UsedReliableWindows { get; set; }
        public ushort[] ReliableWindows { get; } = new ushort[Peer.RELIABLE_WINDOWS];
        public ushort IncomingReliableSequenceNumber { get; set; }
        public ushort IncomingUnreliableSequenceNumber { get; set; }
        public LList<IncomingCommand> IncomingReliableCommands { get; } = new LList<IncomingCommand>();
        public LList<IncomingCommand> IncomingUnreliableCommands { get; } = new LList<IncomingCommand>();
        
        public void Reset()
        {
            OutgoingReliableSequenceNumber = 0;
            OutgoingUnreliableSequenceNumber = 0;
            UsedReliableWindows = 0;
            Array.Clear(ReliableWindows, 0, ReliableWindows.Length);
            IncomingReliableSequenceNumber = 0;
            IncomingUnreliableSequenceNumber = 0;
            IncomingReliableCommands.Clear();
            IncomingUnreliableCommands.Clear();
        }
    }
}
