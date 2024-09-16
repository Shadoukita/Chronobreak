namespace Spells
{
    public class KogMawLivingArtillery : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            SpellFXOverrideSkins = new[] { "NewYearDragonKogMaw", },
            SpellVOOverrideSkins = new[] { "NewYearDragonKogMaw", },
        };
        int[] effect0 = { 80, 120, 160 };
        public override void SelfExecute()
        {
            TeamId teamOfOwner = GetTeamID_CS(owner);
            float aDRatio = 0.5f;
            float bonusDamage = GetFlatPhysicalDamageMod(owner);
            bonusDamage *= aDRatio;
            Vector3 targetPos = GetSpellTargetPos(spell);
            SpellEffectCreate(out _, out _, "KogMawLivingArtillery_mis.troy", default, TeamId.TEAM_ORDER, 100, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, attacker, "C_Mouth_d", default, attacker, default, default, true, default, default, false, false);
            Minion other3 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", targetPos, teamOfOwner, false, true, false, true, true, true, 0, false, false, (Champion)owner);
            float damageAmount = effect0[level - 1];
            float nextBuffVars_BaseDamageAmount = effect0[level - 1];
            float nextBuffVars_BonusDamage = bonusDamage;
            damageAmount += bonusDamage;
            float nextBuffVars_FinalDamage = damageAmount;
            AddBuff(attacker, other3, new Buffs.KogMawLivingArtillery(nextBuffVars_FinalDamage, nextBuffVars_BaseDamageAmount, nextBuffVars_BonusDamage), 1, 1, 0.75f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            int count = GetBuffCountFromAll(owner, nameof(Buffs.KogMawLivingArtilleryCost));
            float count2 = 1 + count;
            float extraCost = 40 * count2;
            extraCost = Math.Min(160, extraCost);
            SetPARCostInc(owner, 3, SpellSlotType.SpellSlots, extraCost, PrimaryAbilityResourceType.MANA);
            AddBuff(attacker, owner, new Buffs.KogMawLivingArtilleryCost(), 5, 1, 6, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            Region nextBuffVars_Bubble = AddPosPerceptionBubble(teamOfOwner, 100, targetPos, 1, default, false); // UNUSED
        }
    }
}
namespace Buffs
{
    public class KogMawLivingArtillery : BuffScript
    {
        float finalDamage;
        float baseDamageAmount;
        float bonusDamage;
        EffectEmitter particle1;
        EffectEmitter particle;
        EffectEmitter a; // UNUSED
        public KogMawLivingArtillery(float finalDamage = default, float baseDamageAmount = default, float bonusDamage = default)
        {
            this.finalDamage = finalDamage;
            this.baseDamageAmount = baseDamageAmount;
            this.bonusDamage = bonusDamage;
        }
        public override void OnActivate()
        {
            //RequireVar(this.finalDamage);
            //RequireVar(this.baseDamageAmount);
            //RequireVar(this.bonusDamage);
            TeamId teamID = GetTeamID_CS(attacker);
            int kMSkinID = GetSkinID(attacker);
            if (kMSkinID == 5)
            {
                SpellEffectCreate(out particle1, out particle, "KogMawLivingArtillery_cas_chinese_green.troy", "KogMawLivingArtillery_cas_chinese_red.troy", teamID, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, true, default, default, false, false);
            }
            else
            {
                SpellEffectCreate(out particle1, out particle, "KogMawLivingArtillery_cas_green.troy", "KogMawLivingArtillery_cas_red.troy", teamID, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, true, default, default, false, false);
            }
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            IncPercentBubbleRadiusMod(owner, -0.9f);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SpellEffectRemove(particle1);
            TeamId teamID = GetTeamID_CS(owner);
            int kMSkinID = GetSkinID(attacker);
            if (kMSkinID != 5)
            {
                SpellEffectCreate(out a, out _, "KogMawLivingArtillery_tar_green.troy", default, teamID, 100, 0, TeamId.TEAM_UNKNOWN, default, default, true, owner, default, default, target, default, default, true, default, default, false, false);
            }
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 240, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, default, true))
            {
                BreakSpellShields(unit);
                AddBuff(attacker, unit, new Buffs.KogMawLivingArtillerySight(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                ApplyDamage(attacker, unit, finalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.3f, 1, false, false, attacker);
            }
            baseDamageAmount *= 2.5f;
            finalDamage = baseDamageAmount + bonusDamage;
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 240, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                AddBuff(attacker, unit, new Buffs.KogMawLivingArtillerySight(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                ApplyDamage(attacker, unit, finalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.3f, 1, false, false, attacker);
            }
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 1000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            IncPercentBubbleRadiusMod(owner, -0.9f);
        }
    }
}