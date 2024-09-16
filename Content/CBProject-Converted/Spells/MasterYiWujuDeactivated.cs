namespace Buffs
{
    public class MasterYiWujuDeactivated : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        EffectEmitter glowblade;
        public override void OnActivate()
        {
            SpellEffectCreate(out glowblade, out _, "yiglowblade.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "weaponstreak", default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(glowblade);
        }
    }
}