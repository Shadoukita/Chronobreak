namespace Spells
{
    public class ViktorGravitonFieldAugment : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 20, 25, 30 };
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 nextBuffVars_TargetPos = targetPos;
            int nextBuffVars_ManaCost = effect0[level - 1]; // UNUSED
            AddBuff(owner, owner, new Buffs.ViktorGravitonField(nextBuffVars_TargetPos), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class ViktorGravitonFieldAugment : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "GlacialStorm",
            BuffTextureName = "Cryophoenix_GlacialStorm.dds",
            SpellToggleSlot = 4,
        };
        Vector3 targetPos;
        EffectEmitter particle;
        EffectEmitter particle2;
        float damageManaTimer;
        float slowTimer;
        int[] effect0 = { 6, 6, 6 };
        float[] effect1 = { -0.28f, -0.32f, -0.36f, -0.4f, -0.44f };
        public ViktorGravitonFieldAugment(Vector3 targetPos = default)
        {
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            //RequireVar(this.manaCost);
            //RequireVar(this.targetPos);
            Vector3 targetPos = this.targetPos;
            TeamId teamOfOwner = GetTeamID_CS(owner);
            SpellEffectCreate(out particle, out particle2, "Viktor_Catalyst_green.troy", "Viktor_Catalyst_green.troy", teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, target, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            float cooldownStat = GetPercentCooldownMod(owner);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float baseCooldown = effect0[level - 1];
            float multiplier = 1 + cooldownStat;
            float newCooldown = baseCooldown * multiplier; // UNUSED
            SpellEffectRemove(particle);
            SpellEffectRemove(particle2);
        }
        public override void OnUpdateActions()
        {
            Vector3 targetPos;
            ObjAIBase caster = GetBuffCasterUnit();
            int level = GetSlotSpellLevel(caster, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (ExecutePeriodically(0.5f, ref damageManaTimer, false))
            {
                float curMana = GetPAR(owner, PrimaryAbilityResourceType.MANA); // UNUSED
                targetPos = this.targetPos;
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 325, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    float nextBuffVars_MovementSpeedMod = effect1[level - 1]; // UNUSED
                    if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.HexMageCrystallize)) == 0)
                    {
                        AddBuff(attacker, unit, new Buffs.HexMageChainReaction(), 100, 1, 1.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.SLOW, 0, true, false, false);
                    }
                }
            }
            if (ExecutePeriodically(0.75f, ref slowTimer, false))
            {
                bool canCast = GetCanCast(owner);
                if (!canCast)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                targetPos = this.targetPos;
                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                Vector3 ownerPos = GetUnitPosition(owner);
                float distance = DistanceBetweenPoints(ownerPos, targetPos);
                if (distance >= 1200)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}