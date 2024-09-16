namespace Spells
{
    public class VladimirSanguinePool : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            SpellFXOverrideSkins = new[] { "BloodkingVladimir", },
            SpellVOOverrideSkins = new[] { "BloodkingVladimir", },
        };
        float[] effect0 = { -0.4f, -0.4f, -0.4f, -0.4f, -0.4f };
        float[] effect1 = { 20, 33.75f, 47.5f, 61.25f, 75 };
        public override void SelfExecute()
        {
            int level = base.level;
            DestroyMissileForTarget(owner);
            float currentHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            float healthCost = currentHealth * -0.2f;
            IncHealth(owner, healthCost, owner);
            SpellEffectCreate(out _, out _, "Vlad_Bloodking_Blood_Skin.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, owner, default, default, true, false, false, false, false);
            float nextBuffVars_MoveSpeedMod = effect0[level - 1];
            float damageTick = effect1[level - 1];
            float maxHP = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            float baseHP = 400;
            float healthPerLevel = 85;
            level = GetLevel(owner);
            float levelHealth = level * healthPerLevel;
            float totalBaseHealth = levelHealth + baseHP;
            float totalBonusHealth = maxHP - totalBaseHealth;
            float healthMod = totalBonusHealth * 0.0375f;
            float nextBuffVars_DamageTick = healthMod + damageTick;
            AddBuff(attacker, attacker, new Buffs.VladimirSanguinePool(nextBuffVars_DamageTick, nextBuffVars_MoveSpeedMod), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class VladimirSanguinePool : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "VladimirSanguinePool",
            BuffTextureName = "Vladimir_SanguinePool.dds",
            IsDeathRecapSource = true,
        };
        float damageTick;
        float moveSpeedMod;
        Fade iD; // UNUSED
        EffectEmitter particle;
        float hasteBoost;
        EffectEmitter particle1; // UNUSED
        float lastTimeExecuted2;
        float damagePulse;
        float slowPulse;
        public VladimirSanguinePool(float damageTick = default, float moveSpeedMod = default)
        {
            this.damageTick = damageTick;
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageTick);
            //RequireVar(this.moveSpeedMod);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetCanAttack(owner, false);
            SetSilenced(owner, true);
            SetForceRenderParticles(owner, true);
            SetCallForHelpSuppresser(owner, true);
            iD = PushCharacterFade(owner, 0, 0.1f);
            TeamId teamOfOwner = GetTeamID_CS(attacker);
            SpellEffectCreate(out particle, out _, "VladSanguinePool_buf.troy", default, teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.UnlockAnimation(), 1, 1, 2.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            PlayAnimation("Idle1down", 2.25f, owner, false, true, true);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_SUMMONER);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_SUMMONER);
            hasteBoost = 0.375f;
        }
        /*
        //TODO: Uncomment and fix
        public override void OnDeactivate(bool expired)
        {
            TeamId teamOfOwner; // UNITIALIZED
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_SUMMONER);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_SUMMONER);
            SetTargetable(owner, true);
            SetGhosted(owner, false);
            SetCanAttack(owner, true);
            SetSilenced(owner, false);
            SetForceRenderParticles(owner, false);
            SetCallForHelpSuppresser(owner, false);
            this.iD = PushCharacterFade(owner, 1, 0.1f);
            SpellEffectRemove(this.particle);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                ApplyDamage(attacker, unit, 0, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
            }
            SpellEffectCreate(out this.particle1, out _, "Vlad_Bloodking_Blood_Skin.troy", default, teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, false, false);
        }
        */
        public override void OnUpdateStats()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted2, false))
            {
                hasteBoost = 0;
            }
            IncFlatAttackRangeMod(owner, -450);
            IncPercentMovementSpeedMod(owner, hasteBoost);
            SetGhosted(owner, true);
            SetForceRenderParticles(owner, true);
        }
        public override void OnUpdateActions()
        {
            float nextBuffVars_MoveSpeedMod = moveSpeedMod;
            if (ExecutePeriodically(0.5f, ref damagePulse, true))
            {
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    ApplyDamage(attacker, unit, damageTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                    float healAmount = 0.15f * damageTick;
                    IncHealth(owner, healAmount, owner);
                }
            }
            if (ExecutePeriodically(0.25f, ref slowPulse, true))
            {
                float duration = GetBuffRemainingDuration(owner, nameof(Buffs.VladimirSanguinePool));
                int skinID = GetSkinID(owner);
                if (skinID == 5)
                {
                    if (duration <= 1)
                    {
                        AddBuff(attacker, target, new Buffs.VladimirSanguinePoolParticle(), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    AddBuff(attacker, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, 1, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                }
            }
        }
    }
}