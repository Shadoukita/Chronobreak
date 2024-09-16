namespace Buffs
{
    public class NocturneUmbraBlades : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "BUFFBONE_CSTM_L_PALM", "BUFFBONE_CSTM_R_PALM", },
            AutoBuffActivateEffect = new[] { "NocturnePassiveReady.troy", "NocturnePassiveReady.troy", },
            BuffName = "NocturneUmbraBlades",
            BuffTextureName = "Nocturne_UmbraBlades.dds",
        };
        int[] effect0 = { 15, 15, 15, 15, 15, 15, 20, 20, 20, 20, 20, 20, 25, 25, 25, 25, 25, 25 };
        public override void OnActivate()
        {
            SetDodgePiercing(owner, true);
            int level = GetLevel(owner);
            float heal = effect0[level - 1];
            SetBuffToolTipVar(1, heal);
        }
        public override void OnDeactivate(bool expired)
        {
            charVars.Count = 0;
            SetDodgePiercing(owner, false);
            RemoveOverrideAutoAttack(owner, true);
        }
        public override void OnPreAttack(AttackableUnit target)
        {
            if (target is ObjAIBase)
            {
                if (target is BaseTurret)
                {
                    RemoveOverrideAutoAttack(owner, true);
                }
                else
                {
                    OverrideAutoAttack(0, SpellSlotType.ExtraSlots, owner, 1, true);
                }
            }
            else
            {
                RemoveOverrideAutoAttack(owner, true);
            }
        }
    }
}