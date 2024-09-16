namespace Spells
{
    public class AhriOrbReturn : SpellScript
    {
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (target != attacker)
            {
                int nextBuffVars_OrbofDeceptionIsActive = charVars.OrbofDeceptionIsActive;
                AddBuff(attacker, target, new Buffs.AhriOrbDamageSilence(nextBuffVars_OrbofDeceptionIsActive), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            else
            {
                DestroyMissile(missileNetworkID);
                if (charVars.OrbofDeceptionIsActive == 1)
                {
                    charVars.OrbofDeceptionIsActive = 0;
                }
            }
        }
    }
}
namespace Buffs
{
    public class AhriOrbReturn : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "root", },
            AutoBuffActivateEffect = new[] { "Ahri_OrbofDeception.troy", },
        };
    }
}