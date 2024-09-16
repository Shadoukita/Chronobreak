namespace Spells
{
    public class Feast : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            SpellFXOverrideSkins = new[] { "DandyChogath", },
            SpellVOOverrideSkins = new[] { "DandyChogath", },
        };
        int[] effect0 = { 0, 0, 0 };
        int[] effect1 = { 300, 475, 650 };
        int[] effect2 = { 1000, 1000, 1000 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float feastBase;
            int count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.Feast)); // UNUSED
            int healthPerStack = effect0[level - 1]; // UNUSED
            if (target is Champion)
            {
                feastBase = effect1[level - 1];
            }
            else
            {
                feastBase = effect2[level - 1];
            }
            float abilityPower = GetFlatMagicDamageMod(owner);
            float halfAbilityPower = abilityPower * 0.7f;
            float feastHealth = halfAbilityPower + feastBase;
            float targetHealth = GetHealth(target, PrimaryAbilityResourceType.MANA);
            if (feastHealth >= targetHealth)
            {
                ApplyDamage(attacker, target, targetHealth, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 0, false, false, attacker);
                SpellEffectCreate(out _, out _, "chogath_feast_sign.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, false, false, false, false);
                if (IsDead(target))
                {
                    AddBuff(owner, owner, new Buffs.Feast(), 6, 1, 30000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
                    AddBuff(owner, owner, new Buffs.Feast_internal(), 1, 1, 30000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
            else
            {
                ApplyDamage(attacker, target, feastHealth, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 1, false, false, attacker);
            }
        }
    }
}
namespace Buffs
{
    public class Feast : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Feast",
            BuffTextureName = "GreenTerror_Feast.dds",
            PersistsThroughDeath = true,
        };
        int[] effect0 = { 90, 120, 150 };
        public override void OnActivate()
        {
            int count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.Feast));
            int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float healthPerStack = effect0[level - 1];
            float bonusHealth = healthPerStack * count;
            SetBuffToolTipVar(1, bonusHealth);
            if (count == 1)
            {
                OverrideAnimation("Run", "Run1", owner);
            }
            else if (count == 2)
            {
                OverrideAnimation("Run", "Run2", owner);
            }
            else if (count == 3)
            {
                OverrideAnimation("Run", "Run3", owner);
            }
            else if (count == 4)
            {
                OverrideAnimation("Run", "Run4", owner);
            }
            else if (count == 5)
            {
                OverrideAnimation("Run", "Run5", owner);
            }
            else if (count == 6)
            {
                OverrideAnimation("Run", "Run6", owner);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            int count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.Feast));
            int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float healthPerStack = effect0[level - 1];
            float bonusHealth = healthPerStack * count;
            SetBuffToolTipVar(1, bonusHealth);
            OverrideAnimation("Run", "Run", owner);
        }
        public override void OnLevelUpSpell(int slot)
        {
            int count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.Feast));
            int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float healthPerStack = effect0[level - 1];
            float bonusHealth = healthPerStack * count;
            SetBuffToolTipVar(1, bonusHealth);
        }
    }
}