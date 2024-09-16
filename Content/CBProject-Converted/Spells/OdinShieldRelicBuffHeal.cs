namespace Buffs
{
    public class OdinShieldRelicBuffHeal : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinShieldRelic",
            BuffTextureName = "JarvanIV_GoldenAegis.dds",
        };
        int[] effect0 = { 90, 99, 108, 117, 126, 135, 144, 153, 162, 171, 180, 189, 198, 207, 216, 225, 234, 243 };
        public override void OnActivate()
        {
            int level = GetLevel(owner);
            float healAmount = effect0[level - 1];
            float manaAmount = healAmount * 0.6f;
            IncPAR(owner, manaAmount, PrimaryAbilityResourceType.MANA);
            IncPAR(owner, 20, PrimaryAbilityResourceType.Energy);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinPlayerBuff)) > 0)
            {
                healAmount *= 1.25f;
            }
            IncHealth(owner, healAmount, owner);
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "Odin_HealthPackHeal.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out _, out _, "Summoner_Mana.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, owner, default, default, false, false, false, false, false);
            SpellBuffClear(owner, nameof(Buffs.OdinShieldRelicBuffHeal));
        }
    }
}