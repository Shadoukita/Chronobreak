namespace Buffs
{
    public class KarmaChakraCharge : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "KarmaMantraCharge",
            BuffTextureName = "KarmaMantra.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnUpdateAmmo()
        {
            int count = GetBuffCountFromAll(owner, nameof(Buffs.KarmaChakraCharge));
            if (count >= 2)
            {
                AddBuff(attacker, owner, new Buffs.KarmaChakraCharge(), 3, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
                SpellBuffRemove(owner, nameof(Buffs.KarmaOneMantraParticle), (ObjAIBase)owner, 0);
                AddBuff(attacker, owner, new Buffs.KarmaTwoMantraParticle(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            else
            {
                AddBuff(attacker, owner, new Buffs.KarmaChakraCharge(), 3, 1, charVars.MantraTimerCooldown, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
                AddBuff(attacker, owner, new Buffs.KarmaOneMantraParticle(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}