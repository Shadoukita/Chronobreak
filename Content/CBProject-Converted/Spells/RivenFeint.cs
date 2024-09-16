namespace Spells
{
    public class RivenFeint : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        EffectEmitter temp_; // UNUSED
        int[] effect0 = { 60, 90, 120, 150, 180 };
        float[] effect1 = { 2.5f, 2.5f, 2.5f, 2.5f, 2.5f };
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            FaceDirection(owner, targetPos);
            float distance = DistanceBetweenObjectAndPoint(owner, targetPos); // UNUSED
            Vector3 pos = GetPointByUnitFacingOffset(owner, 250, 0);
            float baseMS = GetFlatMovementSpeedMod(owner);
            float bonusMS = baseMS + 650;
            PlayAnimation("Spell3", 0, owner, false, true, false);
            Move(owner, pos, 900 + bonusMS, 0, 0, ForceMovementType.FIRST_WALL_HIT, ForceMovementOrdersType.CANCEL_ORDER, 325, ForceMovementOrdersFacing.KEEP_CURRENT_FACING);
            float baseDamageBlock = effect0[level - 1];
            float totalAD = GetTotalAttackDamage(owner);
            float baseAD = GetBaseAttackDamage(owner);
            float bonusAD = totalAD - baseAD;
            float bonusHealth = bonusAD * 1;
            float damageBlock = baseDamageBlock + bonusHealth;
            float nextBuffVars_DamageBlock = damageBlock;
            AddBuff(owner, owner, new Buffs.RivenFeint(nextBuffVars_DamageBlock), 1, 1, effect1[level - 1], BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            SpellEffectCreate(out temp_, out _, "exile_E_mis.troy  ", "exile_E_mis.troy  ", TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class RivenFeint : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "exile_E_shield_01.troy", },
            BuffName = "RivenFeint",
            BuffTextureName = "RivenPathoftheExile.dds",
            OnPreDamagePriority = 3,
        };
        float damageBlock;
        float oldArmorAmount;
        public RivenFeint(float damageBlock = default)
        {
            this.damageBlock = damageBlock;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageBlock);
            IncreaseShield(owner, damageBlock, true, true);
        }
        public override void OnDeactivate(bool expired)
        {
            if (damageBlock > 0)
            {
                RemoveShield(owner, damageBlock, true, true);
            }
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            oldArmorAmount = damageBlock;
            if (damageBlock >= damageAmount)
            {
                damageBlock -= damageAmount;
                damageAmount = 0;
                oldArmorAmount -= damageBlock;
                ReduceShield(owner, oldArmorAmount, true, true);
            }
            else
            {
                TeamId teamID = GetTeamID_CS(owner);
                damageAmount -= damageBlock;
                damageBlock = 0;
                ReduceShield(owner, oldArmorAmount, true, true);
                SpellEffectCreate(out _, out _, "exile_E_interupt.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnMoveEnd()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RivenTriCleave)) == 0)
            {
                UnlockAnimation(owner, true);
            }
        }
    }
}