namespace Spells
{
    public class CaitlynYordleTrapSight : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class CaitlynYordleTrapSight : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head", },
            AutoBuffActivateEffect = new[] { "global_Watched.troy", "", },
            BuffName = "CaitlynYordleTrapSight",
            BuffTextureName = "Caitlyn_YordleSnapTrap.dds",
        };
        float damagePerTick;
        float dOTCounter;
        Region bubbleID;
        Region bubbleID2;
        float lastTimeExecuted;
        int[] effect0 = { 80, 130, 180, 230, 280 };
        public override void OnActivate()
        {
            int level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float baseDamage = effect0[level - 1];
            float aP = GetFlatMagicDamageMod(attacker);
            float aPBonus = aP * 0.6f;
            float totalDamage = baseDamage + aPBonus;
            damagePerTick = totalDamage / 3;
            dOTCounter = 0;
            TeamId team = GetTeamID_CS(attacker);
            bubbleID = AddUnitPerceptionBubble(team, 100, owner, 20, default, default, false);
            bubbleID2 = AddUnitPerceptionBubble(team, 100, owner, 20, default, default, true);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(bubbleID);
            RemovePerceptionBubble(bubbleID2);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, true))
            {
                if (dOTCounter < 1.5f)
                {
                    dOTCounter += 0.5f;
                    ApplyDamage(attacker, owner, damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0, 1, false, false, attacker);
                }
            }
        }
    }
}