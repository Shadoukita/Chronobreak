using System;
using System.Runtime.InteropServices;

namespace ENet
{
    public struct Packet : IDisposable
    {
        private LENet.Packet _packet;

        public Packet(LENet.Packet packet)
        {
            _packet = packet;
        }

        public IntPtr Data
        {
            get
            {
                CheckCreated();
                throw new NotImplementedException("Packet.Data not supported in LENet, use Packet.GetBytes()!");
            }
        }

        public int Length
        {
            get
            {
                CheckCreated();
                return _packet.Data.Length;
            }
        }

        public LENet.Packet NativeData
        {
            get { return _packet; }
            set { _packet = value; }
        }

        public bool IsSet
        {
            get { return _packet != null; }
        }

        public void Dispose()
        {
            if (_packet != null)
            {
                _packet = null;
            }
        }

        internal void CheckCreated()
        {
            if (_packet == null)
            {
                throw new InvalidOperationException("No native packet.");
            }
        }

        public void Create(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            Create(data, 0, data.Length);
        }

        public void Create(byte[] data, int offset, int length)
        {
            Create(data, offset, length, PacketFlags.None);
        }

        public void Create(byte[] data, int offset, int length, PacketFlags flags)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            if (offset < 0 || length < 0 || length > data.Length - offset)
            {
                throw new ArgumentOutOfRangeException();
            }

            _packet = new LENet.Packet((uint)length, (LENet.PacketFlags)flags);
            Array.Copy(data, offset, _packet.Data, 0, length);
        }

        public void Create(IntPtr data, int length, PacketFlags flags)
        {
            _packet = new LENet.Packet((uint)length, (LENet.PacketFlags)flags);
            Marshal.Copy(data, _packet.Data, 0, length);
        }

        public void CopyTo(byte[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            CopyTo(array, 0, array.Length);
        }

        public void CopyTo(byte[] array, int offset, int length)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            if (offset < 0 || length < 0 || length > array.Length - offset)
            {
                throw new ArgumentOutOfRangeException();
            }

            CheckCreated();
            
            if (length > Length - offset)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (length > 0)
            {
                Array.Copy(_packet.Data, (uint)offset, array, (uint)offset, (uint)length);
            }
        }

        public byte[] GetBytes()
        {
            CheckCreated();
            var array = new byte[Length];
            CopyTo(array);
            return array;
        }

        public void Resize(int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length");
            }
            CheckCreated();
            var ret = _packet.Resize((uint)length);
            if (ret < 0)
            {
                throw new ENetException(ret, "Packet resize call failed.");
            }
        }
    }
}