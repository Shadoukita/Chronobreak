namespace Buffs
{
    public class EternalThirst_Internal : BuffScript
    {
        float lifeStealAmount;
        public EternalThirst_Internal(float lifeStealAmount = default)
        {
            this.lifeStealAmount = lifeStealAmount;
        }
        public override void OnActivate()
        {
            //RequireVar(this.lifeStealAmount);
            int count = GetBuffCountFromAll(owner, nameof(Buffs.EternalThirst));
            float lifeStealToHeal = lifeStealAmount * count;
            IncHealth(attacker, lifeStealToHeal, attacker);
            SpellEffectCreate(out _, out _, "EternalThirst_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, target, default, default, false);
        }
    }
}