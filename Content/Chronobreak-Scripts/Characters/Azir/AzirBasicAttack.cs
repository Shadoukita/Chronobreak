using GameServerLib.Services;

namespace Spells;

/// <summary>
/// NOTE: Issue with the beam FX not going off
/// </summary>
public class AzirBasicAttack : SpellScript
{
    public override SpellScriptMetadata MetaData { get; } = new()
    {
        OverrideFlags = (SpellDataFlags)252928,
        TriggersSpellCasts = true,
        IsDamagingSpell = true,
        NotSingleTargetSpell = false
    };

    public override void TargetExecute(AttackableUnit target, SpellMissile? missileNetworkID, ref HitResult hitResult)
    {
        var tar = new EffectEmitter(
            "Azir", 
            "Azir_base_BA_tar.troy", 
            owner, 
            target, 
            target, 
            "", 
            "C_Buffbone_Glb_Chest_Loc", 
            owner.Position, 
            owner.Direction, 
            target.Position,
            flags: (FXFlags) 416, 
            team: owner.Team);
        
        var beam = new EffectEmitter(
            "Azir", 
            "Azir_Base_BA_Beam.troy", 
            owner, 
            target, 
            owner, 
            "C_Buffbone_Glb_Chest_Loc", 
            "Buffbone_Glb_Weapon_1", 
            owner.Position,
            team: owner.Team);
        SpecialEffectService.SpawnFx([[tar],[beam]], owner.NetId);
        
        var baseAttackDamage = GetBaseAttackDamage(target);
        target.TakeDamage(attacker, baseAttackDamage, DamageType.DAMAGE_TYPE_PHYSICAL,
            DamageSource.DAMAGE_SOURCE_ATTACK,
            DamageResultType.RESULT_NORMAL, spell);
    }
}