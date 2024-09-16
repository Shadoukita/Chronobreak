namespace Buffs
{
    public class GarenSlashBuff : BuffScript
    {
        public override void OnActivate()
        {
            DebugSay(owner, "Slash Buff On");
        }
        public override void OnDeactivate(bool expired)
        {
            RemoveOverrideAutoAttack(owner, false);
        }
    }
}