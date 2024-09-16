namespace Spells
{
    public class RunePrison : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = { 1, 1.25f, 1.5f, 1.75f, 2 };
        int[] effect1 = { 60, 95, 130, 165, 200 };
        float[] effect2 = { 0.5f, 0.5f, 0.5f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = base.level;
            AddBuff(attacker, target, new Buffs.RunePrison(), 1, 1, effect0[level - 1], BuffAddType.RENEW_EXISTING, BuffType.CHARM, 0, true, false, false);
            float baseDamage = effect1[level - 1];
            TeamId teamID = GetTeamID_CS(attacker);
            float pAR = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
            level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float manaDamage = pAR * 0.05f;
            float totalDamage = manaDamage + baseDamage;
            float aoEDamage = effect2[level - 1];
            aoEDamage *= totalDamage;
            ApplyDamage(attacker, target, totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.6f, 1, false, false, attacker);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.DesperatePower)) > 0)
            {
                SpellEffectCreate(out _, out _, "DesperatePower_aoe.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                foreach (AttackableUnit unit in GetUnitsInArea(owner, target.Position3D, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    if (target != unit)
                    {
                        SpellEffectCreate(out _, out _, "ManaLeach_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true);
                        ApplyDamage(attacker, unit, aoEDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.3f, 1, false, false, attacker);
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class RunePrison : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Rune Prison",
            BuffTextureName = "Ryze_PowerOverwhelming.dds",
            PopupMessage = new[] { "game_floatingtext_Snared", },
        };
        EffectEmitter asdf1;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(attacker);
            SetCanMove(owner, false);
            ApplyAssistMarker(attacker, owner, 10);
            SpellEffectCreate(out asdf1, out _, "RunePrison_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
            SpellEffectRemove(asdf1);
        }
        public override void OnUpdateStats()
        {
            SetCanMove(owner, false);
        }
    }
}