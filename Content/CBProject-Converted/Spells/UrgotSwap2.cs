namespace Spells
{
    public class UrgotSwap2 : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 100f, 85f, 70f, 55f, 40f, },
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        EffectEmitter gateParticle;
        int[] effect0 = { 80, 105, 130 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (target is Champion)
            {
                Vector3 targetPos = GetSpellTargetPos(spell);
                TeamId teamOfOwner = GetTeamID_CS(owner);
                SpellEffectCreate(out gateParticle, out _, "UrgotSwapTarget.troy", default, teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, target, default, default, target, "root", default, false, default, default, false, false);
                EffectEmitter nextBuffVars_GateParticle = gateParticle;
                Vector3 nextBuffVars_TargetPos = targetPos; // UNUSED
                FaceDirection(owner, targetPos);
                AddBuff(owner, owner, new Buffs.UrgotSwapMarker(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff(owner, target, new Buffs.UrgotSwapMarker(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff((ObjAIBase)target, owner, new Buffs.UrgotSwapMissile(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                int level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                int defInc = effect0[level - 1];
                float nextBuffVars_DefInc = defInc;
                AddBuff(attacker, attacker, new Buffs.UrgotSwapDef(nextBuffVars_DefInc), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                BreakSpellShields(target);
                AddBuff(attacker, target, new Buffs.Suppression(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.SUPPRESSION, 0, true, false, false);
                AddBuff(owner, target, new Buffs.UrgotSwapTarget(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                AddBuff((ObjAIBase)target, owner, new Buffs.UrgotSwap2(nextBuffVars_GateParticle), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            else
            {
                SetSlotSpellCooldownTimeVer2(5, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
                float manaRefund = 120;
                IncPAR(owner, manaRefund, PrimaryAbilityResourceType.MANA);
            }
        }
    }
}
namespace Buffs
{
    public class UrgotSwap2 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "UrgotSwapExecute",
            BuffTextureName = "UrgotPositionReverser.dds",
        };
        int isDisabled;
        EffectEmitter gateParticle;
        EffectEmitter particle3;
        public UrgotSwap2(EffectEmitter gateParticle = default)
        {
            this.gateParticle = gateParticle;
        }
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            bool returnValue = true;
            if (owner.Team != attacker.Team)
            {
                if (type == BuffType.FEAR)
                {
                    isDisabled = 1;
                }
                if (type == BuffType.CHARM)
                {
                    isDisabled = 1;
                }
                if (type == BuffType.SLEEP)
                {
                    isDisabled = 1;
                }
                if (type == BuffType.STUN)
                {
                    isDisabled = 1;
                }
                if (type == BuffType.TAUNT)
                {
                    isDisabled = 1;
                }
                if (type == BuffType.SILENCE)
                {
                    isDisabled = 1;
                }
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            //RequireVar(this.gateParticle);
            //RequireVar(this.targetPos);
            isDisabled = 0;
            TeamId teamOfOwner = GetTeamID_CS(owner);
            SpellEffectCreate(out particle3, out _, "UrgotSwapDrip.troy", default, teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            PlayAnimation("teleUp", 1.2f, owner, false, false, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
            SetCanAttack(owner, true);
            TeamId teamOfOwner = GetTeamID_CS(owner); // UNUSED
            SpellEffectRemove(gateParticle);
            SpellEffectRemove(particle3);
            SpellBuffRemove(owner, nameof(Buffs.UrgotSwapMissile), attacker, 0);
            SpellBuffRemove(owner, nameof(Buffs.Suppression), attacker, 0);
            SpellBuffRemove(owner, nameof(Buffs.UrgotSwapTarget), attacker, 0);
            UnlockAnimation(owner, false);
            if (isDisabled <= 0)
            {
                float distance = DistanceBetweenObjects(owner, attacker);
                if (distance < 3000)
                {
                    AddBuff((ObjAIBase)owner, attacker, new Buffs.UrgotSwapExecute(), 1, 1, 0.1f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                    AddBuff((ObjAIBase)owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    PlayAnimation("teleDwn", 0.7f, owner, false, false, true);
                }
            }
        }
        /*
        //TODO: Uncomment and fix
        public override void OnUpdateStats()
        {
            int isDisabled; // UNITIALIZED
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            if(GetBuffCountFromCaster(attacker, owner, nameof(Buffs.UrgotSwapTarget)) == 0)
            {
                this.isDisabled = 1;
                SpellBuffClear(owner, nameof(Buffs.UrgotSwap2));
            }
            if(isDisabled == 1)
            {
                SpellBuffClear(owner, nameof(Buffs.UrgotSwap2));
            }
        }
        */
    }
}