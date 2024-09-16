namespace Spells
{
    public class ShyvanaFireballMissileMinion : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class ShyvanaFireballMissileMinion : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "ShyvanaFlameBreathDebuff",
            BuffTextureName = "ShyvanaFlameBreath.dds",
        };
        EffectEmitter a;
        EffectEmitter b; // UNUSED
        EffectEmitter c;
        int[] effect0 = { 12, 19, 26, 32, 39 };
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(attacker);
            SpellEffectCreate(out a, out _, "shyvana_flameBreath_dragon_burn.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out b, out _, "shyvana_flameBreath_tar_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            SpellEffectCreate(out c, out _, "shyvana_flameBreath_indicator.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(a);
            SpellEffectRemove(c);
        }
        public override void OnUpdateStats()
        {
            IncPercentArmorMod(owner, -0.15f);
        }
        public override void OnBeingHit(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, HitResult hitResult)
        {
            ObjAIBase caster = GetBuffCasterUnit();
            if (caster == attacker)
            {
                int level = GetSlotSpellLevel(caster, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float procDamage = effect0[level - 1];
                ApplyDamage(caster, target, procDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0.09f, 0, false, false, caster);
                TeamId teamID = GetTeamID_CS(caster);
                SpellEffectCreate(out a, out _, "shyvana_flameBreath_reignite.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            }
        }
    }
}