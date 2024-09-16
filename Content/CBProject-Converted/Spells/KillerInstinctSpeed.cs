namespace Buffs
{
    public class KillerInstinctSpeed : BuffScript
    {
        int level;
        EffectEmitter kISpeed;
        int[] effect0 = { 4, 5, 6, 7, 8 };
        public KillerInstinctSpeed(int level = default, EffectEmitter kISpeed = default)
        {
            this.level = level;
            this.kISpeed = kISpeed;
        }
        public override void OnActivate()
        {
            //RequireVar(this.level);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(kISpeed);
        }
        public override void OnUpdateStats()
        {
            int level = this.level;
            float attackDamageBoon = effect0[level - 1];
            IncFlatPhysicalDamageMod(owner, attackDamageBoon);
        }
    }
}