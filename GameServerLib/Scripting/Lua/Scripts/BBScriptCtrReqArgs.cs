
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;

namespace Chronobreak.GameServer.Scripting.Lua
{
    public class BBScriptCtrReqArgs
    {
        public string Name;
        public AttackableUnit ScriptOwner;
        public Champion? UnitOwner;
        public BBScriptCtrReqArgs(string name, AttackableUnit scriptOwner, Champion? unitOwner = null)
        {
            Name = name;
            ScriptOwner = scriptOwner;
            UnitOwner = unitOwner;
        }
    }
}