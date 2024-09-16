namespace Buffs
{
    public class PropelTimer : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
        };
        public override void OnDeactivate(bool expired)
        {
            foreach (AttackableUnit unit in GetRandomUnitsInArea((ObjAIBase)owner, owner.Position3D, 1000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, default, true))
            {
                Vector3 propelPos = GetRandomPointInAreaUnit(unit, 100, 25);
                TeamId teamID = GetTeamID_CS(owner); // UNUSED
                Minion other1 = SpawnMinion("DontSeeThisPlease", "SpellBook1", "idle.lua", propelPos, TeamId.TEAM_NEUTRAL, false, true, false, true, false, false, 0, default, true);
                AddBuff((ObjAIBase)owner, other1, new Buffs.PropelSpellCaster(), 1, 1, 2.1f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
                SpellEffectCreate(out _, out _, "PropelBubbles.troy", default, TeamId.TEAM_NEUTRAL, 600, 0, TeamId.TEAM_UNKNOWN, default, owner, false, other1, default, propelPos, target, default, default, true);
            }
            foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 800, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, default, true))
            {
                FaceDirection(owner, unit.Position3D);
                SpellCast((ObjAIBase)owner, unit, owner.Position3D, owner.Position3D, 3, SpellSlotType.SpellSlots, 1, false, false, false, false, default, false);
            }
        }
    }
}