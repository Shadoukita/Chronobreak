namespace Spells
{
    public class SiphoningStrike : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
    }
}
namespace Buffs
{
    public class SiphoningStrike : BuffScript
    {
        int[] effect0 = { 3, 3, 3, 3, 3 };
        public override void OnActivate()
        {
            //RequireVar(this.damageBonus);
        }
        public override void OnUpdateActions()
        {
            SpellBuffRemoveCurrent(owner);
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            SpellEffectCreate(out _, out _, "DeathsCaress_nova.prt", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            int level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (!IsDead(attacker))
            {
                int nextBuffVars_DamageBonus = effect0[level - 1];
                AddBuff(attacker, attacker, new Buffs.SiphoningStrikeDamageBonus(nextBuffVars_DamageBonus), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}