namespace CharScripts
{
    public class CharScriptblueDragon : CharScript
    {
        public override void OnUpdateStats()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.DragonApplicator)) == 0)
            {
                AddBuff(owner, owner, new Buffs.DragonApplicator(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 100000, true, false, false);
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.HPByPlayerLevel)) == 0)
            {
                float nextBuffVars_HPPerLevel = 200;
                AddBuff(owner, owner, new Buffs.HPByPlayerLevel(nextBuffVars_HPPerLevel), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.ResistantSkinDragon(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
        }
    }
}