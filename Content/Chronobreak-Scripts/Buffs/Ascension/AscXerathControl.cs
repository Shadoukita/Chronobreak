namespace Buffs
{
    internal class AscXerathControl : BuffScript
    {
        public override BuffScriptMetaData BuffMetaData { get; } = new()
        {
            BuffType = BuffType.COMBAT_ENCHANCER,
            BuffAddType = BuffAddType.REPLACE_EXISTING,
        };

        public override void OnActivate()
        {
            //TODO: Impelemt in Csharp system
            //OverrideAnimation("IDLE1", "IDLE1OVERRIDE", Target);
            int avgLevel = GetPlayerAverageLevel();
            if (target is Minion xerath && xerath.MinionLevel < avgLevel)
            {
                xerath.LevelUp(avgLevel - xerath.MinionLevel);
            }
        }
    }
}