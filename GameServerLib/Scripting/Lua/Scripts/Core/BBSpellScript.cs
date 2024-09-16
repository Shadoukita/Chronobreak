
using System.Numerics;
using GameServerCore.Enums;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.GameObjects.SpellNS;
using Chronobreak.GameServer.GameObjects.SpellNS.Missile;
using Chronobreak.GameServer.Scripting.CSharp;
using Chronobreak.GameServer.Scripting.CSharp.Converted;
using Chronobreak.GameServer.Scripting.Lua.Scripts;
using MoonSharp.Interpreter;

namespace Chronobreak.GameServer.Scripting.Lua;

public class BBSpellScript : BBScriptInstance, ISpellScript
{
    private float OnUpdateStatsTimeTracker;
    private float OnUpdateTimeTracker;
    public Table SpellVars;

    public BBSpellScript(BBScriptCtrReqArgs args) : base(args)
    {
        _bb = new BBScript(this, args);
        MetaData = _bb.CacheEntry.GetMetadata<LuaSpellScriptMetadata>();

        //TODO: Populate SpellVars Table with SpellScriptMetadata
        SpellVars = new Table(_bb.Globals.OwnerScript);
        _bb.PassThrough["SpellVars"] = SpellVars;
    }

    protected override BBScriptType ScriptType => BBScriptType.Spell;
    public Spell Spell { get; internal set; } = null!;

    public SpellScriptMetadata MetaData { get; }

    public void SelfExecute()
    {
        SetTypeDependentArgs();
        _bb.Call("SelfExecute");
    }

    public void TargetExecute(AttackableUnit target, SpellMissile? missile, ref HitResult hitResult)
    {
        SetTypeDependentArgs();
        _bb.PassThrough["Target"] = target;
        _bb.PassThrough["MissileNetworkID"] = missile?.NetId ?? 0;
        _bb.PassThrough["MissileEndPosition"] = missile?.GetPosition3D() ?? Vector3.Zero;
        _bb.PassThrough["HitResult"] = hitResult;
        _bb.Call("TargetExecute");
        hitResult = _bb.PassThrough.RawGet("HitResult")?.ToObject<HitResult>() ?? hitResult;
    }

    public void AdjustCastInfo() //TODO:
    {
        SetTypeDependentArgs();
        _bb.Call("AdjustCastInfo");
    }

    public float AdjustCooldown() //TODO:
    {
        SetTypeDependentArgs();
        return _bb.Call<float>("AdjustCooldown", float.NaN);
    }

    public bool CanCast()
    {
        SetTypeDependentArgs();
        return _bb.Call<bool>("CanCast", true);
    }

    public void ChannelingStart()
    {
        SetTypeDependentArgs();
        _bb.PassThrough["CastPosition"] = Spell.CurrentCastInfo?.TargetPosition ?? Vector3.Zero;
        _bb.Call("ChannelingStart");
    }

    public void ChannelingCancelStop()
    {
        SetTypeDependentArgs();
        _bb.Call("ChannelingCancelStop");
    }

    public void ChannelingSuccessStop()
    {
        SetTypeDependentArgs();
        _bb.Call("ChannelingSuccessStop");
    }

    public void ChannelingStop()
    {
        SetTypeDependentArgs();
        _bb.Call("ChannelingStop");
    }

    public void OnMissileEnd(SpellMissile missile)
    {
        SetTypeDependentArgs();
        _bb.PassThrough["MissileNetworkID"] = missile?.NetId ?? 0;
        _bb.PassThrough["MissileEndPosition"] = missile?.Position3D;
        _bb.Call("SpellOnMissileEnd");
    }

    public void Init(Spell spell, ObjAIBase owner)
    {
        Spell = spell;
        _bb.PassThrough["Attacker"] = owner;
        _bb.PassThrough["Owner"] = owner;
        _bb.PassThrough["Caster"] = owner;
        base.Init(owner, owner, spell);
    }

    public void OnActivate()
    {
        Activate();
    }

    public void OnDeactivate()
    {
        base.OnDeactivate(true);
    }

    public void OnSpellHit(AttackableUnit target, SpellMissile? missile)
    {
        _bb.PassThrough["Target"] = target;
        _bb.PassThrough["MissileNetworkID"] = missile?.NetId ?? 0;
        _bb.PassThrough["MissileEndPosition"] = missile?.Position3D;
        base.OnSpellHit(target);
    }

    public void OnSpellPreCast(AttackableUnit? target, Vector2 start, Vector2 end)
    {
    }

    public void OnSpellCast()
    {
        Functions.CastedSpell = Spell;
        _bb.PassThrough["Target"] = Spell.CurrentCastInfo?.Target?.Unit ?? Spell.CurrentCastInfo?.Caster ?? owner;
        _bb.Call("SelfExecute");
    }

    public void OnSpellPostCast()
    {
    }

    public void OnSpellChannel()
    {
    }

    public void OnSpellChannelCancel(ChannelingStopSource reason)
    {
    }

    public void OnSpellPostChannel()
    {
    }

    public void OnMissileUpdate(SpellMissile missile)
    {
        _bb.PassThrough["MissileNetworkID"] = missile?.NetId ?? 0;
        _bb.PassThrough["MissileEndPosition"] = missile?.Position3D;
        _bb.Call("SpellOnMissileUpdate");
    }

    public override void UpdateStats()
    {
        if (
            Spell.State == SpellState.CHANNELING
            && Functions_CS.ExecutePeriodically(0.25f, ref OnUpdateStatsTimeTracker, true) //TODO: Verify
        )
            ChannelingUpdateStats();
    }

    public override void OnUpdate()
    {
        UpdateTooltip(); //TODO: Verify
        if (
            Spell.State == SpellState.CHANNELING
            && Functions_CS.ExecutePeriodically(0.25f, ref OnUpdateTimeTracker, true) //TODO: Verify
        )
            ChannelingUpdateActions();
    }

    protected override void SetTypeDependentArgs()
    {
        _bb.PassThrough["Level"] = Spell.CurrentCastInfo?.SpellLevel ?? 1;
        var slot = Spell.Slot;
        //TODO: SlotNumber = SlotNumber - SpellSlotType
        //if (Spell.IsSummonerSpell) slot -= (int)SpellSlotType.SummonerSpellSlots;
        _bb.PassThrough["Slot"] = slot;
        _bb.PassThrough["SlotNumber"] = slot;
        _bb.PassThrough["SpellSlot"] = slot;
    }


    public void PreLoad() //TODO:
    {
        SetTypeDependentArgs();
        _bb.Call("PreLoad");
    }

    public void ChannelingUpdateStats()
    {
        SetTypeDependentArgs();
        _bb.Call("ChannelingUpdateStats");
    }

    public void ChannelingUpdateActions()
    {
        SetTypeDependentArgs();
        _bb.Call("ChannelingUpdateActions");
    }

    public void UpdateTooltip() //TODO:
    {
        SetTypeDependentArgs();
        _bb.Call("SpellUpdateTooltip");
    }
}