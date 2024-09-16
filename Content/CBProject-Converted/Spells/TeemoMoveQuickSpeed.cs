namespace Buffs
{
    public class TeemoMoveQuickSpeed : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Move Quick",
            BuffTextureName = "Teemo_MoveQuick.dds",
        };
        EffectEmitter moveQuickParticle;
        float[] effect0 = { 0.1f, 0.14f, 0.18f, 0.22f, 0.26f };
        public override void OnActivate()
        {
            int teemoSkinID = GetSkinID(owner);
            if (teemoSkinID == 4)
            {
                SpellEffectCreate(out moveQuickParticle, out _, "MoveQuick_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "spine", default, owner, default, default, false);
            }
            else
            {
                SpellEffectCreate(out moveQuickParticle, out _, "MoveQuick_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(moveQuickParticle);
        }
        public override void OnUpdateStats()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float moveSpeedBonus = effect0[level - 1];
            IncPercentMovementSpeedMod(owner, moveSpeedBonus);
        }
    }
}