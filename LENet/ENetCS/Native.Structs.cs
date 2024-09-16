using System;
using System.Runtime.InteropServices;

namespace ENet
{
    public static class Native
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct ENetAddress
        {
            public uint host;
            public ushort port;
        }
    }
}
