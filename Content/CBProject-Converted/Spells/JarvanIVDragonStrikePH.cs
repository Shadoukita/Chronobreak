namespace Spells
{
    public class JarvanIVDragonStrikePH : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class JarvanIVDragonStrikePH : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "JarvanQuick_buf.troy", },
        };
        float lastTimeExecuted;
        public override void OnActivate()
        {
            OverrideAnimation("Run", "Run3", owner);
            Move(owner, attacker.Position3D, 1400, 1, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 500, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if (IsBehind(owner, unit))
                {
                    AddBuff(attacker, unit, new Buffs.JarvanIVDragonStrikeBehindMe(), 1, 1, 0.5f, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, true);
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            ClearOverrideAnimation("Run", owner);
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 260, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                int count = GetBuffCountFromAll(unit, nameof(Buffs.SlashBeenHit));
                if (count < 1)
                {
                    bool isStealthed = GetStealthed(unit);
                    if (!isStealthed)
                    {
                        AddBuff((ObjAIBase)owner, unit, new Buffs.SlashBeenHit(), 1, 1, 1, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                        BreakSpellShields(unit);
                        SpellEffectCreate(out _, out _, "BloodSlash.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, target, default, default, false, default, default, false);
                        AddBuff((ObjAIBase)owner, unit, new Buffs.JarvanIVDragonStrikePH2(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.STUN, 0, true, false, false);
                    }
                    else
                    {
                        if (unit is Champion)
                        {
                            AddBuff((ObjAIBase)owner, unit, new Buffs.SlashBeenHit(), 1, 1, 1, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                            BreakSpellShields(unit);
                            SpellEffectCreate(out _, out _, "BloodSlash.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, target, default, default, false, default, default, false);
                            AddBuff((ObjAIBase)owner, unit, new Buffs.JarvanIVDragonStrikePH2(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.STUN, 0, true, false, false);
                        }
                        else
                        {
                            bool canSee = CanSeeTarget(owner, unit);
                            if (canSee)
                            {
                                AddBuff((ObjAIBase)owner, unit, new Buffs.SlashBeenHit(), 1, 1, 1, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                                BreakSpellShields(unit);
                                SpellEffectCreate(out _, out _, "BloodSlash.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, target, default, default, false, default, default, false);
                                AddBuff((ObjAIBase)owner, unit, new Buffs.JarvanIVDragonStrikePH2(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.STUN, 0, true, false, false);
                            }
                        }
                    }
                }
            }
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, true))
            {
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 260, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    int count = GetBuffCountFromAll(unit, nameof(Buffs.SlashBeenHit));
                    if (GetBuffCountFromCaster(unit, attacker, nameof(Buffs.JarvanIVDragonStrikeBehindMe)) == 0)
                    {
                        if (count < 1)
                        {
                            bool isStealthed = GetStealthed(unit);
                            if (!isStealthed)
                            {
                                AddBuff((ObjAIBase)owner, unit, new Buffs.SlashBeenHit(), 1, 1, 1, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                                BreakSpellShields(unit);
                                SpellEffectCreate(out _, out _, "BloodSlash.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, target, default, default, false, default, default, false);
                                AddBuff((ObjAIBase)owner, unit, new Buffs.JarvanIVDragonStrikePH2(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.STUN, 0, true, false, false);
                            }
                            else
                            {
                                if (unit is Champion)
                                {
                                    AddBuff((ObjAIBase)owner, unit, new Buffs.SlashBeenHit(), 1, 1, 1, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                                    BreakSpellShields(unit);
                                    SpellEffectCreate(out _, out _, "BloodSlash.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, target, default, default, false, default, default, false);
                                    AddBuff((ObjAIBase)owner, unit, new Buffs.JarvanIVDragonStrikePH2(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.STUN, 0, true, false, false);
                                }
                                else
                                {
                                    bool canSee = CanSeeTarget(owner, unit);
                                    if (canSee)
                                    {
                                        AddBuff((ObjAIBase)owner, unit, new Buffs.SlashBeenHit(), 1, 1, 1, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                                        BreakSpellShields(unit);
                                        SpellEffectCreate(out _, out _, "BloodSlash.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, target, default, default, false, default, default, false);
                                        AddBuff((ObjAIBase)owner, unit, new Buffs.JarvanIVDragonStrikePH2(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.STUN, 0, true, false, false);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public override void OnMoveEnd()
        {
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 260, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                int count = GetBuffCountFromAll(unit, nameof(Buffs.SlashBeenHit));
                if (count < 1)
                {
                    bool isStealthed = GetStealthed(unit);
                    if (!isStealthed)
                    {
                        AddBuff((ObjAIBase)owner, unit, new Buffs.SlashBeenHit(), 1, 1, 1, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                        BreakSpellShields(unit);
                        SpellEffectCreate(out _, out _, "BloodSlash.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, target, default, default, false, default, default, false);
                        AddBuff((ObjAIBase)owner, unit, new Buffs.JarvanIVDragonStrikePH2(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.STUN, 0, true, false, false);
                    }
                    else
                    {
                        if (unit is Champion)
                        {
                            AddBuff((ObjAIBase)owner, unit, new Buffs.SlashBeenHit(), 1, 1, 1, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                            BreakSpellShields(unit);
                            SpellEffectCreate(out _, out _, "BloodSlash.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, target, default, default, false, default, default, false);
                            AddBuff((ObjAIBase)owner, unit, new Buffs.JarvanIVDragonStrikePH2(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.STUN, 0, true, false, false);
                        }
                        else
                        {
                            bool canSee = CanSeeTarget(owner, unit);
                            if (canSee)
                            {
                                AddBuff((ObjAIBase)owner, unit, new Buffs.SlashBeenHit(), 1, 1, 1, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                                BreakSpellShields(unit);
                                SpellEffectCreate(out _, out _, "BloodSlash.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, target, default, default, false, default, default, false);
                                AddBuff((ObjAIBase)owner, unit, new Buffs.JarvanIVDragonStrikePH2(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.STUN, 0, true, false, false);
                            }
                        }
                    }
                }
            }
            SpellBuffRemoveCurrent(owner);
        }
    }
}