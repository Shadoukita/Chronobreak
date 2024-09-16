namespace Spells
{
    public class Shatter : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 60, 105, 150, 195, 240 };
        int[] effect1 = { -10, -15, -20, -25, -30 };
        public override void SelfExecute()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "Shatter_nova.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            SpellBuffRemove(owner, nameof(Buffs.ShatterSelfBonus), owner, 0);
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            BreakSpellShields(target);
            ApplyDamage(attacker, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 1, false, false, attacker);
            float nextBuffVars_ArmorReduction = effect1[level - 1];
            AddBuff(attacker, target, new Buffs.Shatter(nextBuffVars_ArmorReduction), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.SHRED, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class Shatter : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Shatter",
            BuffTextureName = "GemKnight_Shatter.dds",
        };
        float armorReduction;
        public Shatter(float armorReduction = default)
        {
            this.armorReduction = armorReduction;
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            //RequireVar(this.armorReduction);
            ApplyAssistMarker(attacker, owner, 10);
            SpellEffectCreate(out _, out _, "Shatter_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out _, out _, "BloodSlash.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, armorReduction);
        }
    }
}