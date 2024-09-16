namespace Spells
{
    public class TalonRakeMissileTwo : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = { 30, 55, 80, 105, 130 };
        float[] effect1 = { -0.2f, -0.25f, -0.3f, -0.35f, -0.4f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId ownerTeam = GetTeamID_CS(owner);
            TeamId targetTeam = GetTeamID_CS(target);
            if (targetTeam != ownerTeam)
            {
                int count = GetBuffCountFromCaster(target, target, nameof(Buffs.TalonRakeMissileTwo));
                if (count == 0 && (!GetStealthed(target) || target is Champion || CanSeeTarget(owner, target)))
                {
                    SpellEffectCreate(out _, out _, "talon_w_tar.troy", default, ownerTeam, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, target.Position3D, target, default, default, true, false, false, false, false);
                    AddBuff((ObjAIBase)target, target, new Buffs.TalonRakeMissileTwo(), 1, 1, 1, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                    BreakSpellShields(target);
                    float baseDamage = GetBaseAttackDamage(owner);
                    float totalAD = GetTotalAttackDamage(owner);
                    baseDamage = totalAD - baseDamage;
                    baseDamage *= 0.6f;
                    int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    float bonusDamage = effect0[level - 1];
                    baseDamage += bonusDamage;
                    ApplyDamage(owner, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                    //level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    float nextBuffVars_MoveSpeedMod = effect1[level - 1];
                    AddBuff(attacker, target, new Buffs.TalonSlow(nextBuffVars_MoveSpeedMod), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                }
            }
            else if (target == owner)
            {
                DestroyMissile(missileNetworkID);
            }
        }
    }
}
namespace Buffs
{
    public class TalonRakeMissileTwo : BuffScript
    {
    }
}