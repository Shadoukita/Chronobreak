
using System.Numerics;
using GameServerCore.Enums;
using Chronobreak.GameServer.GameObjects.SpellNS;
using Chronobreak.GameServer.GameObjects.SpellNS.Missile;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;

namespace Chronobreak.GameServer.Scripting.CSharp.Converted;

// Contains all common script methods
public class CScriptBase : CInstance
{
    //public virtual void PreLoad(){}
    //public virtual void OnActivate(){}
    //public virtual void OnDeactivate(){}

    public virtual void OnUpdate() => OnUpdateActions();
    public virtual void UpdateStats() => OnUpdateStats();

    // UPDATE
    public virtual void OnUpdateStats() { }
    public virtual void OnUpdateActions() { }

    public virtual void OnDodge(AttackableUnit attacker) { }
    public virtual void OnBeingDodged(ObjAIBase target) { }

    // HIT
    public virtual void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
        DamageSource damageSource, ref HitResult hitResult)
    { }
    public virtual void OnBeingHit(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
        DamageSource damageSource, HitResult hitResult)
    { }
    public virtual void OnSpellHit(AttackableUnit target) { }
    public virtual void OnBeingSpellHit(ObjAIBase attacker, Spell spell, SpellScriptMetadata spellVars) { }
    public virtual void OnMiss(AttackableUnit target) { }

    public virtual void OnMoveEnd() { }
    public virtual void OnMoveFailure() { }
    public virtual void OnMoveSuccess() { }
    public virtual void OnCollision() { }
    public virtual void OnCollisionTerrain() { }

    // DEATH
    public virtual void OnKill(AttackableUnit target) { }
    public virtual void OnAssist(ObjAIBase attacker, AttackableUnit target) { }
    public virtual void OnDeath(ObjAIBase attacker, ref bool becomeZombie) { }
    public virtual void OnNearbyDeath(ObjAIBase attacker, AttackableUnit target) { }
    public virtual void OnZombie(ObjAIBase attacker) { }
    public virtual void OnResurrect() { }

    // CONNECTION
    public virtual void OnDisconnect() { }
    public virtual void OnReconnect() { }

    // LEVEL
    public virtual void OnLevelUp() { }
    public virtual void OnLevelUpSpell(int slot) { }

    public virtual void OnPreAttack(AttackableUnit target) { }
    public virtual void OnLaunchAttack(AttackableUnit target) { }
    public virtual void OnLaunchMissile(SpellMissile missileId) { }
    public virtual void OnMissileUpdate(SpellMissile missileNetworkID, Vector3 missilePosition) { }
    public virtual void OnMissileEnd(string spellName, Vector3 missileEndPosition) { }

    // DAMAGE
    public virtual void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource) { }
    public virtual void OnPreMitigationDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource) { }
    public virtual void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource) { }
    public virtual void OnTakeDamage(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
        DamageSource damageSource)
    { }
    public virtual void OnDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType,
        DamageSource damageSource)
    { }

    public virtual float OnHeal(float health)
    {
        return 0;
    }

    public virtual void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars) { }
}