namespace Buffs
{
    public class SpellShieldMarker : BuffScript
    {
        public override void OnUpdateActions()
        {
            SpellBuffRemoveCurrent(owner);
        }
    }
}