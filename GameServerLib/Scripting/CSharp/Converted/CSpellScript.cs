using System.Numerics;
using GameServerCore.Enums;
using Chronobreak.GameServer.GameObjects.SpellNS;
using Chronobreak.GameServer.GameObjects.SpellNS.Missile;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using CSF = Chronobreak.GameServer.Scripting.CSharp.Converted.Functions_CS;

namespace Chronobreak.GameServer.Scripting.CSharp.Converted;

public class CSpellScript : CInstance, ISpellScript
{
    protected ObjAIBase owner => _owner;
    protected ObjAIBase attacker => _owner;
    protected AttackableUnit target => _spell.GetTarget();
    protected ObjAIBase caster => _owner;

    private int? _levelOverride = null;
    protected int level
    {
        get
        {
            if (_levelOverride == null)
                return _spell.CurrentCastInfo?.SpellLevel ?? spell.Level;

            var localLevel = _levelOverride.Value;
            _levelOverride = null;
            return localLevel;
        }
        set => _levelOverride = value;
    }

    protected int slot => _spell.Slot;
    protected Spell spell => _spell;

    public virtual SpellScriptMetadata MetaData { get; } = new();

    private Spell _spell = null!;
    private ObjAIBase _owner = null!;
    public virtual void Init(Spell spell, ObjAIBase owner)
    {
        base.InitAPI(owner, spell);

        _spell = spell;
        _owner = owner;
    }

    public virtual void PreLoad() { }
    public virtual void OnMissileUpdate(SpellMissile missileNetworkID, Vector3 missilePosition) { }
    public virtual void OnMissileEnd(string spellName, Vector3 missileEndPosition) { }
    public virtual void OnSpellMissileHitTerrain(string spellName, SpellMissile missile) { }
    // SPELL SPECIFIC
    public virtual bool CanCast()
    {
        return true;
    }
    public virtual void SelfExecute() { }
    public virtual void TargetExecute(AttackableUnit target, SpellMissile? missileNetworkID,
        ref HitResult hitResult)
    { }
    public virtual void AdjustCastInfo() { }
    public virtual float AdjustCooldown()
    {
        return float.NaN;
    }
    public virtual void ChannelingStart() { }
    public virtual void ChannelingCancelStop() { }
    public virtual void ChannelingSuccessStop() { }
    public virtual void ChannelingStop() { }
    public virtual void ChannelingUpdateStats() { }
    public virtual void ChannelingUpdateActions() { }
    public virtual void UpdateTooltip(int spellSlot) { }

    public void OnActivate()
    {
    }

    public void OnDeactivate()
    {
    }

    public void OnSpellHit(AttackableUnit target, SpellMissile missile)
    {
    }

    public void OnSpellPreCast(AttackableUnit target, Vector2 start, Vector2 end)
    {
    }
    public void OnSpellCast()
    {
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

    private float OnUpdateStatsTimeTracker = 0;
    public void OnUpdateStats()
    {
        if (
            _spell.State == SpellState.CHANNELING
            && CSF.ExecutePeriodically(0.25f, ref OnUpdateStatsTimeTracker, true) //TODO: Verify
        )
        {
            ChannelingUpdateStats();
        }
    }
    private float OnUpdateTimeTracker = 0;
    public void OnUpdate()
    {

        UpdateTooltip(_spell.Slot); //TODO: Verify

        if (
            _spell.State == SpellState.CHANNELING
            && CSF.ExecutePeriodically(0.25f, ref OnUpdateTimeTracker, true) //TODO: Verify
        )
        {
            ChannelingUpdateActions();
        }
    }

    public virtual void OnMissileUpdate(SpellMissile missile)
    {
        OnMissileUpdate(missile, missile.Position3D);
    }

    public virtual void OnMissileEnd(SpellMissile missile)
    {
        OnMissileEnd(_spell.Script.GetType().Name, missile.Position3D);
    }

    public virtual void OnSpellMissileHitTerrain(SpellMissile missile)
    {
        OnSpellMissileHitTerrain(_spell.Script.GetType().Name, missile);
    }

    // Functions that require the current spell
    protected ObjAIBase GetBuffCasterUnit()
    {
        return caster;
    }
    public void SetSpellCastRange(float newRange)
    {
        //TODO: _spell.CastRange = newRange;
    }
}
