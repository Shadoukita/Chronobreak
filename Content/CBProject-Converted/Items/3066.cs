namespace ItemPassives
{
    public class ItemID_3066 : ItemScript
    {
        //TODO: This is the only use of this function
        //public override void UpdateAura()
        //{
        //    DefUpdateAura(owner.Position3D, 200, UNITSCAN_Friends, nameof(Buffs.Fervor));
        //}
        public override void OnActivate()
        {
            SpellEffectCreate(out _, out _, "Fervor", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }
    }
}
namespace Buffs
{
    public class _3066 : BuffScript
    {
        public override void OnActivate()
        {
            SpellEffectCreate(out _, out _, "Fervor", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }
    }
}