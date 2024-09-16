namespace Buffs
{
    public class SwainBeamTransition : BuffScript
    {
        int casterID;
        public override void OnActivate()
        {
            casterID = PushCharacterData("SwainNoBird", owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            PopCharacterData(owner, casterID);
        }
    }
}