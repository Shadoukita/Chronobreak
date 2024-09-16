namespace Buffs
{
    public class OdinGuardianStatsByLevel : BuffScript
    {
        float dmgPerLevel;
        float maxPlayerLevel;
        float lastTimeExecuted;
        public OdinGuardianStatsByLevel(float dmgPerLevel = default)
        {
            this.dmgPerLevel = dmgPerLevel;
        }
        public override void OnActivate()
        {
            //RequireVar(this.hPPerLevel);
            //RequireVar(this.dmgPerLevel);
            //RequireVar(this.armorPerLevel);
            //RequireVar(this.mR_per_level);
            maxPlayerLevel = 0;
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 9999, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
            {
                int playerLevel = GetLevel(unit);
                if (playerLevel > maxPlayerLevel)
                {
                    maxPlayerLevel = playerLevel;
                }
            }
        }
        public override void OnUpdateStats()
        {
            if (ExecutePeriodically(10, ref lastTimeExecuted, false))
            {
                float myHealth = GetHealthPercent(target, PrimaryAbilityResourceType.MANA);
                if (myHealth >= 0.99f)
                {
                    foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 9999, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                    {
                        int playerLevel = GetLevel(unit);
                        if (playerLevel > maxPlayerLevel)
                        {
                            maxPlayerLevel = playerLevel;
                        }
                    }
                }
            }
            float dmgIncrease = dmgPerLevel * maxPlayerLevel;
            IncFlatPhysicalDamageMod(owner, dmgIncrease);
        }
    }
}