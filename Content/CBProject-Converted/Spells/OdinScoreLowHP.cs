namespace Buffs
{
    public class OdinScoreLowHP : BuffScript
    {
        public override void OnDeactivate(bool expired)
        {
            if (!IsDead(owner))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.OdinScoreSurvivor(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}