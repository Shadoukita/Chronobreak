
using System.Collections.Generic;
using GameServerCore.Enums;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.GameObjects.SpellNS;
using Chronobreak.GameServer.Scripting.CSharp;
using Chronobreak.GameServer.Scripting.CSharp.Converted;

namespace Chronobreak.GameServer.Scripting.Lua;

public enum BBScriptType
{
    Invalid,
    Char,
    Spell,
    Buff,
    Item
}

public class BBScriptInstance : CScript
{
    protected BBScript _bb = null!;
    public BBScriptInstance(BBScriptCtrReqArgs args)
    {
        ScriptType = BBScriptType.Invalid;
    }

    protected virtual BBScriptType ScriptType { get; }
    protected virtual void SetTypeDependentArgs()
    {
    }

    private void SetDmgArgs(ObjAIBase attacker, ObjAIBase target, float damageAmount, DamageType damageType,
        DamageSource damageSource, HitResult hitResult = HitResult.HIT_Invalid)
    {
        Functions.SourceType = damageSource;
        _bb.PassThrough["Attacker"] = attacker;
        _bb.PassThrough["Target"] = target;
        _bb.PassThrough["DamageType"] = damageType;
        _bb.PassThrough["DamageAmount"] = damageAmount;
        if (hitResult != HitResult.HIT_Invalid) _bb.PassThrough["HitResult"] = hitResult;
    }

    public override void OnTakeDamage(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
        DamageSource damageSource)
    {
        if (BBScriptMap.GetFunctionName(nameof(OnTakeDamage), ScriptType, out var functionName))
        {
            SetTypeDependentArgs();
            SetDmgArgs(attacker, ownerObjAiBase, damageAmount, damageType, damageSource);
            _bb.Call(functionName);
        }
    }

