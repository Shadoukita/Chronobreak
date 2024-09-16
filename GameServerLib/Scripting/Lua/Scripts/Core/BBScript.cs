
using System;
using Chronobreak.GameServer.GameObjects.SpellNS;
using Chronobreak.GameServer.GameObjects.SpellNS.Missile;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using MoonSharp.Interpreter;

namespace Chronobreak.GameServer.Scripting.Lua
{
    public class BBScript
    {
        public BBScriptCtrReqArgs Args;
        public BBCacheEntry CacheEntry;
        public Table Globals => CacheEntry.Globals;
        public Table InstanceVars;
        public Table PassThrough;
        public static Table WorldVars = LuaScriptEngine.NewTable();
        public static BBScript? CurrentlyExecuted;
        public object WrapperScript;
        public BBScript(object wrapperScript, BBScriptCtrReqArgs args, Table? instanceVars = null)
        {
            Args = args;
            WrapperScript = wrapperScript;
            CacheEntry = LuaScriptEngine.GetBBCacheEntry(Args.Name);

            InstanceVars =
                (instanceVars != null) ?
                DeepCopy(instanceVars) :
                new Table(Globals.OwnerScript);

            PassThrough = new Table(Globals.OwnerScript);

            PassThrough["Owner"] = Args.ScriptOwner;

            PassThrough["WorldVars"] = WorldVars;
            if (Args.UnitOwner != null)
            {
                PassThrough["AvatarVars"] = Args.UnitOwner.BBAvatarVars;
            }
            PassThrough["CharVars"] = Args.ScriptOwner.BBCharVars;
            PassThrough["InstanceVars"] = InstanceVars;
        }
        private Table DeepCopy(Table orig)
        {
            var copy = new Table(Globals.OwnerScript);
            foreach (var pair in orig.Pairs)
            {
                copy[pair.Key] =
                    (pair.Value.Table != null) ?
                    DeepCopy(pair.Value.Table) :
                    pair.Value;
            }
            return copy;
        }

        public DynValue? Call(string funcname)
        {
            var bb = Globals.RawGet(funcname + "BuildingBlocks")?.Table;
            if (bb != null)
            {
                var prev = CurrentlyExecuted;
                CurrentlyExecuted = this;
                var ret = LuaScriptEngine.ExecBB(bb, PassThrough, funcname);
                CurrentlyExecuted = prev;
                return ret;
            }
            return null;
        }
        public T Call<T>(string funcname)
        {
            return Call<T>(funcname, DBNull.Value, false)!;
        }
        public T Call<T>(string funcname, object? defaultValue)
        {
            return Call<T>(funcname, defaultValue, false)!;
        }
        public T? CallWithNullableReturn<T>(string funcname)
        {
            return Call<T>(funcname, DBNull.Value, true);
        }
        public T? CallWithNullableReturn<T>(string funcname, object? defaultValue)
        {
            return Call<T>(funcname, defaultValue, true);
        }
        public T? Call<T>(string funcname, object? defaultValue, bool isNullable = false)
        {
            return (T?)LuaScriptEngine.DynValueToObject
            (
                Call(funcname), typeof(T),
                funcname, "ReturnValue",
                defaultValue, isNullable
            );
        }

        public void PreLoad()
        {
            Call("PreLoad");
        }

        internal void SetArgs(
            DamageData? damageData = null,
            DeathData? deathData = null,
            Spell? spell = null,
            SpellMissile? missile = null
        )
        {
            var BB = this;
            if (damageData != null)
            {
                BB.PassThrough["Target"] = damageData.Target;
                BB.PassThrough["Caster"] = damageData.Attacker;
                BB.PassThrough["Attacker"] = damageData.Attacker;
                Functions.SourceType = damageData.DamageSource;
                BB.PassThrough["DamageType"] = damageData.DamageType;
                BB.PassThrough["DamageAmount"] = damageData.Damage;
            }
            if (deathData != null)
            {
                BB.PassThrough["Attacker"] = deathData.Killer;
                BB.PassThrough["Target"] = deathData.Unit;
                //TODO: Everything else (if any)
            }
            if (spell != null)
            {
                Functions.CastedSpell = spell;
                BB.PassThrough["SpellName"] = spell.SpellName;
                BB.PassThrough["SpellVars"] = (spell.Script as BBSpellScript)?.SpellVars;
                BB.PassThrough["Slot"] = spell.Slot;
                BB.PassThrough["Level"] = spell.Level;
                //TODO: Everything else (if any)
            }
            if (missile != null)
            {
                BB.PassThrough["missileId"] = missile;
                //TODO: Everything else (if any)
            }
        }
    }
}