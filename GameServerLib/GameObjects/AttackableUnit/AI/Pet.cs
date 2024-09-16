using System.Numerics;
using GameServerCore.Enums;
using GameServerLib.GameObjects;
using Chronobreak.GameServer.Content;
using Chronobreak.GameServer.GameObjects.SpellNS;
using Chronobreak.GameServer.GameObjects.StatsNS;

namespace Chronobreak.GameServer.GameObjects.AttackableUnits.AI;

public class Pet : Minion
{
    private float _returnRadius;

    /// <summary>
    /// Entity that the pet is cloning (Ex. Who Mordekaiser's ghost is)
    /// </summary>
    public ObjAIBase? ClonedUnit { get; private set; }
    /// <summary>
    /// Buff Assigned to this Pet at Spawn
    /// </summary>
    public string CloneBuffName { get; }
    /// <summary>
    /// Spell that created this Pet
    /// </summary>
    public Spell SourceSpell { get; }
    /// <summary>
    /// Duration of CloneBuff
    /// </summary>
    public float LifeTime { get; }
    public bool CloneInventory { get; }
    public bool DoFade { get; }
    public bool ShowMinimapIconIfClone { get; }
    public bool DisallowPlayerControl { get; }
    public bool IsClone => ClonedUnit is not null;
    public override bool HasSkins => true;

    public Pet
    (
        Champion owner,
        Spell spell,
        Vector2 position,
        string name,
        string model,
        string buffName,
        float lifeTime,
        Stats stats = null,
        bool cloneInventory = true,
        bool showMinimapIfClone = true,
        bool disallowPlayerControl = false,
        bool doFade = false,
        string AIScript = "Pet.lua"
    ) : base(owner, position, model, name, owner.Team, owner.SkinID, stats: stats, AIScript: AIScript)
    {
        _returnRadius = GlobalData.ObjAIBaseVariables.DefaultPetReturnRadius;

        SourceSpell = spell;
        LifeTime = lifeTime;
        CloneBuffName = buffName;
        CloneInventory = cloneInventory;
        ShowMinimapIconIfClone = showMinimapIfClone;
        DisallowPlayerControl = disallowPlayerControl;
        DoFade = doFade;

        GoldOwner = new GoldOwner(owner);
        Owner!.SetPet(this);
        Game.ObjectManager.AddObject(this);
    }

    public Pet
    (
        Champion owner,
        Spell spell,
        ObjAIBase cloned,
        Vector2 position,
        string buffName,
        float lifeTime,
        Stats? stats = null,
        bool cloneInventory = true,
        bool showMinimapIfClone = true,
        bool disallowPlayerControl = false,
        bool doFade = false,
        string AIScript = "Pet.lua"
    ) : base(owner, cloned.Position, cloned.Model, cloned.Name, owner.Team, cloned.SkinID, stats: stats, AIScript: AIScript)
    {
        if (position == Vector2.Zero)
        {
            Position = cloned.Position;
        }
        else
        {
            Position = position;
        }

        SourceSpell = spell;
        LifeTime = lifeTime;
        ClonedUnit = cloned;
        CloneBuffName = buffName;
        CloneInventory = cloneInventory;
        ShowMinimapIconIfClone = showMinimapIfClone;
        DisallowPlayerControl = disallowPlayerControl;
        DoFade = doFade;

        GoldOwner = new GoldOwner(owner);
        Owner!.SetPet(this);
        Game.ObjectManager.AddObject(this);
    }

    protected override void OnSpawn(int userId, TeamId team, bool doVision)
    {
        Game.PacketNotifier.NotifySpawnPet(this, userId, team, doVision);
    }

    internal override void OnAdded()
    {
        base.OnAdded();
        Buffs.Add(CloneBuffName, LifeTime, 1, SourceSpell, this, Owner);

        if (CloneInventory)
        {
            foreach (var item in ClonedUnit?.ItemInventory.GetItems() ?? [])
            {
                ItemInventory.AddItem(item.ItemData);
            }
        }
    }

    protected override void OnReachedDestination()
    {
        SetAIState(AIState.AI_PET_IDLE);
        base.OnReachedDestination();
    }

    public float GetReturnRadius()
    {
        return _returnRadius;
    }

    public void SetReturnRadius(float radius)
    {
        _returnRadius = radius;
    }
}
