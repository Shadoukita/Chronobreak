namespace Buffs
{
    public class OdinGolemBombParticle : BuffScript
    {
        EffectEmitter sCP;
        EffectEmitter agony;
        public override void OnActivate()
        {
            SpellEffectCreate(out sCP, out _, "OdinGolemPlaceHolder.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false);
            SpellEffectCreate(out agony, out _, "OdinGolemPlaceholder2.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(sCP);
            SpellEffectRemove(agony);
        }
    }
}