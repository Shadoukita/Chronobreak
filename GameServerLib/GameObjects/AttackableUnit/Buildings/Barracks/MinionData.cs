public class MinionData
{
    public string Name { get; set; }
    public string CoreName { get; set; }
    public int NumToSpawnForWave { get; set; }
    public int BonusHealth { get; set; }
    public int BonusAttack { get; set; }
    public float ExpGiven { get; set; }
    public float GoldGiven { get; set; }
    public float LocalGoldGiven { get; set; }
    public float Armor { get; set; }
    public float MagicResistance { get; set; }
    public float AttackSpeed { get; set; }
    /// <summary>
    /// Use this ONLY for when you can't get the GameClient to properly spawn LaneMinions (This will make the LaneMinions be notified as NormalMinions)
    /// </summary>
    public bool SpawnTypeOverride { get; set; } = false;
}
