namespace Buffs
{
    public class HalloweenUrfAppear : BuffScript
    {
        EffectEmitter a;
        EffectEmitter b;
        EffectEmitter c;
        EffectEmitter d;
        EffectEmitter e;
        EffectEmitter f;
        EffectEmitter g;
        EffectEmitter h;
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.HalloweenUrfCD(), 1, 1, 9, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
            SetNoRender(owner, false);
            PlayAnimation("Idle1", 0, owner, false, false);
            SpellEffectCreate(out a, out _, "ghostUrf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, owner, default, default, false);
            SpellEffectCreate(out b, out _, "ghostUrf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "fish_main", default, owner, default, default, false);
            SpellEffectCreate(out c, out _, "ghostUrf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_tail", default, owner, default, default, false);
            SpellEffectCreate(out d, out _, "ghostUrf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_tail_3", default, owner, default, default, false);
            SpellEffectCreate(out e, out _, "ghostUrf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_uparm", default, owner, default, default, false);
            SpellEffectCreate(out f, out _, "ghostUrf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_uparm", default, owner, default, default, false);
            SpellEffectCreate(out g, out _, "ghostUrf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_flipper", default, owner, default, default, false);
            SpellEffectCreate(out h, out _, "ghostUrf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_flipper", default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetNoRender(owner, true);
            UnlockAnimation(owner, true);
            SpellEffectRemove(a);
            SpellEffectRemove(b);
            SpellEffectRemove(c);
            SpellEffectRemove(d);
            SpellEffectRemove(e);
            SpellEffectRemove(f);
            SpellEffectRemove(g);
            SpellEffectRemove(h);
        }
    }
}