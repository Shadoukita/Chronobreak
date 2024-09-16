namespace Spells
{
    public class UrgotHeatseekingHomeMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = { 10, 40, 70, 100, 130 };
        float[] effect1 = { -0.2f, -0.25f, -0.3f, -0.35f, -0.4f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_MoveSpeedMod; // UNUSED
            TeamId teamID = GetTeamID_CS(attacker);
            int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float baseDamage = effect0[level - 1];
            float attackDamage = GetTotalAttackDamage(owner);
            float scaling = 0.85f;
            float bonusAD = scaling * attackDamage;
            float totalDamage = baseDamage + bonusAD;
            hitResult = HitResult.HIT_Normal;
            BreakSpellShields(target);
            ApplyDamage(owner, target, totalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, true, attacker);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.UrgotTerrorCapacitorActive2)) > 0)
            {
                level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                nextBuffVars_MoveSpeedMod = effect1[level - 1];
                AddBuff(attacker, target, new Buffs.UrgotSlow(), 100, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
            }
            AddBuff(owner, target, new Buffs.UrgotEntropyPassive(), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            SpellEffectCreate(out _, out _, "UrgotHeatSeekingMissile_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
            DestroyMissile(missileNetworkID);
        }
    }
}
namespace Buffs
{
    public class UrgotHeatseekingHomeMissile : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "GLOBAL_SLOW.TROY", },
            BuffName = "Slow",
            BuffTextureName = "Chronokeeper_Timestop.dds",
        };
    }
}