    public override void OnDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType,
        DamageSource damageSource)
    {
        if (BBScriptMap.GetFunctionName(nameof(OnDealDamage), ScriptType, out var functionName))
        {
            SetTypeDependentArgs();
            SetDmgArgs(ownerObjAiBase, (ObjAIBase)target, damageAmount, damageType, damageSource);
            _bb.Call(functionName);
        }
    }

    public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType,
        DamageSource damageSource)
    {
        if (BBScriptMap.GetFunctionName(nameof(OnPreDealDamage), ScriptType, out var functionName))
        {
            SetTypeDependentArgs();
            SetDmgArgs(ownerObjAiBase, (ObjAIBase)target, damageAmount, damageType, damageSource);
            _bb.Call(functionName);
            damageAmount = (float)(_bb.PassThrough.RawGet("DamageAmount")?.Number ?? damageAmount);
        }
    }

    public override void OnPreMitigationDamage(AttackableUnit target, ref float damageAmount, DamageType damageType,
        DamageSource damageSource)
    {
        if (BBScriptMap.GetFunctionName(nameof(OnPreDealDamage), ScriptType, out var functionName))
        {
            SetTypeDependentArgs();
            SetDmgArgs(ownerObjAiBase, (ObjAIBase)target, damageAmount, damageType, damageSource);
            _bb.Call(functionName);
            damageAmount = (float)(_bb.PassThrough.RawGet("DamageAmount")?.Number ?? damageAmount);
        }
    }

    public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType,
        DamageSource damageSource)
    {
        if (BBScriptMap.GetFunctionName(nameof(OnPreDamage), ScriptType, out var functionName))
        {
            SetTypeDependentArgs();
            SetDmgArgs(ownerObjAiBase, (ObjAIBase)target, damageAmount, damageType, damageSource);
            _bb.Call(functionName);
            damageAmount = (float)(_bb.PassThrough.RawGet("DamageAmount")?.Number ?? damageAmount);
        }
    }

    public override void OnLaunchAttack(AttackableUnit target)
    {
        if (BBScriptMap.GetFunctionName(nameof(OnLaunchAttack), ScriptType, out var functionName))
        {
            SetTypeDependentArgs();
            _bb.PassThrough["Target"] = target;
            _bb.Call(functionName);
        }
    }

    public override void OnPreAttack(AttackableUnit target)
    {
        if (BBScriptMap.GetFunctionName(nameof(OnPreAttack), ScriptType, out var functionName))
        {
            SetTypeDependentArgs();
            _bb.PassThrough["Target"] = target;
            _bb.Call(functionName);
        }
    }

    public override void OnAssist(ObjAIBase attacker, AttackableUnit target)
    {
        if (BBScriptMap.GetFunctionName(nameof(OnAssist), ScriptType, out var functionName))
        {
            SetTypeDependentArgs();
            _bb.PassThrough["Attacker"] = attacker;
            _bb.PassThrough["Target"] = target;
            _bb.Call(functionName);
        }
    }

    public override void OnZombie(ObjAIBase attacker)
    {
        if (BBScriptMap.GetFunctionName(nameof(OnZombie), ScriptType, out var functionName))
        {
            SetTypeDependentArgs();
            _bb.PassThrough["Attacker"] = attacker;
            _bb.Call(functionName);
        }
    }

    public override void OnBeingHit(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
        DamageSource damageSource,
        HitResult hitResult)
    {
        if (BBScriptMap.GetFunctionName(nameof(OnBeingHit), ScriptType, out var functionName))
        {
            SetTypeDependentArgs();
            SetDmgArgs(attacker, ownerObjAiBase, damageAmount, damageType, damageSource, hitResult);
            _bb.Call(functionName);
            damageAmount = (float)(_bb.PassThrough.RawGet("DamageAmount")?.Number ?? damageAmount);
        }
    }

    public override void OnBeingDodged(ObjAIBase target)
    {
        if (BBScriptMap.GetFunctionName(nameof(OnBeingDodged), ScriptType, out var functionName))
        {
            SetTypeDependentArgs();
            _bb.PassThrough["Target"] = target;
            _bb.Call(functionName);
        }
    }

    public override void OnKill(AttackableUnit target)
    {
        if (BBScriptMap.GetFunctionName(nameof(OnKill), ScriptType, out var functionName))
        {
            SetTypeDependentArgs();
            _bb.PassThrough["Target"] = target;
            _bb.Call(functionName);
        }
    }

    public override void OnDisconnect()
    {
        if (BBScriptMap.GetFunctionName(nameof(OnDisconnect), ScriptType, out var functionName))
        {
            SetTypeDependentArgs();
            _bb.Call(functionName);
        }
    }

    public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
    {
        if (BBScriptMap.GetFunctionName(nameof(OnSpellCast), ScriptType, out var functionName))
        {
            Functions.CastedSpell = spell;
            SetTypeDependentArgs();
            _bb.Call(functionName);
        }
    }

    public override void OnMiss(AttackableUnit target)
    {
        if (BBScriptMap.GetFunctionName(nameof(OnMiss), ScriptType, out var functionName))
        {
            SetTypeDependentArgs();
            _bb.PassThrough["Attacker"] = attacker;
            _bb.PassThrough["Target"] = target;
            _bb.Call(functionName);
        }
    }

    public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
    {
        if (BBScriptMap.GetFunctionName(nameof(OnDeath), ScriptType, out var functionName))
        {
            SetTypeDependentArgs();
            _bb.PassThrough["Attacker"] = attacker;
            _bb.Call(functionName);
            becomeZombie = _bb.PassThrough.RawGet("BecomeZombie")?.CastToBool() ?? false;
        }
    }

    public override void OnLevelUp()
    {
        if (BBScriptMap.GetFunctionName(nameof(OnLevelUp), ScriptType, out var functionName))
        {
            SetTypeDependentArgs();
            _bb.Call(functionName);
        }
    }

    public override void OnLevelUpSpell(int slot)
    {
        if (BBScriptMap.GetFunctionName(nameof(OnLevelUpSpell), ScriptType, out var functionName))
        {
            SetTypeDependentArgs();
            _bb.PassThrough["Slot"] = slot;
            _bb.Call(functionName);
        }
    }

    public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
        DamageSource damageSource,
        ref HitResult hitResult)
    {
        if (BBScriptMap.GetFunctionName(nameof(OnHitUnit), ScriptType, out var functionName))
        {
            SetTypeDependentArgs();
            SetDmgArgs(ownerObjAiBase, (ObjAIBase)target, damageAmount, damageType, damageSource, hitResult);
            _bb.Call(functionName);
            damageAmount = (float)(_bb.PassThrough.RawGet("DamageAmount")?.Number ?? damageAmount);
            hitResult = _bb.PassThrough.RawGet("HitResult")?.ToObject<HitResult>() ?? hitResult;
        }
    }

    public override void OnResurrect()
    {
        if (BBScriptMap.GetFunctionName(nameof(OnLevelUpSpell), ScriptType, out var functionName))
        {
            SetTypeDependentArgs();
            _bb.Call(functionName);
        }
    }

    public override void OnReconnect()
    {
        if (BBScriptMap.GetFunctionName(nameof(OnReconnect), ScriptType, out var functionName))
        {
            SetTypeDependentArgs();
            _bb.Call(functionName);
        }
    }

    public new void Activate()
    {
        base.Activate();
        if (BBScriptMap.GetFunctionName("OnActivate", ScriptType, out var functionName))
        {
            SetTypeDependentArgs();
            _bb.Call(functionName);
        }
    }

    public virtual void OnDeactivate(bool expired)
    {
        if (BBScriptMap.GetFunctionName(nameof(OnDeactivate), ScriptType, out var functionName))
        {
            SetTypeDependentArgs();
            _bb.PassThrough["Expired"] = expired;
            _bb.Call(functionName);
        }
    }
}

