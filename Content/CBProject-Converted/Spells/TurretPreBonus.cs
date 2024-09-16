namespace Buffs
{
    public class TurretPreBonus : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Turret Idle",
            BuffTextureName = "096_Eye_of_the_Observer.dds",
        };
        float startDecay;
        public TurretPreBonus(float startDecay = default)
        {
            this.startDecay = startDecay;
        }
        public override void OnActivate()
        {
            //RequireVar(this.startDecay);
        }
        public override void OnDeactivate(bool expired)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.TurretBonus(), 1, 1, startDecay, BuffAddType.RENEW_EXISTING, BuffType.AURA, 60, true, false);
        }
    }
}