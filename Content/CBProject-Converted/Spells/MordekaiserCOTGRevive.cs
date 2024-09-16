namespace Buffs
{
    public class MordekaiserCOTGRevive : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        EffectEmitter mordekaiserParticle;
        public MordekaiserCOTGRevive(EffectEmitter mordekaiserParticle = default)
        {
            this.mordekaiserParticle = mordekaiserParticle;
        }
        public override void OnActivate()
        {
            //RequireVar(this.mordekaiserParticle);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(mordekaiserParticle);
        }
        public override void OnUpdateActions()
        {
            if (IsDead(attacker))
            {
                bool zombie = GetIsZombie(attacker);
                if (!zombie)
                {
                    int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots); // UNUSED
                    Vector3 pos = GetUnitPosition(attacker);
                    Pet other1 = CloneUnitPet(attacker, nameof(Buffs.MordekaiserCOTGPetSlow), 0, pos, 0, 0, false);
                    float temp1 = GetMaxHealth(other1, PrimaryAbilityResourceType.MANA);
                    IncHealth(other1, temp1, other1);
                    AddBuff((ObjAIBase)owner, other1, new Buffs.MordekaiserCOTGPetBuff2(), 1, 1, 30, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    SpellBuffClear(owner, nameof(Buffs.MordekaiserCOTGRevive));
                }
            }
        }
    }
}