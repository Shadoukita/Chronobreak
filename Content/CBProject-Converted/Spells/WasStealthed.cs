namespace Buffs
{
    public class WasStealthed : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "WasStealthed",
        };
        float moveSpeedMod;
        float breakDamage;
        bool willRemove;
        public WasStealthed(float moveSpeedMod = default, float breakDamage = default)
        {
            this.moveSpeedMod = moveSpeedMod;
            this.breakDamage = breakDamage;
        }
        public override void OnActivate()
        {
            //RequireVar(this.breakDamage);
            //RequireVar(this.moveSpeedMod);
            //RequireVar(this.teamID);
        }
        public override void OnUpdateActions()
        {
            if (willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            willRemove = true;
            if (target is ObjAIBase)
            {
                BreakSpellShields(target);
                if (target is not BaseTurret)
                {
                    float nextBuffVars_MoveSpeedMod = moveSpeedMod;
                    AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                }
            }
            if (breakDamage > 0)
            {
                ApplyDamage((ObjAIBase)owner, target, breakDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, attacker);
            }
        }
        public override void OnSpellHit(AttackableUnit target)
        {
            float nextBuffVars_MoveSpeedMod = moveSpeedMod;
            ObjAIBase caster = GetBuffCasterUnit();
            if (caster != target)
            {
                willRemove = true;
                AddBuff((ObjAIBase)owner, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
            }
            if (breakDamage > 0)
            {
                ApplyDamage((ObjAIBase)owner, target, breakDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, attacker);
            }
        }
    }
}