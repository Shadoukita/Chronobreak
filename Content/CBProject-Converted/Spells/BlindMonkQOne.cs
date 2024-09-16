namespace Spells
{
    public class BlindMonkQOne : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 50, 80, 110, 140, 170 };
        int[] effect1 = { 0, 0, 0, 0, 0 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID;
            float baseDamage;
            float bonusAD;
            float damageToDeal;
            bool isStealthed = GetStealthed(target);
            if (!isStealthed)
            {
                teamID = GetTeamID_CS(attacker);
                baseDamage = effect0[level - 1];
                bonusAD = GetFlatPhysicalDamageMod(owner);
                bonusAD *= 0.9f;
                damageToDeal = bonusAD + baseDamage;
                if (teamID == TeamId.TEAM_ORDER)
                {
                    AddBuff(attacker, target, new Buffs.BlindMonkQOne(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                }
                else
                {
                    AddBuff(attacker, target, new Buffs.BlindMonkQOneChaos(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                }
                ApplyDamage(attacker, target, damageToDeal + effect1[level - 1], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, true, attacker);
                SpellEffectCreate(out _, out _, "blindMonk_Q_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                DestroyMissile(missileNetworkID);
                if (!IsDead(target))
                {
                    AddBuff((ObjAIBase)target, owner, new Buffs.BlindMonkQManager(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
            else
            {
                if (target is Champion)
                {
                    teamID = GetTeamID_CS(attacker);
                    baseDamage = effect0[level - 1];
                    bonusAD = GetFlatPhysicalDamageMod(owner);
                    bonusAD *= 0.9f;
                    damageToDeal = bonusAD + baseDamage;
                    if (teamID == TeamId.TEAM_ORDER)
                    {
                        AddBuff(attacker, target, new Buffs.BlindMonkQOne(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                    }
                    else
                    {
                        AddBuff(attacker, target, new Buffs.BlindMonkQOneChaos(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                    }
                    ApplyDamage(attacker, target, damageToDeal + effect1[level - 1], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, true, attacker);
                    SpellEffectCreate(out _, out _, "blindMonk_Q_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                    DestroyMissile(missileNetworkID);
                    if (!IsDead(target))
                    {
                        AddBuff((ObjAIBase)target, owner, new Buffs.BlindMonkQManager(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
                else
                {
                    bool canSee = CanSeeTarget(owner, target);
                    if (canSee)
                    {
                        teamID = GetTeamID_CS(attacker);
                        baseDamage = effect0[level - 1];
                        bonusAD = GetFlatPhysicalDamageMod(owner);
                        bonusAD *= 0.9f;
                        damageToDeal = bonusAD + baseDamage;
                        if (teamID == TeamId.TEAM_ORDER)
                        {
                            AddBuff(attacker, target, new Buffs.BlindMonkQOne(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                        }
                        else
                        {
                            AddBuff(attacker, target, new Buffs.BlindMonkQOneChaos(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                        }
                        ApplyDamage(attacker, target, damageToDeal + effect1[level - 1], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, true, attacker);
                        SpellEffectCreate(out _, out _, "blindMonk_Q_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                        DestroyMissile(missileNetworkID);
                        if (!IsDead(target))
                        {
                            AddBuff((ObjAIBase)target, owner, new Buffs.BlindMonkQManager(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        }
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class BlindMonkQOne : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head", },
            AutoBuffActivateEffect = new[] { "global_Watched.troy", },
            BuffName = "BlindMonkSonicWave",
            BuffTextureName = "BlindMonkQOne.dds",
        };
        Region bubbleID;
        Region bubbleID2;
        EffectEmitter slow;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(attacker);
            bubbleID = AddUnitPerceptionBubble(teamID, 400, owner, 20, default, default, false);
            bubbleID2 = AddUnitPerceptionBubble(teamID, 50, owner, 20, default, default, true);
            ApplyAssistMarker(attacker, owner, 10);
            SpellEffectCreate(out _, out _, "blindMonk_Q_resonatingStrike_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out _, out _, "blindMonk_Q_resonatingStrike_tar_blood.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out slow, out _, "blindMonk_Q_tar_indicator.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(bubbleID);
            RemovePerceptionBubble(bubbleID2);
            SpellEffectRemove(slow);
            if (GetBuffCountFromCaster(attacker, owner, nameof(Buffs.BlindMonkQManager)) > 0)
            {
                SpellBuffRemove(attacker, nameof(Buffs.BlindMonkQManager), (ObjAIBase)owner, 0);
            }
        }
    }
}