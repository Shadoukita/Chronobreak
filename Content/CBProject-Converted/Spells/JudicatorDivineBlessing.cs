namespace Spells
{
    public class JudicatorDivineBlessing : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = { 0.15f, 0.17f, 0.19f, 0.21f, 0.23f };
        int[] effect1 = { 45, 85, 125, 165, 205 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = base.level;
            float nextBuffVars_MoveSpeedMod = effect0[level - 1];
            AddBuff(attacker, target, new Buffs.JudicatorDivineBlessing(nextBuffVars_MoveSpeedMod), 1, 1, 2.5f, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float abilityPower = GetFlatMagicDamageMod(owner);
            abilityPower *= 0.35f;
            float healLevel = effect1[level - 1];
            float healAmount = healLevel + abilityPower;
            IncHealth(target, healAmount, owner);
            ApplyAssistMarker(attacker, target, 10);
            AddBuff(owner, owner, new Buffs.KayleDivineBlessingAnim(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class JudicatorDivineBlessing : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { null, "root", },
            AutoBuffActivateEffect = new[] { "InterventionHeal_buf.troy", "Interventionspeed_buf.troy", },
            BuffName = "JudicatorDivineBlessing",
            BuffTextureName = "Judicator_AngelicEmbrace.dds",
        };
        float moveSpeedMod;
        public JudicatorDivineBlessing(float moveSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, moveSpeedMod);
        }
    }
}