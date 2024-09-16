namespace Talents
{
    public class Talent_127 : TalentScript
    {
        public override void OnDodge(AttackableUnit attacker)
        {
            AddBuff(owner, owner, new Buffs.Nimbleness(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.HASTE, 0);
        }
    }
}
namespace Buffs
{
    public class _127 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "",
            BuffTextureName = "",
        };
    }
}