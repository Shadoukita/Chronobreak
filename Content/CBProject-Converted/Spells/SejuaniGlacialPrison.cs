namespace Spells
{
    public class SejuaniGlacialPrison : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 150, 250, 350 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            bool isStealthed = GetStealthed(target);
            float stunDuration = 2;
            float prisonDamage = effect0[level - 1];
            if (!isStealthed)
            {
                AddBuff(attacker, target, new Buffs.SejuaniGlacialPrisonCheck(), 1, 1, stunDuration, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                DestroyMissile(missileNetworkID);
                BreakSpellShields(target);
                AddBuff(attacker, target, new Buffs.SejuaniGlacialPrison(), 1, 1, stunDuration, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                ApplyDamage(attacker, target, prisonDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.8f, 1, false, false, attacker);
                SpellCast(attacker, target, default, default, 1, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
            }
            else
            {
                if (target is Champion)
                {
                    AddBuff(attacker, target, new Buffs.SejuaniGlacialPrisonCheck(), 1, 1, stunDuration, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    DestroyMissile(missileNetworkID);
                    BreakSpellShields(target);
                    AddBuff(attacker, target, new Buffs.SejuaniGlacialPrison(), 1, 1, stunDuration, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                    ApplyDamage(attacker, target, prisonDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.8f, 1, false, false, attacker);
                    SpellCast(attacker, target, default, default, 1, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
                }
                else
                {
                    bool canSee = CanSeeTarget(owner, target);
                    if (canSee)
                    {
                        AddBuff(attacker, target, new Buffs.SejuaniGlacialPrisonCheck(), 1, 1, stunDuration, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        DestroyMissile(missileNetworkID);
                        BreakSpellShields(target);
                        AddBuff(attacker, target, new Buffs.SejuaniGlacialPrison(), 1, 1, stunDuration, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                        ApplyDamage(attacker, target, prisonDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.8f, 1, false, false, attacker);
                        SpellCast(attacker, target, default, default, 1, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class SejuaniGlacialPrison : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head", },
            AutoBuffActivateEffect = new[] { "Stun_glb.troy", },
            BuffName = "SejuaniGlacialPrison",
            BuffTextureName = "Sejuani_GlacialPrison.dds",
            PopupMessage = new[] { "game_floatingtext_Stunned", },
        };
        EffectEmitter crystalineParticle;
        public override void OnActivate()
        {
            ObjAIBase caster = GetBuffCasterUnit();
            TeamId teamID = GetTeamID_CS(caster);
            SetStunned(owner, true);
            PauseAnimation(owner, true);
            SpellEffectCreate(out crystalineParticle, out _, "sejuani_ult_tar_03.troy", default, teamID, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, owner, "BUFFBONE_GLB_GROUND_LOC", default, attacker, "Bird_head", default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetStunned(owner, false);
            PauseAnimation(owner, false);
            SpellEffectRemove(crystalineParticle);
        }
        public override void OnUpdateStats()
        {
            SetStunned(owner, true);
            PauseAnimation(owner, true);
        }
    }
}