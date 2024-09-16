namespace ENet
{
    public class Event
    {
        internal LENet.Event _event;

        public Event(LENet.Event @event)
        {
            _event = @event;
        }

        public Event()
        {
            _event = null;
        }

        public byte ChannelID
        {
            get { return _event.ChannelID; }
        }

        public uint Data
        {
            get { return _event.Data; }
            set { _event.Data = value; }
        }

        public LENet.Event NativeData
        {
            get { return _event; }
            set { _event = value; }
        }

        public Packet Packet
        {
            get { return new Packet(_event.Packet); }
        }

        public Peer Peer
        {
            get { return new Peer(_event.Peer); }
        }

        public EventType Type
        {
            get { return (EventType)_event.Type; }
        }
    }
}