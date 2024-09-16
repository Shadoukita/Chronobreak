namespace Buffs
{
    public class GrievousWound : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "Head", },
            AutoBuffActivateEffect = new[] { "Global_Mortal_Strike.troy", },
            BuffName = "GrievousWound",
            BuffTextureName = "GW_Debuff.dds",
        };
        float lifeStealMod;
        float spellVampMod;
        public override void OnActivate()
        {
            ApplyAssistMarker(attacker, owner, 10);
            float lifeStealMod = GetPercentLifeStealMod(owner);
            this.lifeStealMod = lifeStealMod * -0.5f;
            float spellVampMod = GetPercentSpellBlockMod(owner);
            this.spellVampMod = spellVampMod * -0.5f;
        }
        public override void OnUpdateStats()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Internal_50MS)) > 0)
            {
                IncPercentHPRegenMod(owner, -0.5f);
                IncPercentLifeStealMod(owner, lifeStealMod);
                IncPercentSpellVampMod(owner, spellVampMod);
            }
        }
        public override float OnHeal(float health)
        {
            float returnValue = 0;
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Internal_50MS)) > 0 && health >= 0)
            {
                float effectiveHeal = health * 0.5f;
                returnValue = effectiveHeal;
            }
            return returnValue;
        }
        public override void OnUpdateActions()
        {
            float lifeStealMod = GetPercentLifeStealMod(owner);
            lifeStealMod -= this.lifeStealMod;
            this.lifeStealMod = lifeStealMod * -0.5f;
            float spellVampMod = GetPercentSpellBlockMod(owner);
            spellVampMod -= this.spellVampMod;
            this.spellVampMod = spellVampMod * -0.5f;
        }
    }
}