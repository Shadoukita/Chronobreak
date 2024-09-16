namespace Spells
{
    public class SummonerBattleCry : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 90f, 90f, 90f, },
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 10, 14, 18, 22, 26, 30, 34, 38, 42, 48, 52, 54, 58, 62, 66, 70, 74, 78 };
        float[] effect1 = { 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f };
        int[] effect4 = { 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12 };
        public override void UpdateTooltip(int spellSlot)
        {
            int level = GetLevel(owner);
            float aPMod = effect0[level - 1];
            float attackSpeedMod = effect1[level - 1];
            if (avatarVars.OffensiveMastery == 1)
            {
                aPMod *= 1.1f;
                attackSpeedMod += 0.05f;
            }
            attackSpeedMod *= 100;
            SetSpellToolTipVar(attackSpeedMod, 1, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, attacker);
            SetSpellToolTipVar(aPMod, 2, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, attacker);
            float baseCooldown = 180;
            if (avatarVars.SummonerCooldownBonus != 0)
            {
                float cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            SetSpellToolTipVar(baseCooldown, 3, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, attacker);
        }
        public override float AdjustCooldown()
        {
            float baseCooldown = 180;
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
            float nextBuffVars_ScaleCoef = 0.04f;
            float nextBuffVars_ScaleCap = 0;
            SpellEffectCreate(out _, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            int level = GetLevel(owner);
            float nextBuffVars_APMod = effect0[level - 1];
            float nextBuffVars_AttackSpeedMod = effect1[level - 1];
            if (avatarVars.OffensiveMastery == 1)
            {
                nextBuffVars_APMod *= 1.1f;
                nextBuffVars_AttackSpeedMod += 0.05f;
            }
            AddBuff(attacker, attacker, new Buffs.SummonerBattleCry(nextBuffVars_ScaleCoef, nextBuffVars_ScaleCap, nextBuffVars_APMod, nextBuffVars_AttackSpeedMod), 1, 1, effect4[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.HASTE, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class SummonerBattleCry : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", "", "", },
            AutoBuffActivateEffect = new[] { "Summoner_cast.troy", "summoner_battlecry.troy", "summoner_battlecry_oc.troy", "", },
            BuffName = "SummonerBattleCry",
            BuffTextureName = "Summoner_BattleCry.dds",
        };
        float scaleCoef;
        float scaleCap;
        float aPMod;
        float attackSpeedMod;
        public SummonerBattleCry(float scaleCoef = default, float scaleCap = default, float aPMod = default, float attackSpeedMod = default)
        {
            this.scaleCoef = scaleCoef;
            this.scaleCap = scaleCap;
            this.aPMod = aPMod;
            this.attackSpeedMod = attackSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.scaleCoef);
            //RequireVar(this.scaleCap);
            IncScaleSkinCoef(scaleCoef, owner);
            //RequireVar(this.aPMod);
            //RequireVar(this.attackSpeedMod);
            //RequireVar(this.allyAttackSpeedMod);
            IncPercentAttackSpeedMod(owner, attackSpeedMod);
            IncFlatMagicDamageMod(owner, aPMod);
            float duration = GetBuffRemainingDuration(owner, nameof(Buffs.SummonerBattleCry)); // UNUSED
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectCreate(out _, out _, "summoner_battlecry_obd.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, owner, default, default, false, false, false, false, false);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, attackSpeedMod);
            IncFlatMagicDamageMod(owner, aPMod);
            if (scaleCap < 4)
            {
                scaleCoef += 0.04f;
                scaleCap++;
            }
            IncScaleSkinCoef(scaleCoef, owner);
        }
    }
}