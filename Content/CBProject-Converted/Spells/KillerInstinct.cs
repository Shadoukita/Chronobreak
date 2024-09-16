namespace Spells
{
    public class KillerInstinct : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            AddBuff(owner, owner, new Buffs.KillerInstinct(), 1, 1, 15, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class KillerInstinct : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "KillerInstinct",
            BuffTextureName = "Katarina_KillerInstincts.dds",
        };
        EffectEmitter kIRHand;
        EffectEmitter kILHand;
        public override void OnActivate()
        {
            SpellEffectCreate(out kIRHand, out _, "katarina_killerInstinct_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_hand", default, target, default, default, false, default, default, false, false);
            SpellEffectCreate(out kILHand, out _, "katarina_killerInstinct_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_hand", default, target, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(kILHand);
            SpellEffectRemove(kIRHand);
        }
    }
}