
using System.Collections.Generic;
using System.Numerics;
using GameServerCore;
using GameServerCore.Enums;
using AFM = Chronobreak.GameServer.API.ApiFunctionManager;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.GameObjects.SpellNS;
using System.Linq;
using System;

namespace Chronobreak.GameServer.Scripting.CSharp.Converted;

public static class Functions_CS
{
    private static Random random = new();

    public static bool ExecutePeriodically(
        float timeBetweenExecutions,
        ref float trackTime,
        bool executeImmediately = false,
        float tickTime = 0
    )
    {
        if (trackTime == 0)
        {
            trackTime = Game.Time.GameTime;
            if (executeImmediately)
            {
                return true;
            }
        }
        else if ((Game.Time.GameTime - trackTime) >= timeBetweenExecutions * 1000f)
        {
            trackTime = Game.Time.GameTime;
            return true;
        }
        return false;
    }

    public static bool IsRanged(ObjAIBase ai)
    {
        return !ai.CharData.IsMelee;
    }

    public static bool IsMelee(ObjAIBase ai)
    {
        return ai.CharData.IsMelee;
    }

    public static bool IsInFront(AttackableUnit me, AttackableUnit target)
    {
        return Vector2.Dot(me.Direction.ToVector2(), target.Position - me.Position) > 0;
    }

    public static bool IsBehind(AttackableUnit me, AttackableUnit target)
    {
        return !IsInFront(me, target);
    }

    public static float RandomChance() // [0;1)
    {
        return random.NextSingle();
    }

    /*
    //TODO: Replace all occurrences with foreach(GetUnitsInArea)+AddBuff
    public static void AddBuffToEachUnitInArea(
        AttackableUnit attacker,
        Vector3 center,
        float range,
        SpellDataFlags flags,
        ObjAIBase buffAttacker,
        CBuffScript buffScript,
        BuffAddType buffAddType,
        BuffType buffType,
        int buffMaxStack,
        int buffNumberOfStacks,
        float buffDuration,
        float tickRate,
        bool isHiddenOnClient,
        bool inclusiveBuffFilter = false
    ){
        foreach(var unit in GetUnitsInArea(attacker, center, range, flags, "", inclusiveBuffFilter))
        {
            AddBuff(buffAttacker, unit, buffScript, buffMaxStack, buffNumberOfStacks, buffDuration, buffAddType, buffType, tickRate, false, false, isHiddenOnClient);
        }
    }
    */

    private static IEnumerable<T> TakeRandom<T>(IEnumerable<T> enumerable, int count)
    {
        var list = enumerable.ToList();
        if (list.Count > count)
        {
            //TODO: Find a more efficient implementation
            var selection = new List<T>(count);
            for (int i = 0; i < count; i++)
            {
                int j = random.Next(0, list.Count);
                selection.Add(list[j]);
                list.RemoveAt(j);
            }
            return selection;
        }
        return list;
    }

    public static IEnumerable<AttackableUnit> GetUnitsInArea(
        AttackableUnit attacker,
        Vector3 center,
        float range,
        SpellDataFlags flags = 0,
        string buffNameFilter = "",
        bool inclusiveBuffFilter = false
    )
    {
        var filtered = AFM.FilterUnitsInRange(attacker, center.ToVector2(), range, flags);
        if (!string.IsNullOrEmpty(buffNameFilter))
        {
            return filtered.Where(unit => unit.Buffs.Has(buffNameFilter) == inclusiveBuffFilter);
        }
        return filtered;
    }
    public static IEnumerable<AttackableUnit> GetRandomUnitsInArea(
        AttackableUnit attacker,
        Vector3 center,
        float range,
        SpellDataFlags flags,
        int maximumUnitsToPick,
        string buffNameFilter = "",
        bool inclusiveBuffFilter = false
    )
    {
        return TakeRandom(
            GetUnitsInArea(
                attacker, center, range, flags, buffNameFilter, inclusiveBuffFilter
            ),
            maximumUnitsToPick
        );
    }
    public static IEnumerable<AttackableUnit> GetClosestUnitsInArea(
        AttackableUnit attacker,
        Vector3 center,
        float range,
        SpellDataFlags flags,
        int maximumUnitsToPick,
        string buffNameFilter = "",
        bool inclusiveBuffFilter = false
    )
    {
        return GetUnitsInArea(attacker, center, range, flags, buffNameFilter, inclusiveBuffFilter)
            .OrderBy(unit => Vector2.DistanceSquared(unit.Position, center.ToVector2())).Take(maximumUnitsToPick);
    }
    public static IEnumerable<AttackableUnit> GetVisibleUnitsInArea(
        AttackableUnit attacker,
        Vector3 center,
        float range,
        SpellDataFlags flags,
        string buffNameFilter = "",
        bool inclusiveBuffFilter = false
    )
    {
        return GetUnitsInArea(
            attacker, center, range, flags, buffNameFilter, inclusiveBuffFilter
        )
        .Where(
            //TODO: unit.IsVisibleForPlayer() if attacker is Champion
            unit => unit.IsVisibleByTeam(attacker.Team)
        );
    }
    public static IEnumerable<AttackableUnit> GetClosestVisibleUnitsInArea(
        AttackableUnit attacker,
        Vector3 center,
        float range,
        SpellDataFlags flags,
        int maximumUnitsToPick,
        string buffNameFilter = "",
        bool inclusiveBuffFilter = false
    )
    {
        return GetVisibleUnitsInArea(
            attacker, center, range, flags, buffNameFilter, inclusiveBuffFilter
        ).OrderBy(
            unit => Vector2.DistanceSquared(unit.Position, center.ToVector2())
        ).Take(
            maximumUnitsToPick
        );
    }
    public static IEnumerable<Champion> GetChampions(
        TeamId team,
        string buffNameFilter = "",
        bool inclusiveBuffFilter = false
    )
    {
        //Game.PlayerManager.GetPlayers(true)
        var champions = Game.ObjectManager.GetAllChampionsFromTeam(team);
        if (!string.IsNullOrEmpty(buffNameFilter))
        {
            champions = champions.FindAll(c => c.Buffs.Has(buffNameFilter));
        }
        return champions;
    }
    public static IEnumerable<AttackableUnit> GetUnitsInRectangle(
        AttackableUnit attacker,
        Vector3 center,
        float halfWidth,
        float halfLength,
        SpellDataFlags flags,
        string buffNameFilter = "",
        bool inclusiveBuffFilter = false
    )
    {
        //TODO: Implement rectangle support in QuadTree.
        // The implementation below is very approximate.
        float range = Math.Max(halfWidth, halfLength);
        var units = GetUnitsInArea(
            attacker, center, range, flags, buffNameFilter, inclusiveBuffFilter
        );
        var c2d = center.ToVector2();
        var dir = (c2d - attacker.Position).Normalized() * halfLength;
        var v = c2d - dir;
        var w = c2d + dir;
        foreach (var u in units)
        {
            var p = u.Position;
            var r = halfWidth + u.CollisionRadius;
            if (p.DistanceToSegmentSquared(v, w) <= r * r)
                yield return u;
        }
    }

