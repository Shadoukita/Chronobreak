namespace Buffs
{
    public class TalonShadowAssaultAnimBuff : BuffScript
    {
        public override void OnActivate()
        {
            PlayAnimation("Spell4", 0, owner, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            StopCurrentOverrideAnimation("Spell4", owner, true);
        }
    }
}