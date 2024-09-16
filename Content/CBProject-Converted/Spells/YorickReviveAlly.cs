namespace Spells
{
    public class YorickReviveAlly : SpellScript
    {
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            AddBuff(owner, target, new Buffs.YorickReviveAllySelf(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            bool zombie = GetIsZombie(owner);
            if (!zombie)
            {
                SpellCast(owner, target, owner.Position3D, owner.Position3D, 3, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class YorickReviveAlly : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "mordekeiser_cotg_tar.troy", },
            BuffName = "MordekaiserCOTGDot",
            BuffTextureName = "Mordekaiser_COTG.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}