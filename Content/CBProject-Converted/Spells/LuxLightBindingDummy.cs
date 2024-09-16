namespace Spells
{
    public class LuxLightBindingDummy : SpellScript
    {
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            DestroyMissile(missileNetworkID);
        }
    }
}