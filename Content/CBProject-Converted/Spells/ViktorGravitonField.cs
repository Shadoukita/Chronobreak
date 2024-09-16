namespace Spells
{
    public class ViktorGravitonField : SpellScript
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
    public class ViktorGravitonField : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "GlacialStorm",
            BuffTextureName = "Cryophoenix_GlacialStorm.dds",
            IsDeathRecapSource = true,
            SpellToggleSlot = 4,
        };
        Vector3 targetPos;
        EffectEmitter particle;
        EffectEmitter particle2;
        float damageManaTimer;
        int[] effect0 = { 6, 6, 6 };
        float[] effect1 = { -0.28f, -0.32f, -0.36f, -0.4f, -0.44f };
        public ViktorGravitonField(Vector3 targetPos = default)
        {
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            //RequireVar(this.manaCost);
            //RequireVar(this.targetPos);
            Vector3 targetPos = this.targetPos;
            TeamId teamOfOwner = GetTeamID_CS(owner);
            int ownerSkinID = GetSkinID(owner);
            if (ownerSkinID == 1)
            {
                SpellEffectCreate(out particle, out particle2, "Viktor_Catalyst_Fullmachine_green.troy", "Viktor_Catalyst_Fullmachine_red.troy", teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, target, default, default, false, false, false, false, false);
            }
            else if (ownerSkinID == 2)
            {
                SpellEffectCreate(out particle, out particle2, "Viktor_Catalyst_Prototype_green.troy", "Viktor_Catalyst_Prototype_red.troy", teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, target, default, default, false, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out particle, out particle2, "Viktor_Catalyst_green.troy", "Viktor_Catalyst_red.troy", teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, target, default, default, false, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            float cooldownStat = GetPercentCooldownMod(owner);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            TeamId ownerTeamID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "viktor_gravitonfield_deactivate_sound.troy", default, ownerTeamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, default, default, targetPos, true, false, false, false, false);
            float baseCooldown = effect0[level - 1];
            float multiplier = 1 + cooldownStat;
            float newCooldown = baseCooldown * multiplier; // UNUSED
            SpellEffectRemove(particle);
            SpellEffectRemove(particle2);
        }
        public override void OnUpdateActions()
        {
            ObjAIBase caster = GetBuffCasterUnit();
            int level = GetSlotSpellLevel(caster, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (ExecutePeriodically(0.5f, ref damageManaTimer, false))
            {
                float curMana = GetPAR(owner, PrimaryAbilityResourceType.MANA); // UNUSED
                Vector3 targetPos = this.targetPos;
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 325, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    float nextBuffVars_MovementSpeedMod = effect1[level - 1];
                    if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.ViktorGravitonFieldStun)) == 0)
                    {
                        AddBuff(attacker, unit, new Buffs.ViktorGravitonFieldDebuff(nextBuffVars_MovementSpeedMod), 100, 1, 1.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.SLOW, 0, true, false, false);
                    }
                }
            }
        }
    }
}