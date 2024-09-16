namespace Spells
{
    public class TosajirosGlare : SpellScript
    {
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (GetBuffCountFromCaster(target, owner, nameof(Buffs.LuxLightBinding)) > 0)
            {
                SpellBuffRemove(target, nameof(Buffs.LuxLightBinding), attacker);
                DebugSay(owner, "DISPELL ROOT !!");
            }
            else
            {
                float nextBuffVars_MoveSpeedMod = -0.5f; // UNUSED
                DebugSay(owner, "TARGET BINDED !!");
                AddBuff(attacker, target, new Buffs.LuxLightBinding(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.CHARM, 0, true, false);
            }
        }
    }
}