namespace Spells
{
    public class PoppyParagonOfDemacia : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            ChainMissileParameters = new()
            {
                CanHitCaster = false,
                CanHitSameTarget = false,
                CanHitSameTargetConsecutively = false,
                MaximumHits = 4,
            },
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            AddBuff(owner, owner, new Buffs.PoppyParagonStats(), 10, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0);
            AddBuff(owner, owner, new Buffs.PoppyParagonStats(), 10, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0);
            AddBuff(owner, owner, new Buffs.PoppyParagonStats(), 10, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0);
            AddBuff(owner, owner, new Buffs.PoppyParagonStats(), 10, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0);
            AddBuff(owner, owner, new Buffs.PoppyParagonStats(), 10, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0);
            AddBuff(owner, owner, new Buffs.PoppyParagonStats(), 10, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0);
            AddBuff(owner, owner, new Buffs.PoppyParagonStats(), 10, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0);
            AddBuff(owner, owner, new Buffs.PoppyParagonStats(), 10, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0);
            AddBuff(owner, owner, new Buffs.PoppyParagonStats(), 10, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0);
            AddBuff(owner, owner, new Buffs.PoppyParagonStats(), 10, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0);
            AddBuff(owner, owner, new Buffs.PoppyParagonParticle(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
            AddBuff(owner, owner, new Buffs.PoppyParagonSpeed(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
            AddBuff(owner, owner, new Buffs.PoppyParagonIcon(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
        }
    }
}
namespace Buffs
{
    public class PoppyParagonOfDemacia : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "",
            BuffTextureName = "",
        };
    }
}