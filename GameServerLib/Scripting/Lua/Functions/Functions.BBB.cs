
using System;
using System.Numerics;
using GameServerCore;
using GameServerCore.Enums;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.GameObjects.SpellNS;
using Chronobreak.GameServer.Scripting.CSharp.Converted;
using F = Chronobreak.GameServer.Scripting.Lua.Functions;
//using FCS = Chronobreak.GameServer.Scripting.CSharp.Converted.Functions_CS;

namespace Chronobreak.GameServer.Scripting.Lua;

public static class Functions_BBB_and_CS
{
    [BBFunc]
    public static Pet SpawnPet(
       string name, string skin, string buff, string? aiScript,
       float duration, Vector3 pos,
       float healthBonus, //TODO: Implement parameter
       float damageBonus,  //TODO: Implement parameter
       Champion? petOwner = null,
       Spell? originSpell = null
   )
    {
        originSpell ??= Functions.CastedSpell;
        petOwner ??= originSpell.Caster as Champion;
        return new
        (
            petOwner,
            originSpell,
            pos.ToVector2(),
            name,
            skin,
            buff,
            duration,
            cloneInventory: false,
            showMinimapIfClone: false,
            disallowPlayerControl: false,
            doFade: false
        );
    }
    [BBFunc]

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
        originSpell ??= F.CastedSpell!;
        petOwner ??= (originSpell.Caster as Champion)!;
        return new Pet(petOwner, originSpell, (unitToClone as ObjAIBase)!, pos.ToVector2(), buff, duration, null, cloneInventory: true, showMinimapIcon, disallowPlayerControl: false, doFade: false, "");
    }

    [BBFunc]
    public static void AddBuff(
        ObjAIBase attacker,
        AttackableUnit target,
        CBuffScript buffScript,
        int maxStack = 1,
        int numberOfStacks = 1,
        float duration = 25000,

        BuffAddType buffAddType = BuffAddType.REPLACE_EXISTING,
        BuffType buffType = BuffType.INTERNAL,
        float tickRate = 0,
        bool stacksExclusive = false,
        bool canMitigateDuration = false,
        bool isHiddenOnClient = false,
        Spell? originSpell = null
    )
    {
        originSpell ??= Functions.CastedSpell;

        target.Buffs.Add(
            attacker,
            buffScript,
            maxStack, numberOfStacks,
            duration,
            buffAddType, buffType,
            tickRate,
            stacksExclusive, canMitigateDuration, isHiddenOnClient,
            originSpell
        );
    }
    [BBFunc]

    public static void ApplySilence(
        ObjAIBase attacker,
        AttackableUnit target,
        float duration,
        Spell? originSpell = null
    )
    {
        originSpell ??= Functions.CastedSpell;
        target.Buffs.Add("Silence", duration, 1, originSpell, target, attacker);
    }
    [BBFunc]

    public static void ApplyStun(
        ObjAIBase attacker,
        AttackableUnit target,
        float duration,
        Spell? originSpell = null
    )
    {
        originSpell ??= Functions.CastedSpell;
        target.Buffs.Add("Stun", duration, 1, originSpell, target, attacker);
    }
    [BBFunc]

    public static void ApplyFear(
        ObjAIBase attacker,
        AttackableUnit target,
        float duration,
        Spell? originSpell = null
    )
    {
        originSpell ??= Functions.CastedSpell;
        target.Buffs.Add("Fear", duration, 1, originSpell, target, attacker);
    }
    [BBFunc]

    public static void ApplyRoot(
        ObjAIBase attacker,
        AttackableUnit target,
        float duration,
        Spell? originSpell = null
    )
    {
        originSpell ??= Functions.CastedSpell;
        target.Buffs.Add("Root", duration, 1, originSpell, target, attacker);
    }



    [BBFunc]
    //TODO: This is broken for Lua scripts and hangs (Annie stun passive, Nunu attack counter passive, etc.)
    public static int GetBuffCountFromCaster(
        AttackableUnit target,
        AttackableUnit? caster, //TODO: AttackableUnit -> ObjAIBase
        [BBBuffName] string buffName
    )
    {
        return F.SpellBuffCount(target, buffName, caster as ObjAIBase);
    }

    [BBFunc]
    public static int GetBuffCountFromAll(
        AttackableUnit target,
        [BBBuffName] string buffName
    )
    {
        return F.SpellBuffCount(target, buffName, null);
    }

    [BBFunc]
    public static void BreakSpellShields(
        AttackableUnit target
    )
    {
        //HACK: This time, not ours, but Riot's
        F.SpellBuffAdd(target as ObjAIBase, target, "SpellShieldMarker", 0, 1, 37037);
    }

    [BBFunc]
    public static void SpellBuffRemoveStacks(
        AttackableUnit target,
        ObjAIBase attacker,
        [BBBuffName] string buffName,
        int numStacks = 0
    )
    {
        if (numStacks == 0)
        {
            target.Buffs.RemoveAllStacks(buffName, attacker);
        }
        else
        {
            target.Buffs.RemoveStacks(buffName, attacker, numStacks);
        }
    }

    [BBFunc]
    public static void ExecutePeriodicallyReset(
        out float trackTime
    )
    {
        trackTime = 0;
    }
}

