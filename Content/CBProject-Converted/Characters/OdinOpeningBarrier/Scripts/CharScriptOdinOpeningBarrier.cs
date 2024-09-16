namespace CharScripts
{
    public class CharScriptOdinOpeningBarrier : CharScript
    {
        public override void OnActivate()
        {
            SetMagicImmune(owner, true);
            SetPhysicalImmune(owner, true);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            SetCanAttack(owner, false);
            SetForceRenderParticles(owner, true);
            AddBuff(owner, owner, new Buffs.OdinOpeningBarrierParticle(), 1, 1, 80, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
        }
    }
}