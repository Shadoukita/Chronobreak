namespace Spells
{
    public class SwainDecrepify : SwainBeam { }
    public class SwainBeam : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = { -0.2f, -0.23f, -0.26f, -0.29f, -0.32f };
        int[] effect1 = { 25, 40, 55, 70, 85 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            Vector3 ravenPosition = GetPointByUnitFacingOffset(owner, 100, 0);
            TeamId teamID = GetTeamID_CS(owner);
            Minion other3 = SpawnMinion("HiddenMinion", "SwainBeam", "idle.lua", ravenPosition, teamID, false, true, false, true, true, true, 0, default, false, (Champion)owner);
            float nextBuffVars_MoveSpeedMod = effect0[level - 1];
            float nextBuffVars_AttackSpeedMod = 0;
            int nextBuffVars_DamagePerHalfSecond = effect1[level - 1];
            if (target is Champion)
            {
                AddBuff(owner, target, new Buffs.SwainBeamDamage(nextBuffVars_DamagePerHalfSecond), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false);
                AddBuff(owner, owner, new Buffs.SwainBeamSelf(), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false);
                AddBuff((ObjAIBase)target, other3, new Buffs.SwainBeam(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false);
            }
            else
            {
                AddBuff(owner, owner, new Buffs.SwainBeamSelf(), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                if (GetBuffCountFromCaster(target, default, nameof(Buffs.ResistantSkin)) > 0)
                {
                    AddBuff((ObjAIBase)target, owner, new Buffs.SwainBeamDamageMinionNashor(nextBuffVars_DamagePerHalfSecond), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false);
                }
                else
                {
                    AddBuff(owner, target, new Buffs.SwainBeamDamageMinion(nextBuffVars_DamagePerHalfSecond), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false);
                    AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false);
                }
                AddBuff((ObjAIBase)target, other3, new Buffs.SwainBeamMinion(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false);
            }
        }
    }
}
namespace Buffs
{
    public class SwainBeam : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "",
            BuffTextureName = "",
        };
        Region bubbleID;
        Region bubbleID2;
        EffectEmitter bParticle;
        EffectEmitter cParticle;
        EffectEmitter dParticle;
        EffectEmitter a; // UNUSED
        float lastTimeExecuted;
        public override void OnActivate()
        {
            TeamId casterID = GetTeamID_CS(attacker);
            bubbleID = AddUnitPerceptionBubble(casterID, 100, owner, 4, default, default, false);
            TeamId ownerID = GetTeamID_CS(owner);
            bubbleID2 = AddUnitPerceptionBubble(ownerID, 100, owner, 4, default, default, false);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetForceRenderParticles(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetNoRender(owner, false);
            SpellEffectCreate(out bParticle, out _, "swain_disintegrationBeam_beam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "head", default, owner, "Bird_head", default, false);
            SpellEffectCreate(out cParticle, out _, "swain_disintegrationBeam_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "head", default, owner, "Bird_head", default, false);
            SpellEffectCreate(out dParticle, out _, "swain_disintegrationBeam_beam_idle.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "Bird_head", default, owner, default, default, false);
            if (GetBuffCountFromCaster(attacker, default, nameof(Buffs.SwainBeamDamage)) == 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectCreate(out a, out _, "swain_disintegrationBeam_cas_end.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true);
            SpellEffectRemove(cParticle);
            SpellEffectRemove(bParticle);
            SpellEffectRemove(dParticle);
            SetNoRender(owner, true);
            AddBuff((ObjAIBase)owner, owner, new Buffs.SwainBeamExpirationTimer(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            RemovePerceptionBubble(bubbleID);
            RemovePerceptionBubble(bubbleID2);
        }
        public override void OnUpdateActions()
        {
            FaceDirection(owner, attacker.Position3D);
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, false))
            {
                float distance = DistanceBetweenObjects(attacker, owner);
                if (distance >= 605)
                {
                    if (GetBuffCountFromCaster(attacker, owner, nameof(Buffs.Slow)) > 0)
                    {
                        SpellBuffRemove(attacker, nameof(Buffs.Slow), (ObjAIBase)owner);
                    }
                    if (GetBuffCountFromCaster(attacker, default, nameof(Buffs.SwainBeamDamage)) > 0)
                    {
                        SpellBuffClear(attacker, nameof(Buffs.SwainBeamDamage));
                    }
                    if (GetBuffCountFromCaster(attacker, default, nameof(Buffs.SwainBeamDamageMinion)) > 0)
                    {
                        SpellBuffClear(attacker, nameof(Buffs.SwainBeamDamageMinion));
                    }
                    SpellBuffRemoveCurrent(owner);
                }
                if (IsDead(attacker))
                {
                    if (GetBuffCountFromCaster(attacker, owner, nameof(Buffs.Slow)) > 0)
                    {
                        SpellBuffRemove(attacker, nameof(Buffs.Slow), (ObjAIBase)owner);
                    }
                    if (GetBuffCountFromCaster(attacker, default, nameof(Buffs.SwainBeamDamage)) > 0)
                    {
                        SpellBuffClear(attacker, nameof(Buffs.SwainBeamDamage));
                    }
                    if (GetBuffCountFromCaster(attacker, default, nameof(Buffs.SwainBeamDamageMinion)) > 0)
                    {
                        SpellBuffClear(attacker, nameof(Buffs.SwainBeamDamageMinion));
                    }
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}