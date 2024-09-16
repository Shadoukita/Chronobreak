namespace Spells
{
    public class ViktorPowerTransfer : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 16f, 14f, 12f, 10f, 8f, },
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = { 80, 125, 170, 215, 260 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(attacker); // UNUSED
            AddBuff(attacker, owner, new Buffs.ViktorPowerTransfer(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float baseDamage = effect0[level - 1];
            float aPVAL = GetFlatMagicDamageMod(owner);
            float aPBONUS = aPVAL * 0.65f;
            charVars.TotalDamage = aPBONUS + baseDamage;
            if (target is Champion)
            {
                charVars.IsChampTarget = true;
                ApplyDamage(attacker, target, charVars.TotalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 1, false, false, attacker);
            }
            else
            {
                charVars.IsChampTarget = true;
                ApplyDamage(attacker, target, charVars.TotalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 1, false, false, attacker);
            }
            Vector3 targetPos = GetUnitPosition(target);
            SpellCast(owner, owner, default, default, 2, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, true, targetPos);
        }
    }
}
namespace Buffs
{
    public class ViktorPowerTransfer : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}