namespace Buffs
{
    public class EvelynnPassive : SilentKiller { }
    public class SilentKiller : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Silent Killer",
            BuffTextureName = "Evelynn_Stalk.dds",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        public override void OnKill(AttackableUnit target)
        {
            if (target is Champion)
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level >= 1)
                {
                    IncHealth(owner, charVars.MaliceHeal, owner);
                    SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                }
            }
        }
        public override void OnAssist(ObjAIBase attacker, AttackableUnit target)
        {
            if (target is Champion)
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level >= 1)
                {
                    IncHealth(owner, charVars.MaliceHeal, owner);
                    SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                }
            }
        }
        public override void OnBeingHit(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, HitResult hitResult)
        {
            TeamId attackerID = GetTeamID_CS(attacker);
            if (attackerID == TeamId.TEAM_NEUTRAL)
            {
            }
            else if (attacker is ObjAIBase)
            {
                if (attacker is Champion)
                {
                }
                else if (attacker is BaseTurret)
                {
                }
                else
                {
                    damageAmount *= 0.5f;
                }
            }
        }
    }
}