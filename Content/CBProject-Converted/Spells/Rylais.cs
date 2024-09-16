namespace Buffs
{
    public class Rylais : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Rylais",
            BuffTextureName = "3022_Frozen_Heart.dds",
        };
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (hitResult != HitResult.HIT_Dodge && hitResult != HitResult.HIT_Miss)
            {
                if (target is ObjAIBase)
                {
                    if (target is not BaseTurret)
                    {
                        float nextBuffVars_MovementSpeedMod = -0.35f;
                        float nextBuffVars_AttackSpeedMod = 0;
                        AddBuff(attacker, target, new Buffs.Chilled(nextBuffVars_MovementSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 2.5f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0);
                    }
                }
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}