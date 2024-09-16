namespace Buffs
{
    public class OdinCenterRelicLightning : BuffScript
    {
        float bounceCounter;
        EffectEmitter particleID; // UNUSED
        public OdinCenterRelicLightning(float bounceCounter = default)
        {
            this.bounceCounter = bounceCounter;
        }
        public override void OnActivate()
        {
            //RequireVar(this.bounceCounter);
            TeamId teamID = GetTeamID_CS(attacker);
            if (bounceCounter <= 2)
            {
                foreach (AttackableUnit unit in GetClosestUnitsInArea(attacker, owner.Position3D, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.OdinCenterRelicLightning), false))
                {
                    SpellEffectCreate(out particleID, out _, "kennen_btl_beam.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, unit, false, owner, "head", default, unit, "root", default, true, default, default, false);
                    bounceCounter++;
                    float nextBuffVars_BounceCounter = bounceCounter;
                    AddBuff(attacker, unit, new Buffs.OdinCenterRelicLightning(nextBuffVars_BounceCounter), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
            ApplyDamage(attacker, owner, 80, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, attacker);
            SpellEffectCreate(out _, out _, "kennen_btl_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false);
        }
    }
}