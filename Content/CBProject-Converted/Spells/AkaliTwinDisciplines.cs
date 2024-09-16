namespace Buffs
{
    public class AkaliTwinDisciplines : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "AkaliTwinDisciplines",
            BuffTextureName = "33.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float akaliAP;
        float akaliDmg;
        float bonusDmgPerc;
        float addBonusDmgPerc;
        float dmgMult;
        float baseVampPercent;
        float additionalVampPercent;
        float vampPercentTooltip;
        public override void OnActivate()
        {
            akaliAP = GetFlatMagicDamageMod(owner);
            akaliDmg = GetFlatPhysicalDamageMod(owner);
        }
        public override void OnUpdateStats()
        {
            float nextBuffVars_AkaliAP;
            if (akaliAP >= 19.5f)
            {
                nextBuffVars_AkaliAP = akaliAP;
                bonusDmgPerc = 0.08f;
                akaliAP -= 20;
                addBonusDmgPerc = akaliAP / 600;
                dmgMult = bonusDmgPerc + addBonusDmgPerc;
                float dmgMultTooltip = 100 * dmgMult;
                SetBuffToolTipVar(1, dmgMultTooltip);
                AddBuff((ObjAIBase)owner, owner, new Buffs.AkaliTwinAP(nextBuffVars_AkaliAP), 1, 1, 1.1f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            }
            else
            {
                SetBuffToolTipVar(1, 0);
            }
            if (akaliDmg >= 9.5f)
            {
                float nextBuffVars_AkaliDmg = akaliDmg;
                baseVampPercent = 0.08f;
                akaliDmg -= 10;
                additionalVampPercent = akaliDmg / 600;
                charVars.VampPercent = baseVampPercent + additionalVampPercent;
                vampPercentTooltip = 100 * charVars.VampPercent;
                SetBuffToolTipVar(2, vampPercentTooltip);
                AddBuff((ObjAIBase)owner, owner, new Buffs.AkaliTwinDmg(nextBuffVars_AkaliDmg), 1, 1, 1.1f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            }
            else
            {
                SetBuffToolTipVar(2, 0);
            }
        }
        public override void OnUpdateActions()
        {
            akaliAP = GetFlatMagicDamageMod(owner);
            akaliDmg = GetFlatPhysicalDamageMod(owner);
        }
    }
}