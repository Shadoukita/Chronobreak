namespace Buffs
{
    public class TeemoMushroomManager : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        public override void OnUpdateActions()
        {
            if (!IsDead(owner) && GetBuffCountFromCaster(owner, owner, nameof(Buffs.TeemoMushroomCounter)) == 0)
            {
                int count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.TeemoMushrooms));
                if (count != 3)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.TeemoMushroomCounter(), 1, 1, charVars.MushroomCooldown, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
            }
        }
    }
}