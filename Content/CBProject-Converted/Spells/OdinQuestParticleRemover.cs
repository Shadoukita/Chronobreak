namespace Buffs
{
    public class OdinQuestParticleRemover : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinCenterShrineBuff",
            BuffTextureName = "48thSlave_Tattoo.dds",
            NonDispellable = false,
        };
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectUseable, nameof(Buffs.OdinGuardianBuff), true))
            {
                SpellBuffClear(unit, nameof(Buffs.OdinQuestIndicator));
            }
        }
    }
}