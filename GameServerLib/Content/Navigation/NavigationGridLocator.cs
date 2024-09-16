using System;
using System.IO;

namespace Chronobreak.GameServer.Content.Navigation
{
    public struct NavigationGridLocator
    {
        public short X { get; private set; }
        public short Y { get; private set; }

        public NavigationGridLocator(short x, short y)
        {
            X = x;
            Y = y;
        }
        public NavigationGridLocator(BinaryReader br)
        {
            X = br.ReadInt16();
            Y = br.ReadInt16();
        }

        public static int Distance(NavigationGridLocator a, NavigationGridLocator b)
        {
            return (Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y));
        }
    }
}