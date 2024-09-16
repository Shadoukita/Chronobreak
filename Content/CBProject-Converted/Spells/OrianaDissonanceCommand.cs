namespace Spells
{
    public class OrianaDissonanceCommand : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 70, 115, 160, 205, 250 };
        public override void SelfExecute()
        {
            Vector3 targetPos = Vector3.Zero;
            EffectEmitter particle = null;
            EffectEmitter particle2 = null;

            PlayAnimation("Spell2", 0, owner, false, true, false);
            AddBuff(owner, owner, new Buffs.OrianaGlobalCooldown(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            TeamId teamID = GetTeamID_CS(owner);
            float damage = effect0[level - 1];
            bool deployed = false;
            float selfAP = GetFlatMagicDamageMod(owner);
            float bonusDamage = selfAP * 0.5f;
            damage += bonusDamage;
            foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf | SpellDataFlags.AffectUntargetable, 1, nameof(Buffs.OrianaGhost), true))
            {
                deployed = true;
                targetPos = GetUnitPosition(unit);
                if (unit is Champion)
                {
                    SpellEffectCreate(out particle, out particle2, "OrianaDissonance_ally_green.troy", "OrianaDissonance_ally_red.troy", teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, default, default, targetPos, true, false, false, false, false);
                }
                else
                {
                    SpellEffectCreate(out particle, out particle2, "OrianaDissonance_ball_green.troy", "OrianaDissonance_ball_red.troy", teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, default, default, targetPos, true, false, false, false, false);
                }
            }
            if (!deployed)
            {
                targetPos = GetUnitPosition(owner);
                if (GetBuffCountFromCaster(owner, default, nameof(Buffs.OriannaBallTracker)) > 0)
                {
                    targetPos = charVars.BallPosition;
                }
                SpellEffectCreate(out particle, out particle2, "OrianaDissonance_cas_green.troy", "OrianaDissonance_cas_red.troy", teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, default, default, targetPos, true, false, false, false, false);
            }
            foreach (AttackableUnit unit in GetUnitsInArea(owner, targetPos, 225, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                SpellEffectCreate(out _, out _, "OrianaDissonance_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                ApplyDamage(owner, unit, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, owner);
                int nextBuffVars_Level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                AddBuff(attacker, unit, new Buffs.OrianaSlow(nextBuffVars_Level), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
            }
            foreach (AttackableUnit unit in GetUnitsInArea(owner, targetPos, 225, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                int nextBuffVars_Level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                AddBuff(attacker, unit, new Buffs.OrianaHaste(nextBuffVars_Level), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            EffectEmitter nextBuffVars_Particle2 = particle2;
            EffectEmitter nextBuffVars_Particle = particle;
            Vector3 nextBuffVars_targetPos = targetPos;
            AddBuff(owner, owner, new Buffs.OrianaDissonanceWave(nextBuffVars_Particle2, nextBuffVars_Particle, nextBuffVars_targetPos), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class OrianaDissonanceCommand : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "",
            BuffTextureName = "",
            PopupMessage = new[] { "game_floatingtext_Snared", },
        };
    }
}