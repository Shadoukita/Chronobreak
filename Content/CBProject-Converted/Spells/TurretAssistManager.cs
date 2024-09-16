namespace Buffs
{
    public class TurretAssistManager : BuffScript
    {
        public override void OnActivate()
        {
            IncPermanentFlatArmorMod(owner, 150);
            IncPermanentFlatSpellBlockMod(owner, 150);
            AddBuff((ObjAIBase)owner, owner, new Buffs.PersonalTurretAssistBonus(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnUpdateActions()
        {
            foreach (AttackableUnit unit in GetRandomUnitsInArea((ObjAIBase)owner, owner.Position3D, 1000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectMinions | SpellDataFlags.AffectBarrackOnly, 1, default, true))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.TurretBackdoorBonus(), 1, 1, 8, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}