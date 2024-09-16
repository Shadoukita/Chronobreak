using Chronobreak.GameServer.API;

namespace Buffs
{
    internal class AscRelicCaptureChannel : BuffScript
    {
        public override BuffScriptMetaData BuffMetaData { get; } = new()
        {
            BuffType = BuffType.AURA,
            BuffAddType = BuffAddType.REPLACE_EXISTING,
            IsHidden = true
        };

        EffectEmitter p1;
        Region r1;
        Region r2;
        float windUpTime = 1500.0f;
        bool castWindUp = false;
        public override void OnActivate()
        {
            castWindUp = true;

            //p1 = AddParticleLink(owner, "OdinCaptureBeam", Buff.SourceUnit, target, 1.5f, bindBone: "buffbone_glb_channel_loc", targetBone: "spine", flags: (FXFlags)32);
            r1 = AddUnitPerceptionBubble(target, 0.0f, Buff.Duration, TeamId.TEAM_ORDER, true, target);
            r2 = AddUnitPerceptionBubble(target, 0.0f, Buff.Duration, TeamId.TEAM_CHAOS, true, target);
        }

        public override void OnDeactivate(bool expired)
        {
            RemoveParticle(p1);
            r1.SetToRemove();
            r2.SetToRemove();
        }

        public override void OnUpdate()
        {
            if (castWindUp)
            {
                windUpTime -= Time.DeltaTime;
                if (windUpTime <= 0)
                {
                    //TODO: Impelemt in Csharp system
                    //PlayAnimation("Channel", 1, Owner, true, true/*flags: (AnimationFlags)224*/); //check
                    OldAPI.AddBuff("AscRelicSuppression", 10.0f, 1, Buff.OriginSpell, target, owner);
                    OldAPI.AddBuff("OdinChannelVision", 20.0f, 1, Buff.OriginSpell, owner, owner);

                    //p1 = AddParticleLink(Buff.SourceUnit, "OdinCaptureBeamEngaged", Buff.SourceUnit, target, Buff.Duration - Buff.TimeElapsed, bindBone: "BuffBone_Glb_CHANNEL_Loc", targetBone: "spine");

                    string teamBuff = "OdinBombSuppressionOrder";
                    if (owner.Team == TeamId.TEAM_CHAOS)
                    {
                        teamBuff = "OdinBombSuppressionChaos";
                    }
                    OldAPI.AddBuff(teamBuff, 10.0f, 1, Buff.OriginSpell, target, owner);

                    castWindUp = false;
                }
            }
        }
    }
}