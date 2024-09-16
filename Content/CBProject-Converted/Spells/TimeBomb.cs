namespace Spells
{
    public class TimeBomb : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 90, 145, 200, 260, 320 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_DamageLevel = effect0[level - 1];
            if (GetBuffCountFromCaster(target, owner, nameof(Buffs.TimeBomb)) > 0)
            {
                SpellEffectCreate(out _, out _, "TimeBombExplo.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, target.Position3D, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    ApplyDamage(attacker, unit, nextBuffVars_DamageLevel, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.9f, 1, false, false, attacker);
                }
            }
            else
            {
                Champion caster;
                TeamId ownerID = GetTeamID_CS(owner);
                caster = GetChampionBySkinName("Zilean", GetEnemyTeam(ownerID));
                if (GetBuffCountFromCaster(target, caster, nameof(Buffs.TimeBomb)) > 0)
                {
                    SpellEffectCreate(out _, out _, "TimeBombExplo.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                    int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    float damageToDeal = effect0[level - 1];
                    foreach (AttackableUnit unit in GetUnitsInArea(caster, target.Position3D, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                    {
                        ApplyDamage(caster, unit, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.9f, 0, false, false, caster);
                    }
                }
            }
            if (!IsDead(target))
            {
                AddBuff(owner, target, new Buffs.TimeBomb(nextBuffVars_DamageLevel), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 1, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class TimeBomb : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Time Bomb",
            BuffTextureName = "Chronokeeper_Chronoblast.dds",
        };
        float damageLevel;
        EffectEmitter particleID2;
        EffectEmitter particleID;
        float tickDamage;
        public TimeBomb(float damageLevel = default)
        {
            this.damageLevel = damageLevel;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageLevel);
            TeamId teamOfOwner = GetTeamID_CS(attacker);
            SpellEffectCreate(out particleID2, out particleID, "TimeBomb_green.troy", "TimeBomb_red.troy", teamOfOwner, 500, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            tickDamage = 3;
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particleID);
            SpellEffectRemove(particleID2);
            if (IsDead(owner))
            {
                TeamId teamID = GetTeamID_CS(attacker);
                SpellEffectCreate(out _, out _, "TimeBombExplo.troy", default, teamID, 500, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, target, default, default, true, default, default, false, false);
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    ApplyDamage(attacker, unit, damageLevel, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.9f, 1, false, false, attacker);
                }
            }
            else if (expired)
            {
                SpellEffectCreate(out _, out _, "TimeBombExplo.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, default, default, false, false);
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    ApplyDamage(attacker, unit, damageLevel, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.9f, 1, false, false, attacker);
                }
            }
        }
        public override void OnUpdateActions()
        {
            if (owner.Team != attacker.Team && tickDamage > 0)
            {
                float nextBuffVars_TickDamage = tickDamage;
                AddBuff(attacker, owner, new Buffs.TimeBombCountdown(nextBuffVars_TickDamage), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                ApplyDamage(attacker, owner, tickDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, attacker);
                tickDamage--;
            }
        }
    }
}