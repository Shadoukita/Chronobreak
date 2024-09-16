namespace Spells
{
    public class SejuaniWintersClaw : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
        };
        float[] effect0 = { -0.3f, -0.4f, -0.5f, -0.6f, -0.7f };
        float[] effect1 = { -0.3f, -0.35f, -0.4f, -0.45f, -0.5f };
        int[] effect2 = { 60, 110, 160, 210, 260 };
        public override bool CanCast()
        {
            bool returnValue = true;
            returnValue = false;
            TeamId teamID = GetTeamID_CS(owner);
            if (teamID == TeamId.TEAM_ORDER)
            {
                foreach (AttackableUnit unit in GetRandomUnitsInArea(owner, owner.Position3D, 900, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.SejuaniFrost), true))
                {
                    returnValue = true;
                }
                foreach (AttackableUnit unit in GetRandomUnitsInArea(owner, owner.Position3D, 900, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.SejuaniFrostResist), true))
                {
                    returnValue = true;
                }
            }
            else
            {
                foreach (AttackableUnit unit in GetRandomUnitsInArea(owner, owner.Position3D, 900, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.SejuaniFrostChaos), true))
                {
                    returnValue = true;
                }
                foreach (AttackableUnit unit in GetRandomUnitsInArea(owner, owner.Position3D, 900, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.SejuaniFrostResistChaos), true))
                {
                    returnValue = true;
                }
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "Sejuani_WintersClaw_cas.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_UpArm", default, owner, default, default, true, false, false, false, false);
            SpellEffectCreate(out _, out _, "Sejuani_WintersClaw_cas_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
        }
        /*
        //TODO: Uncomment and fix
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID, HitResult hitResult)
        {
            AttackableUnit unit; // UNITIALIZED
            TeamId teamID = GetTeamID(owner);
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float nextBuffVars_MovementSpeedMod = this.effect0[level - 1];
            float nextBuffVars_AttackSpeedMod = this.effect1[level - 1]; // UNUSED
            float damageToDeal = this.effect2[level - 1];
            bool damageThis = false;
            if(teamID == TeamId.TEAM_BLUE)
            {
                if(GetBuffCountFromCaster(target, owner, nameof(Buffs.SejuaniFrost)) > 0)
                {
                    damageThis = true;
                }
                if(GetBuffCountFromCaster(target, owner, nameof(Buffs.SejuaniFrostResist)) > 0)
                {
                    damageThis = true;
                }
                if(damageThis)
                {
                    SpellEffectCreate(out particle1, out _, "Sejuani_WintersClaw_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, unit, default, default, false, false, false, false, false);
                    BreakSpellShields(target);
                    AddBuff((ObjAIBase)owner, target, new Buffs.SejuaniWintersClaw(nextBuffVars_MovementSpeedMod), 1, 1, charVars.FrostDuration, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                    ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.5f, 0, false, false, attacker);
                }
            }
            else
            {
                if(GetBuffCountFromCaster(target, owner, nameof(Buffs.SejuaniFrostChaos)) > 0)
                {
                    damageThis = true;
                }
                if(GetBuffCountFromCaster(target, owner, nameof(Buffs.SejuaniFrostResistChaos)) > 0)
                {
                    damageThis = true;
                }
                if(damageThis)
                {
                    SpellEffectCreate(out particle1, out _, "Sejuani_WintersClaw_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, unit, default, default, false, false, false, false, false);
                    BreakSpellShields(target);
                    AddBuff((ObjAIBase)owner, target, new Buffs.SejuaniWintersClawChaos(nextBuffVars_MovementSpeedMod), 1, 1, charVars.FrostDuration, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                    ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.5f, 0, false, false, attacker);
                }
            }
        }
        */
    }
}
namespace Buffs
{
    public class SejuaniWintersClaw : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Sejuani_Frost_Arctic.troy", },
            BuffName = "SejuaniFrostArctic",
            BuffTextureName = "Sejuani_Permafrost.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        float movementSpeedMod;
        EffectEmitter overhead;
        public SejuaniWintersClaw(float movementSpeedMod = default)
        {
            this.movementSpeedMod = movementSpeedMod;
        }
        public override void OnActivate()
        {
            ObjAIBase caster = GetBuffCasterUnit();
            SpellBuffRemove(owner, nameof(Buffs.SejuaniFrost), caster, 0);
            //RequireVar(this.movementSpeedMod);
            IncPercentMultiplicativeMovementSpeedMod(owner, movementSpeedMod);
            ApplyAssistMarker(attacker, owner, 10);
            SpellEffectCreate(out overhead, out _, "Sejuani_Frost_Arctic_Overhead.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, true, owner, default, default, attacker, "Bird_head", default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(overhead);
            if (GetBuffCountFromCaster(owner, caster, nameof(Buffs.SejuaniFrostTracker)) > 0)
            {
                float duration = GetBuffRemainingDuration(owner, nameof(Buffs.SejuaniFrostTracker));
                SpellBuffRemove(owner, nameof(Buffs.SejuaniFrostTracker), attacker, 0);
                float nextBuffVars_MovementSpeedMod = -0.1f;
                AddBuff(attacker, owner, new Buffs.SejuaniFrost(nextBuffVars_MovementSpeedMod), 1, 1, duration, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
            }
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, movementSpeedMod);
        }
    }
}