namespace Spells
{
    public class GravesClusterShotAttack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = { 60, 105, 150, 195, 240 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamOfCaster = GetTeamID_CS(attacker);
            SpellEffectCreate(out _, out _, "Graves_ClusterShot_Tar.troy", default, teamOfCaster, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, target.Position3D, target, default, default, true, false, false, false, false);
            BreakSpellShields(target);
            float totalDamage = GetTotalAttackDamage(attacker);
            float baseDamage = GetBaseAttackDamage(owner);
            float bonusAD = totalDamage - baseDamage;
            int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float bonusDamage = effect0[level - 1];
            bonusAD *= 0.8f;
            bonusDamage += bonusAD;
            int count = GetBuffCountFromAll(target, nameof(Buffs.GravesClusterShotAttack));
            if (count > 0)
            {
                bonusDamage *= 0.25f;
            }
            AddBuff((ObjAIBase)target, target, new Buffs.GravesClusterShotAttack(), 1, 1, 0.25f, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
            ApplyDamage(attacker, target, bonusDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
        }
    }
}
namespace Buffs
{
    public class GravesClusterShotAttack : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Graves_ClusterShot_cas.troy", },
        };
    }
}