using System;

namespace ENet
{
    [Flags]
    public enum PacketFlags
    {
        None = 0,
        Reliable = LENet.PacketFlags.RELIABLE,
        Unsequenced = LENet.PacketFlags.UNSEQUENCED,
        NoAllocate = LENet.PacketFlags.NO_ALLOCATE,
    }
}