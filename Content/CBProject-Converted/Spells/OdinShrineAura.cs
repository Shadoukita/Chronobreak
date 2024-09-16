namespace Buffs
{
    public class OdinShrineAura : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinShamanAura",
            BuffTextureName = "",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        EffectEmitter particleOrder;
        EffectEmitter particleChaos;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            SpellEffectCreate(out particleOrder, out _, "odin_shrine_aura.troy", default, TeamId.TEAM_NEUTRAL, 250, 0, TeamId.TEAM_ORDER, default, owner, false, default, default, owner.Position3D, owner, default, default, false, default, default, false);
            SpellEffectCreate(out particleChaos, out _, "odin_shrine_aura.troy", default, TeamId.TEAM_NEUTRAL, 250, 0, TeamId.TEAM_CHAOS, default, owner, false, default, default, owner.Position3D, owner, default, default, false, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particleOrder);
            SpellEffectRemove(particleChaos);
        }
        public override void OnUpdateActions()
        {
            float _0_5; // UNITIALIZED
            _0_5 = 0.5f; //TODO: Verify
            if (ExecutePeriodically(0, ref lastTimeExecuted, false, _0_5))
            {
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 400, SpellDataFlags.AffectHeroes, default, true))
                {
                    AddBuff((ObjAIBase)owner, unit, new Buffs.OdinShrineBuff(), 1, 1, 45, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
        }
    }
}