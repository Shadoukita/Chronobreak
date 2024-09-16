using System;

namespace ENet
{
    public class Peer
    {
        private LENet.Peer _peer;

        public Peer(LENet.Peer peer)
        {
            _peer = peer;
        }

        public bool IsSet
        {
            get { return _peer != null; }
        }

        public uint RoundTripTime
        {
            get { return _peer.LastRoundTripTime; }
        }

        public LENet.Peer NativeData
        {
            get { return _peer; }
            set { _peer = value; }
        }

        public PeerState State
        {
            get { return IsSet ? (PeerState)_peer.State : PeerState.Uninitialized; }
        }

        private class IntPtrHolder
        {
            internal IntPtr ptr = (IntPtr)0;
        }

        public IntPtr UserData
        {
            get
            {
                CheckCreated();
                return _peer.UserData != null ? ((IntPtrHolder)_peer.UserData).ptr : (IntPtr)0;
            }
            set
            {
                CheckCreated();
                if (_peer.UserData == null)
                {
                    _peer.UserData = new IntPtrHolder { ptr = value };
                }
                else
                {
                    ((IntPtrHolder)_peer.UserData).ptr = value;
                }
            }
        }

        public ushort Mtu
        {
            get { return _peer.MTU; }
            set { _peer.MTU = value; }
        }

        public Native.ENetAddress Address
        {
            get { return new Native.ENetAddress { host = _peer.Address.Host, port = _peer.Address.Port }; }
        }

        private void CheckCreated()
        {
            if (_peer == null)
            {
                throw new InvalidOperationException("No native peer.");
            }
        }

        public void ConfigureThrottle(uint interval, uint acceleration, uint deceleration)
        {
            CheckCreated();
            _peer.ThrottleConfigure(interval, acceleration, deceleration);
        }

        public void Disconnect(uint data)
        {
            CheckCreated();
            _peer.Disconnect(data);
        }

        public void DisconnectLater(uint data)
        {
            CheckCreated();
            _peer.DisconnectLater(data);
        }

        public void DisconnectNow(uint data)
        {
            CheckCreated();
            _peer.DisconnectNow(data);
        }

        public void Ping()
        {
            CheckCreated();
            _peer.Ping();
        }

        public void Reset()
        {
            CheckCreated();
            _peer.Reset();
        }

        public bool Receive(out byte channelID, out Packet packet)
        {
            CheckCreated();
            var result = _peer.Recieve(out channelID);
            if(result == null)
            {
                packet = new Packet();
                return false;
            }
            packet = new Packet(result);
            return true;
        }

        public bool Send(byte channelID, byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            return Send(channelID, data, 0, data.Length);
        }

        public bool Send(byte channelID, byte[] data, int offset, int length)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            bool ret;
            using (var packet = new Packet())
            {
                packet.Create(data, offset, length);
                ret = Send(channelID, packet);
            }
            return ret;
        }

        public bool Send(byte channelID, Packet packet)
        {
            CheckCreated();
            packet.CheckCreated();
            return _peer.Send(channelID, packet.NativeData) >= 0;
        }
    }
}