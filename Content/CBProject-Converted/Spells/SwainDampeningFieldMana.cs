namespace Spells
{
    public class SwainDampeningFieldMana : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
    }
}
namespace Buffs
{
    public class SwainDampeningFieldMana : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "SwainDampeningFieldMana",
            BuffTextureName = "SwainCarrionRenewal.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float manaRegen;
        int[] effect0 = { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
        public override void OnActivate()
        {
            manaRegen = 10;
            SetBuffToolTipVar(1, manaRegen);
        }
        public override void OnKill(AttackableUnit target)
        {
            SpellEffectCreate(out _, out _, "NeutralMonster_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            IncPAR(owner, manaRegen, PrimaryAbilityResourceType.MANA);
        }
        public override void OnLevelUp()
        {
            int level = GetLevel(owner);
            manaRegen = effect0[level - 1];
            SetBuffToolTipVar(1, manaRegen);
        }
    }
}