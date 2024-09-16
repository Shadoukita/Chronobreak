namespace Buffs
{
    public class MonkeyKingKillCloneW : BuffScript
    {
        bool doOnce;
        EffectEmitter a;
        EffectEmitter b;
        EffectEmitter c;
        EffectEmitter d;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            doOnce = false;
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out a, out _, "monkeyKing_W_cas_team_ID_green.troy", default, teamID, 10, 0, teamID, default, owner, false, owner, "L_hand", default, owner, default, default, false, default, default, false, false);
            SpellEffectCreate(out b, out _, "monkeyKing_W_cas_team_ID_green.troy", default, teamID, 10, 0, teamID, default, owner, false, owner, "R_hand", default, owner, default, default, false, default, default, false, false);
            SpellEffectCreate(out c, out _, "monkeyKing_W_cas_team_ID_red.troy", default, teamID, 10, 0, GetEnemyTeam(teamID), default, owner, false, owner, "L_hand", default, owner, default, default, false, default, default, false, false);
            SpellEffectCreate(out d, out _, "monkeyKing_W_cas_team_ID_red.troy", default, teamID, 10, 0, GetEnemyTeam(teamID), default, owner, false, owner, "R_hand", default, owner, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(a);
            SpellEffectRemove(b);
            SpellEffectRemove(c);
            SpellEffectRemove(d);
            SetInvulnerable(owner, false);
            SetTargetable(owner, true);
            SetGhosted(owner, false);
            SetNoRender(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 9999, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, false))
            {
                SetNoRender(owner, true);
                if (!doOnce)
                {
                    Vector3 ownerPos = GetUnitPosition(owner);
                    TeamId teamID = GetTeamID_CS(owner);
                    SpellEffectCreate(out _, out _, "MonkeyKing_W_death_team_ID_green.troy", default, teamID, 10, 0, teamID, default, owner, false, default, default, ownerPos, owner, default, ownerPos, true, default, default, false, false);
                    SpellEffectCreate(out _, out _, "MonkeyKing_W_death_team_ID_red.troy", default, teamID, 10, 0, GetEnemyTeam(teamID), default, owner, false, default, default, ownerPos, owner, default, ownerPos, true, default, default, false, false);
                    doOnce = true;
                }
            }
        }
    }
}