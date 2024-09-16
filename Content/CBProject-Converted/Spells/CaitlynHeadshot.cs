namespace Buffs
{
    public class CaitlynHeadshot : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "R_BUFFBONE_GLB_HAND_LOC", "L_BUFFBONE_GLB_HAND_LOC", },
            AutoBuffActivateEffect = new[] { "caitlyn_headshot_rdy_indicator.troy", "caitlyn_headshot_rdy_indicator.troy", },
            BuffName = "CaitlynHeadshotReady",
            BuffTextureName = "Caitlyn_Headshot2.dds",
        };
        public override void OnActivate()
        {
            SetDodgePiercing(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetDodgePiercing(owner, false);
        }
        public override void OnPreAttack(AttackableUnit target)
        {
            RemoveOverrideAutoAttack(owner, false);
            if (target is ObjAIBase && target is not BaseTurret)
            {
                OverrideAutoAttack(2, SpellSlotType.ExtraSlots, owner, 1, true);
            }
        }
    }
}