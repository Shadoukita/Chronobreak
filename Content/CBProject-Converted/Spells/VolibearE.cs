namespace Spells
{
    public class VolibearE : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        EffectEmitter partname; // UNUSED
        float[] effect0 = { -0.3f, -0.35f, -0.4f, -0.45f, -0.5f };
        int[] effect1 = { 2, 2, 2, 2, 2 };
        int[] effect2 = { 60, 105, 150, 195, 240 };
        int[] effect3 = { 3, 3, 3, 3, 3 };
        public override void SelfExecute()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out partname, out _, "volibear_E_aoe_indicator.troy", default, teamID, 350, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out partname, out _, "volibear_E_aoe_indicator_02.troy", default, teamID, 350, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out _, out _, "Volibear_E_cas_blast.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position3D, default, default, default, true, false, false, false, false);
            SpellEffectCreate(out _, out _, "Volibear_E_cas.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position3D, default, default, default, true, false, false, false, false);
            float nextBuffVars_VolibearESlow = effect0[level - 1];
            int nextBuffVars_VolibearEExtender = effect1[level - 1]; // UNUSED
            float damageToDeal = effect2[level - 1];
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, attacker.Position3D, 425, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                SpellEffectCreate(out _, out _, "volibear_E_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                BreakSpellShields(unit);
                AddBuff(attacker, unit, new Buffs.VolibearE(nextBuffVars_VolibearESlow), 1, 1, effect3[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 0, false, false, attacker);
                if (unit is not Champion && !IsDead(unit))
                {
                    ApplyFear(owner, unit, 2);
                }
            }
        }
    }
}
namespace Buffs
{
    public class VolibearE : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "GLOBAL_SLOW.TROY", "", },
            BuffName = "VolibearE",
            BuffTextureName = "VolibearE.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        float volibearESlow;
        public VolibearE(float volibearESlow = default)
        {
            this.volibearESlow = volibearESlow;
        }
        public override void OnActivate()
        {
            //RequireVar(this.volibearEExtender);
            //RequireVar(this.volibearESlow);
            IncPercentMovementSpeedMod(owner, volibearESlow);
            if (target is not Champion)
            {
                IncPercentMovementSpeedMod(owner, -0.5f);
            }
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, volibearESlow);
            if (target is not Champion)
            {
                IncPercentMovementSpeedMod(owner, -0.5f);
            }
        }
    }
}