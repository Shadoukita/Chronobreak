namespace Spells
{
    public class ViktorChaosStormAOE : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
        };
    }
}
namespace Buffs
{
    public class ViktorChaosStormAOE : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Infernal Guardian",
            BuffTextureName = "Annie_GuardianIncinerate.dds",
        };
        EffectEmitter b;
        EffectEmitter c;
        bool soundClear;
        float cDMOD;
        EffectEmitter partThing;
        float lastTimeExecuted;
        EffectEmitter particleID; // UNUSED
        int[] effect0 = { 10, 15, 20 };
        public override void OnActivate()
        {
            ObjAIBase caster = GetBuffCasterUnit(); // UNUSED
            TeamId ownerTeam = GetTeamID_CS(owner);
            SpellEffectCreate(out b, out c, "Viktor_ChaosStorm_green.troy", "Viktor_ChaosStorm_red.troy", ownerTeam, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            soundClear = true;
            cDMOD = GetPercentCooldownMod(attacker);
        }
        public override void OnDeactivate(bool expired)
        {
            Vector3 centerPos = GetUnitPosition(owner);
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, centerPos, 25000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectUntargetable, nameof(Buffs.ViktorChaosStormGuide), true))
            {
                if (unit is Champion)
                {
                    SpellBuffClear(unit, nameof(Buffs.ViktorChaosStormGuide));
                    SpellBuffRemove(unit, nameof(Buffs.ViktorChaosStormGuide), (ObjAIBase)owner, 0);
                }
                else
                {
                    SetInvulnerable(unit, false);
                    SetTargetable(unit, true);
                    SpellBuffClear(unit, nameof(Buffs.ViktorChaosStormGuide));
                    SpellBuffRemove(unit, nameof(Buffs.ViktorChaosStormGuide), (ObjAIBase)owner, 0);
                    ApplyDamage((ObjAIBase)unit, unit, 25000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, (ObjAIBase)unit);
                }
            }
            SetInvulnerable(owner, false);
            ApplyDamage((ObjAIBase)owner, owner, 10000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 0, false, false, (ObjAIBase)owner);
            SpellBuffRemove(attacker, nameof(Buffs.ViktorChaosStormTimer), attacker, 0);
            SetSpell(attacker, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.ViktorChaosStorm));
            float nEWCD = 120 * cDMOD;
            nEWCD += 120;
            SetSlotSpellCooldownTimeVer2(nEWCD, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker, false);
            SpellEffectRemove(b);
            SpellEffectRemove(c);
            if (!soundClear)
            {
                SpellEffectRemove(partThing);
            }
        }
        public override void OnUpdateStats()
        {
            ObjAIBase caster = GetBuffCasterUnit(); // UNUSED
            float grandDistance = DistanceBetweenObjects(owner, caster);
            float minDistanceCheck = 350;
            float maxDistanceCheck = 950;
            float distanceVariation = maxDistanceCheck - minDistanceCheck;
            float maxSpeed = 450;
            float minSpeed = 175;
            float speedVariation = maxSpeed - minSpeed;
            if (grandDistance <= minDistanceCheck)
            {
                IncMoveSpeedFloorMod(owner, maxSpeed);
                charVars.CurrSpeed = maxSpeed;
            }
            else if (grandDistance >= 1500 + maxDistanceCheck)
            {
                IncMoveSpeedFloorMod(owner, minSpeed);
                charVars.CurrSpeed = minSpeed;
            }
            else
            {
                float offsetValue = grandDistance - minDistanceCheck;
                float percOverMinDist = offsetValue / distanceVariation;
                float speedToReduce = percOverMinDist * speedVariation;
                float adjustedSpeed = maxSpeed - speedToReduce;
                IncMoveSpeedFloorMod(owner, adjustedSpeed);
                charVars.CurrSpeed = adjustedSpeed;
            }
        }
        public override void OnUpdateActions()
        {
            ObjAIBase caster = GetBuffCasterUnit();
            int level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float damageAmount = effect0[level - 1];
            TeamId ownerTeamID = GetTeamID_CS(caster); // UNUSED
            float aPPreMod = GetFlatMagicDamageMod(caster);
            float aPPostMod = 0.06f * aPPreMod;
            float finalDamage = damageAmount + aPPostMod;
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, false))
            {
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    BreakSpellShields(unit);
                    ApplyDamage(attacker, unit, finalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                    SpellEffectCreate(out particleID, out _, "Viktor_ChaosStorm_beam.troy", default, TeamId.TEAM_NEUTRAL, 10, 0, TeamId.TEAM_UNKNOWN, default, unit, false, owner, "head", default, unit, "spine", default, true, false, false, false, false);
                    SpellEffectCreate(out _, out _, "Viktor_ChaosStorm_hit.troy", default, TeamId.TEAM_NEUTRAL, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "root", default, unit, default, default, true, false, false, false, false);
                    if (soundClear)
                    {
                        SpellEffectCreate(out partThing, out _, "viktor_chaosstorm_damage_sound.troy", default, TeamId.TEAM_NEUTRAL, 10, 0, TeamId.TEAM_UNKNOWN, default, unit, false, owner, default, default, owner, default, default, true, false, false, false, false);
                        soundClear = false;
                    }
                }
            }
            if (IsDead(caster))
            {
                SpellBuffRemoveCurrent(owner);
            }
            foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 2500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, default, true))
            {
                float distance = DistanceBetweenObjects(owner, unit);
                if (!soundClear && distance > 350)
                {
                    soundClear = true;
                    SpellEffectRemove(partThing);
                }
            }
        }
    }
}