    public static IEnumerable<AttackableUnit> GetRandomVisibleUnitsInArea(
        AttackableUnit attacker,
        Vector3 center,
        float range,
        SpellDataFlags flags,
        int maximumUnitsToPick,
        string buffNameFilter,
        bool inclusiveBuffFilter
    )
    {
        return TakeRandom(
            GetVisibleUnitsInArea(
                attacker, center, range, flags, buffNameFilter, inclusiveBuffFilter
            ), maximumUnitsToPick
        );
    }
    public static IEnumerable<Vector3> GetPointsOnLine(
        Vector3 center,
        Vector3 faceTowardsPos,
        float size,
        float pushForward,
        int iterations
    )
    {
        Vector3 dir = (faceTowardsPos - center).Normalized();
        Vector3 start = center + dir * pushForward;
        (dir.X, dir.Z) = (-dir.Z, dir.X); // Perpendicular
        start -= dir * (size * 0.5f);
        Vector3 delta = dir * (size / iterations);
        for (int i = 0; i < iterations; i++)
        {
            yield return start;
            start += delta;
        }
    }
    public static IEnumerable<Vector3> GetPointsAroundCircle(
        Vector3 center,
        float radius,
        int iterations
    )
    {
        float slice = 2 * MathF.PI / iterations;
        for (int i = 0; i < iterations; i++)
        {
            var (y, x) = MathF.SinCos(slice * i);
            yield return new Vector3(x, 0, y) * radius + center;
        }
    }

    public static bool IsDead(AttackableUnit u)
    {
        return u.Stats.IsDead;
    }

    #region GetCastInfo
    public static string GetSpellName(Spell spell) => spell.SpellName;
    public static float GetPARCost(Spell spell) => spell.ManaCost;
    public static int GetSpellSlot(Spell spell) => spell.Slot;
    public static int GetSpellLevelPlusOne(Spell spell) => spell.Level;
    public static bool GetIsAttackOverride(Spell spell) => spell.IsAutoAttack;
    public static int GetSpellTargetsHitPlusOne(Spell spell) => spell.GetTargetsHit() + 1;
    #endregion

    public static Vector3 GetSpellTargetPos(Spell spell) => spell.CurrentCastInfo!.TargetPosition;
    public static Vector3 GetSpellDragEndPos(Spell spell) => spell.CurrentCastInfo!.TargetPositionEnd;

    public static TeamId GetEnemyTeam(TeamId team)
    {
        //TODO: return team.GetEnemyTeam();
        return team switch
        {
            TeamId.TEAM_ORDER => TeamId.TEAM_CHAOS,
            TeamId.TEAM_CHAOS => TeamId.TEAM_ORDER,
            _ => TeamId.TEAM_NEUTRAL
        };
    }

    public static Pet CloneUnitPet(
        AttackableUnit unitToClone,
        string buff, float duration,
        Vector3 pos,
        float healthBonus, //TODO: Implement parameter
        float damageBonus, //TODO: Implement parameter
        bool showMinimapIcon,
        Champion? petOwner = null,
        Spell? originSpell = null
    )
    {

        if (petOwner == null)
        {
            return null;
        }

        var clonedUnit = new Pet(
            petOwner,
            originSpell,
            (unitToClone as ObjAIBase)!,
            pos.ToVector2(),
            buff,
            duration,
            null,
            cloneInventory: true,
            showMinimapIcon,
            disallowPlayerControl: false,
            doFade: false,
            "");

        return clonedUnit;
    }
}