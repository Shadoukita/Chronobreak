namespace Buffs
{
    public class RumbleHeatSystem : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "RumbleHeatSystem",
            BuffTextureName = "Rumble_Junkyard Titan1.dds",
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        int[] effect0 = { 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105, 110 };
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, false))
            {
                if (true)
                {
                    int level = GetLevel(owner);
                    float punchdmg = effect0[level - 1];
                    SetBuffToolTipVar(1, punchdmg);
                    float aP = GetFlatMagicDamageMod(owner);
                    aP *= 0.3f;
                    SetBuffToolTipVar(2, aP);
                    float baseCDR = 10;
                    float cDRMod = GetPercentCooldownMod(owner);
                    cDRMod *= -1;
                    cDRMod = 1 - cDRMod;
                    baseCDR *= cDRMod;
                    SetSpellToolTipVar(baseCDR, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
                }
                float heatDecay = -5;
                float bonusHeatDecay = -5;
                float currentHeat = GetPAR(owner, PrimaryAbilityResourceType.Other);
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RumbleHeatingUp)) == 0)
                {
                    IncPAR(owner, heatDecay, PrimaryAbilityResourceType.Other);
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RumbleHeatingUp2)) == 0)
                    {
                        IncPAR(owner, bonusHeatDecay, PrimaryAbilityResourceType.Other);
                    }
                }
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RumbleOverheat)) == 0)
                {
                    if (currentHeat >= charVars.DangerZone)
                    {
                        if (currentHeat >= 100)
                        {
                            AddBuff(attacker, owner, new Buffs.RumbleOverheat(), 1, 1, 5.25f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                            Say(owner, "Overheat!");
                        }
                        else
                        {
                            AddBuff(attacker, owner, new Buffs.RumbleDangerZone(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                            SetPARColorOverride(owner, 255, 255, 0, 255, 175, 175, 0, 255);
                            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RumbleOverheat)) > 0)
                            {
                                SetPARColorOverride(owner, 255, 225, 0, 255, 175, 0, 0, 255);
                            }
                        }
                    }
                    else
                    {
                        SetPARColorOverride(owner, 255, 255, 255, 255, 175, 175, 0, 255);
                        SpellBuffRemove(owner, nameof(Buffs.RumbleOverheat), (ObjAIBase)owner);
                        SpellBuffRemove(owner, nameof(Buffs.RumbleDangerZone), (ObjAIBase)owner);
                    }
                }
            }
        }
    }
}