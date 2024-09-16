namespace CharScripts
{
    public class CharScriptKarthus : CharScript
    {
        float lastTimeExecuted;
        int[] effect0 = { 20, 27, 34, 41, 48 };
        public override void OnKill(AttackableUnit target)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Defile)) == 0)
            {
                int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level > 0)
                {
                    float manaToInc = effect0[level - 1];
                    IncPAR(owner, manaToInc, PrimaryAbilityResourceType.MANA);
                    SpellEffectCreate(out _, out _, "NeutralMonster_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false, false);
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.DeathDefied(), 1, 1, 30000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnResurrect()
        {
            AddBuff(owner, owner, new Buffs.DeathDefied(), 1, 1, 30000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
        public override void OnUpdateActions()
        {
            float _1; // UNITIALIZED
            _1 = 0; //TODO: Verify
            if (ExecutePeriodically(0, ref lastTimeExecuted, true, _1))
            {
                if (GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickRAZombie)) == 0)
                {
                    if (GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickReviveAllySelf)) == 0)
                    {
                        AddBuff(owner, owner, new Buffs.DeathDefied(), 1, 1, 30000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                    }
                }
            }
        }
    }
}