namespace Spells
{
    public class SummonerTeleport : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void UpdateTooltip(int spellSlot)
        {
            float duration = 4;
            if (avatarVars.UtilityMastery == 1)
            {
                duration = 3.5f;
            }
            SetSpellToolTipVar(duration, 1, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, attacker);
            float baseCooldown = 300;
            if (avatarVars.SummonerCooldownBonus != 0)
            {
                float cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            SetSpellToolTipVar(baseCooldown, 2, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, attacker);
        }
        public override float AdjustCooldown()
        {
            float baseCooldown = 300;
            if (avatarVars.SummonerCooldownBonus != 0)
            {
                float cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            return baseCooldown;
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            charVars.TeleportCancelled = false;
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SummonerTeleport)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.SummonerTeleport), owner, 0);
            }
            else
            {
                float duration;
                Vector3 castPosition = GetRandomPointInAreaUnit(target, 100, 50);
                Vector3 nextBuffVars_CastPosition = castPosition;
                if (avatarVars.UtilityMastery == 1)
                {
                    duration = 3.5f;
                }
                else
                {
                    duration = 4;
                }
                float nextBuffVars_BuffDuration = duration;
                AddBuff(owner, owner, new Buffs.SummonerTeleport(nextBuffVars_CastPosition, nextBuffVars_BuffDuration), 1, 1, duration, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                if (target is ObjAIBase)
                {
                    if (target is BaseTurret)
                    {
                        AddBuff(attacker, target, new Buffs.Teleport_Turret(), 1, 1, nextBuffVars_BuffDuration, BuffAddType.RENEW_EXISTING, BuffType.STUN, 0, true, false, false);
                    }
                    else
                    {
                        AddBuff(attacker, target, new Buffs.Teleport_Target(), 1, 1, 0.1f + nextBuffVars_BuffDuration, BuffAddType.RENEW_EXISTING, BuffType.STUN, 0, true, false, false);
                    }
                }
                AddBuff((ObjAIBase)target, owner, new Buffs.Teleport_DeathRemoval(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                if (GetBuffCountFromCaster(target, default, nameof(Buffs.SharedWardBuff)) > 0)
                {
                    AddBuff(attacker, target, new Buffs.Destealth(), 1, 1, 1 + nextBuffVars_BuffDuration, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
    }
}
namespace Buffs
{
    public class SummonerTeleport : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Teleport",
            BuffTextureName = "Summoner_teleport.dds",
        };
        bool interrupted;
        EffectEmitter ak;
        Vector3 castPosition;
        float activateTime;
        int slotNum;
        float buffDuration;
        public SummonerTeleport(Vector3 castPosition = default, float buffDuration = default)
        {
            this.castPosition = castPosition;
            this.buffDuration = buffDuration;
        }
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            bool returnValue = true;
            if (type == BuffType.SILENCE)
            {
                interrupted = true;
                SpellBuffRemoveCurrent(owner);
            }
            else if (type == BuffType.FEAR)
            {
                interrupted = true;
                SpellBuffRemoveCurrent(owner);
            }
            else if (type == BuffType.CHARM)
            {
                interrupted = true;
                SpellBuffRemoveCurrent(owner);
            }
            else if (type == BuffType.SLEEP)
            {
                interrupted = true;
                SpellBuffRemoveCurrent(owner);
            }
            else if (type == BuffType.STUN)
            {
                interrupted = true;
                SpellBuffRemoveCurrent(owner);
            }
            else if (type == BuffType.TAUNT)
            {
                interrupted = true;
                SpellBuffRemoveCurrent(owner);
            }
            else if (type == BuffType.SUPPRESSION)
            {
                interrupted = true;
                SpellBuffRemoveCurrent(owner);
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            SpellEffectCreate(out _, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out ak, out _, "Summoner_Teleport.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            //RequireVar(this.castPosition);
            string name1 = GetSlotSpellName((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            string name2 = GetSlotSpellName((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            activateTime = GetGameTime();
            if (name1 == nameof(Spells.SummonerTeleport))
            {
                SetSpell((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, nameof(Spells.TeleportCancel));
                SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots, 1);
                slotNum = 0;
            }
            else if (name2 == nameof(Spells.SummonerTeleport))
            {
                SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, nameof(Spells.TeleportCancel));
                SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots, 1);
                slotNum = 1;
            }
            else
            {
                slotNum = 2;
            }
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
            interrupted = false;
        }
        public override void OnDeactivate(bool expired)
        {
            float baseCooldown = 300;
            //RequireVar(this.interrupted);
            //RequireVar(this.activateTime);
            float finishedTime = GetGameTime();
            float totalTime = finishedTime - activateTime;
            totalTime += 0.1f;
            if (!charVars.TeleportCancelled)
            {
                if (!interrupted)
                {
                    if (totalTime >= buffDuration)
                    {
                        Vector3 castPosition = this.castPosition;
                        DestroyMissileForTarget(owner);
                        TeleportToPosition(owner, castPosition);
                        SpellEffectCreate(out _, out _, "summoner_teleportarrive.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
                        if (avatarVars.SummonerCooldownBonus != 0)
                        {
                            float cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                            baseCooldown *= cooldownMultiplier;
                        }
                        if (avatarVars.TeleportCooldownBonus != 0)
                        {
                            baseCooldown -= avatarVars.TeleportCooldownBonus;
                        }
                    }
                    else
                    {
                        baseCooldown = 180;
                    }
                }
                else
                {
                    baseCooldown = 180;
                }
            }
            else
            {
                baseCooldown = 180;
            }
            SpellEffectRemove(ak);
            SetCanMove(owner, true);
            SetCanCast(owner, true);
            SetGhosted(owner, false);
            SetCanAttack(owner, true);
            if (slotNum == 0)
            {
                SetSpell((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, nameof(Spells.SummonerTeleport));
                SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots, baseCooldown);
            }
            else if (slotNum == 1)
            {
                SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, nameof(Spells.SummonerTeleport));
                SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots, baseCooldown);
            }
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
        }
    }
}