using System.Collections.Generic;
using System.Numerics;
using GameServerCore.Enums;
using Chronobreak.GameServer.API;
using Chronobreak.GameServer.GameObjects;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.GameObjects.SpellNS;
using Chronobreak.GameServer.GameObjects.SpellNS.Missile;
using Chronobreak.GameServer.Scripting.CSharp;
using Chronobreak.GameServer.Scripting.Lua;
using Chronobreak.GameServer.Scripting.Lua.Scripts;
using FCS = Chronobreak.GameServer.Scripting.CSharp.Converted.Functions_CS;

namespace GameServerLib.Scripting.Lua.Scripts;

public class BBBuffScript : BBScriptInstance, IBuffScript
{
    private readonly List<EffectEmitter> EffectsCreatedOnActivation = [];
    protected Buff _buff = null!;
    private BuffScriptMetaData _metadata = null!;
    private float onUpdateStatsTrackTime;
    private float onUpdateTrackTime;
    private ObjAIBase _attacker;

    public string Name => _bb.Args.Name;
    public BBBuffScript(BBScriptCtrReqArgs baseArgs, GameObject baseAttacker) : base(baseArgs)
    {
        var args = Functions.NextBBBuffScriptCtrArgs;
        _bb = new BBScript(this, baseArgs, args?.BuffVarsTable);
        _metadata = args ?? BuffMetaData;
        MetaData = _bb.CacheEntry.GetMetadata<LuaBuffScriptMetadataUnmutable>();
        _attacker = (ObjAIBase)baseAttacker;
        _bb.PassThrough["Attacker"] = _attacker;
        _bb.PassThrough["Owner"] = baseArgs.ScriptOwner ?? _attacker;
        _bb.PassThrough["Target"] = baseArgs.ScriptOwner ?? _attacker;
    }

    protected override BBScriptType ScriptType => BBScriptType.Buff;
    public Buff Buff => _buff;
    public virtual BuffScriptMetadataUnmutable MetaData { get; }

    public virtual BuffScriptMetaData BuffMetaData => _metadata;

    public void Init(Buff buff)
    {
        base.Init(buff.TargetUnit, buff.SourceUnit, buff.OriginSpell);
        _metadata = new BuffScriptMetaData();
        _buff = buff;
    }

    public new void Activate()
    {
        ApplyAutoActivateEffects();
        base.Activate();
    }

    public void Deactivate(bool expired)
    {
        ApiEventManager.RemoveAllListenersForOwner(this);
        foreach (var effect in EffectsCreatedOnActivation)
        {
            //if(effect.Lifetime <= 0) // That is, if it is an eternal effect.
            {
                effect.SetToRemove();
            }
        }
        base.OnDeactivate(expired);
        Cleanup();
    }

    public override void OnUpdate()
    {
        if (FCS.ExecutePeriodically(_buff.TickRate, ref onUpdateTrackTime, true))
        {
            OnUpdateActions();
        }
    }

    public override void UpdateStats()
    {
        if (FCS.ExecutePeriodically(_buff.TickRate, ref onUpdateStatsTrackTime, true))
        {
            OnUpdateStats();
        }
    }

    public bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
    {
        _bb.PassThrough["Attacker"] = attacker;
        _bb.PassThrough["Type"] = type;
        _bb.PassThrough["Duration"] = duration;
        var ret = _bb.Call<bool>("BuffOnAllowAdd", true);
        duration = (float)(_bb.PassThrough.RawGet("Duration")?.Number ?? duration);
        return ret;
    }

    public void OnStackUpdate(int prevStack, int newStack)
    {
    }

    public object Clone()
    {
        return MemberwiseClone();
    }

    protected override void SetTypeDependentArgs()
    {
        _bb.PassThrough["LifeTime"] = _buff.TimeElapsed; //TODO: or Duration?
        _bb.PassThrough["Type"] = _buff.BuffType;
        _bb.PassThrough["Duration"] = _buff.Duration;
        _bb.PassThrough["Level"] = _buff.OriginSpell?.Level ?? 1;
    }

