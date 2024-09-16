
using GameServerCore.Enums;
using System.Collections.Generic;
using Chronobreak.GameServer.API;
using Chronobreak.GameServer.GameObjects;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using FCS = Chronobreak.GameServer.Scripting.CSharp.Converted.Functions_CS;

namespace Chronobreak.GameServer.Scripting.CSharp.Converted;

public class CBuffScript : CScript, IBuffScript
{
    public virtual BuffScriptMetadataUnmutable MetaData { get; } = new();

    public virtual BuffScriptMetaData BuffMetaData => _metadata;
    //TODO Change?
    public string Name => this.GetType().Name;
    private BuffScriptMetaData _metadata = null!;

    private List<EffectEmitter> EffectsCreatedOnActivation = [];

    protected float lifeTime => _buff.TimeElapsed; //TODO: Verify
    protected new AttackableUnit owner => _buff.TargetUnit;
    protected new ObjAIBase attacker => _buff.SourceUnit;
    protected new AttackableUnit target => _buff.TargetUnit;
    protected ObjAIBase caster => _buff.SourceUnit;
    public Buff Buff => _buff;

    protected Buff _buff = null!;
    public void Init(Buff buff)
    {
        base.Init(buff.TargetUnit, buff.SourceUnit, buff.OriginSpell);
        _metadata = new();
        _buff = buff;
    }

    public virtual void UpdateBuffs() { }
    public virtual void OnUpdateAmmo() { }
    public virtual bool OnAllowAdd(ObjAIBase attacker, BuffType type,
        string scriptName,
        int maxStack, ref float duration)
    {
        return true;
    }
    public new void Activate()
    {
        ApplyAutoActivateEffects();
        OnActivate();
        base.Activate();
    }
    public virtual void OnActivate()
    {
    }

    public void OnDeactivate()
    {

    }

    private void ApplyAutoActivateEffects()
    {
        /*var m = MetaData;
        for (int i = 0; i < m.AutoBuffActivateEffect.Length; i++)
        {
            var effect = m.AutoBuffActivateEffect[i];
            if (!string.IsNullOrEmpty(effect))
            {
                var bone = (
                    //TODO: GetValueOrDefault for Lists?
                    (m.AutoBuffActivateAttachBoneName.Length > i) ?
                    m.AutoBuffActivateAttachBoneName[i] : null
                ) ?? "";
                var part = new Particle(
                    effect, owner,
                    default, owner, bone,
                    default, owner, bone
                );
                //TODO: don't add particles in the constructor!
                //Game.ObjectManager.AddObject(part);
                EffectsCreatedOnActivation.Add(part);
            }
        }*/
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
        OnDeactivate(expired);
        Cleanup();
    }
    public virtual void OnDeactivate(bool expired)
    {
    }
    public void OnStackUpdate(int prevStack, int newStack)
    {
    }

    private float onUpdateTrackTime = 0;
    public override void OnUpdate()
    {
        if (FCS.ExecutePeriodically(_buff.TickRate, ref onUpdateTrackTime, true))
        {
            OnUpdateActions();
        }
    }
    private float onUpdateStatsTrackTime = 0;
    public override void UpdateStats()
    {
        if (FCS.ExecutePeriodically(_buff.TickRate, ref onUpdateStatsTrackTime, true))
        {
            OnUpdateStats();
        }
    }

    // Functions that require the current buff
    protected ObjAIBase GetBuffCasterUnit()
    {
        return caster;
    }
    protected void SpellBuffRemoveCurrent(AttackableUnit target)
    {
        target.Buffs.RemoveStack(_buff.Slot);
    }
    public void SetBuffToolTipVar(int index, float value)
    {
        _buff.SetToolTipVar(index - 1, value);
    }
    public object Clone()
    {
        return MemberwiseClone();
    }
}