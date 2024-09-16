namespace Buffs
{
    public class UrgotDeathParticle : BuffScript
    {
        public override void OnActivate()
        {
            SpellEffectCreate(out _, out _, "UrgotDeath.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "f_flesh", default, target, default, default, false);
        }
    }
}