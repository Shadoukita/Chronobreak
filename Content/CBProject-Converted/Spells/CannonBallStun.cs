namespace Buffs
{
    public class CannonBallStun : BuffScript
    {
        public override void OnActivate()
        {
            ApplyStun((ObjAIBase)owner, owner, 1.5f);
        }
    }
}