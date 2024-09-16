namespace Spells
{
    public class TalonShadowAssaultToggle : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            SpellBuffRemove(owner, nameof(Buffs.TalonShadowAssaultBuff), owner, 0);
            SpellBuffRemove(owner, nameof(Buffs.TalonShadowAssaultMisOne), owner, 0);
        }
    }
}
namespace Buffs
{
    public class TalonShadowAssaultToggle : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "Global_Poison.troy", },
            BuffName = "BladeRogue_BrewPoison",
            BuffTextureName = "Armsmaster_Empower.dds",
        };
    }
}