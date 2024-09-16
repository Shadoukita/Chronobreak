using System;

namespace ENet
{
    public class Host : IDisposable
    {
        private LENet.Host _host;

        public bool IsSet
        {
            get { return _host != null; }
        }

        public LENet.Host NativeData
        {
            get { return _host; }
            set { _host = value; }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        ~Host()
        {
            Dispose(false);
        }

        private void CheckChannelLimit(int channelLimit)
        {
            if (channelLimit < 0 || channelLimit > LENet.Host.MAXIMUM_CHANNEL_COUNT)
            {
                throw new ArgumentOutOfRangeException("channelLimit");
            }
        }

        private void CheckCreated()
        {
            if (_host == null)
            {
                throw new InvalidOperationException("Not created.");
            }
        }

        public void Create(ushort port, int peerLimit)
        {
            var address = new Address();
            address.Port = port;
            Create(address, peerLimit);
        }

        public void Create(Address? address, int peerLimit)
        {
            Create(address, peerLimit, 0);
        }

        public void Create(Address? address, int peerLimit, int channelLimit)
        {
            Create(address, peerLimit, channelLimit, 0, 0);
        }

        public void Create(Address? address, int peerLimit, int channelLimit, uint incomingBandwidth, uint outgoingBandwidth)
        {
            var version = LENet.Version.Patch420;

            if (_host != null)
            {
                throw new InvalidOperationException("Already created.");
            }

            if (peerLimit < 0 || peerLimit > version.MaxPeerID)
            {
                throw new ArgumentOutOfRangeException("peerLimit");
            }

            CheckChannelLimit(channelLimit);

            LENet.Address? nativeAddress = null;
            if(address is Address addr)
            {
                nativeAddress = new LENet.Address(addr.IPv4Host, addr.Port);
            }
            try
            {
                _host = new LENet.Host(version, nativeAddress, (uint)peerLimit, (uint)channelLimit, incomingBandwidth, outgoingBandwidth);    
            }
            catch(Exception)
            {
                throw new ENetException(0, "Host creation call failed.");
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_host != null)
            {
                _host.Dispose();
                _host = null;
            }
        }

        public void Broadcast(byte channelID, ref Packet packet)
        {
            CheckCreated();
            packet.CheckCreated();
            _host.Broadcast(channelID, packet.NativeData);
            packet.NativeData = null;
        }

        public void CompressWithRangeCoder()
        {
            CheckCreated();
            throw new NotImplementedException("Host.CompressWithRangeCoder not supported in LENet!");
        }

        public void DoNotCompress()
        {
            CheckCreated();
            throw new NotImplementedException("Host.DoNotCompress not supported in LENet!");
        }

        public int CheckEvents(out Event @event)
        {
            CheckCreated();
            var nativeEvent = new LENet.Event();
            var ret = _host.CheckEvents(nativeEvent);
            if (ret <= 0)
            {
                @event = new Event();
                return ret;
            }
            @event = new Event(nativeEvent);
            return ret;
        }

        public Peer Connect(Address address, int channelLimit, uint data)
        {
            CheckCreated();
            CheckChannelLimit(channelLimit);

            var nativeAddress = new LENet.Address(address.IPv4Host, address.Port);
            try
            {
                var peer = new Peer(_host.Connect(nativeAddress, (uint)channelLimit));
                if (peer.NativeData == null)
                {
                    throw new ENetException(0, "Host connect call failed.");
                }
                return peer;
            }
            catch(Exception)
            {
                throw new ENetException(0, "Host connect call failed.");
            }
        }

        public void Flush()
        {
            CheckCreated();
            _host.Flush();
        }

        public int Service(int timeout)
        {
            if (timeout < 0)
            {
                throw new ArgumentOutOfRangeException("timeout");
            }
            CheckCreated();
            return _host.HostService(null, (uint)timeout);
        }

        public int Service(int timeout, out Event @event)
        {
            if (timeout < 0)
            {
                throw new ArgumentOutOfRangeException("timeout");
            }
            CheckCreated();
            var nativeEvent = new LENet.Event();

            var ret = _host.HostService(nativeEvent, (uint)timeout);
            if (ret <= 0)
            {
                @event = new Event();
                return ret;
            }
            @event = new Event(nativeEvent);
            return ret;
        }

        public void SetBandwidthLimit(uint incomingBandwidth, uint outgoingBandwidth)
        {
            CheckCreated();
            _host.SetBandwidthLimit(incomingBandwidth, outgoingBandwidth);
        }

        public void SetChannelLimit(int channelLimit)
        {
            CheckChannelLimit(channelLimit);
            CheckCreated();
            _host.SetChannelLimit((uint)channelLimit);
        }

    }
}