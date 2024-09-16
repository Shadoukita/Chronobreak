namespace Spells
{
    public class YorickDecayed : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
        };
        public override void SelfExecute()
        {
            if (GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickSummonDecayed)) > 0)
            {
                SpellBuffClear(owner, nameof(Buffs.YorickSummonDecayed));
            }
            Vector3 targetPos = GetSpellTargetPos(spell);
            SpellCast(owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, false, false, true, false, false);
        }
    }
}