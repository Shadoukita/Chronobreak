namespace Buffs
{
    public class MonkeyKingDoubleAttackDebuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MonkeyKingDoubleAttackDebuff",
            BuffTextureName = "MonkeyKingCrushingBlow.dds",
        };
        float armorDebuff;
        EffectEmitter particle1;
        public MonkeyKingDoubleAttackDebuff(float armorDebuff = default)
        {
            this.armorDebuff = armorDebuff;
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner); // UNUSED
            //RequireVar(this.armorDebuff);
            IncPercentArmorMod(owner, armorDebuff);
            SpellEffectCreate(out particle1, out _, "monkey_king_crushingBlow_armor_debuff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle1);
        }
        public override void OnUpdateStats()
        {
            IncPercentArmorMod(owner, armorDebuff);
        }
    }
}