namespace Buffs
{
    public class UnstoppableForceStun : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "UnstoppableForceStun",
            BuffTextureName = "Malphite_UnstoppableForce.dds",
            PopupMessage = new[] { "game_floatingtext_Knockup", },
        };
        public override void OnActivate()
        {
            ObjAIBase attacker = GetBuffCasterUnit();
            TeamId teamID = GetTeamID_CS(attacker);
            SpellEffectCreate(out _, out _, "UnstoppableForce_stun.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true);
            Vector3 position = GetRandomPointInAreaUnit(owner, 125, 75);
            Move(owner, position, 100, 20, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 100);
            SetStunned(owner, true);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SetStunned(owner, false);
        }
    }
}