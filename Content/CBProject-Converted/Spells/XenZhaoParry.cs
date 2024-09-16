namespace Spells
{
    public class XenZhaoParry : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 125, 225, 325 };
        float[] effect1 = { 0.15f, 0.15f, 0.15f };
        int[] effect2 = { 25, 25, 25, 60, 70 };
        int[] effect4 = { 7, 10, 13 };
        /*
        //TODO: Uncomment and fix
        public override void SelfExecute()
        {
            Vector3 castPos; // UNITIALIZED
            float weaponDmgBonus; // UNITIALIZED
            float dtD = this.effect0[level - 1];
            float percentByLevel = this.effect1[level - 1];
            SpellEffectCreate(out p3, out _, "xenZiou_ult_cas.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_WEAPON_1", castPos, owner, default, default, true, false, false, false, false);
            float dtDReal = dtD + weaponDmgBonus;
            TeamId teamID = GetTeamID(owner);
            float nextBuffVars_Count = 0;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                float currentHP = GetHealth(unit, PrimaryAbilityResourceType.MANA);
                float percentDmg = currentHP * percentByLevel;
                dtDReal = dtD + percentDmg;
                bool isStealthed = GetStealthed(unit);
                SpellEffectCreate(out _, out _, "xenZiou_utl_tar_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                SpellEffectCreate(out _, out _, "xenZiou_utl_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                SpellEffectCreate(out _, out _, "xenZiou_utl_tar_03.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                if(unit is not Champion && dtDReal > 600)
                {
                    dtDReal = 600;
                }
                if(unit is Champion)
                {
                    nextBuffVars_Count++;
                }
                if(!isStealthed)
                {
                    ApplyDamage(attacker, unit, dtDReal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
                }
                else if(unit is Champion)
                {
                    ApplyDamage(attacker, unit, dtDReal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
                }
                else
                {
                    bool canSee = CanSeeTarget(owner, unit);
                    if(canSee)
                    {
                        ApplyDamage(attacker, unit, dtDReal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
                    }
                }
            }
            float nextBuffVars_MRByLevel = this.effect2[level - 1];
            float armorAmount = this.effect2[level - 1];
            float nextBuffVars_ScalingArmor = this.effect4[level - 1];
            float nextBuffVars_ScalingMR = this.effect4[level - 1];
            float nextBuffVars_CountMR = nextBuffVars_Count * nextBuffVars_ScalingMR;
            float nextBuffVars_TotalMR = nextBuffVars_CountMR + nextBuffVars_MRByLevel;
            AddBuff((ObjAIBase)owner, owner, new Buffs.XenZhaoParry(nextBuffVars_MRByLevel, nextBuffVars_TotalMR), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            float nextBuffVars_CountArmor = nextBuffVars_Count * nextBuffVars_ScalingArmor;
            float nextBuffVars_TotalArmor = nextBuffVars_CountArmor + armorAmount;
            AddBuff((ObjAIBase)owner, owner, new Buffs.XenZhaoSweepArmor(nextBuffVars_TotalArmor), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        */
    }
}
namespace Buffs
{
    public class XenZhaoParry : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "XenZhaoParry",
            BuffTextureName = "XinZhao_CrescentSweep.dds",
        };
        float mRByLevel;
        EffectEmitter mRShield;
        float totalMR;
        public XenZhaoParry(float mRByLevel = default, float totalMR = default)
        {
            this.mRByLevel = mRByLevel;
            this.totalMR = totalMR;
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots); // UNUSED
            //RequireVar(this.mRByLevel);
            IncFlatSpellBlockMod(owner, mRByLevel);
            SpellEffectCreate(out mRShield, out _, "xenZiou_SelfShield_01_magic.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(mRShield);
        }
        public override void OnUpdateStats()
        {
            IncFlatSpellBlockMod(owner, totalMR);
        }
    }
}