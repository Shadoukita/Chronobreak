namespace Buffs
{
    internal class AscBuffTransfer : BuffScript
    {
        public override BuffScriptMetaData BuffMetaData { get; } = new()
        {
            BuffType = BuffType.INTERNAL,
            BuffAddType = BuffAddType.REPLACE_EXISTING
        };

        float soundTimer = 1000.0f;
        bool hasNotifiedSound = false;
        public override void OnActivate()
        {
            if (target is ObjAIBase obj)
            {
                if (obj is Champion ch)
                {
                    AnnounceChampionAscended(ch);
                }
                else if (obj is NeutralMinion mo)
                {
                    AnnounceMinionAscended(mo);
                }
                NotifyAscendant(obj);
            }

            target.PauseAnimation(true);

            //AddParticleLink(target, "EggTimer", target, target, Buff.Duration, flags: (FXFlags)32);
            //AddParticleBind(target, "AscTransferGlow", target, lifetime: Buff.Duration, flags: (FXFlags)32);
            //AddParticleBind(target, "AscTurnToStone", target, lifetime: Buff.Duration, flags: (FXFlags)32);

            Buff.SetStatusEffect(StatusFlags.Targetable, false);
            Buff.SetStatusEffect(StatusFlags.Stunned, true);
            Buff.SetStatusEffect(StatusFlags.Invulnerable, true);
        }

        public override void OnDeactivate(bool expired)
        {
            target.PauseAnimation(false);

            //AddParticleLink(target, "CassPetrifyMiss_tar", target, target, scale: 3.0f);
            //AddParticleLink(target, "Rebirth_cas", target, target);
            //AddParticleLink(target, "TurnBack", target, target);
            //AddParticleLink(target, "LeonaPassive_tar", target, target, scale: 2.5f);

            if (target is ObjAIBase obj)
            {
                ApiFunctionManager.AddBuff("AscBuff", 25000.0f, 1, null, target, obj);
            }
        }

        public override void OnUpdate()
        {
            soundTimer -= Time.DeltaTime;
            if (soundTimer <= 0 && !hasNotifiedSound)
            {
                PlaySound("Play_sfx_ZhonyasRingShield_OnBuffActivate", target);
                PlaySound("Play_sfx_Cassiopeia_CassiopeiaPetrifyingGazeStun_OnBuffActivate", target);
                hasNotifiedSound = true;
            }
        }
    }
}