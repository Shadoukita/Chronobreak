namespace Spells
{
    public class MaokaiUnstableGrowth : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 80, 115, 150, 185, 220 };
        float[] effect1 = { 1, 1.25f, 1.5f, 1.75f, 2 };
        public override bool CanCast()
        {
            bool returnValue = true;
            bool canMove = GetCanMove(owner);
            bool canCast = GetCanCast(owner);
            if (!canMove)
            {
                returnValue = false;
            }
            if (!canCast)
            {
                returnValue = false;
            }
            return returnValue;
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int nextBuffVars_BaseDamage = effect0[level - 1];
            float nextBuffVars_RootDuration = effect1[level - 1];
            AddBuff((ObjAIBase)target, owner, new Buffs.MaokaiUnstableGrowth(nextBuffVars_BaseDamage, nextBuffVars_RootDuration), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class MaokaiUnstableGrowth : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MaokaiUnstableGrowth",
            BuffTextureName = "XenZhao_CrescentSweepNew.dds",
        };
        float baseDamage;
        float rootDuration;
        EffectEmitter particle;
        Region unitPerceptionBubble;
        int[] effect0 = { 30, 45, 60, 75, 90 };
        public MaokaiUnstableGrowth(float baseDamage = default, float rootDuration = default)
        {
            this.baseDamage = baseDamage;
            this.rootDuration = rootDuration;
        }
        public override void OnActivate()
        {
            ObjAIBase caster = GetBuffCasterUnit();
            //RequireVar(this.baseDamage);
            //RequireVar(this.rootDuration);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetCanMove(owner, false);
            SetCanAttack(owner, false);
            SetForceRenderParticles(owner, true);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            PlayAnimation("Spell2c", 0, owner, true, true, true);
            TeamId teamOfOwner = GetTeamID_CS(owner);
            SpellEffectCreate(out particle, out _, "maokai_elementalAdvance_mis.troy", default, teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, false, false);
            unitPerceptionBubble = AddUnitPerceptionBubble(teamOfOwner, 10, caster, 5, default, caster, false);
            MoveToUnit(owner, caster, 1300, 0, ForceMovementOrdersType.POSTPONE_CURRENT_ORDER, 0, 2000, 0, 0);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(unitPerceptionBubble);
            SetTargetable(owner, true);
            SetGhosted(owner, false);
            SetCanMove(owner, true);
            SetCanAttack(owner, true);
            SetForceRenderParticles(owner, false);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            if (!IsDead(owner))
            {
                UnlockAnimation(owner, false);
                PlayAnimation("Spell2b", 0.25f, owner, false, true, false);
            }
            SpellEffectRemove(particle);
            ObjAIBase caster = GetBuffCasterUnit(); // UNUSED
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            int nextBuffVars_DefensiveBonus = effect0[level - 1]; // UNUSED
            StopMoveBlock(owner);
        }
        public override void OnMoveEnd()
        {
            SpellBuffRemoveCurrent(owner);
        }
        public override void OnMoveSuccess()
        {
            ObjAIBase caster = GetBuffCasterUnit();
            float baseDamage = this.baseDamage;
            BreakSpellShields(caster);
            ApplyDamage((ObjAIBase)owner, caster, baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.8f, 0, false, false, (ObjAIBase)owner);
            AddBuff((ObjAIBase)owner, caster, new Buffs.MaokaiUnstableGrowthRoot(), 1, 1, rootDuration, BuffAddType.REPLACE_EXISTING, BuffType.CHARM, 0, true, true, false);
            SpellBuffRemoveCurrent(owner);
            if (caster is Champion && caster.Team != owner.Team)
            {
                IssueOrder(owner, OrderType.AttackTo, default, caster);
            }
        }
    }
}