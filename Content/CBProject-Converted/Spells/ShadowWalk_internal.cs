namespace Buffs
{
    public class ShadowWalk_internal : BuffScript
    {
        float timeLastHit;
        float moveSpeedMod;
        float stealthDuration;
        TeamId teamID;
        bool willFade;
        public ShadowWalk_internal(float timeLastHit = default, float moveSpeedMod = default, float stealthDuration = default, TeamId teamID = default)
        {
            this.timeLastHit = timeLastHit;
            this.moveSpeedMod = moveSpeedMod;
            this.stealthDuration = stealthDuration;
            this.teamID = teamID;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
            //RequireVar(this.stealthDuration);
            //RequireVar(this.stealthDuration);
            //RequireVar(this.initialTime);
            //RequireVar(this.timeLastHit);
            Fade iD = PushCharacterFade(owner, 0.2f, 1.5f); // UNUSED
            willFade = false;
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            //RequireVar(this.teamID);
        }
        public override void OnDeactivate(bool expired)
        {
            bool nextBuffVars_WillRemove = false;
            float nextBuffVars_MoveSpeedMod = moveSpeedMod;
            TeamId nextBuffVars_TeamID = teamID;
            AddBuff((ObjAIBase)owner, owner, new Buffs.ShadowWalk(nextBuffVars_MoveSpeedMod, nextBuffVars_WillRemove, nextBuffVars_TeamID), 1, 1, stealthDuration, BuffAddType.REPLACE_EXISTING, BuffType.INVISIBILITY, 0, true, false, false);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
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
                Fade iD = PushCharacterFade(owner, 0.2f, 1); // UNUSED
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            timeLastHit = GetTime();
            Fade iD = PushCharacterFade(owner, 1, 0); // UNUSED
            willFade = true;
        }
        public override void OnTakeDamage(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource)
        {
            timeLastHit = GetTime();
            Fade iD = PushCharacterFade(owner, 1, 0); // UNUSED
            willFade = true;
        }
    }
}