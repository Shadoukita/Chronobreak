namespace Spells
{
    public class UdyrMonkeyAgilityBuff : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class UdyrMonkeyAgilityBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "UdyrMonkeyAgilityBuff",
            BuffTextureName = "Udyr_MonkeysAgility.dds",
        };
        EffectEmitter a;
        public override void OnActivate()
        {
            SpellEffectCreate(out a, out _, "UdyrBuff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(a);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, 0.1f);
            int monkeyStacks = GetBuffCountFromAll(owner, nameof(Buffs.UdyrMonkeyAgilityBuff));
            float attackSpeedMod = 10 * monkeyStacks;
            SetBuffToolTipVar(1, attackSpeedMod);
        }
    }
}