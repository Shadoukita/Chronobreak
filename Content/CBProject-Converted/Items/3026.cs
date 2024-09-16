namespace ItemPassives
{
    public class ItemID_3026 : ItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(9, ref lastTimeExecuted, true))
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.HasBeenRevived)) == 0)
                {
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.GuardianAngel)) == 0)
                    {
                        AddBuff(owner, owner, new Buffs.WillRevive(), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                    }
                }
            }
        }
        public override void OnActivate()
        {
            if (owner is not Champion)
            {
                ObjAIBase caster = GetPetOwner((Pet)owner);
                if (GetBuffCountFromCaster(caster, caster, nameof(Buffs.WillRevive)) > 0)
                {
                    AddBuff(owner, owner, new Buffs.WillRevive(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                }
                else
                {
                    AddBuff(owner, owner, new Buffs.HasBeenRevived(), 1, 1, 300, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
            else
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.WillRevive)) == 0 && GetBuffCountFromCaster(owner, owner, nameof(Buffs.HasBeenRevived)) == 0)
                {
                    AddBuff(owner, owner, new Buffs.WillRevive(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                }
            }
        }
    }
}
namespace Buffs
{
    public class _3026 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "GuardianAngel_tar.troy", },
        };
    }
}