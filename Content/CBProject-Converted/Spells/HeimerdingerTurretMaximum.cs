namespace Buffs
{
    public class HeimerdingerTurretMaximum : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
        };
        public override void OnDeactivate(bool expired)
        {
            if (GetBuffCountFromCaster(attacker, owner, nameof(Buffs.H28GEvolutionTurret)) > 0)
            {
                SpellBuffRemove(attacker, nameof(Buffs.H28GEvolutionTurret), (ObjAIBase)owner, 0);
            }
        }
    }
}