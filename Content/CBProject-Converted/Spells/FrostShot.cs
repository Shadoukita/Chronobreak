namespace Spells
{
    public class FrostShot : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.FrostShot)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.FrostShot), owner, 0);
            }
            else
            {
                float nextBuffVars_ManaCostPerAttack = 8;
                AddBuff(attacker, target, new Buffs.FrostShot(nextBuffVars_ManaCostPerAttack), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class FrostShot : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Frost Shot",
            BuffTextureName = "Bowmaster_IceArrow.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
            SpellToggleSlot = 1,
        };
        float manaCostPerAttack;
        public FrostShot(float manaCostPerAttack = default)
        {
            this.manaCostPerAttack = manaCostPerAttack;
        }
        public override void OnActivate()
        {
            //RequireVar(this.manaCostPerAttack);
            OverrideAutoAttack(1, SpellSlotType.ExtraSlots, owner, 1, false);
        }
        public override void OnDeactivate(bool expired)
        {
            RemoveOverrideAutoAttack(owner, false);
        }
        public override void OnLaunchMissile(SpellMissile missileId)
        {
            float temp = GetPAR(owner, PrimaryAbilityResourceType.MANA);
            if (target is ObjAIBase && target is not BaseTurret)
            {
                if (temp >= manaCostPerAttack)
                {
                    float manaToInc = manaCostPerAttack * -1;
                    IncPAR(owner, manaToInc, PrimaryAbilityResourceType.MANA);
                }
                else
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}