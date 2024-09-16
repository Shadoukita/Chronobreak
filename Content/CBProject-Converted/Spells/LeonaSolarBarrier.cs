namespace Spells
{
    public class LeonaSolarBarrier : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 30, 40, 50, 60, 70 };
        int[] effect1 = { 60, 110, 160, 210, 260 };
        public override void SelfExecute()
        {
            float nextBuffVars_DefenseBonus = effect0[level - 1];
            int nextBuffVars_MagicDamage = effect1[level - 1];
            AddBuff(attacker, attacker, new Buffs.LeonaSolarBarrier(nextBuffVars_MagicDamage, nextBuffVars_DefenseBonus), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class LeonaSolarBarrier : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "LeonaSolarBarrier",
            BuffTextureName = "LeonaSolarBarrier.dds",
            SpellToggleSlot = 2,
        };
        float magicDamage;
        float defenseBonus;
        EffectEmitter particle;
        public LeonaSolarBarrier(float magicDamage = default, float defenseBonus = default)
        {
            this.magicDamage = magicDamage;
            this.defenseBonus = defenseBonus;
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            //RequireVar(this.magicDamage);
            //RequireVar(this.defenseBonus);
            IncFlatArmorMod(owner, defenseBonus);
            IncFlatSpellBlockMod(owner, defenseBonus);
            SpellEffectCreate(out particle, out _, "Leona_SolarBarrier_buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
            OverrideAnimation("Idle1", "Spell2_idle", owner);
            OverrideAnimation("Idle2", "Spell2_idle", owner);
            OverrideAnimation("Idle3", "Spell2_idle", owner);
            OverrideAnimation("Idle4", "Spell2_idle", owner);
            OverrideAnimation("Attack1", "Spell2_attack", owner);
            OverrideAnimation("Attack2", "Spell2_attack", owner);
            OverrideAnimation("Attack3", "Spell2_attack", owner);
            OverrideAnimation("Crit", "Spell2_attack", owner);
            OverrideAnimation("Run", "Spell2_run", owner);
        }
        public override void OnDeactivate(bool expired)
        {
            if (!IsDead(owner))
            {
                bool targetStruck = false;
                float magicDamage = this.magicDamage;
                TeamId teamID = GetTeamID_CS(owner);
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 450, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    targetStruck = true;
                    BreakSpellShields(unit);
                    AddBuff(attacker, unit, new Buffs.LeonaSunlight(), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                    ApplyDamage(attacker, unit, magicDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 0, false, false, attacker);
                    SpellEffectCreate(out _, out _, "Leona_SolarBarrier_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                }
                SpellEffectRemove(particle);
                ClearOverrideAnimation("Idle1", owner);
                ClearOverrideAnimation("Idle2", owner);
                ClearOverrideAnimation("Idle3", owner);
                ClearOverrideAnimation("Idle4", owner);
                ClearOverrideAnimation("Attack1", owner);
                ClearOverrideAnimation("Attack2", owner);
                ClearOverrideAnimation("Attack3", owner);
                ClearOverrideAnimation("Crit", owner);
                ClearOverrideAnimation("Run", owner);
                if (targetStruck)
                {
                    SpellEffectCreate(out _, out _, "Leona_SolarBarrier_nova.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, target, default, default, true, default, default, false, false);
                    float nextBuffVars_DefenseBonus = defenseBonus;
                    AddBuff(attacker, attacker, new Buffs.LeonaSolarBarrier2(nextBuffVars_DefenseBonus), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
                else
                {
                    SpellEffectCreate(out _, out _, "Leona_SolarBarrier_nova_whiff.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, target, default, default, true, default, default, false, false);
                }
            }
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, defenseBonus);
            IncFlatSpellBlockMod(owner, defenseBonus);
        }
    }
}