namespace Spells
{
    public class SkarnerVirulentSlash : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            SpellFXOverrideSkins = new[] { "ReefMalphite", },
        };
        EffectEmitter partname; // UNUSED
        int[] effect0 = { 25, 40, 55, 70, 85 };
        int[] effect1 = { 24, 36, 48, 60, 72 };
        float[] effect2 = { -0.2f, -0.25f, -0.3f, -0.35f, -0.4f };
        public override void SelfExecute()
        {
            float baseDamage = effect0[level - 1];
            float procDamage = effect1[level - 1];
            float nextBuffVars_SlowPercent = effect2[level - 1];
            float ratioVar = 0.3f; // UNUSED
            TeamId teamID = GetTeamID_CS(owner);
            PlayAnimation("Spell1", 0, owner, false, true, false);
            AddBuff(owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            int count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.SkarnerVirulentSlash));
            if (count == 0)
            {
                SpellEffectCreate(out partname, out _, "Skarner_Crystal_Slash_Mini_Nova.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, target, default, default, true, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out partname, out _, "Skarner_Crystal_Slash_Buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, target, default, default, true, false, false, false, false);
            }
            bool championHit = false;
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0.8f, false, false, attacker);
                championHit = true;
                if (count == 0)
                {
                    SpellEffectCreate(out _, out _, "chogath_basic_attack_01.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                }
                else
                {
                    ApplyDamage(attacker, unit, procDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 0, false, false, attacker);
                    SpellEffectCreate(out _, out _, "Skarner_Crystal_Slash_Tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                    AddBuff(owner, unit, new Buffs.SkarnerVirulentSlashSlow(nextBuffVars_SlowPercent), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                }
            }
            if (championHit)
            {
                AddBuff(attacker, attacker, new Buffs.SkarnerVirulentSlash(), 1, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                if (count == 0)
                {
                    AddBuff(attacker, attacker, new Buffs.SkarnerVirulentSlashEnergy1(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
    }
}
namespace Buffs
{
    public class SkarnerVirulentSlash : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", "", },
            AutoBuffActivateEffect = new[] { "", "", "", },
            BuffName = "SkarnerVirulentSlash",
            BuffTextureName = "SkarnerVirulentSlash.dds",
        };
        public override void OnDeactivate(bool expired)
        {
            SpellBuffClear(owner, nameof(Buffs.SkarnerVirulentSlashEnergy1));
            SpellBuffClear(owner, nameof(Buffs.SkarnerVirulentSlashEnergy2));
        }
    }
}