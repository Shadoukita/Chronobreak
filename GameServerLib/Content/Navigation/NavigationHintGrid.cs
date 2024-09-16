using System.IO;

namespace Chronobreak.GameServer.Content.Navigation
{
    public class NavigationHintGrid
    {
        public NavigationHintNode[] HintNodes { get; private set; } = new NavigationHintNode[900];

        public NavigationHintGrid(BinaryReader br)
        {
            for (int i = 0; i < HintNodes.Length; i++)
            {
                HintNodes[i] = new NavigationHintNode(br);
            }
        }
    }
}
