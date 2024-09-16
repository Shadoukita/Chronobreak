using GameServerCore.Enums;
using Chronobreak.GameServer.Scripting.CSharp;

namespace Chronobreak.GameServer.GameObjects.AttackableUnits;

public class HealData
{

    public AttackableUnit? SourceUnit { get; private set; }
    public AttackableUnit TargetUnit { get; private set; }
    public float HealAmount { get; internal set; }
    public AddHealthType HealType { get; private set; }
    public IEventSource SourceScript { get; private set; }

    //I'm not exactly sure what to do with the variables bellow.
    /*
    public uint ScriptNameHash { get; private set; }
    public uint ParentScriptNameHash { get; private set; }
    public TeamType TeamType { get; private set; }
    EventSystem::EventSourceType parentEventSourceType
    bool IsDeathRecapSource;
    */

    public HealData(AttackableUnit targetUnit, float healAmount, AttackableUnit? sourceUnit = null, AddHealthType healType = AddHealthType.HEAL, IEventSource sourceScript = null)
    {
        TargetUnit = targetUnit;
        SourceUnit = sourceUnit ?? TargetUnit;
        HealAmount = healAmount;
        HealType = healType;
        SourceScript = sourceScript;
    }
}
