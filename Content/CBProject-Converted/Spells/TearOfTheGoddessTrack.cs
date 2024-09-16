namespace Buffs
{
    public class TearOfTheGoddessTrack : BuffScript
    {
        float cooldownResevoir;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            cooldownResevoir = 0;
        }
        public override void OnUpdateStats()
        {
            IncFlatPARPoolMod(owner, charVars.TearBonusMana, PrimaryAbilityResourceType.MANA);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(3, ref lastTimeExecuted, true))
            {
                if (cooldownResevoir < 2)
                {
                    cooldownResevoir++;
                }
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            if (!spellVars.DoesntTriggerSpellCasts && cooldownResevoir > 0)
            {
                SpellEffectCreate(out _, out _, "TearoftheGoddess_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
                charVars.TearBonusMana += 4;
                charVars.TearBonusMana = Math.Min(charVars.TearBonusMana, 1000);
                cooldownResevoir -= 1;
            }
        }
    }
}