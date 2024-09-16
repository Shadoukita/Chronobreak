namespace GameServerLib.GameObjects.Stats
{
    public class EvolutionState
    {
        public uint EvolvePoints { get; set; }
        public uint EvolveFlags { get; set; }

        public void IncrementEvolvePoints(uint amount)
        {
            EvolvePoints += amount;
        }

        public void DecrementEvolvePoints(uint amount)
        {
            EvolvePoints -= amount;
        }

        public void SetEvolvePoints(uint amount)
        {
            EvolvePoints = amount;
        }

        public void SetEvolveFlags(uint flags)
        {
            EvolveFlags = flags;
        }
    }
}
