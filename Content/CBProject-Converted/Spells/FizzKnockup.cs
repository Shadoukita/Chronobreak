namespace Buffs
{
    public class FizzKnockup : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Stun",
            BuffTextureName = "Minotaur_TriumphantRoar.dds",
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            Vector3 targetPos = GetRandomPointInAreaPosition(owner.Position3D, 60, 60);
            Move(owner, targetPos, 125, 12, 0, ForceMovementType.FIRST_WALL_HIT, ForceMovementOrdersType.CANCEL_ORDER, 100, ForceMovementOrdersFacing.KEEP_CURRENT_FACING);
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanAttack(owner, true);
            SetCanCast(owner, true);
            SetCanMove(owner, true);
            bool zombie = GetIsZombie(owner);
            if (!zombie && IsDead(owner))
            {
                string name = GetUnitSkinName(owner);
                if (name == "Annie")
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.FizzSharkDissappear(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                if (name == "Annie")
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.FizzSharkDissappear(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                if (name == "Amumu")
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.FizzSharkDissappear(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                if (name == "Kennen")
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.FizzSharkDissappear(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                if (name == "Fizz")
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.FizzSharkDissappear(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                if (name == "Poppy")
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.FizzSharkDissappear(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                if (name == "Veigar")
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.FizzSharkDissappear(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                if (name == "Tristana")
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.FizzSharkDissappear(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
        }
    }
}