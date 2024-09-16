namespace Spells
{
    public class BlindingDart : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
            SpellFXOverrideSkins = new[] { "AstronautTeemo", },
        };
        float[] effect0 = { 1.5f, 1.75f, 2, 2.25f, 2.5f };
        int[] effect1 = { 80, 125, 170, 215, 260 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            AddBuff(attacker, target, new Buffs.BlindingDart(), 100, 1, effect0[level - 1], BuffAddType.STACKS_AND_OVERLAPS, BuffType.BLIND, 0, true, false);
            ApplyDamage(attacker, target, effect1[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.8f, 1, false, false, attacker);
        }
    }
}
namespace Buffs
{
    public class BlindingDart : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head", },
            AutoBuffActivateEffect = new[] { "Global_miss.troy", },
            BuffName = "Blind",
            BuffTextureName = "Teemo_TranquilizingShot.dds",
            PopupMessage = new[] { "game_floatingtext_Blinded", },
        };
        public override void OnActivate()
        {
            IncFlatMissChanceMod(owner, 1);
        }
        public override void OnUpdateStats()
        {
            IncFlatMissChanceMod(owner, 1);
        }
    }
}