namespace Buffs
{
    public class JackInTheBoxHardLock : BuffScript
    {
        public override void OnActivate()
        {
            SpellBuffClear(owner, nameof(Buffs.JackInTheBoxSoftLock));
        }
    }
}