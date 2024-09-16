namespace Spells
{
    public class RapidFire : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
            SpellFXOverrideSkins = new[] { "RocketTristana", },
        };
        float[] effect0 = { 0.3f, 0.45f, 0.6f, 0.75f, 0.9f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_AttackSpeedMod = effect0[level - 1];
            AddBuff(attacker, target, new Buffs.RapidFire(nextBuffVars_AttackSpeedMod), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class RapidFire : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "weapon", },
            AutoBuffActivateEffect = new[] { "rapidfire_buf.troy", },
            BuffName = "Rapid Fire",
            BuffTextureName = "Tristana_headshot.dds",
        };
        float attackSpeedMod;
        public RapidFire(float attackSpeedMod = default)
        {
            this.attackSpeedMod = attackSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.attackSpeedMod);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, attackSpeedMod);
        }
    }
}