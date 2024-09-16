using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Chronobreak.GameServer;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.SpellNS.Missile;
using Chronobreak.GameServer.Scripting.Lua;

namespace GameServerLib.Scripting.Lua;

//These functions act like a proxy function to the real ones as the arguments in the Lua functions need conversion or are swapped.
//Do not touch
public static class FunctionsLua
{
    [BBFunc]
    public static void DestroyMissile(uint missileID)
    {
        if (Game.ObjectManager.GetObjectById(missileID) is SpellMissile missile) missile.SetToRemove();
    }

    //This function dynamically creates method proxies for stats related functions from Functions (Stats) with no attributes (exclude BB) and
    //swapped parameters (float, AttackableUnit) as the LUA functions use these arguments and converted the other (AttackableUnit, float) 

    internal static List<MethodInfo> GenerateDynamicProxies()
    {
        var methods = typeof(Functions)
            //To exclude the BB functions
            .GetMethods(BindingFlags.Public | BindingFlags.Static).Where(a => !a.CustomAttributes.Any())
            .Where(a => a.GetParameters().Select(p => p.ParameterType)
                .SequenceEqual(new[] { typeof(AttackableUnit), typeof(float) }));
        var dynamicAssembly =
            AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("FunctionsProxy"), AssemblyBuilderAccess.Run);
        var proxyMethodParams = new[] { typeof(float), typeof(AttackableUnit) };
        var dynamicModule = dynamicAssembly.DefineDynamicModule("FunctionsProxyModule");
        var newMethods = new List<MethodInfo>();
        foreach (var methodInfo in methods)
        {
            var dynamicType = dynamicModule.DefineType($"FunctionsProxyType_{methodInfo.Name}", TypeAttributes.Public);
            var dynamicMethod = dynamicType.DefineMethod(methodInfo.Name,
                MethodAttributes.Public | MethodAttributes.Static, null, proxyMethodParams);
            var ilGenerator = dynamicMethod.GetILGenerator();
            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.EmitCall(OpCodes.Call, methodInfo, null);
            ilGenerator.Emit(OpCodes.Ret);
            var proxyType = dynamicType.CreateType();
            newMethods.Add(proxyType.GetMethod(methodInfo.Name));
        }
        return newMethods;
    }
}