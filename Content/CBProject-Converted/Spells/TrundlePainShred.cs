namespace Buffs
{
    public class TrundlePainShred : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "TrundlePainShred",
            BuffTextureName = "Trundle_Agony.dds",
        };
        float survivability;
        float ownerArmor;
        float ownerMR;
        float lowerArmor;
        float lowerMR;
        float instancedArmor;
        float instancedMR;
        float lastTimeExecuted;
        public TrundlePainShred(float survivability = default)
        {
            this.survivability = survivability;
        }
        public override void OnActivate()
        {
            //RequireVar(this.survivability);
            ownerArmor = GetArmor(owner);
            ownerMR = GetSpellBlock(owner);
            lowerArmor = survivability * ownerArmor;
            lowerMR = survivability * ownerMR;
            survivability /= 6;
            instancedArmor = lowerArmor * -1;
            instancedMR = lowerMR * -1;
            if (instancedArmor < 0)
            {
                IncFlatArmorMod(owner, instancedArmor);
            }
            IncFlatArmorMod(owner, instancedArmor);
            if (instancedMR < 0)
            {
                IncFlatSpellBlockMod(owner, instancedMR);
            }
            IncFlatSpellBlockMod(owner, instancedMR);
            AddBuff(attacker, attacker, new Buffs.TrundlePainBuff(), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
        public override void OnUpdateStats()
        {
            if (instancedArmor < 0)
            {
                IncFlatArmorMod(owner, instancedArmor);
            }
            if (instancedMR < 0)
            {
                IncFlatSpellBlockMod(owner, instancedMR);
            }
            float trundleArmor = instancedArmor * -1;
            float trundleMR = instancedMR * -1;
            if (trundleArmor > 0)
            {
                IncFlatArmorMod(attacker, trundleArmor);
            }
            if (trundleMR > 0)
            {
                IncFlatSpellBlockMod(attacker, trundleMR);
            }
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, true))
            {
                float lowerArmorLess = survivability * ownerArmor;
                float lowerMRLess = survivability * ownerMR;
                instancedArmor -= lowerArmorLess;
                instancedMR -= lowerMRLess;
            }
        }
    }
}