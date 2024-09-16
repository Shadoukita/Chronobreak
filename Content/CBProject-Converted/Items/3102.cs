namespace ItemPassives
{
    public class ItemID_3102 : ItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(2, ref lastTimeExecuted, false))
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.BansheesVeilTimer)) == 0)
                {
                    bool nextBuffVars_WillRemove = false;
                    AddBuff(owner, owner, new Buffs.BansheesVeil(nextBuffVars_WillRemove), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
                }
            }
        }
        public override void OnActivate()
        {
            bool nextBuffVars_WillRemove;
            if (owner is not Champion)
            {
                ObjAIBase caster = GetPetOwner((Pet)owner);
                if (GetBuffCountFromCaster(caster, caster, nameof(Buffs.BansheesVeil)) > 0)
                {
                    nextBuffVars_WillRemove = false;
                    AddBuff(owner, owner, new Buffs.BansheesVeil(nextBuffVars_WillRemove), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
                }
            }
            else
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.BansheesVeil)) == 0 && GetBuffCountFromCaster(owner, owner, nameof(Buffs.BansheesVeilTimer)) == 0)
                {
                    nextBuffVars_WillRemove = false;
                    AddBuff(owner, owner, new Buffs.BansheesVeil(nextBuffVars_WillRemove), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
                }
            }
        }
    }
}
namespace Buffs
{
    public class _3102 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "GuardianAngel_tar.troy", },
        };
    }
}