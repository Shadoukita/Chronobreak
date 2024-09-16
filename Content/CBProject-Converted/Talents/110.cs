namespace Talents
{
    public class Talent_110 : TalentScript
    {
        public override void OnUpdateStats()
        {
            float magicResistanceMod = 2 * talentLevel;
            IncFlatSpellBlockMod(owner, magicResistanceMod);
        }
    }
}