namespace Buffs
{
    public class XenZhaoPuncture : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "XenZhaoPuncture",
            BuffTextureName = "XinZhao_TirelessWarrior.dds",
        };
        float healAmount;
        int[] effect0 = { 25, 25, 30, 30, 35, 35, 40, 40, 45, 45, 50, 50, 55, 55, 60, 60, 65, 65 };
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (hitResult != HitResult.HIT_Dodge && hitResult != HitResult.HIT_Miss)
            {
                charVars.ComboCounter++;
                if (charVars.ComboCounter >= 3)
                {
                    charVars.ComboCounter = 0;
                    int level = GetLevel(owner);
                    healAmount = effect0[level - 1];
                    IncHealth(owner, healAmount, owner);
                    SpellEffectCreate(out _, out _, "xenZiou_heal_passive.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, false, default, default, false, false);
                }
            }
        }
        public override void OnUpdateActions()
        {
            int level = GetLevel(owner);
            healAmount = effect0[level - 1];
            SetBuffToolTipVar(1, healAmount);
        }
    }
}