using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;

namespace ENet
{
    public struct Address : IEquatable<Address>
    {
        public const uint IPv4HostAny = LENet.Address.Any;
        
        public const uint IPv4HostBroadcast = LENet.Address.Broadcast;

        private uint _host;
        private ushort _port;

        public Address(uint address, ushort port)
        {
            _host = address;
            _port = port;
        }

        public Native.ENetAddress NativeData 
        {
            get => new Native.ENetAddress { host = _host, port = _port };
            set { _host = value.host; _port = value.port; }
        }

        public uint IPv4Host
        {
            get => _host;
            set => _host = value;
        }

        public ushort Port
        {
            get => _port;
            set => _port = value;
        }

        public AddressType Type => AddressType.IPv4;

        public bool Equals(Address other)
        {
            return Type == other.Type && NativeData.Equals(other.NativeData);
        }

        public override bool Equals(object obj)
        {
            return obj is Address && Equals((Address)obj);
        }

        public override int GetHashCode()
        {
            return Type.GetHashCode() ^ Port.GetHashCode() ^ IPv4Host.GetHashCode();
        }

        public byte[] GetHostBytes()
        {
            return BitConverter.GetBytes(IPAddress.NetworkToHostOrder((int)IPv4Host));
        }

        public string GetHostName()
        {
            try
            {
                var result = Dns.GetHostEntry(new IPAddress(IPv4Host));
                if(result == null)
                {
                    return null;
                }
                return result.HostName;
            }
            catch(Exception)
            {
                return null;
            }
        }

        public string GetHostIP()
        {
            return new IPAddress(IPv4Host).ToString();
        }

        public bool SetHost(string hostName)
        {
            try
            {
                var result1 = Dns.GetHostEntry(hostName);
                if(result1 == null)
                {
                    return false;
                }

                if(result1.AddressList.Length == 0)
                {
                    return false;
                }

                var result2 = result1.AddressList.First(addr => addr.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                if (result2 == null)
                {
                    return false;
                }

                IPv4Host = (uint)result2.Address;
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
