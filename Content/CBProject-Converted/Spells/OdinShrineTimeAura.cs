namespace Buffs
{
    public class OdinShrineTimeAura : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinShrineAura",
            BuffTextureName = "",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        EffectEmitter particleOrder;
        EffectEmitter particleChaos;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            SpellEffectCreate(out particleOrder, out _, "odin_shrine_time.troy", default, TeamId.TEAM_NEUTRAL, 250, 0, TeamId.TEAM_ORDER, default, owner, false, default, default, owner.Position3D, owner, default, default, false);
            SpellEffectCreate(out particleChaos, out _, "odin_shrine_time.troy", default, TeamId.TEAM_NEUTRAL, 250, 0, TeamId.TEAM_CHAOS, default, owner, false, default, default, owner.Position3D, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particleOrder);
            SpellEffectRemove(particleChaos);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, false))
            {
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 400, SpellDataFlags.AffectHeroes, default, true))
                {
                    if (GetBuffCountFromCaster(unit, unit, nameof(Buffs.OdinShrineTimeBuff)) == 0)
                    {
                        AddBuff((ObjAIBase)unit, unit, new Buffs.OdinShrineTimeBuff(), 1, 1, 60, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    }
                }
            }
        }
    }
}