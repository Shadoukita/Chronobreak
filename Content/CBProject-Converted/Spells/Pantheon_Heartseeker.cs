namespace Spells
{
    public class PantheonE : Pantheon_Heartseeker { }
    public class Pantheon_Heartseeker : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            ChannelDuration = 0.75f,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
        };
        /*
        //TODO: Uncomment and fix
        public override void ChannelingStart()
        {
            Vector3 castPosition; // UNITIALIZED
            Vector3 nextBuffVars_CastPosition = castPosition;
            FaceDirection(owner, castPosition);
            Vector3 sourcePosition = GetPointByUnitFacingOffset(owner, -25, 0);
            Vector3 nextBuffVars_sourcePosition = sourcePosition;
            AddBuff((ObjAIBase)owner, owner, new Buffs.Pantheon_Heartseeker(nextBuffVars_CastPosition, nextBuffVars_sourcePosition), 1, 1, 0.75f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0.25f, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.Pantheon_HeartseekerSound(), 1, 1, 0.75f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.Pantheon_HeartseekerChannel(), 1, 1, 15, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pantheon_AegisShield2)) == 0 && GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pantheon_AegisShield)) == 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.Pantheon_Aegis_Counter(), 5, 1, 25000, BuffAddType.STACKS_AND_OVERLAPS, BuffType.AURA, 0, false, false, false);
                int count = GetBuffCountFromAll(owner, nameof(Buffs.Pantheon_Aegis_Counter));
                if(count >= 4)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.Pantheon_AegisShield(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                    SpellBuffClear(owner, nameof(Buffs.Pantheon_Aegis_Counter));
                }
            }
        }
        */
        public override void ChannelingStop()
        {
            SpellBuffRemove(owner, nameof(Buffs.Pantheon_HeartseekerChannel), owner, 0);
        }
        public override void ChannelingCancelStop()
        {
            SpellBuffRemove(owner, nameof(Buffs.Pantheon_Heartseeker), owner, 0);
            SpellBuffRemove(owner, nameof(Buffs.Pantheon_HeartseekerSound), owner, 0);
            SpellBuffRemove(owner, nameof(Buffs.Pantheon_HeartseekerChannel), owner, 0);
        }
    }
}
namespace Buffs
{
    public class Pantheon_Heartseeker : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Heartseeker",
        };
        Vector3 castPosition;
        Vector3 sourcePosition;
        float ticksRemaining;
        float lastTimeExecuted;
        public Pantheon_Heartseeker(Vector3 castPosition = default, Vector3 sourcePosition = default)
        {
            this.castPosition = castPosition;
            this.sourcePosition = sourcePosition;
        }
        public override void OnActivate()
        {
            //RequireVar(this.castPosition);
            //RequireVar(this.sourcePosition);
            ticksRemaining = 2;
            int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            Vector3 castPosition = this.castPosition;
            Vector3 sourcePosition = this.sourcePosition;
            SpellCast((ObjAIBase)owner, default, castPosition, castPosition, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false, sourcePosition);
        }
        public override void OnDeactivate(bool expired)
        {
            if (ticksRemaining >= 1 && expired)
            {
                ticksRemaining--;
                int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                Vector3 castPosition = this.castPosition;
                Vector3 sourcePosition = this.sourcePosition;
                SpellCast((ObjAIBase)owner, default, castPosition, castPosition, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false, sourcePosition);
            }
            SpellBuffRemove(owner, nameof(Buffs.Pantheon_HeartseekerSound), (ObjAIBase)owner, 0);
            SpellBuffRemove(owner, nameof(Buffs.Pantheon_Heartseeker), (ObjAIBase)owner, 0);
            SpellBuffRemove(owner, nameof(Buffs.Pantheon_HeartseekerChannel), (ObjAIBase)owner, 0);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, false))
            {
                if (ticksRemaining >= 1)
                {
                    ticksRemaining--;
                    int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    Vector3 castPosition = this.castPosition;
                    Vector3 sourcePosition = this.sourcePosition;
                    SpellCast((ObjAIBase)owner, default, castPosition, castPosition, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false, sourcePosition);
                }
                else
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}