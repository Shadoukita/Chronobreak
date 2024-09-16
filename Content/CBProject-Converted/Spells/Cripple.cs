namespace Buffs
{
    public class Cripple : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Cripple",
            BuffTextureName = "48thSlave_Enfeeble.dds",
        };
        float attackSpeedMod;
        public Cripple(float attackSpeedMod = default)
        {
            this.attackSpeedMod = attackSpeedMod;
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, attackSpeedMod);
        }
        public override void OnActivate()
        {
            //RequireVar(this.attackSpeedMod);
            ApplyAssistMarker(attacker, target, 10);
        }
    }
}