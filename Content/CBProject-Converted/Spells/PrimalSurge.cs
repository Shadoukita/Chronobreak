namespace Spells
{
    public class PrimalSurge : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { 0.2f, 0.3f, 0.4f, 0.5f, 0.6f };
        int[] effect1 = { 50, 85, 120, 155, 190 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_ASMod = effect0[level - 1];
            float tempAbilityPower = GetFlatMagicDamageMod(owner);
            float healthToRestore = effect1[level - 1];
            float healingBonus = tempAbilityPower * 0.7f;
            healthToRestore += healingBonus;
            AddBuff(attacker, target, new Buffs.PrimalSurge(nextBuffVars_ASMod), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            IncHealth(target, healthToRestore, owner);
        }
    }
}
namespace Buffs
{
    public class PrimalSurge : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { null, "pelvis", "L_hand", "R_hand", },
            AutoBuffActivateEffect = new[] { "nidalee_primalSurge_tar.troy", "nidalee_primalSurge_tar_flash.troy", "nidalee_primal_surge_attack_buf.troy", "nidalee_primal_surge_attack_buf.troy", },
            BuffName = "PrimalSurge",
            BuffTextureName = "Nidalee_PrimalSurge.dds",
        };
        float aSMod;
        public PrimalSurge(float aSMod = default)
        {
            this.aSMod = aSMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.aSMod);
            IncPercentAttackSpeedMod(owner, aSMod);
            float tooltip = aSMod * 100;
            SetBuffToolTipVar(1, tooltip);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, aSMod);
        }
    }
}