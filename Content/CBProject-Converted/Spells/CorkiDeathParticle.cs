namespace Buffs
{
    public class CorkiDeathParticle : BuffScript
    {
        public override void OnActivate()
        {
            SpellEffectCreate(out _, out _, "CrashBoom.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, target, default, default, false);
        }
    }
}