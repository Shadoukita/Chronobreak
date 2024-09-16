namespace Buffs
{
    public class KogMawIcathianSurpriseReady : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "KogMawIcathianSurpriseReady",
            BuffTextureName = "KogMaw_IcathianSurprise.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        EffectEmitter a;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            int kogMawSkinID = GetSkinID(attacker);
            if (kogMawSkinID == 4)
            {
                SpellEffectCreate(out a, out _, "KogNoseGlow.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_nose", default, owner, default, default, false, false, false, false, false);
            }
            else if (kogMawSkinID == 6)
            {
                SpellEffectCreate(out a, out _, "Kogmaw_deepsea_glow.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_ANGLER", default, owner, default, default, false, false, false, false, false);
            }
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            if (owner is Champion)
            {
                becomeZombie = true;
            }
        }
        public override void OnZombie(ObjAIBase attacker)
        {
            int kogMawSkinID = GetSkinID(owner);
            if (kogMawSkinID == 4)
            {
                SpellEffectRemove(a);
            }
            else if (kogMawSkinID == 6)
            {
                SpellEffectRemove(a);
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.KogMawIcathianSurprise)) == 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.KogMawIcathianSurprise(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.KogMawIcathianSurprise)) > 0)
            {
                damageAmount = 0;
            }
        }
    }
}