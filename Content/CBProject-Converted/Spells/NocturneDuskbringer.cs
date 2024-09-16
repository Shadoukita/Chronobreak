namespace Spells
{
    public class NocturneDuskbringer : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = { 0.15f, 0.2f, 0.25f, 0.3f, 0.35f };
        int[] effect1 = { 15, 25, 35, 45, 55 };
        float[] effect6 = { 48.75f, 86.25f, 123.75f, 161.25f, 198.75f };
        public override void OnMissileUpdate(SpellMissile missileNetworkID, Vector3 missilePosition)
        {
            int level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float nextBuffVars_HastePercent = effect0[level - 1];
            int nextBuffVars_BonusAD = effect1[level - 1];
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 groundHeight = GetGroundHeight(missilePosition);
            groundHeight.Y += 10;
            Minion other3 = SpawnMinion("DarkPath", "testcube", "idle.lua", groundHeight, teamID, true, true, true, true, false, true, 0, false, true);
            Vector3 targetPos = GetSpellTargetPos(spell);
            FaceDirection(other3, targetPos);
            AddBuff(owner, other3, new Buffs.NocturneDuskbringer(nextBuffVars_HastePercent, nextBuffVars_BonusAD), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void SelfExecute()
        {
            int level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float nextBuffVars_HastePercent = effect0[level - 1];
            float nextBuffVars_BonusAD = effect1[level - 1];
            AddBuff(attacker, attacker, new Buffs.NocturneDuskbringerHaste(nextBuffVars_HastePercent, nextBuffVars_BonusAD), 1, 1, 0.5f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_HastePercent;
            int nextBuffVars_BonusAD;
            int nocturneSkinID = GetSkinID(owner);
            TeamId teamID = GetTeamID_CS(owner);
            if (target == owner)
            {
                int level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                nextBuffVars_HastePercent = effect0[level - 1];
                nextBuffVars_BonusAD = effect1[level - 1];
                teamID = GetTeamID_CS(owner);
                Vector3 myPosition = GetUnitPosition(owner);
                Minion other3 = SpawnMinion("DarkPath", "testcube", "idle.lua", myPosition, teamID, true, true, true, true, false, true, 0, false, true);
                Vector3 targetPos = GetSpellTargetPos(spell);
                FaceDirection(other3, targetPos);
                AddBuff(owner, other3, new Buffs.NocturneDuskbringer(nextBuffVars_HastePercent, nextBuffVars_BonusAD), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            else
            {
                Vector3 lastPosition;
                Vector3 nextBuffVars_LastPosition;
                TeamId teamID2 = GetTeamID_CS(target); // UNUSED
                float physPreMod = GetFlatPhysicalDamageMod(owner);
                float physPostMod = 0.75f * physPreMod;
                bool isStealthed = GetStealthed(target);
                if (!isStealthed)
                {
                    if (target is Champion)
                    {
                        BreakSpellShields(target);
                        lastPosition = GetPointByUnitFacingOffset(target, 2000, 0);
                        nextBuffVars_LastPosition = lastPosition;
                        AddBuff(owner, target, new Buffs.NocturneDuskbringerTrail(nextBuffVars_LastPosition), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                        int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        SpellEffectCreate(out _, out _, "NocturneDuskbringer_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, target, default, default, target, default, default, true, false, false, false, false);
                        ApplyDamage(attacker, target, physPostMod + effect6[level - 1], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, true, attacker);
                        if (nocturneSkinID == 1)
                        {
                            SpellEffectCreate(out _, out _, "NocturneDuskbringer_frost_buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, target, default, default, target, default, default, false, false, false, false, false);
                        }
                        else
                        {
                            SpellEffectCreate(out _, out _, "NocturneDuskbringer_buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, target, default, default, target, default, default, false, false, false, false, false);
                        }
                    }
                    else
                    {
                        BreakSpellShields(target);
                        int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        SpellEffectCreate(out _, out _, "NocturneDuskbringer_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, target, default, default, target, default, default, true, false, false, false, false);
                        ApplyDamage(attacker, target, physPostMod + effect6[level - 1], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, true, attacker);
                    }
                }
                else
                {
                    if (target is Champion)
                    {
                        BreakSpellShields(target);
                        lastPosition = GetPointByUnitFacingOffset(target, 2000, 0);
                        nextBuffVars_LastPosition = lastPosition;
                        AddBuff(owner, target, new Buffs.NocturneDuskbringerTrail(nextBuffVars_LastPosition), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                        int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        SpellEffectCreate(out _, out _, "NocturneDuskbringer_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, target, default, default, target, default, default, true, false, false, false, false);
                        SpellEffectCreate(out _, out _, "NocturneDuskbringer_buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, target, default, default, target, default, default, false, false, false, false, false);
                        ApplyDamage(attacker, target, physPostMod + effect6[level - 1], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, true, attacker);
                        if (nocturneSkinID == 1)
                        {
                            SpellEffectCreate(out _, out _, "NocturneDuskbringer_frost_buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, target, default, default, target, default, default, false, false, false, false, false);
                        }
                        else
                        {
                            SpellEffectCreate(out _, out _, "NocturneDuskbringer_buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, target, default, default, target, default, default, false, false, false, false, false);
                        }
                    }
                    else
                    {
                        bool canSee = CanSeeTarget(owner, target);
                        if (canSee)
                        {
                            BreakSpellShields(target);
                            int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                            SpellEffectCreate(out _, out _, "NocturneDuskbringer_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, target, default, default, target, default, default, true, false, false, false, false);
                            ApplyDamage(attacker, target, physPostMod + effect6[level - 1], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, true, attacker);
                        }
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class NocturneDuskbringer : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "EzrealEssenceFluxDebuff",
            BuffTextureName = "Nocturne_Duskbringer.dds",
            IsDeathRecapSource = true,
        };
        float hastePercent;
        int bonusAD;
        EffectEmitter particle2;
        EffectEmitter particle;
        float lastTimeExecuted;
        public NocturneDuskbringer(float hastePercent = default, int bonusAD = default)
        {
            this.hastePercent = hastePercent;
            this.bonusAD = bonusAD;
        }
        public override void OnActivate()
        {
            SetInvulnerable(owner, true);
            SetGhosted(owner, true);
            SetStealthed(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetForceRenderParticles(owner, true);
            SetNoRender(owner, true);
            //RequireVar(this.hastePercent);
            //RequireVar(this.bonusAD);
            TeamId teamOfOwner = GetTeamID_CS(owner);
            Vector3 orientationPoint = GetPointByUnitFacingOffset(owner, 10000, 0);
            int nocturneSkinID = GetSkinID(attacker);
            if (nocturneSkinID == 1)
            {
                SpellEffectCreate(out particle2, out particle, "NocturneDuskbringer_path_frost_green.troy", "NocturneDuskbringer_path_frost_red.troy", teamOfOwner, 240, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, true, false, orientationPoint);
            }
            else if (nocturneSkinID == 4)
            {
                SpellEffectCreate(out particle2, out particle, "NocturneDuskbringer_path_ghost_green.troy", "NocturneDuskbringer_path_ghost_red.troy", teamOfOwner, 240, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, true, false, orientationPoint);
            }
            else
            {
                SpellEffectCreate(out particle2, out particle, "NocturneDuskbringer_path_green.troy", "NocturneDuskbringer_path_red.troy", teamOfOwner, 240, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, true, false, orientationPoint);
            }
            SetTargetable(owner, false);
            Vector3 point1 = GetPointByUnitFacingOffset(owner, 0, 0); // UNUSED
        }
        public override void OnDeactivate(bool expired)
        {
            SetInvulnerable(owner, false);
            SetTargetable(owner, true);
            SpellEffectRemove(particle);
            SpellEffectRemove(particle2);
            ApplyDamage((ObjAIBase)owner, owner, 5000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            SetGhosted(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetSuppressCallForHelp(owner, true);
            SetStealthed(owner, true);
            SetTargetable(owner, false);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, true))
            {
                float distance = DistanceBetweenObjects(owner, attacker);
                if (distance <= 80)
                {
                    float nextBuffVars_HastePercent = hastePercent;
                    float nextBuffVars_BonusAD = bonusAD;
                    AddBuff(attacker, attacker, new Buffs.NocturneDuskbringerHaste(nextBuffVars_HastePercent, nextBuffVars_BonusAD), 1, 1, 0.5f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                }
            }
        }
    }
}