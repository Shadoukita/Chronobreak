namespace Buffs
{
    public class TurretBonusHQ : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Magical Sight",
            BuffTextureName = "096_Eye_of_the_Observer.dds",
        };
        float maxIncreases;
        float damageMod;
        float armorMod;
        float resistMod;
        float loopOffset;
        float startDecay;
        float maximumArmor;
        float maximumResist;
        float maximumDamage;
        Region thisBubble;
        float bonusHealth;
        float looper;
        float lastTimeExecuted;
        public TurretBonusHQ(float maxIncreases = default, float loopOffset = default, float startDecay = default)
        {
            this.maxIncreases = maxIncreases;
            this.loopOffset = loopOffset;
            this.startDecay = startDecay;
        }
        public override void OnActivate()
        {
            int numChampions;
            //RequireVar(this.maxIncreases);
            //RequireVar(this.damageMod);
            //RequireVar(this.armorMod);
            //RequireVar(this.resistMod);
            //RequireVar(this.loopOffset);
            //RequireVar(this.startDecay);
            maximumArmor = 2.5f * maxIncreases;
            maximumResist = 2.5f * maxIncreases;
            maximumDamage = 7 * maxIncreases;
            TeamId teamID = GetTeamID_CS(owner);
            thisBubble = AddUnitPerceptionBubble(teamID, 800, owner, 25000, default, default, true);
            numChampions = GetNumberOfHeroesOnTeam(GetEnemyTeam(teamID), true, true);
            numChampions = Math.Min(5, numChampions);
            bonusHealth = numChampions * 150;
            looper = 0;
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(thisBubble);
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, damageMod);
            IncFlatArmorMod(owner, armorMod);
            IncFlatSpellBlockMod(owner, resistMod);
            if (ExecutePeriodically(60, ref lastTimeExecuted, false))
            {
                if (looper >= startDecay)
                {
                    armorMod -= 5;
                    resistMod -= 5;
                }
                else if (looper >= loopOffset)
                {
                    armorMod += 2.5f;
                    resistMod += 2.5f;
                    damageMod += 7;
                    damageMod = Math.Min(damageMod, maximumDamage);
                    armorMod = Math.Min(armorMod, maximumArmor);
                    resistMod = Math.Min(resistMod, maximumResist);
                    looper++;
                }
                else
                {
                    looper++;
                }
            }
            IncFlatHPPoolMod(owner, bonusHealth);
        }
    }
}