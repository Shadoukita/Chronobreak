namespace Buffs
{
    public class UrAniumRounds : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "UrAniumRounds",
            BuffTextureName = "Heimerdinger_Level2Turret.dds",
        };
        public override void OnSpellHit(AttackableUnit target)
        {
            if (target is ObjAIBase && target is not BaseTurret)
            {
                TeamId teamID = GetTeamID_CS(owner);
                ObjAIBase attacker = GetChampionBySkinName("Heimerdinger", teamID);
                AddBuff(attacker, target, new Buffs.UrAniumRoundsHit(), 25, 1, 3, BuffAddType.STACKS_AND_RENEWS, BuffType.SHRED, 0, true, false, false);
                if (GetBuffCountFromCaster(owner, attacker, nameof(Buffs.UpgradeSlow)) > 0)
                {
                    SpellEffectCreate(out _, out _, "AbsoluteZero_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
                }
            }
        }
    }
}