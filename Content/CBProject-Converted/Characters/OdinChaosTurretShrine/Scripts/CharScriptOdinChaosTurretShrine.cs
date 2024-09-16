namespace CharScripts
{
    public class CharScriptOdinChaosTurretShrine : CharScript
    {
        public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            int count = GetBuffCountFromAll(target, nameof(Buffs.OdinTurretDamage));
            if (count > 0)
            {
                float multiplier = count * 0.4f;
                multiplier++;
                damageAmount *= multiplier;
            }
            AddBuff(owner, target, new Buffs.OdinTurretDamage(), 8, 1, 4, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.TurretBonus(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 60, true, false, false);
            float nextBuffVars_BonusHealth = 0;
            float nextBuffVars_BubbleSize = 1600;
            AddBuff(owner, owner, new Buffs.TurretBonusHealth(nextBuffVars_BonusHealth, nextBuffVars_BubbleSize), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
            {
                AddBuff(owner, unit, new Buffs.CallForHelpManager(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            //float range = GetFlatAttackRangeMod(owner); // UNUSED
            Vector3 ownerPosition = GetUnitPosition(owner);
            TeamId myTeam = GetTeamID_CS(owner);
            SetTargetable(owner, false);
            ownerPosition = GetUnitPosition(owner);
            myTeam = GetTeamID_CS(owner);
            Region perceptionBubble = AddPosPerceptionBubble(myTeam, 1600, ownerPosition, 25000, owner, true); // UNUSED
            TeamId enemyTeam = TeamId.TEAM_ORDER;
            Region perceptionBubble2 = AddPosPerceptionBubble(enemyTeam, 50, ownerPosition, 25000, default, false); // UNUSED
            SetDodgePiercing(owner, true);
        }
    }
}