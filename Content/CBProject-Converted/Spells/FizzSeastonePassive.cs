namespace Spells
{
    public class FizzSeastonePassive : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            AddBuff(owner, owner, new Buffs.FizzSeastonePassive(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, false, false, false);
        }
    }
}
namespace Buffs
{
    public class FizzSeastonePassive : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "BUFFBONE_CSTM_WEAPON_1", "Chest", },
            AutoBuffActivateEffect = new[] { "Fizz_SeastonePassive_Weapon.troy", "Fizz_SeastonePassive.troy", },
            BuffName = "FizzSeastoneActive",
            BuffTextureName = "FizzSeastoneActive.dds",
            SpellToggleSlot = 2,
        };
        int[] effect0 = { 10, 15, 20, 25, 30 };
        public override void OnActivate()
        {
            SetSlotSpellIcon(1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, 2);
        }
        public override void OnDeactivate(bool expired)
        {
            SetSlotSpellIcon(1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, 1);
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is ObjAIBase && target is not BaseTurret)
            {
                AddBuff((ObjAIBase)target, target, new Buffs.Internal_50MS(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff(attacker, target, new Buffs.GrievousWound(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                int level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float magicDamageAmount = effect0[level - 1];
                float selfAP = GetFlatMagicDamageMod(owner);
                float aPBonus = selfAP * 0.35f;
                magicDamageAmount += aPBonus;
                ApplyDamage(attacker, target, magicDamageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
                TeamId teamID = GetTeamID_CS(owner);
                SpellEffectCreate(out _, out _, "fizz_seastoneactive_hit_sound.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            }
        }
    }
}