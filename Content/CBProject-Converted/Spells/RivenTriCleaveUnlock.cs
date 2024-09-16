namespace Buffs
{
    public class RivenTriCleaveUnlock : BuffScript
    {
        public override void OnDeactivate(bool expired)
        {
            SpellBuffClear(owner, nameof(Buffs.UnlockAnimation));
        }
        public override void OnUpdateActions()
        {
            bool temp = IsMoving(owner);
            if (temp)
            {
                SpellBuffClear(owner, nameof(Buffs.RivenTriCleaveUnlock));
            }
        }
    }
}