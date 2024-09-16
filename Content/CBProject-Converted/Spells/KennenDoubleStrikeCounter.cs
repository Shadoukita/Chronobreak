namespace Buffs
{
    public class KennenDoubleStrikeCounter : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "KennenDoubleStrikeCounter",
            BuffTextureName = "Kennen_ElectricalSurge.dds",
        };
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            charVars.Count++;
            if (charVars.Count < 5)
            {
                if (charVars.Count >= 4)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.KennenDoubleStrikeProc(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true);
                    SpellBuffRemoveStacks(owner, (ObjAIBase)owner, nameof(Buffs.KennenDoubleStrikeIndicator), 0);
                }
                else
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.KennenDoubleStrikeIndicator(), 8, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true);
                }
            }
        }
    }
}