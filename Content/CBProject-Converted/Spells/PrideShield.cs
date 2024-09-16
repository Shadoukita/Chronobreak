namespace Spells
{
    public class PrideShield : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float aP = GetFlatMagicDamageMod(owner);
            float aPBonus = aP * 1.5f;
            float shieldHealth = aPBonus + 200;
            float nextBuffVars_ShieldHealth = shieldHealth;
            AddBuff(owner, owner, new Buffs.PrideShield(nextBuffVars_ShieldHealth), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 1);
            string name = GetSlotSpellName(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name1 = GetSlotSpellName(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name2 = GetSlotSpellName(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name3 = GetSlotSpellName(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name4 = GetSlotSpellName(owner, 4, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name5 = GetSlotSpellName(owner, 5, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            if (name == nameof(Spells.PrideShield))
            {
                SetSlotSpellCooldownTimeVer2(45, 0, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            }
            if (name1 == nameof(Spells.PrideShield))
            {
                SetSlotSpellCooldownTimeVer2(45, 1, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            }
            if (name2 == nameof(Spells.PrideShield))
            {
                SetSlotSpellCooldownTimeVer2(45, 2, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            }
            if (name3 == nameof(Spells.PrideShield))
            {
                SetSlotSpellCooldownTimeVer2(45, 3, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            }
            if (name4 == nameof(Spells.PrideShield))
            {
                SetSlotSpellCooldownTimeVer2(45, 4, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            }
            if (name5 == nameof(Spells.PrideShield))
            {
                SetSlotSpellCooldownTimeVer2(45, 5, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            }
        }
    }
}
namespace Buffs
{
    public class PrideShield : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", "", "", },
            BuffName = "RazzlesPride",
            BuffTextureName = "082_Rune_of_Rebirth.dds",
        };
        float shieldHealth;
        float initialHealth;
        bool _100Destroyed;
        bool _66Destroyed;
        EffectEmitter particle1;
        EffectEmitter particle2;
        EffectEmitter particle3;
        public PrideShield(float shieldHealth = default)
        {
            this.shieldHealth = shieldHealth;
        }
        public override void OnActivate()
        {
            //RequireVar(this.shieldHealth);
            initialHealth = shieldHealth;
            _100Destroyed = false;
            _66Destroyed = false;
            SpellEffectCreate(out particle1, out _, "razzlespride_100.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            SpellEffectCreate(out particle2, out _, "razzlespride_66.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            SpellEffectCreate(out particle3, out _, "razzlespride_33.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            SetBuffToolTipVar(1, shieldHealth);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle3);
            if (!_66Destroyed)
            {
                SpellEffectRemove(particle2);
            }
            if (!_100Destroyed)
            {
                SpellEffectRemove(particle1);
            }
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (shieldHealth >= damageAmount)
            {
                shieldHealth -= damageAmount;
                damageAmount = 0;
                SetBuffToolTipVar(1, shieldHealth);
                float percentRemain = shieldHealth / initialHealth;
                if (!_66Destroyed)
                {
                    if (percentRemain <= 0.33f)
                    {
                        _66Destroyed = true;
                        SpellEffectRemove(particle2);
                    }
                }
                if (!_100Destroyed)
                {
                    if (percentRemain <= 0.66f)
                    {
                        SpellEffectRemove(particle1);
                        _100Destroyed = true;
                    }
                }
            }
            else
            {
                damageAmount -= shieldHealth;
                shieldHealth = 0;
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}