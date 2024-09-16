namespace Spells
{
    public class BlindMonkETwoMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        float[] effect0 = { -0.3f, -0.375f, -0.45f, -0.525f, -0.6f };
        int[] effect1 = { 4, 4, 4, 4, 4 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float nextBuffVars_PercentReduction = effect0[level - 1];
            AddBuff(attacker, target, new Buffs.BlindMonkETwoMissile(nextBuffVars_PercentReduction), 1, 1, effect1[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class BlindMonkETwoMissile : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "blindMonk_E_tempestFist_cripple.troy", "blindMonk_E_tempestFist_cripple_02.troy", },
            BuffName = "BlindMonkCripple",
            BuffTextureName = "BlindMonkETwo.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        float percentReduction;
        float initialPercentReduction;
        int count;
        float lastTimeExecuted;
        public BlindMonkETwoMissile(float percentReduction = default)
        {
            this.percentReduction = percentReduction;
        }
        public override void OnActivate()
        {
            //RequireVar(this.percentReduction);
            initialPercentReduction = percentReduction;
            ApplyAssistMarker(attacker, owner, 10);
            count = 0;
        }
        public override void OnUpdateStats()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                if (count == 0)
                {
                    percentReduction = initialPercentReduction * 0.75f;
                    count = 1;
                }
                else if (count == 1)
                {
                    percentReduction = initialPercentReduction * 0.5f;
                    count = 2;
                }
                else
                {
                    percentReduction = initialPercentReduction * 0.25f;
                }
            }
            IncPercentMultiplicativeMovementSpeedMod(owner, percentReduction);
            IncPercentMultiplicativeAttackSpeedMod(owner, percentReduction);
        }
    }
}