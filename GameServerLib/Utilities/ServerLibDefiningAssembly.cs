using System.Reflection;

namespace Chronobreak.GameServer
{
    public static class ServerLibAssemblyDefiningType
    {
        public static Assembly Assembly => typeof(ServerLibAssemblyDefiningType).Assembly;
    }
}
