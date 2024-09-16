namespace ItemPassives
{
    public class ItemID_3001 : ItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateStats()
        {
            if (ExecutePeriodically(0.9f, ref lastTimeExecuted, false))
            {
                if (!IsDead(owner))
                {
                    int nextBuffVars_MagicResistanceMod = -20;
                    AddBuff(owner, owner, new Buffs.AbyssalScepterAuraSelf(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                    foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.AbyssalScepterAura), false))
                    {
                        AddBuff(owner, unit, new Buffs.AbyssalScepterAura(nextBuffVars_MagicResistanceMod), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.SHRED, 0, true, false, false);
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class _3001 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Abyssalscepter_itm.troy", },
        };
    }
}