using CharScripts;

namespace Spells;

public class AzirBasicAttackSoldier: SpellScript
{
    public override SpellScriptMetadata MetaData { get; } = new()
    {
        OverrideFlags = (SpellDataFlags)252928,
        TriggersSpellCasts = true,
        IsDamagingSpell = true,
        NotSingleTargetSpell = false,
    };

    public override void SelfExecute()
    {
        //owner.PlayAnimation("",0,0,1, (AnimationFlags));
    }

    public override void TargetExecute(AttackableUnit target, SpellMissile? missileNetworkID, ref HitResult hitResult)
    {
        //((CharScriptAzir)owner.CharScript).AzirMinionManager.Soldiers[0].PlayAnimation("Dance");
    }
}