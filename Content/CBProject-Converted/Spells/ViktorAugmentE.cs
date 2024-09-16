namespace Buffs
{
    public class ViktorAugmentE : BuffScript
    {
        float abilityPower;
        EffectEmitter staffIdleRED; // UNUSED
        /*
        //TODO: Uncomment and fix
        public override void OnActivate()
        {
            TeamId ownerTeam; // UNITIALIZED
            float aP = GetFlatMagicDamageMod(owner);
            this.abilityPower = aP * 0.1f;
            SetSlotSpellIcon(2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, 2);
            SpellEffectCreate(out this.staffIdleRED, out _, "Viktorb_red.troy", default, ownerTeam, 200, 0, TeamId.TEAM_NEUTRAL, ownerTeam, owner, false, owner, "BUFFBONE_CSTM_WEAPON_1", default, owner, "BUFFBONE_CSTM_WEAPON_1", default, false, false, false, false, false);
        }
        */
        public override void OnUpdateActions()
        {
            float aP = GetFlatMagicDamageMod(owner);
            aP -= abilityPower;
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.ZhonyasRing)) > 0)
            {
                abilityPower = aP * 0.07f;
            }
            else
            {
                abilityPower = aP * 0.1f;
            }
        }
    }
}