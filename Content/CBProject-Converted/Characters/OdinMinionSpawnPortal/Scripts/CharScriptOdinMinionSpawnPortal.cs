namespace CharScripts
{
    public class CharScriptOdinMinionSpawnPortal : CharScript
    {
        public override void OnActivate()
        {
            SetMagicImmune(owner, true);
            SetPhysicalImmune(owner, true);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            SetCanAttack(owner, false);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            AddBuff(owner, owner, new Buffs.OdinMinionPortal(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
        }
    }
}