namespace Buffs
{
    public class PoppyParagonManager : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "",
            BuffTextureName = "",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyParagonStats(), 10, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0);
            AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyParagonIcon(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
            int count = GetBuffCountFromAll(owner, nameof(Buffs.PoppyParagonStats));
            if (count == 10)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyParagonParticle(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
            }
            else
            {
                SpellEffectCreate(out _, out _, "poppydam_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "hammer_b", default, target, default, default, false);
                SpellEffectCreate(out _, out _, "poppydef_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_finger", default, target, default, default, false);
            }
        }
        public override void OnBeingHit(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, HitResult hitResult)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyParagonStats(), 10, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0);
            AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyParagonIcon(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
            int count = GetBuffCountFromAll(owner, nameof(Buffs.PoppyParagonStats));
            if (count == 10)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyParagonParticle(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
            }
            else
            {
                SpellEffectCreate(out _, out _, "poppydam_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "hammer_b", default, target, default, default, false);
                SpellEffectCreate(out _, out _, "poppydef_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_finger", default, target, default, default, false);
            }
        }
    }
}