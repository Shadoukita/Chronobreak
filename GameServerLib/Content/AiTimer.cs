using System;

namespace GameServerLib.Content;
internal class AiTimer
{
    internal float Elapsed;
    internal float Delay;
    internal bool Repeat;
    internal bool Enabled;
    internal string Name;
    internal Action Callback;
}
