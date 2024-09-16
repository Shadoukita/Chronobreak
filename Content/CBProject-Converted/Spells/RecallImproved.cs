namespace Spells
{
    public class RecallImproved : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            ChannelDuration = 7f,
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        EffectEmitter particleID;
        public override void ChannelingStart()
        {
            bool nextBuffVars_WillRemove = false;
            SpellEffectCreate(out particleID, out _, "TeleportHomeImproved.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            AddBuff(owner, owner, new Buffs.RecallImproved(nextBuffVars_WillRemove), 1, 1, 7.9f, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
        }
        public override void ChannelingSuccessStop()
        {
            TeamId teamID = GetTeamID_CS(owner);
            if (teamID == TeamId.TEAM_ORDER)
            {
                TeleportToKeyLocation(attacker, SpawnType.SPAWN_LOCATION, TeamId.TEAM_ORDER);
            }
            else if (true)
            {
                TeleportToKeyLocation(attacker, SpawnType.SPAWN_LOCATION, TeamId.TEAM_CHAOS);
            }
            Vector3 camPos = GetUnitPosition(owner);
            SetCameraPosition((Champion)owner, camPos);
            SpellEffectCreate(out _, out _, "teleportarrive.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void ChannelingCancelStop()
        {
            SpellEffectRemove(particleID);
            SpellBuffRemove(owner, nameof(Buffs.RecallImproved), owner, 0);
        }
    }
}
namespace Buffs
{
    public class RecallImproved : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Recall",
            BuffTextureName = "RecallHome.dds",
        };
        bool willRemove;
        public RecallImproved(bool willRemove = default)
        {
            this.willRemove = willRemove;
        }
        public override void OnActivate()
        {
            //RequireVar(this.willRemove);
        }
        public override void OnUpdateActions()
        {
            if (willRemove)
            {
                StopChanneling((ObjAIBase)owner, ChannelingStopCondition.Cancel, ChannelingStopSource.LostTarget);
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnTakeDamage(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource)
        {
            if (damageSource != DamageSource.DAMAGE_SOURCE_PERIODIC)
            {
                willRemove = true;
            }
        }
    }
}