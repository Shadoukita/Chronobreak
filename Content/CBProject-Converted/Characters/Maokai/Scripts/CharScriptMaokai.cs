namespace CharScripts
{
    public class CharScriptMaokai : CharScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, false))
            {
                if (!IsDead(owner))
                {
                    TeamId teamID = GetTeamID_CS(owner);
                    foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1800, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes | SpellDataFlags.AlwaysSelf, default, true))
                    {
                        if (teamID == TeamId.TEAM_ORDER)
                        {
                            AddBuff(attacker, unit, new Buffs.MaokaiSapMagic(), 1, 1, 0.75f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        }
                        else
                        {
                            AddBuff(attacker, unit, new Buffs.MaokaiSapMagicChaos(), 1, 1, 0.75f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        }
                    }
                }
            }
        }
        public override void OnPreAttack(AttackableUnit target)
        {
            if (target is ObjAIBase)
            {
                if (target is not BaseTurret)
                {
                    if (GetBuffCountFromCaster(owner, attacker, nameof(Buffs.MaokaiSapMagicMelee)) > 0)
                    {
                        float healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
                        if (healthPercent < 1)
                        {
                            OverrideAnimation("Attack", "Passive", owner);
                            OverrideAnimation("Attack2", "Passive", owner);
                            OverrideAnimation("Crit", "Passive", owner);
                        }
                        else
                        {
                            ClearOverrideAnimation("Attack", owner);
                            ClearOverrideAnimation("Attack2", owner);
                            ClearOverrideAnimation("Crit", owner);
                        }
                    }
                    else
                    {
                        ClearOverrideAnimation("Attack", owner);
                        ClearOverrideAnimation("Attack2", owner);
                        ClearOverrideAnimation("Crit", owner);
                    }
                }
                else
                {
                    ClearOverrideAnimation("Attack", owner);
                    ClearOverrideAnimation("Attack2", owner);
                    ClearOverrideAnimation("Crit", owner);
                }
            }
            else
            {
                ClearOverrideAnimation("Attack", owner);
                ClearOverrideAnimation("Attack2", owner);
                ClearOverrideAnimation("Crit", owner);
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.MaokaiSapMagicPass(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            charVars.Tally = 0;
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}