namespace Spells
{
    public class ManaBarrier : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 0f, 0f, 0f, 0f, 0f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class ManaBarrier : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "ManaBarrier",
            BuffTextureName = "Blitzcrank_ManaBarrier.dds",
            NonDispellable = true,
            DoOnPreDamageInExpirationOrder = true,
        };
        float manaShield;
        float amountToSubtract;
        EffectEmitter asdf1;
        float oldArmorAmount;
        public ManaBarrier(float manaShield = default, float amountToSubtract = default)
        {
            this.manaShield = manaShield;
            this.amountToSubtract = amountToSubtract;
        }
        public override void OnActivate()
        {
            //RequireVar(this.manaShield);
            //RequireVar(this.amountToSubtract);
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out asdf1, out _, "SteamGolemShield.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            IncreaseShield(owner, manaShield, true, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(asdf1);
            if (manaShield > 0)
            {
                RemoveShield(owner, manaShield, true, true);
            }
        }
        public override void OnUpdateActions()
        {
            ReduceShield(owner, amountToSubtract, true, true);
            manaShield -= amountToSubtract;
            amountToSubtract = 0;
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            oldArmorAmount = manaShield;
            TeamId teamID = GetTeamID_CS(owner);
            if (manaShield >= damageAmount)
            {
                manaShield -= damageAmount;
                damageAmount = 0;
                oldArmorAmount -= manaShield;
                ReduceShield(owner, oldArmorAmount, true, true);
                SpellEffectCreate(out _, out _, "SteamGolemShield_hit.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
            }
            else
            {
                damageAmount -= manaShield;
                manaShield = 0;
                ReduceShield(owner, oldArmorAmount, true, true);
                SpellEffectCreate(out _, out _, "SteamGolemShield_hit.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}