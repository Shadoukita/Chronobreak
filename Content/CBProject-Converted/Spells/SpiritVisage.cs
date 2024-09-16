namespace Buffs
{
    public class SpiritVisage : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Spirit Visage",
            BuffTextureName = "3065_Spirit_Visage.dds",
        };
        float lifeStealMod;
        float spellVampMod;
        public override void OnActivate()
        {
            float lifeStealMod = GetPercentLifeStealMod(owner);
            this.lifeStealMod = lifeStealMod * 0.15f;
            float spellVampMod = GetPercentSpellBlockMod(owner);
            this.spellVampMod = spellVampMod * 0.15f;
        }
        public override void OnUpdateStats()
        {
            IncPercentCooldownMod(owner, -0.1f);
            IncPercentHPRegenMod(owner, 0.15f);
            IncPercentLifeStealMod(owner, lifeStealMod);
            IncPercentSpellVampMod(owner, spellVampMod);
        }
        public override void OnUpdateActions()
        {
            float lifeStealMod = GetPercentLifeStealMod(owner);
            lifeStealMod -= this.lifeStealMod;
            this.lifeStealMod = lifeStealMod * 0.15f;
            float spellVampMod = GetPercentSpellVampMod(owner);
            spellVampMod -= this.spellVampMod;
            this.spellVampMod = spellVampMod * 0.15f;
        }
        public override float OnHeal(float health)
        {
            float returnValue = 0;
            if (health >= 0 && owner == target)
            {
                float effectiveHeal = health * 1.15f;
                returnValue = effectiveHeal;
            }
            return returnValue;
        }
    }
}