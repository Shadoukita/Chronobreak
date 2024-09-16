
namespace Buffs
{
    internal class AscBuff : BuffScript
    {
        public override BuffScriptMetaData BuffMetaData { get; } = new()
        {
            BuffType = BuffType.INTERNAL,
            BuffAddType = BuffAddType.REPLACE_EXISTING
        };

        EffectEmitter p1;
        EffectEmitter p2;
        Region r1;
        Region r2;
        public override void OnActivate()
        {
            if (target is ObjAIBase obj)
            {
                ApiFunctionManager.AddBuff("AscBuffIcon", 25000.0f, 1, null, target, obj);
            }

            PlaySound("Stop_sfx_ZhonyasRingShield_OnBuffActivate", target);
            PlaySound("Play_sfx_Leona_LeonaSolarFlare_hit", target);
            PlaySound("Play_sfx_Lux_LuxIlluminationPassive_hit", target);

            //p1 = AddParticleBind(target, "Global_Asc_Avatar", target, lifetime: -1);
            //p2 = AddParticleBind(target, "Global_Asc_Aura", target, lifetime: -1);
            //AddParticleLink(target, "AscForceBubble", target, target, scale: 3.3f);

            ApiEventManager.OnDeath.AddListener(target, target, OnDeath, true);

            //Unit Perception bubbles seems to be broken
            r1 = AddUnitPerceptionBubble(target, 0.0f, 25000f, TeamId.TEAM_ORDER, true, target);
            r2 = AddUnitPerceptionBubble(target, 0.0f, 25000f, TeamId.TEAM_CHAOS, true, target);

            //Note: The ascension applies a "MoveAway" knockback debuff on all enemies around.
            //The duration varies based on the distance (yet unknown) you were to whoever ascended. And the PathSpeedOverride and ParabolicGravity varies based on the duration.
            //PathSpeedOverride and ParabolicGravity with 0.5 duration: Speed - 1200 / ParabolicGravity - 10.0
            //PathSpeedOverride and ParabolicGravity with 0.75 duration: Speed - 1600 / ParabolicGravity - 7.0
        }

        public void OnDeath(DeathData deathData)
        {
            if (deathData.Unit is NeutralMinion xerath)
            {
                ApiFunctionManager.AddBuff("AscBuffTransfer", 5.7f, 1, null, deathData.Killer, xerath);
            }
            else if (deathData.Unit is Champion)
            {
                AnnounceStartGameMessage(3, 8);
                AnnounceClearAscended();
            }

            //TODO: Impelemt in Csharp system
            //SpellBuffRemove(deathData.Unit, "AscBuffIcon");
            //TODO: Impelemt in Csharp system
            //SpellBuffRemove(deathData.Unit, "AscBuff");
        }

        public override void OnDeactivate(bool expired)
        {
            RemoveParticle(p1);
            RemoveParticle(p2);
            r1.SetToRemove();
            r2.SetToRemove();
            target.PauseAnimation(false);
        }
    }
}