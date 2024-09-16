namespace Buffs
{
    public class Pantheon_Aegis : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Pantheon Aegis",
            BuffTextureName = "Pantheon_AOZ.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        int aegisCounter; // UNUSED
        public override void OnActivate()
        {
            aegisCounter = 0;
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (hitResult != HitResult.HIT_Miss && hitResult != HitResult.HIT_Dodge && GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pantheon_AegisShield2)) == 0 && GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pantheon_AegisShield)) == 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.Pantheon_Aegis_Counter(), 5, 1, 25000, BuffAddType.STACKS_AND_OVERLAPS, BuffType.AURA, 0, false, false, false);
                int count = GetBuffCountFromAll(owner, nameof(Buffs.Pantheon_Aegis_Counter));
                if (count >= 4)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.Pantheon_AegisShield(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                    SpellBuffClear(owner, nameof(Buffs.Pantheon_Aegis_Counter));
                }
            }
        }
    }
}