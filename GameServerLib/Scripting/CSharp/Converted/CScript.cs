
using GameServerCore.Enums;
using Chronobreak.GameServer.API;
using Chronobreak.GameServer.GameObjects.SpellNS;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;

namespace Chronobreak.GameServer.Scripting.CSharp.Converted;

public class CScript : CScriptBase
{
    private AttackableUnit _owner = null!;
    protected AttackableUnit owner => _owner!;
    protected ObjAIBase ownerObjAiBase => (_owner as ObjAIBase)!;
    protected ObjAIBase attacker => (_owner as ObjAIBase)!; //TODO: Move to child scripts?

    protected AttackableUnit target => _owner!; //TODO: Move to child scripts?

    public static DamageResultType HR2DRT(HitResult hr)
    {
        return hr switch
        {
            HitResult.HIT_Normal => DamageResultType.RESULT_NORMAL,
            HitResult.HIT_Critical => DamageResultType.RESULT_CRITICAL,
            HitResult.HIT_Dodge => DamageResultType.RESULT_DODGE,
            HitResult.HIT_Miss => DamageResultType.RESULT_MISS,
            _ => 0
        };
    }

    public static HitResult DRT2HR(DamageResultType hr)
    {
        return hr switch
        {
            DamageResultType.RESULT_NORMAL => HitResult.HIT_Normal,
            DamageResultType.RESULT_CRITICAL => HitResult.HIT_Critical,
            DamageResultType.RESULT_DODGE => HitResult.HIT_Dodge,
            DamageResultType.RESULT_MISS => HitResult.HIT_Miss,
            _ => 0
        };
    }

    protected void Init(AttackableUnit owner, ObjAIBase? attacker, Spell? spell)
    {
        base.InitAPI(attacker, spell);
        _owner = owner;
    }

