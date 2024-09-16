namespace Spells
{
    public class Gate : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 100f, 85f, 70f, 55f, 40f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        EffectEmitter gateParticle;
        EffectEmitter gateParticle2;
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            targetPos = GetNearestPassablePosition(owner, targetPos);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Destiny_marker)) > 0)
            {
                TeamId teamOfOwner = GetTeamID_CS(owner);
                SpellEffectCreate(out gateParticle, out gateParticle2, "GateMarker_green.troy", "GateMarker_red.troy", teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, target, default, default, false, default, default, false, false);
                EffectEmitter nextBuffVars_GateParticle = gateParticle;
                EffectEmitter nextBuffVars_GateParticle2 = gateParticle2;
                Vector3 nextBuffVars_TargetPos = targetPos;
                AddBuff(owner, owner, new Buffs.Gate(nextBuffVars_GateParticle, nextBuffVars_GateParticle2, nextBuffVars_TargetPos), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                SpellBuffRemove(owner, nameof(Buffs.Destiny_marker), owner, 0);
            }
            else
            {
                SpellCast(owner, owner, targetPos, targetPos, 1, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
            }
            TeamId teamID = GetTeamID_CS(owner); // UNUSED
        }
    }
}
namespace Buffs
{
    public class Gate : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Gate",
            BuffTextureName = "Cardmaster_Premonition.dds",
        };
        int isDisabled;
        EffectEmitter gateParticle;
        EffectEmitter gateParticle2;
        Vector3 targetPos;
        EffectEmitter particle3;
        EffectEmitter particle4;
        public Gate(EffectEmitter gateParticle = default, EffectEmitter gateParticle2 = default, Vector3 targetPos = default)
        {
            this.gateParticle = gateParticle;
            this.gateParticle2 = gateParticle2;
            this.targetPos = targetPos;
        }
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            bool returnValue = true;
            if (owner.Team != attacker.Team)
            {
                if (type == BuffType.FEAR)
                {
                    isDisabled = 1;
                    SpellBuffRemoveCurrent(owner);
                }
                if (type == BuffType.CHARM)
                {
                    isDisabled = 1;
                    SpellBuffRemoveCurrent(owner);
                }
                if (type == BuffType.SLEEP)
                {
                    isDisabled = 1;
                    SpellBuffRemoveCurrent(owner);
                }
                if (type == BuffType.STUN)
                {
                    isDisabled = 1;
                    SpellBuffRemoveCurrent(owner);
                }
                if (type == BuffType.TAUNT)
                {
                    isDisabled = 1;
                    SpellBuffRemoveCurrent(owner);
                }
                if (type == BuffType.SILENCE)
                {
                    isDisabled = 1;
                    SpellBuffRemoveCurrent(owner);
                }
                if (type == BuffType.SUPPRESSION)
                {
                    isDisabled = 1;
                    SpellBuffRemoveCurrent(owner);
                }
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            //RequireVar(this.gateParticle);
            //RequireVar(this.gateParticle2);
            //RequireVar(this.targetPos);
            isDisabled = 0;
            TeamId teamOfOwner = GetTeamID_CS(owner);
            SpellEffectCreate(out particle3, out particle4, "CardmasterTeleport_green.troy", "CardmasterTeleport_red.troy", teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_SUMMONER);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_SUMMONER);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
            SetCanAttack(owner, true);
            TeamId teamOfOwner = GetTeamID_CS(owner); // UNUSED
            SpellEffectRemove(gateParticle2);
            SpellEffectRemove(gateParticle);
            SpellEffectRemove(particle3);
            SpellEffectRemove(particle4);
            DestroyMissileForTarget(owner);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_SUMMONER);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_SUMMONER);
            if (isDisabled == 0 && expired)
            {
                Vector3 targetPos = this.targetPos;
                TeleportToPosition(owner, targetPos);
            }
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
        }
    }
}