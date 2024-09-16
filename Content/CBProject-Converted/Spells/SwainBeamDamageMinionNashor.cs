namespace Buffs
{
    public class SwainBeamDamageMinionNashor : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SwainBeamDamage",
            BuffTextureName = "SwainDecrepify.dds",
        };
        float damagePerHalfSecond;
        int casterID; // UNUSED
        float lastTimeExecuted;
        public SwainBeamDamageMinionNashor(float damagePerHalfSecond = default)
        {
            this.damagePerHalfSecond = damagePerHalfSecond;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damagePerHalfSecond);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SwainMetamorphism)) == 0)
            {
                casterID = PushCharacterData("SwainNoBird", owner, false);
            }
            ApplyDamage((ObjAIBase)owner, attacker, damagePerHalfSecond, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0.3f, 1, false, false, (ObjAIBase)owner);
        }
        public override void OnDeactivate(bool expired)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SwainMetamorphism)) == 0)
            {
                PopAllCharacterData(owner);
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SwainBeamTransition)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.SwainBeamTransition), (ObjAIBase)owner, 0);
            }
            SpellBuffRemove(owner, nameof(Buffs.SwainBeamSelf), (ObjAIBase)owner, 0);
        }
        public override void OnUpdateActions()
        {
            if (IsDead(attacker))
            {
                SpellBuffRemoveCurrent(owner);
            }
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                ApplyDamage((ObjAIBase)owner, attacker, damagePerHalfSecond, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0.3f, 1, false, false, (ObjAIBase)owner);
            }
        }
    }
}