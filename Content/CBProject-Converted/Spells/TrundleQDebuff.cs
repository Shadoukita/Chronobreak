namespace Buffs
{
    public class TrundleQDebuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "TrundleQDebuff",
            BuffTextureName = "Trundle_Bite.dds",
        };
        int sapVar;
        float negSapVar;
        EffectEmitter sappedDebuff;
        public TrundleQDebuff(int sapVar = default, float negSapVar = default)
        {
            this.sapVar = sapVar;
            this.negSapVar = negSapVar;
        }
        public override void OnActivate()
        {
            //RequireVar(this.negSapVar);
            //RequireVar(this.sapVar);
            float nextBuffVars_SapVar = sapVar;
            IncFlatPhysicalDamageMod(owner, negSapVar);
            SpellEffectCreate(out sappedDebuff, out _, "TrundleQDebuff_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            AddBuff(attacker, attacker, new Buffs.TrundleQ(nextBuffVars_SapVar), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(sappedDebuff);
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, negSapVar);
        }
    }
}