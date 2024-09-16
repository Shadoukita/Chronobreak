namespace Spells
{
    public class SkarnerImpale : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 100, 150, 200, 0, 0 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float suppressionDuration = 1.75f;
            AttackableUnit nextBuffVars_Victim = target; // UNUSED
            AddBuff(attacker, target, new Buffs.SkarnerImpale(), 1, 1, suppressionDuration, BuffAddType.REPLACE_EXISTING, BuffType.SUPPRESSION, 0, true, false, false);
            AddBuff((ObjAIBase)target, owner, new Buffs.SkarnerImpaleBuff(), 1, 1, suppressionDuration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            float damagePerTick = effect0[level - 1];
            ApplyDamage(attacker, target, damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.5f, 0, false, false, attacker);
            float hP = GetHealth(target, PrimaryAbilityResourceType.MANA);
            if (hP > 0)
            {
                IssueOrder(owner, OrderType.Hold, default, owner);
                Vector3 pos = GetPointByUnitFacingOffset(owner, 100, 180);
                FaceDirection(owner, pos);
                PlayAnimation("Spell4_Idleback", 0, owner, false, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class SkarnerImpale : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SkarnerImpale",
            BuffTextureName = "SkarnerImpale.dds",
            PopupMessage = new[] { "game_floatingtext_Suppressed", },
        };
        int numHitsRemaining;
        EffectEmitter chainPartID;
        EffectEmitter zParticle;
        EffectEmitter cParticle;
        EffectEmitter crystalineParticle;
        Region victimBubble;
        float lastTimeExecuted;
        int[] effect0 = { 100, 150, 200, 0, 0 };
        public override void OnActivate()
        {
            TeamId ownerTeamID = GetTeamID_CS(attacker);
            SetCanAttack(attacker, false);
            SetStunned(owner, true);
            SetSuppressed(owner, true);
            PauseAnimation(owner, true);
            int level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level > 0)
            {
                SealSpellSlot(2, SpellSlotType.SpellSlots, attacker, true, SpellbookType.SPELLBOOK_CHAMPION);
            }
            string flashCheck = GetSlotSpellName(attacker, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if (flashCheck == nameof(Spells.SummonerFlash))
            {
                SealSpellSlot(0, SpellSlotType.SpellSlots, attacker, true, SpellbookType.SPELLBOOK_SUMMONER);
            }
            flashCheck = GetSlotSpellName(attacker, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if (flashCheck == nameof(Spells.SummonerFlash))
            {
                SealSpellSlot(1, SpellSlotType.SpellSlots, attacker, true, SpellbookType.SPELLBOOK_SUMMONER);
            }
            OverrideAnimation("Run", "Spell4_Backstep", attacker);
            OverrideAnimation("Idle1", "Spell4_Idleback", attacker);
            OverrideAnimation("Idle2", "Spell4_Idleback", attacker);
            OverrideAnimation("Idle3", "Spell4_Idleback", attacker);
            OverrideAnimation("Idle4", "Spell4_Idleback", attacker);
            OverrideAnimation("Spell2", "spell4_W", attacker);
            OverrideAnimation("Spell1", "spell4_Q", attacker);
            numHitsRemaining = 4;
            SpellEffectCreate(out chainPartID, out _, "skarner_ult_beam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, owner, "spine", default, attacker, "tail_t", default, false, false, false, false, false);
            SpellEffectCreate(out zParticle, out _, "skarner_ult_tail_tip.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, attacker, "tail_t", default, attacker, "Bird_head", default, false, false, false, false, false);
            SpellEffectCreate(out cParticle, out _, "skarner_ult_tar_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, owner, "spine", default, attacker, "Bird_head", default, false, false, false, false, false);
            SpellEffectCreate(out crystalineParticle, out _, "skarner_ult_tar_04.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, owner, "spine", default, attacker, "Bird_head", default, false, false, false, false, false);
            victimBubble = AddUnitPerceptionBubble(ownerTeamID, 10, owner, 2, default, owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(chainPartID);
            SpellEffectRemove(cParticle);
            SpellEffectRemove(zParticle);
            SpellEffectRemove(crystalineParticle);
            PauseAnimation(owner, false);
            SetCanAttack(attacker, true);
            SetStunned(owner, false);
            SetSuppressed(owner, false);
            int level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level > 0)
            {
                SealSpellSlot(0, SpellSlotType.SpellSlots, attacker, false, SpellbookType.SPELLBOOK_CHAMPION);
            }
            level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level > 0)
            {
                SealSpellSlot(1, SpellSlotType.SpellSlots, attacker, false, SpellbookType.SPELLBOOK_CHAMPION);
            }
            level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level > 0)
            {
                SealSpellSlot(2, SpellSlotType.SpellSlots, attacker, false, SpellbookType.SPELLBOOK_CHAMPION);
            }
            string flashCheck = GetSlotSpellName(attacker, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if (flashCheck == nameof(Spells.SummonerFlash))
            {
                SealSpellSlot(0, SpellSlotType.SpellSlots, attacker, false, SpellbookType.SPELLBOOK_SUMMONER);
            }
            flashCheck = GetSlotSpellName(attacker, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if (flashCheck == nameof(Spells.SummonerFlash))
            {
                SealSpellSlot(1, SpellSlotType.SpellSlots, attacker, false, SpellbookType.SPELLBOOK_SUMMONER);
            }
            ClearOverrideAnimation("Run", attacker);
            ClearOverrideAnimation("Idle1", attacker);
            ClearOverrideAnimation("Idle2", attacker);
            ClearOverrideAnimation("Idle3", attacker);
            ClearOverrideAnimation("Idle4", attacker);
            ClearOverrideAnimation("Spell2", attacker);
            ClearOverrideAnimation("Spell1", attacker);
            level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float damagePerTick = effect0[level - 1];
            float duration = GetBuffRemainingDuration(owner, nameof(Buffs.SkarnerImpale));
            if (duration <= 0)
            {
                ApplyDamage(attacker, owner, damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.5f, 0, false, false, attacker);
                TeamId teamID = GetTeamID_CS(attacker);
                SpellEffectCreate(out _, out _, "skarner_ult_tar_03.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, owner, default, default, owner, default, default, true, false, false, false, false);
            }
            RemovePerceptionBubble(victimBubble);
            SpellBuffClear(attacker, nameof(Buffs.SkarnerImpaleBuff));
            float hP = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            if (hP > 0)
            {
                Vector3 pos = GetPointByUnitFacingOffset(attacker, 100, 180);
                FaceDirection(attacker, pos);
                PlayAnimation("Run", 0, attacker, false, false, false);
            }
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(attacker, false);
            SetStunned(owner, true);
            SetSuppressed(owner, true);
        }
        /*
        //TODO: Uncomment and fix
        public override void OnUpdateActions()
        {
            float damageTime; // UNITIALIZED
            PauseAnimation(owner, true);
            Vector3 pos = GetPointByUnitFacingOffset(attacker, -75, 0);
            float distance = DistanceBetweenObjectAndPoint(owner, pos);
            float mS = distance * 2.6f;
            Move(owner, pos, mS, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 0, ForceMovementOrdersFacing.KEEP_CURRENT_FACING);
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, true, damageTime))
            {
                TeamId teamID = GetTeamID(attacker); // UNUSED
                if(this.numHitsRemaining <= 0)
                {
                    SpellBuffRemoveCurrent(attacker);
                }
                if(IsDead(owner))
                {
                    SpellBuffRemoveCurrent(attacker);
                }
            }
            string flashCheck = GetSlotSpellName(attacker, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if(flashCheck == nameof(Spells.SummonerFlash))
            {
                SealSpellSlot(0, SpellSlotType.SpellSlots, attacker, true, SpellbookType.SPELLBOOK_SUMMONER);
            }
            flashCheck = GetSlotSpellName(attacker, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if(flashCheck == nameof(Spells.SummonerFlash))
            {
                SealSpellSlot(1, SpellSlotType.SpellSlots, attacker, true, SpellbookType.SPELLBOOK_SUMMONER);
            }
        }
        */
    }
}