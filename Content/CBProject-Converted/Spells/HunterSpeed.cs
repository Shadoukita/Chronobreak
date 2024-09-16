namespace Buffs
{
    public class HunterSpeed : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "HunterSpeedBuff",
            BuffTextureName = "Sivir_Sprint.dds",
        };
        float movementSpeedMod;
        float lastTimeExecuted;
        int[] effect0 = { 30, 40, 50, 60, 70 };
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            bool returnValue = true;
            if (owner.Team != attacker.Team)
            {
                if (type == BuffType.COMBAT_DEHANCER)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else if (type == BuffType.DAMAGE)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else if (type == BuffType.FEAR)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else if (type == BuffType.CHARM)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else if (type == BuffType.POLYMORPH)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else if (type == BuffType.SILENCE)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else if (type == BuffType.SLEEP)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else if (type == BuffType.SNARE)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else if (type == BuffType.STUN)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else if (type == BuffType.SLOW)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            //RequireVar(this.movementSpeedMod);
        }
        public override void OnUpdateStats()
        {
            if (ExecutePeriodically(8, ref lastTimeExecuted, false))
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                movementSpeedMod = effect0[level - 1];
            }
            IncFlatMovementSpeedMod(owner, movementSpeedMod);
        }
        public override void OnTakeDamage(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource)
        {
            SpellBuffRemoveCurrent(owner);
        }
    }
}