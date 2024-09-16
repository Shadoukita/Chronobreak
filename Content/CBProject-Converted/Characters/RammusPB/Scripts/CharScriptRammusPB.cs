namespace CharScripts
{
    public class CharScriptRammusPB : CharScript
    {
        int[] effect0 = { 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 5, 4, 4, 4, 4, 4, 4 };
        int[] effect1 = { 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3 };
        public override void SetVarsByLevel()
        {
            charVars.KillsPerArmor = effect0[level - 1];
            charVars.ArmorPerChampionKill = effect1[level - 1];
        }
        public override void OnKill(AttackableUnit target)
        {
            if (target is ObjAIBase)
            {
                bool nextBuffVars_IsChampion; // UNUSED
                if (target is Champion)
                {
                    nextBuffVars_IsChampion = true;
                }
                else
                {
                    nextBuffVars_IsChampion = false;
                }
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.ScavengeArmor)) > 0)
                {
                    charVars.NumMinionsKilled++;
                }
                else
                {
                    charVars.NumMinionsKilled = 1;
                    charVars.ScavengeArmorTotal = 0;
                }
                AddBuff(attacker, owner, new Buffs.ScavengeArmor(), 1, 1, 20000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
            }
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false);
        }
    }
}