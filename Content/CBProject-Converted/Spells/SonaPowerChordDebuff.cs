namespace Buffs
{
    public class SonaPowerChordDebuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SonaPowerChordDebuff",
            BuffTextureName = "Sona_PowerChordCharged.dds",
        };
        EffectEmitter b;
        public override void OnActivate()
        {
            SpellEffectCreate(out b, out _, "SonaPowerChord_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, owner, default, default, false, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(b);
        }
        public override void OnDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource)
        {
            if (damageType != DamageType.DAMAGE_TYPE_TRUE)
            {
                damageAmount *= 0.8f;
            }
        }
    }
}