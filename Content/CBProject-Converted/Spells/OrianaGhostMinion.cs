namespace Buffs
{
    public class OrianaGhostMinion : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
        };
        Region tempVision;
        EffectEmitter ring2;
        EffectEmitter ring1;
        EffectEmitter ring4;
        EffectEmitter ring3;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 ownerPos = GetUnitPosition(owner);
            SetGhosted(owner, true);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            SetForceRenderParticles(owner, true);
            SetInvulnerable(owner, true);
            SetTargetable(owner, false);
            tempVision = AddPosPerceptionBubble(teamID, 225, ownerPos, 25000, default, false);
            SpellEffectCreate(out ring2, out ring1, "yomu_ring_green.troy", "yomu_ring_red.troy", teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, ownerPos, owner, default, ownerPos, false, default, default, false, false);
            SpellEffectCreate(out ring4, out ring3, "oriana_ball_glow_green.troy", "oriana_ball_glow_red.troy", teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "balldown", ownerPos, owner, default, ownerPos, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetNoRender(owner, true);
            SetInvulnerable(owner, false);
            RemovePerceptionBubble(tempVision);
            SpellEffectRemove(ring1);
            SpellEffectRemove(ring2);
            SpellEffectRemove(ring3);
            SpellEffectRemove(ring4);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ExpirationTimer(), 1, 1, 0.249f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}