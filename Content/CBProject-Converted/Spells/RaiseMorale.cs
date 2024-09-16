namespace Spells
{
    public class RaiseMorale : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = { 0.08f, 0.11f, 0.14f, 0.17f, 0.2f };
        int[] effect1 = { 12, 19, 26, 33, 40 };
        float[] effect2 = { 0.04f, 0.055f, 0.07f, 0.085f, 0.1f };
        float[] effect3 = { 6, 9.5f, 13, 16.5f, 20 };
        public override void SelfExecute()
        {
            SpellBuffRemove(owner, nameof(Buffs.RaiseMorale), owner, 0);
            float nextBuffVars_MoveSpeedMod = effect0[level - 1];
            float nextBuffVars_AttackDmgMod = effect1[level - 1];
            AddBuff(attacker, attacker, new Buffs.RaiseMoraleTeamBuff(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackDmgMod), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            nextBuffVars_MoveSpeedMod = effect2[level - 1];
            nextBuffVars_AttackDmgMod = effect3[level - 1];
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1500, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, default, true))
            {
                AddBuff(attacker, unit, new Buffs.RaiseMoraleTeamBuff(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackDmgMod), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class RaiseMorale : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "RaiseMorale",
            BuffTextureName = "Pirate_RaiseMorale.dds",
            IsPetDurationBuff = true,
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float[] effect0 = { 0.03f, 0.04f, 0.05f, 0.06f, 0.07f };
        int[] effect1 = { 8, 10, 12, 14, 16 };
        public override void OnUpdateStats()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            IncPercentMovementSpeedMod(owner, effect0[level - 1]);
            IncFlatPhysicalDamageMod(owner, effect1[level - 1]);
        }
    }
}