namespace Spells
{
    public class BlindMonkEOne : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            SpellFXOverrideSkins = new[] { "ReefMalphite", },
        };
        EffectEmitter partname; // UNUSED
        int[] effect0 = { 60, 95, 130, 165, 200 };
        float[] effect1 = { -0.2f, -0.25f, -0.3f, -0.35f, -0.4f };
        public override void SelfExecute()
        {
            bool hasHitTarget = false;
            TeamId casterID = GetTeamID_CS(owner);
            float baseDamage = effect0[level - 1];
            float bonusAD = GetFlatPhysicalDamageMod(owner);
            float damageToDeal = bonusAD + baseDamage;
            float nextBuffVars_MoveSpeedMod = effect1[level - 1]; // UNUSED
            SpellEffectCreate(out partname, out _, "blindMonk_thunderCrash_impact_cas.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_BUFFBONE_GLB_CHEST_LOC", owner.Position3D, target, default, default, true, default, default, false, false);
            SpellEffectCreate(out partname, out _, "blindMonk_thunderCrash_impact_02.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_BUFFBONE_GLB_CHEST_LOC", owner.Position3D, target, default, default, true, default, default, false, false);
            SpellEffectCreate(out partname, out _, "blindMonk_E_cas.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "l_hand", owner.Position3D, target, default, default, true, default, default, false, false);
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 450, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.SharedWardBuff), false))
            {
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                AddBuff(attacker, unit, new Buffs.BlindMonkEOne(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                SpellEffectCreate(out _, out _, "blindMonk_thunderCrash_impact_unit_tar.troy", default, casterID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                SpellEffectCreate(out _, out _, "blindMonk_E_thunderCrash_tar.troy", default, casterID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                SpellEffectCreate(out _, out _, "blindMonk_E_thunderCrash_unit_tar_blood.troy", default, casterID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                hasHitTarget = true;
            }
            if (hasHitTarget)
            {
                AddBuff(owner, owner, new Buffs.BlindMonkEManager(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class BlindMonkEOne : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head", },
            AutoBuffActivateEffect = new[] { "global_Watched.troy", },
            BuffName = "BlindMonkTempest",
            BuffTextureName = "BlindMonkEOne.dds",
        };
        Region bubbleID;
        Region bubbleID2;
        public override void OnActivate()
        {
            TeamId team = GetTeamID_CS(attacker);
            bubbleID = AddUnitPerceptionBubble(team, 400, owner, 20, default, default, false);
            bubbleID2 = AddUnitPerceptionBubble(team, 50, owner, 20, default, default, true);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(bubbleID);
            RemovePerceptionBubble(bubbleID2);
        }
    }
}