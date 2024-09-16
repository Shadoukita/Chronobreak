namespace CharScripts
{
    public class CharScriptMordekaiser : CharScript
    {
        public override void OnUpdateActions()
        {
            if (IsDead(owner))
            {
                AddBuff(owner, owner, new Buffs.MordekaiserDeathParticle(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            }
            else
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.MordekaiserDeathParticle)) > 0)
                {
                    SpellBuffRemove(owner, nameof(Buffs.MordekaiserDeathParticle), owner);
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(owner, owner, new Buffs.MordekaiserIronMan(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            IncPAR(owner, -180, PrimaryAbilityResourceType.Shield);
        }
        public override void OnLevelUp()
        {
            IncPermanentFlatPARPoolMod(owner, 30, PrimaryAbilityResourceType.Shield);
            IncPAR(owner, -30, PrimaryAbilityResourceType.Shield);
        }
        public override void OnResurrect()
        {
            float temp1 = GetPAR(owner, PrimaryAbilityResourceType.Shield);
            temp1 *= -1;
            IncPAR(owner, temp1, PrimaryAbilityResourceType.Shield);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}