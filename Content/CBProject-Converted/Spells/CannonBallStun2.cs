namespace Buffs
{
    public class CannonBallStun2 : BuffScript
    {
        public override void OnActivate()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.CannonBallStun)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.CannonBallStun), (ObjAIBase)owner);
            }
            ApplyStun((ObjAIBase)owner, owner, 0.75f);
        }
    }
}