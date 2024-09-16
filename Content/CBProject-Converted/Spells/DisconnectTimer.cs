namespace Buffs
{
    public class DisconnectTimer : BuffScript
    {
        public override void OnReconnect()
        {
            SpellBuffRemoveCurrent(owner);
        }
        public override void OnActivate()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
        public override void OnUpdateStats()
        {
            if (lifeTime >= 60)
            {
                IncFlatSpellBlockMod(owner, 1000);
            }
        }
        public override void OnTakeDamage(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource)
        {
            TeamId teamID = GetTeamID_CS(owner);
            Minion other1 = SpawnMinion("RunToMe", "TestCube", "idle.lua", owner.Position3D, teamID, false, false, false, true, false, true, 0, default, true);
            AddBuff((ObjAIBase)owner, other1, new Buffs.DisconnectTarget(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}