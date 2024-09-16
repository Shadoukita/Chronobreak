namespace Spells
{
    public class SwainMetamorphism : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 5, 7, 9 };
        int[] effect1 = { 30, 32, 34 };
        int[] effect2 = { 0, 0, 0 };
        public override float AdjustCooldown()
        {
            float returnValue = float.NaN;
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SwainMetamorphism)) == 0)
            {
                returnValue = 0.5f;
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SwainMetamorphism)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.SwainMetamorphism), owner, 0);
            }
            else
            {
                float nextBuffVars_ManaCostInc = effect0[level - 1];
                float nextBuffVars_ManaCost = effect1[level - 1];
                AddBuff(attacker, attacker, new Buffs.SwainMetamorphism(nextBuffVars_ManaCost, nextBuffVars_ManaCostInc), 1, 1, 25000 + effect2[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class SwainMetamorphism : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SwainMetamorphism",
            BuffTextureName = "SwainRavenousFlock.dds",
            NonDispellable = true,
            SpellToggleSlot = 4,
        };
        float manaCost;
        float manaCostInc;
        int ravenID; // UNUSED
        EffectEmitter particle2;
        EffectEmitter particle3;
        float lastTimeExecuted;
        public SwainMetamorphism(float manaCost = default, float manaCostInc = default)
        {
            this.manaCost = manaCost;
            this.manaCostInc = manaCostInc;
        }
        public override void OnActivate()
        {
            bool result;
            //RequireVar(this.manaCost);
            //RequireVar(this.manaCostInc);
            ravenID = PushCharacterData("SwainRaven", owner, false);
            SpellEffectCreate(out _, out _, "swain_metamorph.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            SpellEffectCreate(out particle2, out _, "swain_metamorph_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            SpellEffectCreate(out particle3, out _, "swain_demonForm_idle.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float count = 0;
            float maxMissiles = 3;
            foreach (AttackableUnit unit in GetRandomUnitsInArea((ObjAIBase)owner, owner.Position3D, 625, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 3, default, true))
            {
                result = CanSeeTarget(owner, unit);
                if (result && count < maxMissiles)
                {
                    count++;
                    SpellCast((ObjAIBase)owner, unit, default, default, 0, SpellSlotType.ExtraSlots, level, false, true, false, false, false, false);
                }
            }
            foreach (AttackableUnit unit in GetRandomUnitsInArea((ObjAIBase)owner, owner.Position3D, 625, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, 3, default, true))
            {
                result = CanSeeTarget(owner, unit);
                if (result && count < maxMissiles)
                {
                    count++;
                    SpellCast((ObjAIBase)owner, unit, default, default, 0, SpellSlotType.ExtraSlots, level, false, true, false, false, false, false);
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle2);
            SpellEffectRemove(particle3);
            SpellEffectCreate(out _, out _, "swain_metamorph.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            float buffCheck = 0;
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SwainBeamSelf)) > 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.SwainBeamTransition(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                buffCheck++;
            }
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = 10 * multiplier;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            if (buffCheck == 0)
            {
                PopAllCharacterData(owner);
            }
        }
        public override void OnUpdateActions()
        {
            float count = 0;
            float maxMissiles = 3;
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                bool result;
                float curMana = GetPAR(owner, PrimaryAbilityResourceType.MANA);
                if (manaCost > curMana)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else
                {
                    float negMana = manaCost * -1;
                    IncPAR(owner, negMana, PrimaryAbilityResourceType.MANA);
                }
                manaCost += manaCostInc;
                int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                foreach (AttackableUnit unit in GetRandomUnitsInArea((ObjAIBase)owner, owner.Position3D, 625, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 3, default, true))
                {
                    result = CanSeeTarget(owner, unit);
                    if (result)
                    {
                        if (count < maxMissiles)
                        {
                            count++;
                            SpellCast((ObjAIBase)owner, unit, default, default, 0, SpellSlotType.ExtraSlots, level, false, true, false, false, false, false);
                        }
                    }
                }
                foreach (AttackableUnit unit in GetRandomUnitsInArea((ObjAIBase)owner, owner.Position3D, 625, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, 3, default, true))
                {
                    result = CanSeeTarget(owner, unit);
                    if (result)
                    {
                        if (count < maxMissiles)
                        {
                            count++;
                            SpellCast((ObjAIBase)owner, unit, default, default, 0, SpellSlotType.ExtraSlots, level, false, true, false, false, false, false);
                        }
                    }
                }
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            string spellCastName = GetSpellName(spell);
            if (spellCastName == nameof(Spells.SwainBeam))
            {
                PlayAnimation("Spell1", 0, owner, false, true, false);
            }
        }
    }
}