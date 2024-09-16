using GameServerLib.Services;

namespace Spells
{
    public class Recall : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            ChannelDuration = 8f,
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        
        private EffectEmitter _homeFx;
        private EffectEmitter _arriveFx;
        public override void ChannelingStart()
        {
            bool nextBuffVars_WillRemove = false;
            _homeFx = new EffectEmitter("TeleportHome.troy", owner, owner, owner);
            SpecialEffectService.SpawnFx([_homeFx], _homeFx.NetId);
            AddBuff(owner, owner, new Buffs.Recall(nextBuffVars_WillRemove), 1, 1, 7.9f, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0);
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
            _arriveFx = new EffectEmitter("teleportarrive.troy", owner, owner, owner);
            SpecialEffectService.SpawnFx([_arriveFx], _arriveFx.NetId);
        }
        public override void ChannelingCancelStop()
        {
            SpellEffectRemove(_homeFx);
            SpellBuffRemove(owner, nameof(Buffs.Recall), owner);
        }
    }
}
namespace Buffs
{
    public class Recall : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Recall",
            BuffTextureName = "RecallHome.dds",
        };
        bool willRemove;
        public Recall(bool willRemove = default)
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