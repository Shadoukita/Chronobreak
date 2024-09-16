using System.IO;

namespace Chronobreak.GameServer.Content.Navigation
{
    public class NavigationHintNode
    {
        public float[] Distances { get; private set; } = new float[900];
        public NavigationGridLocator Locator { get; private set; }

        public NavigationHintNode(BinaryReader br)
        {
            for (int i = 0; i < Distances.Length; i++)
            {
                Distances[i] = br.ReadSingle();
            }

            Locator = new NavigationGridLocator(br);
        }
    }
}
