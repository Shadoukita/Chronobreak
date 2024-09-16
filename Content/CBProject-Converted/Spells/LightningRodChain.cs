namespace Buffs
{
    public class LightningRodChain : BuffScript
    {
        float bounceCounter;
        EffectEmitter particleID; // UNUSED
        public LightningRodChain(float bounceCounter = default)
        {
            this.bounceCounter = bounceCounter;
        }
        public override void OnActivate()
        {
            //RequireVar(this.bounceCounter);
            TeamId teamID = GetTeamID_CS(attacker);
            if (bounceCounter <= 3)
            {
                foreach (AttackableUnit unit in GetClosestUnitsInArea(attacker, owner.Position3D, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.LightningRodChain), false))
                {
                    SpellEffectCreate(out particleID, out _, "kennen_btl_beam.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, unit, false, owner, "head", default, unit, "root", default, true, false, false, false, false);
                    bounceCounter++;
                    float nextBuffVars_BounceCounter = bounceCounter;
                    AddBuff(attacker, unit, new Buffs.LightningRodChain(nextBuffVars_BounceCounter), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
            ApplyDamage(attacker, owner, 110, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, attacker);
            SpellEffectCreate(out _, out _, "kennen_btl_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
        }
    }
}