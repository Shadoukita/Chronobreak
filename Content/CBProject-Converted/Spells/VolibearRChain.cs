namespace Buffs
{
    public class VolibearRChain : BuffScript
    {
        float bounceCounter;
        float volibearRDamage;
        float volibearRRatio;
        EffectEmitter particleID; // UNUSED
        public VolibearRChain(float bounceCounter = default, float volibearRDamage = default, float volibearRRatio = default)
        {
            this.bounceCounter = bounceCounter;
            this.volibearRDamage = volibearRDamage;
            this.volibearRRatio = volibearRRatio;
        }
        public override void OnActivate()
        {
            float nextBuffVars_BounceCounter;
            TeamId unitTeamID;
            float nextBuffVars_VolibearRDamage;
            float nextBuffVars_VolibearRRatio;
            bool last = true;
            TeamId teamID = GetTeamID_CS(attacker);
            SpellEffectCreate(out _, out _, "Volibear_R_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_BUFFBONE_GLB_CENTER_LOC", default, owner, default, default, true, false, false, false, false);
            //RequireVar(this.bounceCounter);
            //RequireVar(this.volibearRRatio);
            //RequireVar(this.volibearRDamage);
            float championPriority = 0;
            volibearRDamage *= 1;
            volibearRRatio *= 1;
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, default, true))
            {
                championPriority++;
            }
            if (championPriority > 0 && bounceCounter <= 3)
            {
                foreach (AttackableUnit unit in GetRandomVisibleUnitsInArea(attacker, owner.Position3D, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.VolibearRChain), false))
                {
                    SpellEffectCreate(out particleID, out _, "volibear_R_chain_lighting_01.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, unit, false, owner, "head", default, unit, "root", default, true, false, false, false, false);
                    bounceCounter++;
                    nextBuffVars_BounceCounter = bounceCounter;
                    nextBuffVars_VolibearRDamage = volibearRDamage;
                    nextBuffVars_VolibearRRatio = volibearRRatio;
                    championPriority += 999;
                    unitTeamID = GetTeamID_CS(unit);
                    AddUnitPerceptionBubble(unitTeamID, 10, owner, 0.75f, default, owner, false);
                    AddBuff(attacker, unit, new Buffs.VolibearRChain(nextBuffVars_BounceCounter, nextBuffVars_VolibearRDamage, nextBuffVars_VolibearRRatio), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    last = false;
                }
            }
            if (championPriority < 4 && bounceCounter <= 3)
            {
                foreach (AttackableUnit unit in GetRandomVisibleUnitsInArea(attacker, owner.Position3D, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, 1, nameof(Buffs.VolibearRChain), false))
                {
                    SpellEffectCreate(out particleID, out _, "volibear_R_chain_lighting_01.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, unit, false, owner, "head", default, unit, "root", default, true, false, false, false, false);
                    bounceCounter++;
                    nextBuffVars_BounceCounter = bounceCounter;
                    nextBuffVars_VolibearRDamage = volibearRDamage;
                    nextBuffVars_VolibearRRatio = volibearRRatio;
                    unitTeamID = GetTeamID_CS(unit);
                    AddUnitPerceptionBubble(unitTeamID, 10, owner, 0.75f, default, owner, false);
                    AddBuff(attacker, unit, new Buffs.VolibearRChain(nextBuffVars_BounceCounter, nextBuffVars_VolibearRDamage, nextBuffVars_VolibearRRatio), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    last = false;
                }
            }
            if (last)
            {
                teamID = GetTeamID_CS(owner);
                SpellEffectCreate(out _, out _, "Volibear_R_lasthit_sound.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            }
            TeamId ownerTeamID = GetTeamID_CS(owner);
            AddUnitPerceptionBubble(ownerTeamID, 250, attacker, 0.75f, default, default, false);
            BreakSpellShields(owner);
            ApplyDamage(attacker, owner, volibearRDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, volibearRRatio, 0, false, false, attacker);
        }
    }
}