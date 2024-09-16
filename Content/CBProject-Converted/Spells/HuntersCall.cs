namespace Spells
{
    public class HuntersCall : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { 0.4f, 0.5f, 0.6f, 0.7f, 0.8f };
        float[] effect1 = { 0.2f, 0.25f, 0.3f, 0.35f, 0.4f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_AttackSpeedVar = effect0[level - 1];
            float nextBuffVars_AttackSpeedOther = effect1[level - 1];
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1200, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
            {
                AddBuff(owner, unit, new Buffs.HuntersCall(nextBuffVars_AttackSpeedVar, nextBuffVars_AttackSpeedOther), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true);
            }
        }
    }
}
namespace Buffs
{
    public class HuntersCall : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "r_hand", "l_hand", },
            AutoBuffActivateEffect = new[] { "Global_DmgHands_buf.troy", "Global_DmgHands_buf.troy", },
            BuffName = "Hunter's Call",
            BuffTextureName = "Wolfman_FuryStance.dds",
        };
        float attackSpeedVar;
        float attackSpeedOther;
        public HuntersCall(float attackSpeedVar = default, float attackSpeedOther = default)
        {
            this.attackSpeedVar = attackSpeedVar;
            this.attackSpeedOther = attackSpeedOther;
        }
        public override void OnActivate()
        {
            //RequireVar(this.attackSpeedVar);
            //RequireVar(this.attackSpeedOther);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnUpdateStats()
        {
            if (owner == attacker)
            {
                IncPercentAttackSpeedMod(owner, attackSpeedVar);
            }
            else
            {
                IncPercentAttackSpeedMod(owner, attackSpeedOther);
            }
        }
    }
}