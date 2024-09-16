namespace Spells
{
    public class SummonerOdinGarrison : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override bool CanCast()
        {
            bool returnValue = true;
            if (ExecutePeriodically(0.25f, ref avatarVars.LastTimeExecutedGarrison, true))
            {
                avatarVars.CanCastGarrison = false;
                foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1250, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.NotAffectSelf | SpellDataFlags.AffectUseable | SpellDataFlags.AffectWards, nameof(Buffs.OdinGuardianBuff), true))
                {
                    avatarVars.CanCastGarrison = true;
                }
            }
            returnValue = avatarVars.CanCastGarrison;
            return returnValue;
        }
        public override void UpdateTooltip(int spellSlot)
        {
            float baseCooldown = 210;
            if (avatarVars.SummonerCooldownBonus != 0)
            {
                float cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            SetSpellToolTipVar(baseCooldown, 1, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, attacker);
        }
        public override float AdjustCooldown()
        {
            float baseCooldown = 210;
            if (avatarVars.SummonerCooldownBonus != 0)
            {
                float cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            return baseCooldown;
        }
        public override void SelfExecute()
        {
            SpellEffectCreate(out _, out _, "summoner_cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 1800, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectTurrets | SpellDataFlags.AffectUseable | SpellDataFlags.AffectWards, 1, nameof(Buffs.OdinGuardianBuff), true))
            {
                bool nextBuffVars_Splash;
                if (avatarVars.DefensiveMastery == 1)
                {
                    nextBuffVars_Splash = true;
                }
                else
                {
                    nextBuffVars_Splash = false;
                }
                TeamId teamOfOwner = GetTeamID_CS(owner);
                TeamId teamOfTarget = GetTeamID_CS(unit);
                if (GetBuffCountFromCaster(unit, unit, nameof(Buffs.OdinGuardianBuff)) > 0)
                {
                    if (teamOfOwner == teamOfTarget)
                    {
                        AddBuff(owner, unit, new Buffs.SummonerOdinGarrison(nextBuffVars_Splash), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.INVULNERABILITY, 0, true, false, false);
                    }
                    else
                    {
                        AddBuff(owner, unit, new Buffs.SummonerOdinGarrisonDebuff(), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.INVULNERABILITY, 0, true, false, false);
                    }
                }
                else
                {
                    string slotName = GetSlotSpellName(owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
                    if (slotName == nameof(Spells.SummonerOdinGarrison))
                    {
                        SetSlotSpellCooldownTime(owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots, 1);
                    }
                    else
                    {
                        SetSlotSpellCooldownTime(owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots, 1);
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class SummonerOdinGarrison : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "Fortify",
            BuffTextureName = "SummonerGarrison.dds",
        };
        EffectEmitter auraParticle;
        EffectEmitter particle;
        EffectEmitter particle2;
        bool splash;
        public SummonerOdinGarrison(bool splash = default)
        {
            this.splash = splash;
        }
        public override void OnActivate()
        {
            SpellEffectCreate(out auraParticle, out _, "Summoner_ally_capture_buf_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out particle, out _, "Summoner_ally_capture_buf_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out particle2, out _, "Summoner_capture_Pulse.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SetPhysicalImmune(owner, true);
            SetMagicImmune(owner, true);
            //RequireVar(this.splash);
            ApplyAssistMarker(attacker, owner, 10);
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 1200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectHeroes, default, true))
            {
                SpellBuffClear(unit, nameof(Buffs.OdinCaptureChannel));
                SpellEffectCreate(out _, out _, "Ezreal_essenceflux_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, unit, false, unit, "root", default, unit, default, default, true, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(auraParticle);
            SpellEffectRemove(particle2);
            SpellEffectRemove(particle);
            SetPhysicalImmune(owner, false);
            SetMagicImmune(owner, false);
        }
        public override void OnUpdateStats()
        {
            TeamId teamOfOwner = GetTeamID_CS(owner);
            if (teamOfOwner == TeamId.TEAM_NEUTRAL)
            {
                SpellBuffClear(owner, nameof(Buffs.SummonerOdinGarrison));
            }
            else
            {
                SetPhysicalImmune(owner, true);
                SetMagicImmune(owner, true);
                IncFlatPhysicalDamageMod(owner, 0);
                IncPercentCooldownMod(owner, -1);
                IncPAR(owner, 800, PrimaryAbilityResourceType.MANA);
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (splash)
            {
                float newDamage = damageAmount * 0.5f;
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position3D, 250, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    if (unit != target)
                    {
                        ApplyDamage(attacker, unit, newDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 1, 1, false, false, attacker);
                    }
                }
            }
        }
    }
}