namespace Spells
{
    public class MordekaiserSyphonOfDestruction : SpellScript
    {
        int[] effect0 = { 24, 36, 48, 60, 72 };
        public override void SelfExecute()
        {
            Vector3 castPos = GetSpellTargetPos(spell); // UNUSED
            float healthCost = effect0[level - 1];
            float temp1 = GetHealth(owner, PrimaryAbilityResourceType.Shield);
            if (healthCost >= temp1)
            {
                healthCost = temp1 - 1;
            }
            healthCost *= -1;
            IncHealth(owner, healthCost, owner);
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(owner);
            AddBuff((ObjAIBase)target, owner, new Buffs.MordekaiserSyphonDmg(), 100, 1, 0.001f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.MordekaiserSyphonParticle(), 1, 1, 0.2f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellEffectCreate(out _, out _, "mordakaiser_siphonOfDestruction_tar_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out _, out _, "mordakaiser_siphonOfDestruction_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
        }
    }
}