    protected void Activate()
    {
        if (this.OnPreDealDamage != base.OnPreDealDamage)
        {
            ApiEventManager.OnPreDealDamage.AddListener(this, owner, (data) =>
            {
                var damageAmount = data.Damage;
                OnPreDealDamage(data.Target, ref damageAmount, data.DamageType, data.DamageSource);
                data.Damage = damageAmount;
            });
        }
        if (this.OnPreMitigationDamage != base.OnPreMitigationDamage)
        {
            ApiEventManager.OnPreMitigationDamage.AddListener(this, owner, (data) =>
            {
                var damageAmount = data.Damage;
                OnPreMitigationDamage(data.Target, ref damageAmount, data.DamageType, data.DamageSource);
                data.Damage = damageAmount;
            });
        }
        if (this.OnPreDamage != base.OnPreDamage)
        {
            ApiEventManager.OnPreTakeDamage.AddListener(this, owner, (data) =>
            {
                var damageAmount = data.Damage;
                OnPreDamage(data.Target, ref damageAmount, data.DamageType, data.DamageSource);
                data.Damage = damageAmount;
            });
        }
        if (this.OnTakeDamage != base.OnTakeDamage)
        {
            ApiEventManager.OnTakeDamage.AddListener(this, owner, (data) =>
            {
                var damageAmount = data.Damage;
                OnTakeDamage((ObjAIBase)data.Attacker, ref damageAmount, data.DamageType, data.DamageSource);
                data.Damage = damageAmount;
            });
        }
        if (this.OnDealDamage != base.OnDealDamage)
        {
            ApiEventManager.OnDealDamage.AddListener(this, owner, (data) =>
            {
                var damageAmount = data.Damage;
                OnDealDamage(data.Target, ref damageAmount, data.DamageType, data.DamageSource);
                data.Damage = damageAmount;
            });
        }
        if (this.OnHitUnit != base.OnHitUnit && owner is ObjAIBase a1)
        {
            ApiEventManager.OnHitUnit.AddListener(this, a1, (data) =>
            {
                var damage = data.Damage;
                var hitResult = DRT2HR(data.DamageResultType);
                OnHitUnit(data.Target, ref damage, data.DamageType, data.DamageSource,
                    ref hitResult);
                data.Damage = damage;
                data.DamageResultType = HR2DRT(hitResult);
            });
        }
        if (this.OnKill != base.OnKill)
        {
            ApiEventManager.OnKill.AddListener(this, owner, (data) => OnKill(data.Unit));
        }
        if (this.OnDeath != base.OnDeath)
        {
            ApiEventManager.OnDeath.AddListener(this, owner, (data) =>
            {
                bool becomeZombie = data.BecomeZombie;
                OnDeath((ObjAIBase)data.Killer, ref becomeZombie);
                data.BecomeZombie = becomeZombie;
            });
        }
        if (this.OnZombie != base.OnZombie)
        {
            ApiEventManager.OnZombie.AddListener(this, owner, data => OnZombie((ObjAIBase)data.Killer));
        }
        if (this.OnBeingHit != base.OnBeingHit)
        {
            ApiEventManager.OnBeingHit.AddListener(this, owner,
                (_, data) =>
                {
                    var damage = data.Damage;
                    OnBeingHit((ObjAIBase)data.Attacker, ref damage, data.DamageType, data.DamageSource,
                        DRT2HR(data.DamageResultType));
                    data.Damage = damage;
                });
        }
        if (this.OnBeingSpellHit != base.OnBeingSpellHit)
        {
            ApiEventManager.OnBeingSpellHit.AddListener(this, owner, (target, spell, missile) => OnBeingSpellHit(spell.Caster, spell, spell.Script.MetaData));
        }
        if (this.OnCollision != base.OnCollision)
        {
            //TODO: Call once and only at the end of dashes
            //TODO: ApiEventManager.OnCollision.AddListener(this, owner, (unit, with) => OnCollision(with));
        }
        if (this.OnCollisionTerrain != base.OnCollisionTerrain)
        {
            //TODO: Call once and only at the end of dashes
            //TODO: ApiEventManager.OnCollisionTerrain.AddListener(this, owner, (unit) => OnCollisionTerrain());
        }

        if (this.OnDisconnect != base.OnDisconnect)
        {
            if (owner is Champion champion)
            {
                ApiEventManager.OnDisconnect.AddListener(this, champion, _ => OnDisconnect());
            }
        }

        if (this.OnReconnect != base.OnReconnect)
        {
            if (owner is Champion champion)
            {
                ApiEventManager.OnReconnect.AddListener(this, champion, _ => OnReconnect());
            }
        }

        if (this.OnHeal != base.OnHeal)
        {
            ApiEventManager.OnHeal.AddListener(this, owner, heal =>
            {
                heal.HealAmount = OnHeal(heal.HealAmount);
            });
        }

        if (this.OnDodge != base.OnDodge)
        {
            ApiEventManager.OnDodge.AddListener(this, owner, (attacker, _) => OnDodge(attacker));
        }

        if (this.OnBeingDodged != base.OnBeingDodged)
        {
            ApiEventManager.OnBeingDodged.AddListener(this, owner, (_, target) => OnBeingDodged((ObjAIBase)target));
        }

        if (this.OnMiss != base.OnMiss)
        {
            ApiEventManager.OnMiss.AddListener(this, owner, (_, target) => OnMiss((ObjAIBase)target));
        }

        if (owner is ObjAIBase a2)
        {
            if (this.OnSpellCast != base.OnSpellCast)
            {
                ApiEventManager.OnUnitSpellCast.AddListener(this, a2, spell => OnSpellCast(spell, spell.Name, spell.Script.MetaData));
            }
            if (this.OnLevelUpSpell != base.OnLevelUpSpell)
            {
                ApiEventManager.OnUnitLevelUpSpell.AddListener(this, a2, (spell) => OnLevelUpSpell(spell.Slot));
            }
            if (this.OnSpellHit != base.OnSpellHit)
            {
                ApiEventManager.OnUnitSpellHit.AddListener(this, a2, (_, spell, target, missile) => OnSpellHit(target));
            }

            //HACK: Because spells can change
            foreach (Spell spell in a2.Spells)
            {
                if (this.OnLaunchMissile != base.OnLaunchMissile || this.OnMissileEnd != base.OnMissileEnd)
                {
                    ApiEventManager.OnLaunchMissile.AddListener(this, spell, (spell, missile) =>
                    {
                        //TODO: Do we need this at all?
                        //if (this.OnMissileEnd != base.OnMissileEnd)
                        //{
                        //    ApiEventManager.OnSpellMissileEnd.AddListener(this, missile, (missile) =>
                        //        OnMissileEnd(spell.SpellName, missile.Position3D)
                        //    );
                        //}
                        OnLaunchMissile(missile);
                    });
                }
            }
            if (this.OnPreAttack != base.OnPreAttack)
            {
                ApiEventManager.OnPreAttack.AddListener(this, a2, (attacker, spell, target) => OnPreAttack(target));
            }
            if (this.OnLaunchAttack != base.OnLaunchAttack)
            {
                ApiEventManager.OnLaunchAttack.AddListener(this, a2, (attacker, spell, target) => OnLaunchAttack(target));
            }
            if (this.OnResurrect != base.OnResurrect)
            {
                ApiEventManager.OnResurrect.AddListener(this, a2, (unit) => OnResurrect());
            }
        }

        if (this.OnLevelUp != base.OnLevelUp)
        {
            ApiEventManager.OnLevelUp.AddListener(this, owner, (unit) => OnLevelUp());
        }
        if (this.OnMoveEnd != base.OnMoveEnd)
        {
            ApiEventManager.OnMoveEnd.AddListener(this, owner, (unit) => OnMoveEnd());
        }
        if (this.OnMoveFailure != base.OnMoveFailure)
        {
            ApiEventManager.OnMoveFailure.AddListener(this, owner, (unit) => OnMoveFailure());
        }
        if (this.OnMoveSuccess != base.OnMoveSuccess)
        {
            ApiEventManager.OnMoveSuccess.AddListener(this, owner, (unit) => OnMoveSuccess());
        }
    }
}