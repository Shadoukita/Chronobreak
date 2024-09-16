namespace Spells
{
    public class AhriFoxFireMissileTwo : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = { 20, 35, 50, 65, 80 };
        int[] effect2 = { 40, 70, 100, 130, 160 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID; // UNITIALIZED
            teamID = TeamId.TEAM_UNKNOWN; //TODO: Verify
            if (GetBuffCountFromCaster(target, attacker, nameof(Buffs.AhriFoxFireMissileTwo)) > 0)
            {
                if (charVars.FoxFireIsActive == 1)
                {
                    SpellEffectCreate(out _, out _, "Ahri_PassiveHeal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, false, false, false, false, false);
                    SpellEffectCreate(out _, out _, "Ahri_passive_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, target, default, default, true, false, false, false, false);
                    float nextBuffVars_DrainPercent = 0.35f;
                    bool nextBuffVars_DrainedBool = false;
                    AddBuff(attacker, attacker, new Buffs.GlobalDrain(nextBuffVars_DrainPercent, nextBuffVars_DrainedBool), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    ApplyDamage(attacker, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.1875f, 0, false, false, attacker);
                }
                else
                {
                    if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.AhriSoulCrusher3)) == 0)
                    {
                        if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.AhriSoulCrusher)) == 0)
                        {
                            AddBuff(attacker, attacker, new Buffs.AhriSoulCrusherCounter(), 9, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
                        }
                        ApplyDamage(attacker, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.1875f, 0, false, false, attacker);
                    }
                }
                AddBuff(attacker, target, new Buffs.AhriFoxFireMissileTwo(), 3, 1, 3, BuffAddType.STACKS_AND_CONTINUE, BuffType.INTERNAL, 0, true, false, false);
            }
            else
            {
                if (charVars.FoxFireIsActive == 1)
                {
                    SpellEffectCreate(out _, out _, "Ahri_PassiveHeal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, false, false, false, false, false);
                    SpellEffectCreate(out _, out _, "Ahri_passive_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, target, default, default, true, false, false, false, false);
                    AddBuff(attacker, attacker, new Buffs.AhriSoulCrusher3(), 3, 3, 5, BuffAddType.STACKS_AND_OVERLAPS, BuffType.INTERNAL, 0.25f, true, false, false);
                    float nextBuffVars_DrainPercent = 0.35f;
                    bool nextBuffVars_DrainedBool = false;
                    AddBuff(attacker, attacker, new Buffs.GlobalDrain(nextBuffVars_DrainPercent, nextBuffVars_DrainedBool), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    ApplyDamage(attacker, target, effect2[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.375f, 0, false, false, attacker);
                    if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.AhriSoulCrusher)) > 0)
                    {
                        SpellBuffRemoveStacks(attacker, attacker, nameof(Buffs.AhriSoulCrusher), 1);
                    }
                }
                else
                {
                    if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.AhriSoulCrusher3)) == 0)
                    {
                        if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.AhriSoulCrusher)) == 0)
                        {
                            AddBuff(attacker, attacker, new Buffs.AhriSoulCrusherCounter(), 9, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
                        }
                        ApplyDamage(attacker, target, effect2[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.375f, 0, false, false, attacker);
                    }
                }
                AddBuff(attacker, target, new Buffs.AhriFoxFireMissileTwo(), 3, 1, 6, BuffAddType.STACKS_AND_CONTINUE, BuffType.INTERNAL, 0, true, false, false);
            }
            DestroyMissile(missileNetworkID);
        }
    }
}
namespace Buffs
{
    public class AhriFoxFireMissileTwo : BuffScript
    {
        public override void OnUpdateActions()
        {
            int count = GetBuffCountFromAll(owner, nameof(Buffs.AhriFoxFireMissileTwo));
            if (count == 3)
            {
                SpellBuffClear(owner, nameof(Buffs.AhriFoxFireMissileTwo));
            }
        }
    }
}