namespace Spells
{
    public class BrandFissure : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
        };
        int[] effect0 = { 75, 120, 165, 210, 255 };
        float[] effect1 = { 93.75f, 150, 206.25f, 262.5f, 318.75f };
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            TeamId teamOfOwner = GetTeamID_CS(owner);
            Minion other3 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", targetPos, teamOfOwner, false, true, false, true, true, true, 0, false, false, (Champion)owner);
            float nextBuffVars_FissureDamage = effect0[level - 1];
            float nextBuffVars_AblazeBonusDamage = effect1[level - 1];
            AddBuff(attacker, other3, new Buffs.BrandFissure(nextBuffVars_FissureDamage, nextBuffVars_AblazeBonusDamage), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class BrandFissure : BuffScript
    {
        float fissureDamage;
        float ablazeBonusDamage;
        EffectEmitter groundParticleEffect;
        EffectEmitter groundParticleEffect2;
        EffectEmitter a;
        public BrandFissure(float fissureDamage = default, float ablazeBonusDamage = default)
        {
            this.fissureDamage = fissureDamage;
            this.ablazeBonusDamage = ablazeBonusDamage;
        }
        public override void OnActivate()
        {
            //RequireVar(this.fissureDamage);
            //RequireVar(this.ablazeBonusDamage);
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            TeamId teamOfOwner = GetTeamID_CS(attacker);
            SpellEffectCreate(out groundParticleEffect, out groundParticleEffect2, "BrandPOF_tar_green.troy", "BrandPOF_tar_red.troy", teamOfOwner, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, false, false);
            int brandSkinID = GetSkinID(attacker);
            if (brandSkinID == 3)
            {
                SpellEffectCreate(out a, out _, "BrandPOF_Frost_charge.troy", default, teamOfOwner, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out a, out _, "BrandPOF_charge.troy", default, teamOfOwner, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(a);
            SpellEffectRemove(groundParticleEffect);
            SpellEffectRemove(groundParticleEffect2);
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 ownerPos = GetUnitPosition(owner);
            int brandSkinID = GetSkinID(attacker);
            if (brandSkinID == 3)
            {
                SpellEffectCreate(out _, out _, "BrandPOF_Frost_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out _, out _, "BrandPOF_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
            }
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 260, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                if (GetBuffCountFromCaster(unit, attacker, nameof(Buffs.BrandAblaze)) > 0)
                {
                    ApplyDamage(attacker, unit, ablazeBonusDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.75f, 1, false, false, attacker);
                }
                else
                {
                    ApplyDamage(attacker, unit, fissureDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 1, false, false, attacker);
                }
                AddBuff(attacker, unit, new Buffs.BrandAblaze(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                if (brandSkinID == 3)
                {
                    SpellEffectCreate(out _, out _, "BrandCritAttack_Frost_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                }
                else
                {
                    SpellEffectCreate(out _, out _, "BrandCritAttack_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                }
            }
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 1000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
    }
}