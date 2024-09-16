namespace Buffs
{
    public class GalioBulwarkHeal : BuffScript
    {
        float healAmount;
        public GalioBulwarkHeal(float healAmount = default)
        {
            this.healAmount = healAmount;
        }
        public override void OnActivate()
        {
            //RequireVar(this.healAmount);
            IncHealth(owner, healAmount, owner);
            SpellEffectCreate(out _, out _, "galio_bulwark_heal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
        }
    }
}