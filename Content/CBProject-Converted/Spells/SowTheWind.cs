namespace Spells
{
    public class SowTheWind : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        float[] effect0 = { -0.24f, -0.3f, -0.36f, -0.42f, -0.48f };
        int[] effect1 = { 60, 115, 170, 225, 280 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(attacker);
            BreakSpellShields(target);
            float nextBuffVars_AttackSpeedMod = 0;
            float nextBuffVars_MoveSpeedMod = effect0[level - 1];
            ApplyDamage(attacker, target, effect1[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.8f, 1, false, false, attacker);
            SpellEffectCreate(out _, out _, "SowTheWind_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
            AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 4, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class SowTheWind : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Zephyr",
            BuffTextureName = "Janna_Zephyr.dds",
        };
        EffectEmitter particle;
        int sowCast;
        float[] effect0 = { 0.08f, 0.1f, 0.12f, 0.14f, 0.16f };
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "SowTheWind_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
            SpellEffectCreate(out particle, out _, "SowTheWind_buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, owner, "head", default, false);
            SetGhosted(owner, true);
            sowCast = 0;
        }
        public override void OnDeactivate(bool expired)
        {
            SetGhosted(owner, false);
            SpellEffectRemove(particle);
        }
        public override void OnUpdateStats()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float movementSpeedMod = effect0[level - 1];
            IncPercentMovementSpeedMod(owner, movementSpeedMod);
            SetGhosted(owner, true);
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            spellName = GetSpellName(spell);
            if (spellName == nameof(Spells.SowTheWind))
            {
                sowCast = 1;
            }
        }
        public override void OnLaunchMissile(SpellMissile missileId)
        {
            if (sowCast == 1)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}