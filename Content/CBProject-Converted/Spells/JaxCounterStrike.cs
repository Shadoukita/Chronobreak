namespace Spells
{
    public class JaxCounterStrike : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 0, 0, 0, 0, 0 };
        float[] effect1 = { 1.5f, 1.5f, 1.5f, 1.5f, 1.5f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            PlayAnimation("Spell3", 0, owner, false, false, false);
            charVars.NumCounter = effect0[level - 1];
            AddBuff(attacker, target, new Buffs.JaxCounterStrike(), 1, 1, effect1[level - 1], BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class JaxCounterStrike : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "JaxDodger.troy", },
            BuffName = "JaxEvasion",
            BuffTextureName = "Armsmaster_Disarm.dds",
            OnPreDamagePriority = 3,
        };
        EffectEmitter particle;
        int[] effect0 = { 10, 15, 20, 25, 30 };
        public override void OnActivate()
        {
            TeamId teamID; // UNITIALIZED
            teamID = TeamId.TEAM_UNKNOWN; //TODO: Verify
            SpellEffectCreate(out particle, out _, "JaxDodger.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, owner, "head", default, false, false, false, false, false);
            //RequireVar(this.movementSpeedBonusPercent);
            charVars.NumCounter = 0;
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            if (!IsDead(owner))
            {
                SpellCast((ObjAIBase)owner, owner, owner.Position3D, owner.Position3D, 3, SpellSlotType.ExtraSlots, 0, false, true, false, false, false, false);
            }
        }
        public override void OnUpdateStats()
        {
            IncFlatDodgeMod(owner, 100);
        }
        /*
        public override void OnBeingHit(ObjAIBase attacker, float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(attacker is Champion)
            {
            }
            if(damageType == DamageSource.DAMAGE_SOURCE_ATTACK)
            {
                DebugSay(owner, "YO!");
            }
        }
        */
        public override void OnDodge(AttackableUnit attacker)
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float minionBonus = effect0[level - 1];
            float championBonus = effect0[level - 1];
            if (attacker is Champion)
            {
                charVars.NumCounter += championBonus;
            }
            else
            {
                charVars.NumCounter += minionBonus;
            }
        }
    }
}