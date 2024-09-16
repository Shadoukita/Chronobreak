namespace Buffs
{
    public class DetonatingShot_Target : BuffScript
    {
        public override void OnUpdateActions()
        {
            SpellBuffRemoveCurrent(owner);
        }
    }
}