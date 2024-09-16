namespace Buffs
{
    public class OdinSpeedShrineAura : BuffScript
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
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            bool returnValue = true;
            if (owner.Team != attacker.Team)
            {
                if (type == BuffType.FEAR)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if (type == BuffType.CHARM)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if (type == BuffType.SILENCE)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if (type == BuffType.SLEEP)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if (type == BuffType.SLOW)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if (type == BuffType.SNARE)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if (type == BuffType.STUN)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if (type == BuffType.TAUNT)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if (type == BuffType.BLIND)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if (type == BuffType.SUPPRESSION)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else
                {
                    returnValue = true;
                }
            }
            else
            {
                returnValue = true;
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            TeamId orderTeam = TeamId.TEAM_ORDER;
            TeamId chaosTeam = TeamId.TEAM_CHAOS;
            SpellEffectCreate(out particleOrder, out _, "odin_shrine_time.troy", default, TeamId.TEAM_NEUTRAL, 250, 0, TeamId.TEAM_ORDER, default, owner, false, default, default, owner.Position3D, owner, default, default, false, true, false, false, false);
            SpellEffectCreate(out particleChaos, out _, "odin_shrine_time.troy", default, TeamId.TEAM_NEUTRAL, 250, 0, TeamId.TEAM_CHAOS, default, owner, false, default, default, owner.Position3D, owner, default, default, false, true, false, false, false);
            Region myBubble = AddPosPerceptionBubble(orderTeam, 250, owner.Position3D, 1, default, false); // UNUSED
            Region myBubble2 = AddPosPerceptionBubble(chaosTeam, 250, owner.Position3D, 1, default, false); // UNUSED
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particleOrder);
            SpellEffectRemove(particleChaos);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, false))
            {
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 365, SpellDataFlags.AffectHeroes, default, true))
                {
                    int count = GetBuffCountFromAll(unit, nameof(Buffs.OdinShrineBombBuff));
                    if (count < 1)
                    {
                        float newDuration = 10;
                        if (GetBuffCountFromCaster(unit, unit, nameof(Buffs.MonsterBuffs)) > 0)
                        {
                            newDuration *= 1.2f;
                        }
                        float nextBuffVars_SpeedMod = 0.3f;
                        AddBuff((ObjAIBase)unit, unit, new Buffs.OdinSpeedShrineBuff(nextBuffVars_SpeedMod), 1, 1, newDuration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    }
                }
            }
        }
    }
}