namespace Spells
{
    public class JudicatorReckoning : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = { -0.35f, -0.35f, -0.35f, -0.35f, -0.35f };
        int[] effect1 = { 60, 110, 160, 210, 260 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float nextBuffVars_MoveSpeedMod = effect0[level - 1];
            float aP = GetFlatMagicDamageMod(owner);
            float baseAD = GetBaseAttackDamage(owner);
            float totalAD = GetTotalAttackDamage(owner);
            float bonusAD = totalAD - baseAD;
            bonusAD *= 1;
            aP *= 1;
            float finalDamage = aP + bonusAD;
            ApplyDamage(attacker, target, finalDamage + effect1[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, false, attacker);
            AddBuff(attacker, target, new Buffs.JudicatorReckoning(nextBuffVars_MoveSpeedMod), 100, 1, 4, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class JudicatorReckoning : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head", },
            AutoBuffActivateEffect = new[] { "Global_Slow.troy", },
            BuffName = "JudicatorReckoning",
            BuffTextureName = "Judicator_Reckoning.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        float moveSpeedMod;
        public JudicatorReckoning(float moveSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, moveSpeedMod);
        }
    }
}