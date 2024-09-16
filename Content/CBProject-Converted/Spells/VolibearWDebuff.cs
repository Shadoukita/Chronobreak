namespace Spells
{
    public class VolibearWDebuff : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
        };
    }
}
namespace Buffs
{
    public class VolibearWDebuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "VayneSilverDebuff",
            BuffTextureName = "Vayne_SilveredBolts.dds",
        };
        bool ready;
        bool critical; // UNUSED
        EffectEmitter particle1;
        public override void OnActivate()
        {
            ready = false;
            critical = false;
            float cD = GetSlotSpellCooldownTime(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (cD <= 0)
            {
                int count = GetBuffCountFromAll(attacker, nameof(Buffs.VolibearWStats));
                if (count == 4)
                {
                    ready = true;
                    SpellEffectCreate(out particle1, out _, "Volibear_tar_indicator.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, true, owner, default, default, target, default, default, false, false, false, false, false);
                    float healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA); // UNUSED
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            if (ready)
            {
                SpellEffectRemove(particle1);
            }
        }
        public override void OnUpdateActions()
        {
            bool readyNew = false;
            bool criticalNew = false; // UNUSED
            float cD = GetSlotSpellCooldownTime(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (cD <= 0)
            {
                int count = GetBuffCountFromAll(attacker, nameof(Buffs.VolibearWStats));
                if (count == 4)
                {
                    float distance = DistanceBetweenObjects(attacker, owner);
                    if (distance <= 350)
                    {
                        readyNew = true;
                    }
                }
            }
            if (readyNew && !ready)
            {
                ready = true;
                SpellEffectCreate(out particle1, out _, "Volibear_tar_indicator.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, true, owner, default, default, target, default, default, false, false, false, false, false);
            }
            if (!readyNew && ready)
            {
                ready = false;
                SpellEffectRemove(particle1);
            }
        }
    }
}