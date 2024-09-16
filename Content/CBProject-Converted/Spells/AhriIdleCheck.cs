namespace Buffs
{
    public class AhriIdleCheck : BuffScript
    {
        public override void OnDeactivate(bool expired)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.AhriSoulCrusher)) > 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.AhriPassiveParticle(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            else
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.AhriIdleParticle(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}