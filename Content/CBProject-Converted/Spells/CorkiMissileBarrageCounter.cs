namespace Buffs
{
    public class CorkiMissileBarrageCounter : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnUpdateActions()
        {
            if (!IsDead(owner) && GetBuffCountFromCaster(owner, owner, nameof(Buffs.CorkiMissileBarrageTimer)) == 0)
            {
                int count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.MissileBarrage));
                if (count != 7)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.CorkiMissileBarrageTimer(), 1, 1, charVars.ChargeCooldown, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            int spellSlot = GetSpellSlot(spell);
            if (spellSlot == 3)
            {
                charVars.BarrageCounter++;
            }
        }
    }
}