namespace Spells
{
    public class GarenRPreCast: GarenJusticePreCast {}
    public class GarenJusticePreCast : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.GarenBladestorm)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.GarenBladestorm), owner, 0);
            }
            AddBuff(owner, owner, new Buffs.GarenJusticePreCast(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellCast(owner, target, default, default, 1, SpellSlotType.ExtraSlots, level, false, false, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class GarenJusticePreCast : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "root", },
            AutoBuffActivateEffect = new[] { "dr_mundo_burning_agony_cas_02.troy", },
        };
        EffectEmitter kIRHand;
        public override void OnActivate()
        {
            SpellEffectCreate(out kIRHand, out _, "garen_damacianJustice_cas_instant.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false, false);
            SpellEffectCreate(out kIRHand, out _, "garen_damacianJustice_cas_sword.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_hand", default, target, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(kIRHand);
        }
    }
}