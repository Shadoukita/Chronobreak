namespace Spells
{
    public class UrgotTerrorCapacitorActive2 : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            ChainMissileParameters = new()
            {
                CanHitCaster = false,
                CanHitEnemies = true,
                CanHitFriends = false,
                CanHitSameTarget = false,
                CanHitSameTargetConsecutively = false,
                MaximumHits = 4,
            },
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 80, 140, 200, 260, 320 };
        public override void SelfExecute()
        {
            int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float shieldAmount = effect0[level - 1];
            float abilityPower = GetFlatMagicDamageMod(owner);
            float bonusShield = abilityPower * 0.8f;
            float shield = shieldAmount + bonusShield;
            float nextBuffVars_Shield = shield;
            AddBuff(attacker, attacker, new Buffs.UrgotTerrorCapacitorActive2(nextBuffVars_Shield), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class UrgotTerrorCapacitorActive2 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "UrgotTerrorCapacitor",
            BuffTextureName = "UrgotTerrorCapacitor.dds",
            OnPreDamagePriority = 3,
            DoOnPreDamageInExpirationOrder = true,
            SpellToggleSlot = 2,
        };
        float shield;
        EffectEmitter particle1;
        float oldArmorAmount;
        int[] effect0 = { 20, 25, 30, 35, 40 };
        float[] effect1 = { -0.2f, -0.25f, -0.3f, -0.35f, -0.4f };
        public UrgotTerrorCapacitorActive2(float shield = default)
        {
            this.shield = shield;
        }
        public override void OnActivate()
        {
            //RequireVar(this.shield);
            SpellEffectCreate(out particle1, out _, "UrgotTerrorCapacitor_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float slowPercent = effect0[level - 1];
            SetBuffToolTipVar(1, shield);
            SetBuffToolTipVar(2, slowPercent);
            IncreaseShield(owner, shield, true, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle1);
            if (shield > 0)
            {
                RemoveShield(owner, shield, true, true);
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is ObjAIBase && target is not BaseTurret && !IsDead(target) && hitResult != HitResult.HIT_Miss)
            {
                int level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float nextBuffVars_MoveSpeedMod = effect1[level - 1]; // UNUSED
                AddBuff(attacker, target, new Buffs.UrgotSlow(), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
            }
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            oldArmorAmount = shield;
            if (shield >= damageAmount)
            {
                shield -= damageAmount;
                damageAmount = 0;
                oldArmorAmount -= shield;
                ReduceShield(owner, oldArmorAmount, true, true);
            }
            else
            {
                damageAmount -= shield;
                shield = 0;
                ReduceShield(owner, oldArmorAmount, true, true);
                SpellBuffRemoveCurrent(owner);
            }
            SetBuffToolTipVar(1, shield);
        }
    }
}