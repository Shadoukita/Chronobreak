namespace Spells
{
    public class KarthusDefile: Defile {}
    public class Defile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Defile)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.Defile), owner, 0);
            }
            else
            {
                AddBuff(attacker, target, new Buffs.Defile(), 1, 1, 30000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class Defile : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "L_weapon", "", },
            AutoBuffActivateEffect = new[] { "Defile_glow.troy", "", },
            BuffName = "Defile",
            BuffTextureName = "Lich_Defile.dds",
            PersistsThroughDeath = true,
            SpellToggleSlot = 3,
        };
        EffectEmitter particle;
        EffectEmitter particle2;
        float lastTimeExecuted;
        int[] effect0 = { 30, 50, 70, 90, 110 };
        int[] effect1 = { 30, 42, 54, 66, 78 };
        public override void OnActivate()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float damageToDeal = effect0[level - 1];
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 550, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.25f, 1, false, false, attacker);
            }
            TeamId teamOfOwner = GetTeamID_CS(owner);
            SpellEffectCreate(out particle, out particle2, "Defile_green_cas.troy", "Defile_red_cas.troy", teamOfOwner, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SpellEffectRemove(particle2);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.DeathDefiedBuff)) == 0)
                {
                    float manaCost = effect1[level - 1];
                    float ownerMana = GetPAR(owner, PrimaryAbilityResourceType.MANA);
                    if (ownerMana < manaCost)
                    {
                        SpellBuffRemoveCurrent(owner);
                    }
                    else
                    {
                        float negManaCost = -1 * manaCost;
                        IncPAR(owner, negManaCost, PrimaryAbilityResourceType.MANA);
                    }
                }
                float damageToDeal = effect0[level - 1];
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 550, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.25f, 1, false, false, attacker);
                }
            }
        }
    }
}