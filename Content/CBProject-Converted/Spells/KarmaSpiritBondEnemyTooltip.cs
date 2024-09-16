namespace Buffs
{
    public class KarmaSpiritBondEnemyTooltip : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "KarmaSpiritBondEnemy",
            BuffTextureName = "KarmaSpiritBond.dds",
        };
        public override void OnActivate()
        {
            if (GetBuffCountFromCaster(attacker, owner, nameof(Buffs.KarmaSpiritBondC)) == 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}