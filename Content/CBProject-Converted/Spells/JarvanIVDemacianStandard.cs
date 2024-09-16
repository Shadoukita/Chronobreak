namespace Spells
{
    public class JarvanIVDemacianStandard : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastTime = 0.115f,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 60, 105, 150, 195, 240 };
        public override void SelfExecute()
        {
            TeamId teamOfOwner = GetTeamID_CS(owner);
            Vector3 targetPos = GetSpellTargetPos(spell);
            SpellEffectCreate(out _, out _, "JarvanDemacianStandard_mis.troy", default, TeamId.TEAM_ORDER, 100, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, attacker, "R_Hand", default, attacker, default, default, true, default, default, false, false);
            Minion other3 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", targetPos, teamOfOwner, false, true, false, true, true, false, 0, false, false, (Champion)owner);
            int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            int nextBuffVars_Level = level;
            float baseDamage = effect0[level - 1];
            float abilityPower = GetFlatMagicDamageMod(owner);
            float abilityPowerPostMod = 0.8f * abilityPower;
            float damageToDeal = abilityPowerPostMod + baseDamage;
            float nextBuffVars_DamageToDeal = damageToDeal;
            AddBuff(attacker, other3, new Buffs.JarvanIVDemacianStandardDelay(nextBuffVars_Level, nextBuffVars_DamageToDeal), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            FaceDirection(owner, targetPos);
            PlayAnimation("Spell3", 0.75f, owner, false, true, false);
        }
    }
}
namespace Buffs
{
    public class JarvanIVDemacianStandard : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "JarvanIVDemacianStandard",
            BuffTextureName = "JarvanIV_DemacianStandard.dds",
        };
        int armorMod;
        float attackSpeedMod;
        int count; // UNUSED
        EffectEmitter particle;
        EffectEmitter particle2;
        float lastTimeExecuted;
        public JarvanIVDemacianStandard(int armorMod = default, float attackSpeedMod = default)
        {
            this.armorMod = armorMod;
            this.attackSpeedMod = attackSpeedMod;
        }
        public override void OnActivate()
        {
            IncPermanentFlatBubbleRadiusMod(owner, -500);
            IncPercentBubbleRadiusMod(owner, -0.5f);
            //RequireVar(this.armorMod);
            //RequireVar(this.attackSpeedMod);
            count = 0;
            SetSuppressCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetIgnoreCallForHelp(owner, true);
            TeamId teamID = GetTeamID_CS(attacker);
            SpellEffectCreate(out particle, out particle2, "JarvanDemacianStandard_buf_green.troy", "JarvanDemacianStandard_buf_red.troy", teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
            SetNotTargetableToTeam(owner, false, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SpellEffectRemove(particle2);
            AddBuff((ObjAIBase)owner, owner, new Buffs.NoRenderExpirationBuff(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnUpdateActions()
        {
            float nextBuffVars_AttackSpeedMod = attackSpeedMod;
            float nextBuffVars_ArmorMod = armorMod;
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, true))
            {
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                {
                    AddBuff(attacker, unit, new Buffs.JarvanIVDemacianStandardBuff(nextBuffVars_AttackSpeedMod, nextBuffVars_ArmorMod), 1, 1, 0.5f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                }
            }
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (attacker is not BaseTurret)
            {
                if (damageType == DamageType.DAMAGE_TYPE_TRUE)
                {
                    damageAmount = 0;
                }
                else
                {
                    damageAmount = 1;
                }
            }
        }
    }
}