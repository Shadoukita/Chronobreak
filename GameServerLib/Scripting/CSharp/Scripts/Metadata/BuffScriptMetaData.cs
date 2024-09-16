
using GameServerCore.Enums;

namespace Chronobreak.GameServer.Scripting.CSharp;

public class BuffScriptMetaData
{
    public BuffAddType BuffAddType = BuffAddType.RENEW_EXISTING;
    public BuffType BuffType = BuffType.INTERNAL;
    public int MaxStacks = 1;
    public float TickRate;
    public bool StacksExclusive;
    public bool CanMitigateDuration;
    public bool IsHidden = false;
}