namespace Talents
{
    public class Talent_312 : TalentScript
    {
        public override void OnUpdateStats()
        {
            float magicResistanceMod = 2 * talentLevel;
            IncFlatSpellBlockMod(owner, magicResistanceMod);
        }
    }
}