namespace Spells
{
    public class ViktorPowerTransferReturn : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 16f, 14f, 12f, 10f, 8f, },
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = { 40, 65, 90, 115, 140 };
        float[] effect1 = { 0.5f, 0.5f, 0.5f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = base.level;
            TeamId teamID = GetTeamID_CS(attacker); // UNUSED
            float pAR = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
            float baseDamage = effect0[level - 1];
            level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float bonusDamage = pAR * 0.08f;
            float totalDamage = bonusDamage + baseDamage;
            float aoEDamage = effect1[level - 1];
            aoEDamage *= totalDamage;
            TeamId ownerTeam = GetTeamID_CS(owner); // UNUSED
            TeamId targetTeam = GetTeamID_CS(target); // UNUSED
            if (owner.Team != target.Team)
            {
                ApplyDamage(attacker, target, totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.2f, 1, false, false, attacker);
            }
            else
            {
                AddBuff(attacker, target, new Buffs.ViktorPowerTransferReturn(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class ViktorPowerTransferReturn : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Viktor_Reverb_shield.troy", },
            BuffName = "ViktorShield",
            BuffTextureName = "ViktorPowerTransfer.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            if (charVars.IsChampTarget)
            {
                charVars.TotalDamage *= 0.4f;
                IncreaseShield(owner, charVars.TotalDamage, true, true);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            RemoveShield(owner, 10000, true, true);
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (damageAmount > charVars.TotalDamage)
            {
                damageAmount -= charVars.TotalDamage;
                ReduceShield(owner, damageAmount, true, true);
                RemoveShield(owner, 0, true, true);
                SpellBuffRemove(owner, default, (ObjAIBase)owner, 0);
            }
            else
            {
                ReduceShield(owner, damageAmount, true, true);
                charVars.TotalDamage -= damageAmount;
                damageAmount = 0;
            }
        }
    }
}