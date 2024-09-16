namespace Buffs
{
    public class KarmaChakraTimer : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "KarmaMantraRefresh",
            BuffTextureName = "KarmaMantraCharging.dds",
            NonDispellable = true,
        };
        public override void OnDeactivate(bool expired)
        {
            if (!IsDead(owner))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.KarmaChakraCharge(), 2, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.KarmaOneMantraParticle)) > 0)
                {
                    SpellBuffRemove(owner, nameof(Buffs.KarmaOneMantraParticle), (ObjAIBase)owner);
                    AddBuff((ObjAIBase)owner, owner, new Buffs.KarmaTwoMantraParticle(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.KarmaOneMantraParticle)) == 0)
                {
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.KarmaTwoMantraParticle)) == 0)
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.KarmaOneMantraParticle(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
                int count = GetBuffCountFromAll(owner, nameof(Buffs.KarmaChakraCharge));
                if (count <= 1)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.KarmaChakraTimer(), 1, 1, charVars.MantraTimerCooldown, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
        }
    }
}