namespace Spells
{
    public class TeemoMushrooms : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            ChainMissileParameters = new()
            {
                CanHitCaster = false,
                CanHitEnemies = true,
                CanHitFriends = false,
                CanHitSameTarget = false,
                CanHitSameTargetConsecutively = false,
                MaximumHits = 10,
            },
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class TeemoMushrooms : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Mushroom Stored",
            BuffTextureName = "Bowmaster_ArchersMark.dds",
            PersistsThroughDeath = true,
        };
        public override void OnUpdateAmmo()
        {
            int count = GetBuffCountFromAll(owner, nameof(Buffs.TeemoMushrooms));
            if (count >= 3)
            {
                AddBuff(attacker, owner, new Buffs.TeemoMushrooms(), 4, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
            }
            else
            {
                AddBuff(attacker, owner, new Buffs.TeemoMushrooms(), 4, 1, charVars.MushroomCooldown, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
            }
        }
    }
}