namespace Spells
{
    public class TrundleQ : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = { 0.8f, 0.9f, 1, 1.1f, 1.2f };
        int[] effect1 = { 30, 45, 60, 75, 90 };
        int[] effect2 = { 20, 25, 30, 35, 40 };
        float[] effect3 = { -10, -12.5f, -15, -17.5f, -20 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (hitResult == HitResult.HIT_Critical)
            {
                hitResult = HitResult.HIT_Normal;
            }
            int level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float scaling = effect0[level - 1];
            Vector3 attackerPos = GetUnitPosition(attacker); // UNUSED
            Vector3 targetPos = GetUnitPosition(target);
            float distance = DistanceBetweenObjects(attacker, target);
            targetPos = GetPointByUnitFacingOffset(attacker, 50, 0);
            float bonusDamage = effect1[level - 1];
            float totalDamage = GetTotalAttackDamage(attacker);
            float scaledDamage = scaling * totalDamage;
            float dtD = scaledDamage + bonusDamage;
            ApplyDamage(attacker, target, dtD, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, true, attacker);
            int nextBuffVars_SapVar = effect2[level - 1];
            float nextBuffVars_NegSapVar = effect3[level - 1];
            SpellEffectCreate(out _, out _, "globalhit_physical.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, default, default, false, false);
            AddBuff(attacker, target, new Buffs.TrundleQDebuff(nextBuffVars_SapVar, nextBuffVars_NegSapVar), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            AddBuff(attacker, owner, new Buffs.UnlockAnimation(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            Move(attacker, targetPos, 100, 0, 25, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 50, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            if (distance >= 75)
            {
                PlayAnimation("Spell1a", 0, attacker, false, true, true);
            }
            else
            {
                PlayAnimation("Spell1", 0, attacker, false, true, true);
            }
        }
    }
}
namespace Buffs
{
    public class TrundleQ : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "TrundleQ",
            BuffTextureName = "Trundle_Bite.dds",
        };
        float sapVar;
        EffectEmitter rh;
        public TrundleQ(float sapVar = default)
        {
            this.sapVar = sapVar;
        }
        public override void OnActivate()
        {
            //RequireVar(this.sapVar);
            IncFlatPhysicalDamageMod(owner, sapVar);
            SpellEffectCreate(out rh, out _, "TrundleQ_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_WEAPON_1", default, owner, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(rh);
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, sapVar);
        }
    }
}