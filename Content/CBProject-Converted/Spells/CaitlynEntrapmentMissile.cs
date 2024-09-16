namespace Spells
{
    public class CaitlynEntrapmentMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        float[] effect0 = { 1, 1.25f, 1.5f, 1.75f, 2 };
        int[] effect2 = { 80, 130, 180, 230, 280 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(owner);
            int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float nextBuffVars_MoveSpeedMod = -0.5f;
            bool isStealthed = GetStealthed(target);
            if (!isStealthed)
            {
                BreakSpellShields(target);
                AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, effect0[level - 1], BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                AddBuff(attacker, target, new Buffs.CaitlynEntrapmentMissile(), 100, 1, effect0[level - 1], BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, true);
                ApplyDamage(attacker, target, effect2[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.8f, 1, false, false, attacker);
                SpellEffectCreate(out _, out _, "caitlyn_entrapment_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, owner, default, default, true);
                DestroyMissile(missileNetworkID);
            }
            else
            {
                if (target is Champion)
                {
                    BreakSpellShields(target);
                    AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, effect0[level - 1], BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                    AddBuff(attacker, target, new Buffs.CaitlynEntrapmentMissile(), 100, 1, effect0[level - 1], BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, true);
                    ApplyDamage(attacker, target, effect2[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.8f, 1, false, false, attacker);
                    SpellEffectCreate(out _, out _, "caitlyn_entrapment_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, owner, default, default, true);
                    DestroyMissile(missileNetworkID);
                }
                else
                {
                    bool canSee = CanSeeTarget(owner, target);
                    if (canSee)
                    {
                        BreakSpellShields(target);
                        AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, effect0[level - 1], BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                        AddBuff(attacker, target, new Buffs.CaitlynEntrapmentMissile(), 100, 1, effect0[level - 1], BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, true);
                        ApplyDamage(attacker, target, effect2[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.8f, 1, false, false, attacker);
                        SpellEffectCreate(out _, out _, "caitlyn_entrapment_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, owner, default, default, true);
                        DestroyMissile(missileNetworkID);
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class CaitlynEntrapmentMissile : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "caitlyn_entrapment_slow.troy", },
            BuffName = "",
            BuffTextureName = "",
        };
    }
}