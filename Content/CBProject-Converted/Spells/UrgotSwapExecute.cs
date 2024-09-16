namespace Spells
{
    public class UrgotSwapExecute : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = false,
        };
    }
}
namespace Buffs
{
    public class UrgotSwapExecute : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "UrgotSwapExecute",
            BuffTextureName = "UrgotPositionReverser.dds",
        };
        public override void OnActivate()
        {
            Vector3 urgotPos = GetUnitPosition(attacker);
            Vector3 targetPos = GetUnitPosition(owner);
            TeleportToPosition(attacker, targetPos);
            TeleportToPosition(owner, urgotPos);
            TeleportToPosition(attacker, targetPos);
            AddBuff(attacker, owner, new Buffs.UrgotSwapMissile(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(attacker, attacker, new Buffs.UrgotSwapMissile2(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            SetCanCast(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanCast(owner, true);
            float nextBuffVars_MoveSpeedMod = -0.4f;
            AddBuff(attacker, owner, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false);
        }
    }
}