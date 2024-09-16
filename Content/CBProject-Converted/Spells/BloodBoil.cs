namespace Spells
{
    public class BloodBoil : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = { 0.25f, 0.35f, 0.45f, 0.55f, 0.65f };
        float[] effect1 = { 0.11f, 0.12f, 0.13f, 0.14f, 0.15f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_AttackSpeedBonusPercent = effect0[level - 1];
            float nextBuffVars_MovementSpeedBonusPercent = effect1[level - 1];
            AddBuff(attacker, target, new Buffs.BloodBoil(nextBuffVars_AttackSpeedBonusPercent, nextBuffVars_MovementSpeedBonusPercent), 1, 1, 15, BuffAddType.REPLACE_EXISTING, BuffType.HASTE, 0, true);
            AddBuff(attacker, attacker, new Buffs.BloodBoil(nextBuffVars_AttackSpeedBonusPercent, nextBuffVars_MovementSpeedBonusPercent), 1, 1, 15, BuffAddType.REPLACE_EXISTING, BuffType.HASTE, 0, true);
        }
    }
}
namespace Buffs
{
    public class BloodBoil : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "l_hand", "r_hand", },
            AutoBuffActivateEffect = new[] { "bloodboil_buf.troy", "bloodboil_buf.troy", "", },
            BuffName = "Blood Boil",
            BuffTextureName = "Yeti_YetiSmash.dds",
        };
        float attackSpeedBonusPercent;
        float movementSpeedBonusPercent;
        public BloodBoil(float attackSpeedBonusPercent = default, float movementSpeedBonusPercent = default)
        {
            this.attackSpeedBonusPercent = attackSpeedBonusPercent;
            this.movementSpeedBonusPercent = movementSpeedBonusPercent;
        }
        public override void OnActivate()
        {
            //RequireVar(this.attackSpeedBonusPercent);
            ApplyAssistMarker(attacker, owner, 10);
            //RequireVar(this.movementSpeedBonusPercent);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, attackSpeedBonusPercent);
            IncPercentMovementSpeedMod(owner, movementSpeedBonusPercent);
        }
    }
}