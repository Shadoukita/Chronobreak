namespace Buffs
{
    public class MonkeyKingCloneSweep : BuffScript
    {
        public override void OnActivate()
        {
            int _1; // UNITIALIZED
            _1 = 1; //TODO: Verify
            SpellCast((ObjAIBase)owner, owner, default, default, 2, SpellSlotType.SpellSlots, _1, false, false, false, true, false, false);
        }
    }
}