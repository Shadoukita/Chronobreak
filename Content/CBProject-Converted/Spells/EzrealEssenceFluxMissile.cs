namespace Spells
{
    public class EzrealEssenceFluxMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            SpellFXOverrideSkins = new[] { "CyberEzreal", },
        };
        int[] effect0 = { 0, 0, 0, 0, 0 };
        float[] effect1 = { 0.2f, 0.25f, 0.3f, 0.35f, 0.4f };
        int[] effect3 = { 80, 130, 180, 230, 280 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = base.level;
            AddBuff(attacker, attacker, new Buffs.EzrealRisingSpellForce(), 5, 1, 6 + effect0[level - 1], BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float abilityPower = GetFlatMagicDamageMod(attacker);
            float abilityPowerMod = abilityPower * 0.7f;
            TeamId casterID = GetTeamID_CS(attacker);
            TeamId casterID2 = GetTeamID_CS(target);
            SpellEffectCreate(out _, out _, "Ezreal_essenceflux_tar.troy", default, casterID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "root", default, target, default, default, true, false, false, false, false);
            level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float attackSpeedMod = effect1[level - 1]; // UNUSED
            float nextBuffVars_AttackSpeedMod = effect1[level - 1];
            if (casterID == casterID2)
            {
                ApplyAssistMarker(attacker, target, 10);
                AddBuff(attacker, target, new Buffs.EzrealEssenceFlux(nextBuffVars_AttackSpeedMod), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            else
            {
                float nextBuffVars_AttackSpeedModNegative = attackSpeedMod * -1;
                BreakSpellShields(target);
                ApplyDamage(attacker, target, effect3[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 1, false, false, attacker);
                AddBuff(attacker, target, new Buffs.EzrealEssenceFluxMissile(nextBuffVars_AttackSpeedModNegative), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class EzrealEssenceFluxMissile : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "EzrealEssenceFluxDebuff",
            BuffTextureName = "Ezreal_EssenceFlux.dds",
        };
        float attackSpeedModNegative;
        public EzrealEssenceFluxMissile(float attackSpeedModNegative = default)
        {
            this.attackSpeedModNegative = attackSpeedModNegative;
        }
        public override void OnActivate()
        {
            //RequireVar(this.attackSpeedModNegative);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeAttackSpeedMod(owner, attackSpeedModNegative);
        }
    }
}