namespace Spells
{
    public class Meditate : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 50f, 50f, 50f, 50f, 50f, },
            ChannelDuration = 5f,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { 11.7f, 23.3f, 35, 46.7f, 58.3f };
        float[] effect1 = { 25, 50, 83.3f, 125, 183.3f };
        public override void ChannelingStart()
        {
            float healthTick = effect0[level - 1];
            float abilityPower = GetFlatMagicDamageMod(owner);
            abilityPower *= 0.33f;
            healthTick += abilityPower;
            float nextBuffVars_HealthTick = healthTick;
            AddBuff(owner, owner, new Buffs.Meditate(nextBuffVars_HealthTick), 1, 1, 4.9f, BuffAddType.RENEW_EXISTING, BuffType.HEAL, 0, true, false);
        }
        public override void ChannelingSuccessStop()
        {
            IncHealth(owner, effect1[level - 1], owner);
            SpellBuffRemove(owner, nameof(Buffs.Meditate), owner);
        }
        public override void ChannelingCancelStop()
        {
            SpellBuffRemove(owner, nameof(Buffs.Meditate), owner);
        }
    }
}
namespace Buffs
{
    public class Meditate : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Meditate",
            BuffTextureName = "MasterYi_Vanish.dds",
        };
        float healthTick;
        float lastTimeExecuted;
        int[] effect0 = { 100, 150, 200, 250, 300 };
        public Meditate(float healthTick = default)
        {
            this.healthTick = healthTick;
        }
        public override void OnActivate()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots); // UNUSED
            //RequireVar(this.healthTick);
            IncHealth(owner, healthTick, owner);
            SpellEffectCreate(out _, out _, "Meditate_eff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }
        public override void OnUpdateStats()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            IncFlatArmorMod(owner, effect0[level - 1]);
            IncFlatSpellBlockMod(owner, effect0[level - 1]);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, false))
            {
                IncHealth(owner, healthTick, owner);
                SpellEffectCreate(out _, out _, "Meditate_eff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            }
        }
    }
}