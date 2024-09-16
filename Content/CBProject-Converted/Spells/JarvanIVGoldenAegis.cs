namespace Spells
{
    public class JarvanIVGoldenAegis : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            ChainMissileParameters = new()
            {
                CanHitCaster = false,
                CanHitSameTarget = false,
                CanHitSameTargetConsecutively = false,
                MaximumHits = 4,
            },
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 50, 90, 130, 170, 210 };
        int[] effect1 = { 20, 25, 30, 35, 40 };
        float[] effect2 = { -0.15f, -0.2f, -0.25f, -0.3f, -0.35f };
        public override void SelfExecute()
        {
            int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float shieldAmount = effect0[level - 1];
            float shieldBonus = effect1[level - 1];
            float bonusShield = 0;
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, default, true))
            {
                bonusShield += shieldBonus;
            }
            float shield = shieldAmount + bonusShield;
            float nextBuffVars_Shield = shield;
            AddBuff(attacker, attacker, new Buffs.JarvanIVGoldenAegis(nextBuffVars_Shield), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            float nextBuffVars_MoveSpeedMod = effect2[level - 1];
            float nextBuffVars_AttackSpeedMod = 0;
            SpellEffectCreate(out _, out _, "JarvanGoldenAegis_nova.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false);
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                AddBuff(attacker, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                SpellEffectCreate(out _, out _, "JarvanGoldenAegis_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, unit, "spine", default, unit, default, default, true, default, default, false);
            }
        }
    }
}
namespace Buffs
{
    public class JarvanIVGoldenAegis : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "JarvanIVGoldenAegis",
            BuffTextureName = "JarvanIV_GoldenAegis.dds",
            OnPreDamagePriority = 3,
            SpellToggleSlot = 2,
        };
        EffectEmitter particle1;
        float shield;
        float oldArmorAmount;
        public JarvanIVGoldenAegis(float shield = default)
        {
            this.shield = shield;
        }
        public override void OnActivate()
        {
            SpellEffectCreate(out particle1, out _, "JarvanGoldenAegis_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots); // UNUSED
            SetBuffToolTipVar(1, shield);
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