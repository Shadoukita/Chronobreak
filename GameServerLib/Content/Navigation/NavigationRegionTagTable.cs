using System.IO;

namespace Chronobreak.GameServer.Content.Navigation
{
    public class NavigationRegionTagTable
    {
        public NavigationRegionTagTableGroupTag[] Groups { get; private set; }

        public NavigationRegionTagTable(BinaryReader br, uint groupCount)
        {
            Groups = new NavigationRegionTagTableGroupTag[groupCount];

            for (int i = 0; i < Groups.Length; i++)
            {
                Groups[i] = new NavigationRegionTagTableGroupTag(br);
            }
        }
    }
}
