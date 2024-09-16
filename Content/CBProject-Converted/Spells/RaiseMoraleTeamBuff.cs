namespace Buffs
{
    public class RaiseMoraleTeamBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "L_Hand", "R_Hand", },
            AutoBuffActivateEffect = new[] { "pirate_attack_buf_01.troy", "pirate_attack_buf_01.troy", },
            BuffName = "RaiseMoraleTeamBuff",
            BuffTextureName = "Pirate_RaiseMorale.dds",
        };
        float moveSpeedMod;
        float attackDmgMod;
        public RaiseMoraleTeamBuff(float moveSpeedMod = default, float attackDmgMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
            this.attackDmgMod = attackDmgMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
            //RequireVar(this.attackDmgMod);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, moveSpeedMod);
            IncFlatPhysicalDamageMod(owner, attackDmgMod);
        }
        public override void OnDeactivate(bool expired)
        {
            if (attacker == owner)
            {
                AddBuff(attacker, attacker, new Buffs.RaiseMorale(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
    }
}