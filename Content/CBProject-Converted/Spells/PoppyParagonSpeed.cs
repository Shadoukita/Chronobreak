namespace Buffs
{
    public class PoppyParagonSpeed : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "PoppyParagonSpeed",
            BuffTextureName = "Poppy_MightOfDemacia.dds",
        };
        EffectEmitter speedParticle;
        float moveSpeedVar;
        float[] effect0 = { 0.17f, 0.19f, 0.21f, 0.23f, 0.25f };
        public override void OnActivate()
        {
            SpellEffectCreate(out speedParticle, out _, "Global_Haste.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            moveSpeedVar = effect0[level - 1];
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(speedParticle);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, moveSpeedVar);
        }
    }
}