internal static class BBScriptMap
{
    private static readonly Dictionary<string, Dictionary<BBScriptType, string>> DictionaryMap =
        new()
        {
            {
                "OnTakeDamage", new Dictionary<BBScriptType, string>
                {
                    { BBScriptType.Buff, "BuffOnTakeDamage" },
                    { BBScriptType.Char, "CharOnTakeDamage" }
                }
            },
            {
                "OnActivate", new Dictionary<BBScriptType, string>
                {
                    { BBScriptType.Char, "CharOnActivate" },
                    { BBScriptType.Item, "OnActivate" },
                    { BBScriptType.Buff, "OnBuffActivate" }
                }
            },
            {
                "OnDeactivate", new Dictionary<BBScriptType, string>
                {
                    { BBScriptType.Item, "OnDeactivate" },
                    { BBScriptType.Buff, "OnBuffDeactivate" }
                }
            },
            {
                "UpdateSelfBuffActions", new Dictionary<BBScriptType, string>
                {
                    { BBScriptType.Char, "UpdateSelfBuffActions" },
                    { BBScriptType.Item, "UpdateSelfBuffActions" }
                }
            },
            {
                "UpdateSelfBuffStats", new Dictionary<BBScriptType, string>
                {
                    { BBScriptType.Char, "UpdateSelfBuffStats" },
                    { BBScriptType.Item, "UpdateSelfBuffStats" }
                }
            },
            {
                "OnAssist", new Dictionary<BBScriptType, string>
                {
                    { BBScriptType.Char, "CharOnAssistUnit" },
                    { BBScriptType.Item, "ItemOnAssist" },
                    { BBScriptType.Buff, "BuffOnAssist" }
                }
            },
            {
                "OnBeingHit", new Dictionary<BBScriptType, string>
                {
                    { BBScriptType.Char, "CharOnBeingHit" },
                    { BBScriptType.Item, "ItemOnBeingHit" },
                    { BBScriptType.Buff, "BuffOnBeingHit" }
                }
            },
            {
                "OnHitUnit", new Dictionary<BBScriptType, string>
                {
                    { BBScriptType.Char, "CharOnHitUnit" },
                    { BBScriptType.Item, "ItemOnHitUnit" },
                    { BBScriptType.Buff, "BuffOnHitUnit" }
                }
            },
            {
                "OnKill", new Dictionary<BBScriptType, string>
                {
                    { BBScriptType.Char, "CharOnKillUnit" },
                    { BBScriptType.Item, "ItemOnKill" },
                    { BBScriptType.Buff, "BuffOnKill" }
                }
            },
            {
                "OnMiss", new Dictionary<BBScriptType, string>
                {
                    { BBScriptType.Char, "CharOnMiss" },
                    { BBScriptType.Item, "ItemOnMiss" },
                    { BBScriptType.Buff, "BuffOnMiss" }
                }
            },
            {
                "OnPreDamage", new Dictionary<BBScriptType, string>
                {
                    { BBScriptType.Char, "CharOnPreDamage" },
                    { BBScriptType.Item, "ItemOnPreDamage" },
                    { BBScriptType.Buff, "BuffOnPreDamage" }
                }
            },
            {
                "OnPreDealDamage", new Dictionary<BBScriptType, string>
                {
                    { BBScriptType.Char, "CharOnPreDealDamage" },
                    { BBScriptType.Item, "ItemOnPreDealDamage" },
                    { BBScriptType.Buff, "BuffOnPreDealDamage" }
                }
            },
            {
                "OnSpellCast", new Dictionary<BBScriptType, string>
                {
                    { BBScriptType.Char, "CharOnSpellCast" },
                    { BBScriptType.Item, "ItemOnSpellCast" },
                    { BBScriptType.Buff, "BuffOnSpellCast" }
                }
            },
            {
                "OnLevelUp", new Dictionary<BBScriptType, string>
                {
                    { BBScriptType.Char, "CharOnLevelUp" },
                    { BBScriptType.Buff, "BuffOnLevelUp" }
                }
            },
            {
                "OnLevelUpSpell", new Dictionary<BBScriptType, string>
                {
                    { BBScriptType.Char, "CharOnLevelUpSpell" },
                    { BBScriptType.Buff, "BuffOnLevelUpSpell" }
                }
            },
            {
                "OnDeath", new Dictionary<BBScriptType, string>
                {
                    { BBScriptType.Item, "ItemOnDeath" },
                    { BBScriptType.Buff, "BuffOnDeath" }
                }
            },
            {
                "OnLaunchAttack", new Dictionary<BBScriptType, string>
                {
                    { BBScriptType.Char, "CharOnLaunchAttack" },
                    { BBScriptType.Buff, "BuffOnLaunchAttack" }
                }
            },
            {
                "OnPreAttack", new Dictionary<BBScriptType, string>
                {
                    { BBScriptType.Char, "CharOnPreAttack" },
                    { BBScriptType.Buff, "BuffOnPreAttack" }
                }
            }
        };

    public static bool GetFunctionName(string callingName, BBScriptType type, out string functionName)
    {
        if (DictionaryMap.TryGetValue(callingName, out var availableTypes))
            if (availableTypes.TryGetValue(type, out var result))
            {
                functionName = result;
                return true;
            }

        functionName = null!;
        return false;
    }
}