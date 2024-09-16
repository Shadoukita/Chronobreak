using System.Numerics;
using System.Collections.Generic;
using Chronobreak.GameServer.GameObjects;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using F = Chronobreak.GameServer.Scripting.Lua.Functions;
using Chronobreak.GameServer.GameObjects.SpellNS;
using GameServerCore.Enums;
using Chronobreak.GameServer.Scripting.Lua;

namespace Chronobreak.GameServer.Scripting.CSharp.Converted;

// Contains API functions that require knowledge of the current instance to work.
public class CInstance
{
    private Champion? _petOwner;
    private Spell? _originSpell;
    protected internal void InitAPI(ObjAIBase? attacker, Spell? spell)
    {
        //TODO: Give grace
        _petOwner = (attacker as Champion) ?? (attacker as Minion)?.Owner as Champion;
        _originSpell = spell;
    }

    public Pet SpawnPet(
        string name, string skin, string buff, string? aiScript,
        float duration, Vector3 pos,
        float healthBonus, //TODO: Implement parameter
        float damageBonus  //TODO: Implement parameter
    )
    {
        return Functions_BBB_and_CS.SpawnPet(name, skin, buff, aiScript, duration, pos, healthBonus, damageBonus,
            _petOwner, _originSpell);
    }
    public Pet CloneUnitPet(
        AttackableUnit unitToClone,
        string buff, float duration,
        Vector3 pos,
        float healthBonus, //TODO: Implement parameter
        float damageBonus, //TODO: Implement parameter
        bool showMinimapIcon
    )
    {
        return Functions_CS.CloneUnitPet(unitToClone, buff, duration, pos, healthBonus, damageBonus, showMinimapIcon, _petOwner, _originSpell);
    }

    public void AddBuff(
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
        bool isHiddenOnClient = false
    )
    {
        Functions_BBB_and_CS.AddBuff(attacker, target, buffScript, maxStack, numberOfStacks, duration, buffAddType,
            buffType, tickRate, stacksExclusive, canMitigateDuration, isHiddenOnClient, _originSpell);
    }

    public void ApplySilence(
        ObjAIBase attacker,
        AttackableUnit target,
        float duration
    )
    {
        Functions_BBB_and_CS.ApplySilence(attacker, target, duration, _originSpell);
    }
    public void ApplyStun(
        ObjAIBase attacker,
        AttackableUnit target,
        float duration
    )
    {
        Functions_BBB_and_CS.ApplyStun(attacker, target, duration, _originSpell);
    }
    public void ApplyFear(
        ObjAIBase attacker,
        AttackableUnit target,
        float duration
    )
    {
        Functions_BBB_and_CS.ApplyFear(attacker, target, duration, _originSpell);
    }
    public void ApplyRoot(
        ObjAIBase attacker,
        AttackableUnit target,
        float duration
    )
    {
        Functions_BBB_and_CS.ApplyRoot(attacker, target, duration, _originSpell);
    }

    private List<EffectEmitter> _createdParticles = [];

    public void SpellEffectCreate(
        out EffectEmitter effectID,
        out EffectEmitter? effectID2,

        string effectName = "",
        string effectNameForOtherTeam = "",

        TeamId FOWTeam = TeamId.TEAM_UNKNOWN,
        float FOWVisibilityRadius = 0,

        FXFlags flags = 0,

        TeamId specificTeamOnly = TeamId.TEAM_UNKNOWN,
        TeamId specificTeamOnlyOverride = TeamId.TEAM_UNKNOWN,
        AttackableUnit? specificUnitOnly = null,
        bool useSpecificUnit = false,

        AttackableUnit? bindObject = null,
        string boneName = "",
        Vector3 pos = default,

        AttackableUnit? targetObject = null,
        string targetBoneName = "",
        Vector3 targetPos = default,

        bool sendIfOnScreenOrDiscard = false,
        bool persistsThroughReconnect = false,
        bool bindFlexToOwnerPAR = false,
        bool followsGroundTilt = false,
        bool facesTarget = false,

        object? orientTowards = null
    )
    {
        F.SpellEffectCreate(
            out effectID,
            out effectID2,
            effectName,
            effectNameForOtherTeam,
            FOWTeam,
            FOWVisibilityRadius,
            flags,
            specificTeamOnly,
            specificTeamOnlyOverride,
            specificUnitOnly,
            useSpecificUnit,
            bindObject,
            boneName,
            pos,
            targetObject,
            targetBoneName,
            targetPos,
            sendIfOnScreenOrDiscard,
            persistsThroughReconnect,
            bindFlexToOwnerPAR,
            followsGroundTilt,
            facesTarget,
            orientTowards
        );
        if (effectID != null) _createdParticles.Add(effectID);
        if (effectID2 != null) _createdParticles.Add(effectID2);
    }

    public void SpellEffectRemove(EffectEmitter? effectID)
    {
        if (effectID != null)
        {
            F.SpellEffectRemove(effectID);
            _createdParticles.Remove(effectID);
        }
    }

    //TODO: Call from all child class `Deactivate`s
    protected internal void Cleanup()
    {
        foreach (var particle in _createdParticles)
        {
            particle.SetToRemove();
        }
    }
}