namespace Buffs
{
    public class AbyssalScepterAura : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Abyssal Scepter",
            BuffTextureName = "3001_Abyssal_Scepter.dds",
        };
        float magicResistanceMod;
        public AbyssalScepterAura(float magicResistanceMod = default)
        {
            this.magicResistanceMod = magicResistanceMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.magicResistanceMod);
        }
        public override void OnUpdateStats()
        {
            IncFlatSpellBlockMod(owner, magicResistanceMod);
            if (IsDead(attacker))
            {
                SpellBuffRemoveCurrent(owner);
            }
            else
            {
                float dist = DistanceBetweenObjects(attacker, owner);
                if (dist >= 1000)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}