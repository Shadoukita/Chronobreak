namespace Spells
{
    public class InfernalGuardian : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = { 0, 400, 800, 600, 800 };
        int[] effect1 = { 0, 25, 50 };
        int[] effect2 = { 0, 20, 40 };
        int[] effect4 = { 0, 400, 800 };
        int[] effect5 = { 35, 35, 35 };
        int[] effect6 = { 200, 325, 450 };
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            TeamId teamID = GetTeamID_CS(owner);
            int annieSkinID = GetSkinID(owner);
            if (annieSkinID == 5)
            {
                SpellEffectCreate(out _, out _, "infernalguardian_tar_frost.troy", default, teamID, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, target, default, default, true, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out _, out _, "InfernalGuardian_tar.troy", default, teamID, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, target, default, default, true, false, false, false, false);
            }
            Pet other1 = SpawnPet("Tibbers", "AnnieTibbers", nameof(Buffs.InfernalGuardian), default, 45, targetPos, effect0[level - 1], effect1[level - 1]);
            float nextBuffVars_ArmorAmount = effect2[level - 1];
            float nextBuffVars_MRAmount = effect2[level - 1];
            float nextBuffVars_HealthAmount = effect4[level - 1];
            float damageAmount = effect5[level - 1];
            float aPPreMod = GetFlatMagicDamageMod(owner);
            float aPPostMod = 0.2f * aPPreMod;
            float nextBuffVars_FinalDamage = damageAmount + aPPostMod;
            AddBuff(attacker, attacker, new Buffs.InfernalGuardianTimer(), 1, 1, 45, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff(owner, other1, new Buffs.InfernalGuardianBurning(nextBuffVars_ArmorAmount, nextBuffVars_MRAmount, nextBuffVars_HealthAmount, nextBuffVars_FinalDamage), 1, 1, 45, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            charVars.SpellWillStun = false;
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pyromania_particle)) > 0)
            {
                charVars.SpellWillStun = true;
                SpellBuffRemove(owner, nameof(Buffs.Pyromania_particle), owner, 0);
            }
            AddBuff(owner, owner, new Buffs.Pyromania(), 5, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (charVars.SpellWillStun)
            {
                ApplyStun(attacker, target, charVars.StunDuration);
            }
            ApplyDamage(attacker, target, effect6[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 0, false, false, attacker);
        }
    }
}
namespace Buffs
{
    public class InfernalGuardian : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            IsPetDurationBuff = true,
        };
    }
}