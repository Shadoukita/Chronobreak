namespace Spells
{
    public class FizzTrickSlam : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
    }
}
namespace Buffs
{
    public class FizzTrickSlam : BuffScript
    {
        int[] effect0 = { 70, 120, 170, 220, 270 };
        float[] effect1 = { -0.4f, -0.45f, -0.5f, -0.55f, -0.6f };
        public override void OnActivate()
        {
            PlayAnimation("Spell3c", 0, owner, false, false, false);
            IncPercentMovementSpeedMod(owner, 0.5f);
            SetCanMove(owner, true);
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "fizz_playfultrickster_flip_sound.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", owner.Position3D, owner, default, default, true, true, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            float nextBuffVars_MoveSpeedMod;
            int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "Fizz_TrickSlam.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 375, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                ApplyDamage((ObjAIBase)owner, unit, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.75f, 1, false, false, attacker);
                SpellEffectCreate(out _, out _, "Fizz_TrickSlam_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                nextBuffVars_MoveSpeedMod = effect1[level - 1];
                AddBuff((ObjAIBase)owner, unit, new Buffs.FizzWSlow(nextBuffVars_MoveSpeedMod), 1, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
            }
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            AddBuff((ObjAIBase)owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, 0.5f);
        }
    }
}