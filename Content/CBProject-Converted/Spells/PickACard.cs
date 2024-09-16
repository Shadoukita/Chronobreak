namespace Spells
{
    public class PickACard : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float rnd1; // UNITIALIZED
                  rnd1 = RandomChance(); //TODO: Verify
            float nextBuffVars_Counter;
            if(rnd1 < 0.34f)
            {
                nextBuffVars_Counter = 0;
                SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.BlueCardLock));
                SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0.005f);
            }
            else if(rnd1 < 0.67f)
            {
                nextBuffVars_Counter = 2;
                SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.RedCardLock));
                SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0.005f);
            }
            else
            {
                nextBuffVars_Counter = 4;
                SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.GoldCardLock));
                SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0.005f);
            }
            bool nextBuffVars_WillRemove = false; // UNUSED
            AddBuff((ObjAIBase)owner, target, new Buffs.PickACard_tracker(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff((ObjAIBase)owner, target, new Buffs.PickACard(nextBuffVars_Counter), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class PickACard : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Pick A Card",
            BuffTextureName = "CardMaster_FatesGambit.dds",
        };
        float counter;
        EffectEmitter effectID;
        int frozen;
        int removeParticle;
        public PickACard(float counter = default)
        {
            this.counter = counter;
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "AnnieSparks.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            //RequireVar(this.counter);
            //RequireVar(this.willRemove);
            if (counter < 2)
            {
                SpellEffectCreate(out effectID, out _, "Card_Blue.troy", default, teamID, 600, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position3D, target, default, default, false, false, false, false, false);
            }
            else if (counter < 4)
            {
                SpellEffectCreate(out effectID, out _, "Card_Red.troy", default, teamID, 600, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position3D, target, default, default, false, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out effectID, out _, "Card_Yellow.troy", default, teamID, 600, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position3D, target, default, default, false, false, false, false, false);
            }
            frozen = 0;
            removeParticle = 1;
        }
        public override void OnDeactivate(bool expired)
        {
            SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.PickACard));
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            float baseCooldown = 6;
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = multiplier * baseCooldown;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            if (removeParticle != 2)
            {
                SpellEffectRemove(effectID);
            }
            SpellBuffRemove(owner, nameof(Buffs.GoldCardPreAttack), (ObjAIBase)owner, 0);
            SpellBuffRemove(owner, nameof(Buffs.RedCardPreAttack), (ObjAIBase)owner, 0);
            SpellBuffRemove(owner, nameof(Buffs.BlueCardPreattack), (ObjAIBase)owner, 0);
            SetAutoAcquireTargets(owner, true);
        }
        public override void OnUpdateActions()
        {
            if (frozen == 0)
            {
                TeamId teamID = GetTeamID_CS(owner);
                counter++;
                if (counter == 2)
                {
                    SpellEffectRemove(effectID);
                    SpellEffectCreate(out effectID, out _, "Card_Red.troy", default, teamID, 600, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
                    SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.RedCardLock));
                }
                else if (counter == 4)
                {
                    SpellEffectRemove(effectID);
                    SpellEffectCreate(out effectID, out _, "Card_Yellow.troy", default, teamID, 600, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position3D, target, default, default, false, false, false, false, false);
                    SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.GoldCardLock));
                }
                else if (counter >= 6)
                {
                    SpellEffectRemove(effectID);
                    SpellEffectCreate(out effectID, out _, "Card_Blue.troy", default, teamID, 600, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position3D, target, default, default, false, false, false, false, false);
                    SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.BlueCardLock));
                    counter = 0;
                }
            }
            if (removeParticle == 0)
            {
                SpellEffectRemove(effectID);
                removeParticle = 2;
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            float displayDuration;
            spellName = GetSpellName(spell);
            if (spellName == nameof(Spells.PickACardLock))
            {
                if (frozen != 1)
                {
                    SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
                    frozen = 1;
                }
            }
            else if (spellName == nameof(Spells.RedCardLock))
            {
                if (frozen != 1)
                {
                    displayDuration = GetBuffRemainingDuration(owner, nameof(Buffs.PickACard));
                    SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
                    frozen = 1;
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RedCardPreAttack(), 1, 1, displayDuration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    removeParticle = 0;
                    SpellBuffRemove(owner, nameof(Buffs.PickACard_tracker), (ObjAIBase)owner, 0);
                    SetAutoAcquireTargets(owner, false);
                }
            }
            else if (spellName == nameof(Spells.GoldCardLock))
            {
                if (frozen != 1)
                {
                    displayDuration = GetBuffRemainingDuration(owner, nameof(Buffs.PickACard));
                    SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
                    frozen = 1;
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GoldCardPreAttack(), 1, 1, displayDuration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    removeParticle = 0;
                    SpellBuffRemove(owner, nameof(Buffs.PickACard_tracker), (ObjAIBase)owner, 0);
                    SetAutoAcquireTargets(owner, false);
                }
            }
            else if (spellName == nameof(Spells.BlueCardLock))
            {
                if (frozen != 1)
                {
                    displayDuration = GetBuffRemainingDuration(owner, nameof(Buffs.PickACard));
                    SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
                    frozen = 1;
                    AddBuff((ObjAIBase)owner, owner, new Buffs.BlueCardPreattack(), 1, 1, displayDuration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    removeParticle = 0;
                    SpellBuffRemove(owner, nameof(Buffs.PickACard_tracker), (ObjAIBase)owner, 0);
                    SetAutoAcquireTargets(owner, false);
                }
            }
        }
        public override void OnPreAttack(AttackableUnit target)
        {
            if (target is ObjAIBase && frozen == 1)
            {
                SkipNextAutoAttack((ObjAIBase)owner);
                int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (counter <= 1)
                {
                    SpellCast((ObjAIBase)owner, target, target.Position3D, target.Position3D, 5, SpellSlotType.ExtraSlots, level, true, false, false, true, true, false);
                }
                else if (counter <= 3)
                {
                    SpellCast((ObjAIBase)owner, target, target.Position3D, target.Position3D, 6, SpellSlotType.ExtraSlots, level, true, false, false, true, true, false);
                }
                else
                {
                    SpellCast((ObjAIBase)owner, target, target.Position3D, target.Position3D, 1, SpellSlotType.ExtraSlots, level, true, false, false, true, true, false);
                }
            }
        }
    }
}