    private void ApplyAutoActivateEffects()
    {
        BuffScriptMetadataUnmutable m = MetaData;
        for (var i = 0; i < m.AutoBuffActivateEffect.Length; i++)
        {
            string? effect = m.AutoBuffActivateEffect[i];
            if (!string.IsNullOrEmpty(effect))
            {
                //TODO: GetValueOrDefault for Lists?
                string? bone = m.AutoBuffActivateAttachBoneName.Length > i ? m.AutoBuffActivateAttachBoneName[i] : "";

                EffectEmitter part = new
                (
                    effect,
                    owner,
                    owner,
                    owner,
                    bone,
                    bone
                );
                //TODO: don't add particles in the constructor!
                //Game.ObjectManager.AddObject(part);
                EffectsCreatedOnActivation.Add(part);
            }
        }
    }

    // Functions that require the current buff
    protected ObjAIBase GetBuffCasterUnit()
    {
        return _attacker;
    }

    protected void SpellBuffRemoveCurrent(AttackableUnit target)
    {
        target.Buffs.RemoveStack(_buff.Name);
    }

    public void SetBuffToolTipVar(int index, float value)
    {
        _buff.SetToolTipVar(index - 1, value);
    }

    public void UpdateBuffs()
    {
        SetTypeDependentArgs();
        _bb.Call("UpdateBuffs");
    }

    public override void OnUpdateStats()
    {
        SetTypeDependentArgs();
        _bb.Call("BuffOnUpdateStats");
    }

    public void OnActivate()
    {
        OnActivate();
    }

    public void OnDeactivate()
    {

    }

    public override void OnUpdateActions()
    {
        SetTypeDependentArgs();
        _bb.Call("BuffOnUpdateActions");
    }

    public bool OnAllowAdd(Buff buff)
    {
        _bb.PassThrough["Attacker"] = buff.SourceUnit;
        _bb.PassThrough["Type"] = buff.BuffType;
        _bb.PassThrough["Duration"] = buff.Duration;
        var ret = _bb.Call<bool>("BuffOnAllowAdd", true);
        buff.Duration = (float)(_bb.PassThrough.RawGet("Duration")?.Number ?? buff.Duration);
        return ret;
    }

    public override void OnBeingSpellHit(ObjAIBase attacker, Spell spell, SpellScriptMetadata spellVars)
    {
        SetTypeDependentArgs();
        _bb.PassThrough["Attacker"] = attacker;
        _bb.PassThrough["SpellName"] = spell.SpellName;
        _bb.PassThrough["SpellVars"] = (spell.Script as BBSpellScript)?.SpellVars;
        _bb.PassThrough["Slot"] = spell.Slot; //TODO: Slot = Slot - SlotType
        _bb.PassThrough["Level"] = spell.Level;
        _bb.Call("BuffOnBeingSpellHit");
    }

    public override void OnCollision()
    {
        SetTypeDependentArgs();
        _bb.Call("BuffOnCollision");
    }

    public override void OnCollisionTerrain()
    {
        SetTypeDependentArgs();
        _bb.Call("BuffOnCollisionTerrain");
    }

    public override float OnHeal(float health)
    {
        SetTypeDependentArgs();
        _bb.PassThrough["EffectiveHeal"] = health;
        return _bb.Call<float>("BuffOnHeal", health);
    }

    public override void OnLaunchMissile(SpellMissile missileId)
    {
        _bb.PassThrough["SpellName"] = missileId.SpellOrigin.SpellName;
        _bb.PassThrough["SpellVars"] = (missileId.SpellOrigin.Script as BBSpellScript)?.SpellVars;
        _bb.PassThrough["Slot"] = missileId.SpellOrigin.Slot; //TODO: Slot = Slot - SlotType
        _bb.PassThrough["Level"] = missileId.SpellOrigin.Level;
        _bb.PassThrough["missileId"] = missileId;
        _bb.Call("BuffOnLaunchMissile");
    }

    public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
    {
        SetTypeDependentArgs();
        _bb.Call("BuffOnMissileEnd");
    }

    public override void OnMoveEnd()
    {
        SetTypeDependentArgs();
        _bb.Call("BuffOnMoveEnd");
    }

    public override void OnMoveFailure()
    {
        SetTypeDependentArgs();
        _bb.Call("BuffOnMoveFailure");
    }

    public override void OnMoveSuccess()
    {
        SetTypeDependentArgs();
        _bb.Call("BuffOnMoveSuccess");
    }

    public override void OnSpellHit(AttackableUnit target)
    {
        SetTypeDependentArgs();
        _bb.PassThrough["Target"] = target;
        _bb.Call("BuffOnSpellHit");
    }

    public void OnUpdateAmmo()
    {
        SetTypeDependentArgs();
        _bb.Call("BuffOnUpdateAmmo");
    }
}