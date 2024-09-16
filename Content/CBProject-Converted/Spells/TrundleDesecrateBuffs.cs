namespace Buffs
{
    public class TrundleDesecrateBuffs : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "TrundleDesecrateBuffs",
            BuffTextureName = "Trundle_Contaminate.dds",
        };
        float selfASMod;
        float selfMSMod;
        float cCReduc;
        public TrundleDesecrateBuffs(float selfASMod = default, float selfMSMod = default, float cCReduc = default)
        {
            this.selfASMod = selfASMod;
            this.selfMSMod = selfMSMod;
            this.cCReduc = cCReduc;
        }
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            bool returnValue = true;
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots); // UNUSED
            if (owner.Team != attacker.Team)
            {
                if (type == BuffType.SNARE)
                {
                    duration *= cCReduc;
                }
                if (type == BuffType.SLOW)
                {
                    duration *= cCReduc;
                }
                if (type == BuffType.FEAR)
                {
                    duration *= cCReduc;
                }
                if (type == BuffType.CHARM)
                {
                    duration *= cCReduc;
                }
                if (type == BuffType.SLEEP)
                {
                    duration *= cCReduc;
                }
                if (type == BuffType.STUN)
                {
                    duration *= cCReduc;
                }
                if (type == BuffType.TAUNT)
                {
                    duration *= cCReduc;
                }
                if (type == BuffType.SILENCE)
                {
                    duration *= cCReduc;
                }
                if (type == BuffType.BLIND)
                {
                    duration *= cCReduc;
                }
                duration = Math.Max(0.3f, duration);
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            //RequireVar(this.selfASMod);
            //RequireVar(this.selfMSMod);
            //RequireVar(this.cCReduc);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, selfMSMod);
            IncPercentAttackSpeedMod(owner, selfASMod);
        }
    }
}