namespace Buffs
{
    public class KennenDoubleStrikeProc : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "KennenDoubleStrikeProc",
            BuffTextureName = "Kennen_ElectricalSurge.dds",
        };
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (hitResult != HitResult.HIT_Dodge && hitResult != HitResult.HIT_Miss && target is not BaseTurret && target is ObjAIBase)
            {
                charVars.Count++;
                AddBuff((ObjAIBase)owner, owner, new Buffs.KennenDoubleStrikeIndicator(), 8, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true);
                if (charVars.Count >= 4)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.KennenDoubleStrikeLive(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true);
                    SpellBuffRemoveStacks(owner, (ObjAIBase)owner, nameof(Buffs.KennenDoubleStrikeIndicator), 0);
                }
            }
        }
    }
}