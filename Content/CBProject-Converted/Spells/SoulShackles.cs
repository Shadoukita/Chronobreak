namespace Spells
{
    public class SoulShackles : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 80f, 80f, 80f, },
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 175, 250, 325 };
        float[] effect1 = { 1.5f, 1.5f, 1.5f };
        public override bool CanCast()
        {
            bool returnValue = true;
            returnValue = false;
            foreach (AttackableUnit unit in GetRandomUnitsInArea(owner, owner.Position3D, 600, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
            {
                returnValue = true;
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            AddBuff(owner, owner, new Buffs.SoulShacklesOwner(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_BreakDamage = effect0[level - 1];
            float nextBuffVars_BreakStun = effect1[level - 1];
            bool nextBuffVars_Broken = false;
            AddBuff(attacker, target, new Buffs.SoulShackles(nextBuffVars_BreakDamage, nextBuffVars_BreakStun, nextBuffVars_Broken), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            ApplyDamage(attacker, target, nextBuffVars_BreakDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.8f, 1, false, false, attacker);
            float nextBuffVars_MoveSpeedMod = -0.2f;
            float nextBuffVars_AttackSpeedMod = 0;
            AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class SoulShackles : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "SoulShackle_buf.troy", "SoulShackle_tar.troy", },
            BuffName = "Soul Shackles",
            BuffTextureName = "FallenAngel_Purgatory.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        float breakDamage;
        float breakStun;
        bool broken;
        EffectEmitter particleID;
        float activateTime;
        float lastTimeExecuted;
        public SoulShackles(float breakDamage = default, float breakStun = default, bool broken = default)
        {
            this.breakDamage = breakDamage;
            this.breakStun = breakStun;
            this.broken = broken;
        }
        public override void OnActivate()
        {
            //RequireVar(this.breakDamage);
            //RequireVar(this.breakStun);
            //RequireVar(this.broken);
            SpellEffectCreate(out particleID, out _, "SoulShackle_beam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "spine", default, owner, "spine", default, false, default, default, false, false);
            activateTime = GetGameTime();
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particleID);
            if (!broken)
            {
                float deactivateTime = GetGameTime();
                float timeElapsed = deactivateTime - activateTime;
                if (timeElapsed >= 3)
                {
                    ApplyDamage(attacker, owner, breakDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.8f, 1, false, false, attacker);
                    ApplyStun(attacker, owner, breakStun);
                }
            }
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, true))
            {
                if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.SoulShacklesOwner)) == 0)
                {
                    broken = true;
                    SpellBuffRemoveCurrent(owner);
                }
                if (IsDead(attacker))
                {
                    broken = true;
                    SpellBuffRemove(owner, nameof(Buffs.SoulShackleSlow), attacker, 0);
                    SpellBuffRemoveCurrent(owner);
                }
                else
                {
                    if (IsDead(owner))
                    {
                        broken = true;
                        SpellBuffRemove(owner, nameof(Buffs.SoulShackleSlow), attacker, 0);
                        SpellBuffRemoveCurrent(owner);
                    }
                    else
                    {
                        float distance = DistanceBetweenObjects(owner, attacker);
                        if (distance > 600)
                        {
                            broken = true;
                            SpellBuffRemove(owner, nameof(Buffs.SoulShackleSlow), attacker, 0);
                            SpellBuffRemoveCurrent(owner);
                        }
                    }
                }
            }
        }
    }
}