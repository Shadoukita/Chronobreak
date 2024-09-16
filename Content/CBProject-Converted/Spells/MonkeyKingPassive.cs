namespace Buffs
{
    public class MonkeyKingPassive : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MonkeyKingPassive",
            BuffTextureName = "Cassiopeia_PetrifyingGaze.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float defenseToAdd;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            defenseToAdd = 0;
            SetBuffToolTipVar(1, 4);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, defenseToAdd);
            IncFlatSpellBlockMod(owner, defenseToAdd);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, true))
            {
                float count = 0;
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 1400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, default, true))
                {
                    bool canSee = CanSeeTarget(owner, unit);
                    if (canSee)
                    {
                        count++;
                    }
                }
                int ownerLevel = GetLevel(owner);
                if (ownerLevel > 12)
                {
                    defenseToAdd = count * 8;
                    defenseToAdd = Math.Min(defenseToAdd, 40);
                    SetBuffToolTipVar(1, 8);
                }
                else if (ownerLevel > 6)
                {
                    defenseToAdd = count * 6;
                    defenseToAdd = Math.Min(defenseToAdd, 30);
                    SetBuffToolTipVar(1, 6);
                }
                else
                {
                    defenseToAdd = count * 4;
                    defenseToAdd = Math.Min(defenseToAdd, 20);
                    SetBuffToolTipVar(1, 4);
                }
            }
        }
    }
}