//public static partial class Functions
public static class Functions_BBB
{
    [BBFunc]
    public static void If(
        [BBParam("Var", "VarTable", null, null)]
        object src1,
        [BBParam(null, null, "", "ByLevel")]
        object? value1,
        //CompareOp compareOp,
        [BBParam("Var", "VarTable", null, null)]
        object? src2,
        [BBParam(null, null, "", "ByLevel")]
        object? value2,

        [BBSubBlocksAttribute]
        Action? subBlocks = null
    )
    { }

    [BBFunc]
    public static T SetVarInTable<T>(
        [BBParam("Var", "VarTable", "Value", "ValueByLevel")]
        T src
    )
    {
        return src;
    }

    [BBFunc]
    public static float Math(
        [BBParam("Var", "VarTable", "Value", "ValueByLevel")]
        float src1,
        //MathOp mathOp,
        [BBParam("Var", "VarTable", "Value", "ValueByLevel")]
        float src2
    )
    {
        return default!;
    }

    [BBFunc]
    public static void SetStatus(
        Action<AttackableUnit, bool> status,
        AttackableUnit target,
        [BBParam("Var", "VarTable", "Value", "ValueByLevel")]
        bool src
    )
    {
        status(target, src);
    }

    [BBFunc]
    public static void Else(
        [BBSubBlocksAttribute]
        Action? subBlocks = null
    )
    { }

    [BBFunc]
    public static object GetSlotSpellInfo<T>(
        Func<AttackableUnit, int, SpellbookType, SpellSlotType, T> function,
        ObjAIBase owner,
        [BBParam("Var", "VarTable", "Value", "ValueByLevel")]
        int spellSlot,
        SpellbookType spellbookType,
        SpellSlotType slotType
    )
    {
        return function(owner, spellSlot, spellbookType, slotType)!; // Union<string, float>
    }

    [BBFunc]
    public static void RequireVar(
        object? required
    )
    { }

    [BBFunc]
    public static void IncStat(
        Action<AttackableUnit, float> stat,
        AttackableUnit target,
        float delta
    )
    { }

    [BBFunc]
    public static void IfHasBuff(
        AttackableUnit owner,
        AttackableUnit attacker,
        [BBBuffName] string buffName,

        [BBSubBlocksAttribute]
        Action? subBlocks = null
    )
    { }

    [BBFunc]
    public static void ElseIf(
        [BBParam("Var", "VarTable", null, null)]
        object src1,
        [BBParam(null, null, "", "ByLevel")]
        object? value1,
        //CompareOp compareOp,
        [BBParam("Var", "VarTable", null, null)]
        object? src2,
        [BBParam(null, null, "", "ByLevel")]
        object? value2,

        [BBSubBlocksAttribute]
        Action? subBlocks = null
    )
    { }

    [BBFunc]
    public static float GetStat(
        Func<AttackableUnit, float> stat,
        AttackableUnit target
    )
    {
        return default!;
    }

    [BBFunc]
    public static void ExecutePeriodically(
        float timeBetweenExecutions,
        ref float trackTime,
        bool executeImmediately = false,
        float tickTime = 0,

        [BBSubBlocksAttribute]
        Action? subBlocks = null
    )
    { }

    [BBFunc]
    public static void SetReturnValue(
        [BBParam("Var", "VarTable", "Value", "ValueByLevel")]
        object src
    )
    { }

    [BBFunc]
    public static float GetPAROrHealth(
        Func<AttackableUnit, PrimaryAbilityResourceType, float> function,
        AttackableUnit owner,
        PrimaryAbilityResourceType PARType
    )
    {
        return function(owner, PARType);
    }

    [BBFunc]
    public static void IfNotHasBuff(
        AttackableUnit owner,
        AttackableUnit caster,
        [BBBuffName] string buffName,

        [BBSubBlocksAttribute]
        Action? subBlocks = null
    )
    { }

    [BBFunc]
    public static void IncPermanentStat(
        Action<AttackableUnit, float> stat,
        AttackableUnit target,
        float delta
    )
    { }

    [BBFunc]
    public static bool GetStatus(
        Func<AttackableUnit, bool> status,
        AttackableUnit target
    )
    {
        return status(target);
    }

    [BBFunc]
    public static T GetCastInfo<T>(
        Func<T> info
    )
    {
        return default!; // Union<string, float>
    }

    //[BBFunc]
    //public static float GetTime(){
    //    return default!;
    //}

    [BBFunc]
    public static void While(
        [BBParam("Var", "VarTable", null, null)]
        object src1,
        [BBParam(null, null, "", "ByLevel")]
        object? value1,
        //CompareOp compareOp,
        [BBParam("Var", "VarTable", null, null)]
        object? src2,
        [BBParam(null, null, "", "ByLevel")]
        object? value2,

        [BBSubBlocksAttribute]
        Action? subBlocks = null
    )
    { }

    [BBFunc]
    public static void IfHasBuffOfType(
        AttackableUnit target,
        BuffType buffType,

        [BBSubBlocksAttribute]
        Action? subBlocks = null
    )
    { }

    [BBFunc]
    public static void BreakExecution()
    { }
}