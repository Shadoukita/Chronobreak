/*namespace ItemSpells
{
    public class AscWarp : SpellScript
    {
        public override SpellScriptMetadata ScriptMetadata { get; } = new()
        {
            // TODO
            TriggersSpellCasts = true
        };

        public Vector2 teleportTo;

        public override void OnSpellPreCast(AttackableUnit target, Vector2 start, Vector2 end)
        {
            var units = GetUnitsInRange(Spell.CurrentCastInfo.TargetPosition.ToVector2(), 500.0f, true).FindAll(x => x is Minion);

            if (units != null)
            {
                foreach (var unit in units)
                {
                    if (unit is Minion m && m.Name is "AscWarpIcon" && unit.Team == Owner.Team)
                    {
                        AddBuff("AscWarp", 3.5f, 1, Spell, Owner, Owner);
                        var minion = AddMinion(Owner, "TestCubeRender10Vision", "k", start, team: Owner.Team, targetable: false, isVisible: false, skinId: Owner.SkinID, isWard: true);
                        //NotifySpawnBroadcast(minion);
                        AddBuff("AscWarpTarget", 3.5f, 1, Spell, minion, Owner);
                    }
                }
            }
        }
    }
}*/