namespace Buffs
{
    public class AkaliShadowSwipeHealingParticle : BuffScript
    {
        public override void OnActivate()
        {
            SpellEffectCreate(out _, out _, "akali_shadowSwipe_heal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true);
        }
    }
}