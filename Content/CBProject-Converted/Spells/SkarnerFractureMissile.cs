namespace Spells
{
    public class SkarnerFractureMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 80, 120, 160, 200, 240 };
        int[] effect1 = { 30, 45, 60, 75, 90 };
        float[] effect2 = { 1, 0.5f, 0.25f, 0.125f, 0.0625f, 0.0625f, 0.0625f, 0.0625f, 0.0625f, 0.0625f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            BreakSpellShields(target);
            ApplyDamage(attacker, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 1, false, false, attacker);
            if (IsDead(target))
            {
                int count = GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.SkarnerFracture));
                TeamId teamID = GetTeamID_CS(attacker);
                int level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float healingAmount = effect1[level - 1];
                float aPStat = GetFlatMagicDamageMod(attacker);
                float bonusHeal = aPStat * 0.3f;
                healingAmount += bonusHeal;
                level = count;
                level++;
                float healingMod = effect2[level - 1];
                healingAmount *= healingMod;
                SpellEffectCreate(out _, out _, "Skarner_Fracture_Tar_Consume.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                IncHealth(attacker, healingAmount, attacker);
                SpellEffectCreate(out _, out _, "galio_bulwark_heal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, attacker, default, default, attacker, default, default, true, false, false, false, false);
                AddBuff(attacker, attacker, new Buffs.SkarnerFracture(), 8, 1, 6, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
            }
            else
            {
                AddBuff(attacker, target, new Buffs.SkarnerFractureMissile(), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class SkarnerFractureMissile : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SkarnerFracture",
            BuffTextureName = "SkarnerFracture.dds",
        };
        EffectEmitter particle1;
        int[] effect0 = { 30, 45, 60, 75, 90 };
        float[] effect1 = { 1, 0.5f, 0.25f, 0.125f, 0.0625f, 0.0625f, 0.0625f, 0.0625f, 0.0625f, 0.0625f };
        public override void OnActivate()
        {
            ObjAIBase caster = GetBuffCasterUnit();
            TeamId teamID = GetTeamID_CS(caster);
            SpellEffectCreate(out particle1, out _, "Skarner_Fracture_Tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle1);
        }
        public override void OnTakeDamage(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource)
        {
            ObjAIBase caster = GetBuffCasterUnit();
            if (caster == attacker)
            {
                int count = GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.SkarnerFracture));
                float duration = GetBuffRemainingDuration(owner, nameof(Buffs.SkarnerFractureMissile));
                TeamId teamID = GetTeamID_CS(attacker);
                SpellBuffClear(owner, nameof(Buffs.SkarnerFractureMissile));
                int level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float healingAmount = effect0[level - 1];
                float aPStat = GetFlatMagicDamageMod(attacker);
                float bonusHeal = aPStat * 0.3f;
                healingAmount += bonusHeal;
                level = count;
                level++;
                float healingMod = effect1[level - 1];
                healingAmount *= healingMod;
                SpellEffectCreate(out EffectEmitter motaExplosion /*UNUSED*/, out _, "Skarner_Fracture_Tar_Consume.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                IncHealth(attacker, healingAmount, attacker);
                SpellEffectCreate(out EffectEmitter healVFX /*UNUSED*/, out _, "galio_bulwark_heal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, attacker, default, default, attacker, default, default, true, false, false, false, false);
                AddBuff(attacker, attacker, new Buffs.SkarnerFracture(), 8, 1, duration, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}