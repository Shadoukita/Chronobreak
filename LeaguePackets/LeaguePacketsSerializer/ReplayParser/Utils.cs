using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENetUnpack.ReplayParser
{
    public static class Utils
    {
        public static byte[] ReadExactBytes(this BinaryReader reader, int count)
        {
            var result = reader.ReadBytes(count);
            if(result.Length != count)
            {
                throw new IOException($"Failed to read bytes, expected: {count}, got: {result.Length}!");
            }
            return result;
        }

        public static ushort ReadUInt16(this BinaryReader reader, bool bigEndian = true)
        {
            if (!bigEndian)
                return reader.ReadUInt16();
            else
                return BitConverter.ToUInt16(reader.ReadExactBytes(2).Reverse().ToArray(), 0);
        }

        public static short ReadInt16(this BinaryReader reader, bool bigEndian = true)
        {
            if (!bigEndian)
                return reader.ReadInt16();
            else
                return BitConverter.ToInt16(reader.ReadExactBytes(2).Reverse().ToArray(), 0);
        }

        public static uint ReadUInt32(this BinaryReader reader, bool bigEndian = false)
        {
            if (!bigEndian)
                return reader.ReadUInt32();
            else
                return BitConverter.ToUInt32(reader.ReadExactBytes(4).Reverse().ToArray(), 0);
        }

        public static int ReadInt32(this BinaryReader reader, bool bigEndian = false)
        {
            if (!bigEndian)
                return reader.ReadInt32();
            else
                return BitConverter.ToInt32(reader.ReadExactBytes(4).Reverse().ToArray(), 0);
        }

        public static long BytesLeft(this BinaryReader reader)
        {
            return reader.BaseStream.Length - reader.BaseStream.Position;
        }
    }
}
