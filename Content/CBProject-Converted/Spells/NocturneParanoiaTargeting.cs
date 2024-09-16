namespace Spells
{
    public class NocturneParanoiaTargeting : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class NocturneParanoiaTargeting : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            AutoBuffActivateEvent = "DeathsCaress_buf.prt",
            BuffName = "NocturneParanoiaTarget",
            BuffTextureName = "Nocturne_Paranoia.dds",
        };
        bool partCreated;
        int range;
        EffectEmitter tpar;
        int[] effect0 = { 2000, 2750, 3500 };
        public override void OnActivate()
        {
            partCreated = false;
            int level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            range = effect0[level - 1];
            float distance = DistanceBetweenObjects(owner, attacker);
            if (distance <= range)
            {
                SpellEffectCreate(out tpar, out _, "NocturneParanoiaTargeting.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, true, owner, default, default, owner, default, default, false, default, default, false);
                partCreated = true;
            }
        }
        public override void OnDeactivate(bool expired)
        {
            if (partCreated)
            {
                SpellEffectRemove(tpar);
            }
        }
        public override void OnUpdateActions()
        {
            float distance = DistanceBetweenObjects(owner, attacker);
            if (partCreated)
            {
                if (distance > range)
                {
                    SpellEffectRemove(tpar);
                    partCreated = false;
                }
            }
            else
            {
                if (distance <= range)
                {
                    SpellEffectCreate(out tpar, out _, "NocturneParanoiaTargeting.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, true, owner, default, default, owner, default, default, false, default, default, false);
                    partCreated = true;
                }
            }
        }
    }
}