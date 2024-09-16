namespace Spells
{
    public class MissFortuneScattershot : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            TeamId teamOfOwner = GetTeamID_CS(owner);
            Vector3 targetPos = GetSpellTargetPos(spell);
            Region bubbleID = AddPosPerceptionBubble(teamOfOwner, 200, targetPos, 2.6f, default, false); // UNUSED
            Minion other3 = SpawnMinion("SpellBook1", "SpellBook1", "idle.lua", targetPos, teamOfOwner, true, true, true, true, true, true, 0, default, true, (Champion)owner);
            AddBuff(attacker, other3, new Buffs.MissFortuneScattershot(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(attacker, other3, new Buffs.MissFortuneScatterParticle(), 1, 1, 2.75f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class MissFortuneScattershot : BuffScript
    {
        int[] effect0 = { 90, 145, 200, 255, 310 };
        public override void OnDeactivate(bool expired)
        {
            int level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float nextBuffVars_Damage = effect0[level - 1];
            AddBuff(attacker, owner, new Buffs.MissFortuneScatterAoE(nextBuffVars_Damage), 1, 1, 1.75f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
    }
}