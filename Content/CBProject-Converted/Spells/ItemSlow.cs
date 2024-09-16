namespace Buffs
{
    public class ItemSlow : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "GLOBAL_FREEZE.TROY", },
            BuffName = "Frozen Mallet Slow",
            BuffTextureName = "3022_Frozen_Heart.dds",
        };
        public override void OnUpdateStats()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Internal_50Slow)) > 0)
            {
                IncPercentMultiplicativeMovementSpeedMod(owner, -0.5f);
            }
            else
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Internal_40Slow)) > 0)
                {
                    IncPercentMultiplicativeMovementSpeedMod(owner, -0.4f);
                }
                else
                {
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Internal_35Slow)) > 0)
                    {
                        IncPercentMultiplicativeMovementSpeedMod(owner, -0.35f);
                    }
                    else
                    {
                        if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Internal_30Slow)) > 0)
                        {
                            IncPercentMultiplicativeMovementSpeedMod(owner, -0.3f);
                        }
                        else
                        {
                            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Internal_20Slow)) > 0)
                            {
                                IncPercentMultiplicativeMovementSpeedMod(owner, -0.2f);
                            }
                            else
                            {
                                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Internal_15Slow)) > 0)
                                {
                                    IncPercentMultiplicativeMovementSpeedMod(owner, -0.15f);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}