namespace Spells
{
    public class Instagate : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 100f, 85f, 70f, 55f, 40f, },
            ChannelDuration = 4f,
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        EffectEmitter particle; // UNUSED
        EffectEmitter particle2; // UNUSED
        /*
        //TODO: Uncomment and fix
        public override void SelfExecute()
        {
            Particle gateParticle; // UNITIALIZED
            Vector3 targetPos = GetCastSpellTargetPos(spell);
            TeamId teamOfOwner = GetTeamID(owner);
            SpellEffectCreate(out this.particle, out _, "CardmasterTeleport_red.troy", default, teamOfOwner, 200, 0, GetEnemyTeam(teamOfOwner), default, default, true, owner, default, default, target, default, default, false);
            SpellEffectCreate(out this.particle, out _, "GateMarker_red.troy", default, teamOfOwner, 200, 0, GetEnemyTeam(teamOfOwner), default, default, true, default, default, targetPos, target, default, default, false);
            SpellEffectCreate(out this.particle2, out _, "GateMarker_green.troy", default, teamOfOwner, 200, 0, teamOfOwner, default, default, true, default, default, targetPos, target, default, default, false);
            SpellEffectCreate(out this.particle2, out _, "CardmasterTeleport_green.troy", default, teamOfOwner, 200, 0, teamOfOwner, default, default, true, owner, default, default, target, default, default, false);
            Particle nextBuffVars_GateParticle = gateParticle;
            Vector3 nextBuffVars_CurrentPos = GetUnitPosition(owner); // UNUSED
            AddBuff((ObjAIBase)owner, owner, new Buffs.Instagate(nextBuffVars_GateParticle), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
        }
        public override void ChannelingSuccessStop()
        {
            Vector3 castPosition; // UNITIALIZED
            TeleportToPosition(owner, castPosition);
            SpellEffectCreate(out _, out _, "CardmasterTeleportArrive.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            SpellBuffRemove(owner, nameof(Buffs.Gate), (ObjAIBase)owner);
        }
        */
        public override void ChannelingCancelStop()
        {
            SpellBuffRemove(owner, nameof(Buffs.Gate), owner);
            SpellBuffRemove(owner, nameof(Buffs.Instagate), owner);
        }
    }
}
namespace Buffs
{
    public class Instagate : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Gate",
            BuffTextureName = "Cardmaster_Premonition.dds",
        };
        EffectEmitter gateParticle;
        EffectEmitter gateParticle2;
        float lastTimeExecuted;
        public Instagate(EffectEmitter gateParticle = default, EffectEmitter gateParticle2 = default)
        {
            this.gateParticle = gateParticle;
            this.gateParticle2 = gateParticle2;
        }
        public override void OnActivate()
        {
            //RequireVar(this.gateParticle);
            //RequireVar(this.gateParticle2);
            //RequireVar(this.currentPos);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(2.675f, ref lastTimeExecuted, true))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.GateFix(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(gateParticle);
            SpellEffectRemove(gateParticle2);
        }
    }
}