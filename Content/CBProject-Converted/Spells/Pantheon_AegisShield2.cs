namespace Buffs
{
    public class Pantheon_AegisShield2 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "C_BUFFBONE_GLB_CENTER_LOC", },
            AutoBuffActivateEffect = new[] { "pantheon_aoz_passive.troy", },
            BuffName = "PantheonAegisShield",
            BuffTextureName = "Pantheon_AOZ.dds",
        };
        bool executeOnce;
        public override void OnActivate()
        {
            //RequireVar(this.executeOnce);
            if (!executeOnce)
            {
                bool isMoving = IsMoving(owner);
                if (isMoving)
                {
                    executeOnce = true;
                    OverrideAnimation("Run", "Run2", owner);
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            if (executeOnce)
            {
                ClearOverrideAnimation("Run", owner);
            }
        }
        public override void OnUpdateActions()
        {
            if (!executeOnce)
            {
                bool isMoving = IsMoving(owner);
                if (isMoving)
                {
                    executeOnce = true;
                    OverrideAnimation("Run", "Run2", owner);
                }
            }
        }
    }
}