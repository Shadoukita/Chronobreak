namespace Buffs
{
    public class HideInShadows_internal : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
        };
        float timeLastHit;
        float attackSpeedMod;
        float stealthDuration;
        bool willFade;
        public HideInShadows_internal(float timeLastHit = default, float attackSpeedMod = default, float stealthDuration = default)
        {
            this.timeLastHit = timeLastHit;
            this.attackSpeedMod = attackSpeedMod;
            this.stealthDuration = stealthDuration;
        }
        public override void OnActivate()
        {
            //RequireVar(this.attackSpeedMod);
            //RequireVar(this.stealthDuration);
            //RequireVar(this.initialTime);
            //RequireVar(this.timeLastHit);
            PushCharacterFade(owner, 0.2f, 1.5f);
            willFade = false;
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnDeactivate(bool expired)
        {
            bool nextBuffVars_WillRemove = false;
            float nextBuffVars_AttackSpeedMod = attackSpeedMod;
            AddBuff((ObjAIBase)owner, owner, new Buffs.HideInShadows(nextBuffVars_AttackSpeedMod, nextBuffVars_WillRemove), 1, 1, stealthDuration, BuffAddType.REPLACE_EXISTING, BuffType.INVISIBILITY, 0, true, false);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnUpdateActions()
        {
            float curTime = GetTime();
            float timeSinceLastHit = curTime - timeLastHit;
            if (timeSinceLastHit >= 1.5f)
            {
                SpellBuffRemoveCurrent(owner);
            }
            else if (willFade)
            {
                PushCharacterFade(owner, 0.2f, 1.5f);
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            timeLastHit = GetTime();
            PushCharacterFade(owner, 1, 0);
            willFade = true;
        }
        public override void OnTakeDamage(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource)
        {
            timeLastHit = GetTime();
            PushCharacterFade(owner, 1, 0);
            willFade = true;
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            if (IsDead(owner))
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}