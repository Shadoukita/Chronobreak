namespace Spells
{
    public class DarkWind : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            ChainMissileParameters = new()
            {
                CanHitCaster = false,
                CanHitSameTarget = true,
                CanHitSameTargetConsecutively = false,
                MaximumHitsByLevel = new[] { 2, 4, 6, 8, 10, },
            },
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (target is Champion)
            {
                AddBuff(owner, target, new Buffs.DarkWind(), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.SILENCE, 0, true);
            }
            int targetNum = GetSpellTargetsHitPlusOne(spell);
            if (targetNum == 1)
            {
                ApplyDamage(attacker, target, 100, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.35f, 1, false, false);
            }
            else
            {
                ApplyDamage(attacker, target, 100, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.35f, 1, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class DarkWind : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Global_Silence.troy", },
            BuffName = "Silence",
            BuffTextureName = "Fiddlesticks_DarkWind.dds",
            PopupMessage = new[] { "game_floatingtext_Silenced", },
        };
        public override void OnActivate()
        {
            SetSilenced(owner, true);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SetSilenced(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetSilenced(owner, true);
        }
    }
}