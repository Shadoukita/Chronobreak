//#nullable enable

using System;
using System.Linq;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using GameServerCore.Enums;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;

namespace Chronobreak.GameServer.Scripting.CSharp;

public class BehaviourTree
{
    internal Champion Owner;
    private int timesExecuted = 0;
    public virtual void Update()
    {
        timesExecuted++;
    }

    //TODO: Rename
    protected static bool ForEach<T>(IEnumerable<T> Collection, Func<T, bool>? Child0 = null)
    {
        foreach (var item in Collection)
        {
            Child0(item);
        }
        return true;
    }

    /// <summary>
    /// Creates a dynamic BTInstance based on the name of the provided tree.
    /// </summary>
    /// <remarks>
    /// For now this should generally be used for one-off tress that delete themselves since references outside of this function are difficult
    /// </remarks>
    /// <param name="TreeName">Name of the tree to instantiate</param>
    /// <param name="Type">The type of the instance.
    /// For now I strongly recommend only using DELETE_SELF.</param>
    //protected static bool CreateDynamicBTInstance(string TreeName, BTInstanceType Type)
    //{
    //    return false;
    //}

    /// <summary>
    /// Debug node used to return an explicit value and write a string to log.
    /// </summary>
    /// <param name="String">The string that should be outputted to log</param>
    protected static bool DebugAction(string String)
    {
        //TODO: Reduce spam frequency
        //Console.WriteLine(String);
        return true;
    }

    /// <summary>
    /// Decorator that allows user to specify the number of times the subtree will run.
    /// </summary>
    /// <remarks>
    /// Need comment
    /// </remarks>
    /// <param name="RunningLimit">The number of times the sub tree is to be executed</param>
    protected static async Task<bool> LoopNTimes(int RunningLimit, Func<Task<bool>>? Child0 = null)
    {
        return false;
    }

    /// <summary>
    /// Return RUNNING for X seconds after first tick.
    /// </summary>
    /// <remarks>
    /// This is a blocking delay and it uses the real timer not the game timer, so it is unaffected by pause.
    /// </remarks>
    /// <param name="DelayAmount">The amount of time to delay after first tick.</param>
    protected static async Task<bool> DelayNSecondsBlocking(float DelayAmount)
    {
        return false;
    }

    /// <summary>
    /// Enable/Disable a quest by name
    /// </summary>
    /// <remarks>
    /// This will fail if the quest does not exist
    /// </remarks>
    /// <param name="Enabled">Should the quest be enabled or disabled?</param>
    /// <param name="Name">The name of the quest to adjust</param>
    protected static bool SetBTInstanceStatus(bool Enabled, string Name)
    {
        return false;
    }

    /// <summary>
    /// Set all map barracks active/inactive
    /// </summary>
    /// <remarks>
    /// This functionally is the same as the kill minions cheat
    /// </remarks>
    /// <param name="Enable">The status of the barracks</param>
    protected static bool SetBarrackStatus(bool Enable)
    {
        return false;
    }

    /// <summary>
    /// Display objective text using the Tutorial1 flash element
    /// </summary>
    /// <remarks>
    /// This should only accept localized strings
    /// </remarks>
    /// <param name="String">The localized string to display.</param>
    protected static bool ShowObjectiveText(string String)
    {
        return false;
    }

    /// <summary>
    /// Hide objective text using the Tutorial1 flash element
    /// </summary>
    /// <remarks>
    /// Will always return success even if no objective text is displayed
    /// </remarks>
    protected static bool HideObjectiveText()
    {
        return false;
    }

    /// <summary>
    /// Display auxiliary text using the Tutorial1 flash element
    /// </summary>
    /// <remarks>
    /// This should only accept localized strings
    /// </remarks>
    /// <param name="String">The localized string to display.</param>
    protected static bool ShowAuxiliaryText(string String)
    {
        return false;
    }

    /// <summary>
    /// Hide auxiliary text using the Tutorial1 flash element
    /// </summary>
    /// <remarks>
    /// Will always return success even if no auxiliary text is displayed
    /// </remarks>
    protected static bool HideAuxiliaryText()
    {
        return false;
    }

    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for bool References
    /// </remarks>
    /// <param name="Output">Destination Reference</param>
    /// <param name="Input">Source Reference</param>
    protected static bool SetVarBool(out bool Output, bool Input)
    {
        Output = Input;
        return true;
    }

    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for AttackableUnit References
    /// </remarks>
    /// <param name="Output">Destination Reference</param>
    /// <param name="Input">Source Reference</param>
    protected static bool SetVarAttackableUnit(out AttackableUnit Output, AttackableUnit Input)
    {
        Output = Input;
        return true;
    }

    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for int References
    /// </remarks>
    /// <param name="Output">Destination Reference</param>
    /// <param name="Input">Source Reference</param>
    protected static bool SetVarInt(out int Output, int Input)
    {
        Output = Input;
        return true;
    }

    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for int References
    /// </remarks>
    /// <param name="Output">Destination Reference</param>
    /// <param name="Input">Source Reference</param>
    protected static bool SetVarDWORD(out uint Output, uint Input)
    {
        Output = Input;
        return true;
    }

    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for string References
    /// </remarks>
    /// <param name="Output">Destination Reference</param>
    /// <param name="Input">Source Reference</param>
    protected static bool SetVarString(out string Output, string Input)
    {
        Output = Input;
        return true;
    }

    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for float References
    /// </remarks>
    /// <param name="Output">Destination Reference</param>
    /// <param name="Input">Source Reference</param>
    protected static bool SetVarFloat(out float Output, float Input)
    {
        Output = Input;
        return true;
    }

    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for Vector References.
    /// If you want to make a vector out of 3 floats, use MakeVector.
    /// </remarks>
    /// <param name="Output">Destination Reference</param>
    /// <param name="Input">Source Reference</param>
    protected static bool SetVarVector(out Vector3 Output, Vector3 Input)
    {
        Output = Input;
        return true;
    }

    /// <summary>
    /// Adds the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.
    /// This will always return SUCCESS. 
    /// </remarks>
    /// <param name="Output">Output reference of the operation</param>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    protected static bool AddInt(out int Output, int LeftHandSide, int RightHandSide)
    {
        Output = LeftHandSide + RightHandSide;
        return true;
    }

    /// <summary>
    /// Subtracts the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.
    /// This will always return SUCCESS. 
    /// </remarks>
    /// <param name="Output">Output reference of the operation</param>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    protected static bool SubtractInt(out int Output, int LeftHandSide, int RightHandSide)
    {
        Output = LeftHandSide - RightHandSide;
        return true;
    }

    /// <summary>
    /// Multiplies the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.
    /// This will always return SUCCESS. 
    /// </remarks>
    /// <param name="Output">Output reference of the operation</param>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    protected static bool MultiplyInt(out int Output, int LeftHandSide, int RightHandSide)
    {
        Output = LeftHandSide * RightHandSide;
        return true;
    }

    /// <summary>
    /// Divides the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.
    /// This will always return SUCCESS. 
    /// </remarks>
    /// <param name="Output">Output reference of the operation</param>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    protected static bool DivideInt(out int Output, int LeftHandSide, int RightHandSide)
    {
        Output = LeftHandSide / RightHandSide;
        return true;
    }

    /// <summary>
    /// Divides the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.
    /// This will always return SUCCESS. 
    /// </remarks>
    /// <param name="Output">Output reference of the operation</param>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    protected static bool ModulusInt(out int Output, int LeftHandSide, int RightHandSide)
    {
        Output = LeftHandSide % RightHandSide;
        return true;
    }

    /// <summary>
    /// Compares LeftHandSide to the RightHandSide and places the lesser value in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.
    /// This will always return SUCCESS. 
    /// </remarks>
    /// <param name="Output">Output reference of the operation</param>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    protected static bool MinInt(out int Output, int LeftHandSide, int RightHandSide)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Compares LeftHandSide to the RightHandSide and places the greater value in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.
    /// This will always return SUCCESS. 
    /// </remarks>
    /// <param name="Output">Output reference of the operation</param>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    protected static bool MaxInt(out int Output, int LeftHandSide, int RightHandSide)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Adds the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Floats.
    /// This will always return SUCCESS. 
    /// </remarks>
    /// <param name="Output">Output reference of the operation</param>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    protected static bool AddFloat(out float Output, float LeftHandSide, float RightHandSide)
    {
        Output = LeftHandSide + RightHandSide;
        return true;
    }

    /// <summary>
    /// Subtracts the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Floats.
    /// This will always return SUCCESS. 
    /// </remarks>
    /// <param name="Output">Output reference of the operation</param>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    protected static bool SubtractFloat(out float Output, float LeftHandSide, float RightHandSide)
    {
        Output = LeftHandSide - RightHandSide;
        return true;
    }

    /// <summary>
    /// Multiplies the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Floats.
    /// This will always return SUCCESS. 
    /// </remarks>
    /// <param name="Output">Output reference of the operation</param>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    protected static bool MultiplyFloat(out float Output, float LeftHandSide, float RightHandSide)
    {
        Output = LeftHandSide * RightHandSide;
        return true;
    }

    /// <summary>
    /// Divides the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Floats.
    /// This will always return SUCCESS. 
    /// </remarks>
    /// <param name="Output">Output reference of the operation</param>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    protected static bool DivideFloat(out float Output, float LeftHandSide, float RightHandSide)
    {
        Output = LeftHandSide / RightHandSide;
        return true;
    }

    /// <summary>
    /// Compares LeftHandSide to the RightHandSide and places the lesser value in Output
    /// </summary>
    /// <remarks>
    /// This version is for Floats.
    /// This will always return SUCCESS. 
    /// </remarks>
    /// <param name="Output">Output reference of the operation</param>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    protected static bool MinFloat(out float Output, float LeftHandSide, float RightHandSide)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Compares LeftHandSide to the RightHandSide and places the greater value in Output
    /// </summary>
    /// <remarks>
    /// This version is for Floats.
    /// This will always return SUCCESS. 
    /// </remarks>
    /// <param name="Output">Output reference of the operation</param>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    protected static bool MaxFloat(out float Output, float LeftHandSide, float RightHandSide)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Gets a handle to the player and puts it in OutputRef
    /// </summary>
    /// <remarks>
    /// Only works in Tutorial, or other situation where there's only one player.
    /// Works by getting the first player in the roster that has a legal client ID.
    /// </remarks>
    /// <param name="Output">Destination reference; holds a hero object handle</param>
    protected static bool GetTutorialPlayer(out AttackableUnit Output)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Returns a handle to a collection containing all champions in the game.
    /// </summary>
    /// <remarks>
    /// This is an unfiltered collection, so it contains champions who have disconnected or are played by bots.
    /// </remarks>
    /// <param name="Output">Destination reference; holds the collection of all champions in the game.</param>
    protected static bool GetChampionCollection(out IEnumerable<Champion> Output)
    {
        //Output = Game.PlayerManager.GetPlayers(true).Select(info => info.Champion);
        Output = Game.ObjectManager.GetAllChampions();
        return false;
    }

    /// <summary>
    /// Returns a handle to a collection containing all turrets alive in the game.
    /// </summary>
    /// <remarks>
    /// This is an unfiltered collection, so it contains turrets on both teams.
    /// </remarks>
    /// <param name="Output">Destination reference; holds the collection of all champions in the game.</param>
    protected static bool GetTurretCollection(out IEnumerable<LaneTurret> Output)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Gets a handle to the turret in a specific lane
    /// </summary>
    /// <remarks>
    /// I think this will return FAILURE if the turret is not alive, should confirm.
    /// </remarks>
    /// <param name="Turret">Destination Reference; holds a turret object handle</param>
    /// <param name="Team">Team of the turrets to be checked.</param>
    /// <param name="Lane">Lane of the turret.
    /// Check the level script for the enum.</param>
    /// <param name="Position">Position of the turret.
    /// Check the level script for the enum.</param>
    protected static bool GetTurret(out AttackableUnit Turret, TeamId Team, int Lane, int Position)
    {
        Turret = default;
        return false;
    }

    /// <summary>
    /// Gets a handle to the inhibitor in a specific lane
    /// </summary>
    /// <remarks>
    /// I think this will return FAILURE if the inhibitor is not alive, should confirm.
    /// </remarks>
    /// <param name="Inhibitor">Destination Reference; holds an inhibitor object handle</param>
    /// <param name="Team">Team of the inhibitor to be checked.</param>
    /// <param name="Lane">Lane of the inhibitor.
    /// Check the level script for the enum.</param>
    protected static bool GetInhibitor(out AttackableUnit Inhibitor, TeamId Team, int Lane)
    {
        Inhibitor = default;
        return false;
    }

    /// <summary>
    /// Gets a handle to the nexus on a specific teamin a specific lane
    /// </summary>
    /// <remarks>
    /// I think this will return FAILURE if the Nexus is not alive, should confirm.
    /// </remarks>
    /// <param name="Nexus">Destination Reference; holds a nexus object handle</param>
    /// <param name="Team">Team of the nexus to return.</param>
    protected static bool GetNexus(out AttackableUnit Nexus, TeamId Team)
    {
        Nexus = default;
        return false;
    }

    /// <summary>
    /// Returns the current position of a specific unit
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS
    /// </remarks>
    /// <param name="Output">Destination reference; contains the current position of the unit.</param>
    /// <param name="Unit">Unit to poll.</param>
    protected static bool GetUnitPosition(out Vector3 Output, AttackableUnit Unit)
    {
        Output = Unit.Position3D;
        return true;
    }

    /// <summary>
    /// Returns the current elapsed game time.
    /// This will be affected by pausing, cheats, or other things.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Output">Destination reference; contains the currently elapsed game time.</param>
    protected static bool GetGameTime(out float Output)
    {
        Output = Game.Time.GameTime / 1000f;
        return true;
    }

    /// <summary>
    /// Returns the lane position of a turret.
    /// </summary>
    /// <remarks>
    /// This position is defined in the level script and is map specific.
    /// </remarks>
    /// <param name="Output">Destination reference; contains the integer position of the turret.
    /// This is defined in the level script.</param>
    /// <param name="Turret">Turret to poll.</param>
    protected static bool GetTurretPosition(out int Output, AttackableUnit Turret)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Returns the max health of a specific unit
    /// </summary>
    /// <remarks>
    /// MAX health
    /// </remarks>
    /// <param name="Output">Destination reference; contains the max health of the unit.</param>
    /// <param name="Unit">Unit to poll.</param>
    protected static bool GetUnitMaxHealth(out float Output, AttackableUnit Unit)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Returns the current health of a specific unit
    /// </summary>
    /// <remarks>
    /// CURRENT health
    /// </remarks>
    /// <param name="Output">Destination reference; contains the current health of the unit.</param>
    /// <param name="Unit">Unit to poll.</param>
    protected static bool GetUnitCurrentHealth(out float Output, AttackableUnit Unit)
    {
        Output = Unit.Stats.CurrentHealth;
        return true;
    }

    /// <summary>
    /// Returns the current Primary Ability Resource of a specific unit
    /// </summary>
    /// <remarks>
    /// CURRENT health
    /// </remarks>
    /// <param name="Output">Destination reference; contains the current Primary Ability Resource value of the unit.</param>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="PrimaryAbilityResourceType">Primary Ability Resource type.</param>
    protected static bool GetUnitCurrentPAR(out float Output, AttackableUnit Unit, PrimaryAbilityResourceType PrimaryAbilityResourceType)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Returns the maximum Primary Ability Resource of a specific unit
    /// </summary>
    /// <remarks>
    /// MAX PAR
    /// </remarks>
    /// <param name="Output">Destination reference; contains the maximum Primary Ability Resource value of the unit.</param>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="PrimaryAbilityResourceType">Primary Ability Resource type.</param>
    protected static bool GetUnitMaxPAR(out float Output, AttackableUnit Unit, PrimaryAbilityResourceType PrimaryAbilityResourceType)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Returns the current armor of a specific unit
    /// </summary>
    /// <remarks>
    /// CURRENT armor
    /// </remarks>
    /// <param name="Output">Destination reference; contains the current armor of the unit.</param>
    /// <param name="Unit">Unit to poll.</param>
    protected static bool GetUnitArmor(out float Output, AttackableUnit Unit)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Returns the number of discrete elements contained within the collection.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Output">Destination reference; contains the number of elements in the collection.</param>
    /// <param name="Collection">Collection to count.</param>
    protected static bool GetCollectionCount(out int Output, IEnumerable<AttackableUnit> Collection)
    {
        Output = Collection.Count();
        return true;
    }

    /// <summary>
    /// Returns the current skin of a specific unit
    /// </summary>
    /// <remarks>
    /// Since buildings don't hame skins, it will return the name of the building.
    /// </remarks>
    /// <param name="Output">Destination reference; contains the skin name of the unit.</param>
    /// <param name="Unit">Unit to poll.</param>
    protected static bool GetUnitSkinName(out string Output, AttackableUnit Unit)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Turns on or off the highlight of a unit.
    /// </summary>
    /// <remarks>
    /// Creates a unit highlight akin to what is used in the tutorial.
    /// This highlight is by default blue.
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Enable">Should the Highlight be turned on or turned off?</param>
    /// <param name="TargetUnit">Unit to be highlighted.</param>
    protected static bool ToggleUnitHighlight(bool Enable, AttackableUnit TargetUnit)
    {
        //TODO: Implement.
        return true;
    }

    /// <summary>
    /// Pings a unit on the minimap.
    /// </summary>
    /// <remarks>
    /// Which team receives the ping is determined by the PingingUnit.
    /// Currently this block can not ping for both teams simultaneously.
    /// </remarks>
    /// <param name="PingingUnit">Unit originating the ping.
    /// Important for team coloration and chat info.</param>
    /// <param name="TargetUnit">Unit to be pinged.</param>
    /// <param name="PlayAudio">Play audio with ping?</param>
    protected static bool PingMinimapUnit(AttackableUnit PingingUnit, AttackableUnit TargetUnit, bool PlayAudio)
    {
        return false;
    }

    /// <summary>
    /// Pings a location on the minimap.
    /// </summary>
    /// <remarks>
    /// Which team receives the ping is determined by the PingingUnit.
    /// Currently this block can not ping for both teams simultaneously.
    /// </remarks>
    /// <param name="PingingUnit">Unit originating the ping.
    /// Important for team coloration and chat info.</param>
    /// <param name="TargetPosition">Location to be pinged.</param>
    /// <param name="PlayAudio">Play audio with ping?</param>
    protected static bool PingMinimapLocation(AttackableUnit PingingUnit, Vector3 TargetPosition, bool PlayAudio)
    {
        return false;
    }

    /// <summary>
    /// Create a new quest and display it in the HUD
    /// </summary>
    /// <remarks>
    /// This should only accept localized strings
    /// </remarks>
    /// <param name="QuestId">Gives a unique identifier to refer back to this quest</param>
    /// <param name="String">The localized string to display.</param>
    /// <param name="Player">The player whose quest you want to activate</param>
    /// <param name="QuestType">Quest type; which quest tracker you want the quest to be added to</param>
    /// <param name="HandleRollOver">OPTIONAL. Should we handle the mousing rolling over and rolling out from this quest?</param>
    /// <param name="Tooltip">Optional: The tooltip to display on rollover of the quest.</param>
    protected static bool ActivateQuest(out int QuestId, string String, AttackableUnit Player, QuestType QuestType, bool HandleRollOver, string Tooltip)
    {
        QuestId = default;
        return false;
    }

    /// <summary>
    /// Plays a quest completion animation and then removes it from the HUD
    /// </summary>
    /// <remarks>
    /// Used on quest ids returned by the ActivateQuest node
    /// </remarks>
    /// <param name="QuestId">Unique identfier used to refer to the quest; returned by ActivateQuest</param>
    protected static bool CompleteQuest(int QuestId)
    {
        return false;
    }

    /// <summary>
    /// Removes quest from the HUD immediately
    /// </summary>
    /// <remarks>
    /// Used on quest ids returned by the ActivateQuest node; there is no ceremony involved in quest removal
    /// </remarks>
    /// <param name="QuestId">Unique identfier used to refer to the quest; returned by ActivateQuest</param>
    protected static bool RemoveQuest(int QuestId)
    {
        return false;
    }

    /// <summary>
    /// Test to see if the quest has the mouse rolled over it
    /// </summary>
    /// <remarks>
    /// This quest must have been activated with HandleRollOver=true in ActivateQuest
    /// </remarks>
    /// <param name="QuestId">Which Quest should we check?</param>
    protected static bool TestQuestRolledOver(int QuestId)
    {
        return false;
    }

    /// <summary>
    /// Test to see if the quest is being clicked right now with the mouse down over it.
    /// </summary>
    /// <remarks>
    /// Tests to see if the quest is being clicked right now, or if the mouse is not clicking it right now.
    /// </remarks>
    /// <param name="QuestId">Which Quest should we check?</param>
    protected static bool TestQuestClicked(int QuestId)
    {
        return false;
    }

    /// <summary>
    /// Create a new Tip and display it in the TipTracker
    /// </summary>
    /// <remarks>
    /// This should only accept localized strings
    /// </remarks>
    /// <param name="TipId">Gives a unique identifier to refer back to this Tip.</param>
    /// <param name="Player">The player whose tip you want to activate.</param>
    /// <param name="TipName">The localized string for the Tip Name.</param>
    /// <param name="TipCategory">The localized string for the Tip Category.</param>
    protected static bool ActivateTip(out int TipId, AttackableUnit Player, string TipName, string TipCategory)
    {
        TipId = default;
        return false;
    }

    /// <summary>
    /// Removes Tip from the Tip Tracker immediately
    /// </summary>
    /// <remarks>
    /// Used on Tip Ids returned by the ActivateTip node; there is no ceremony involved in Tip removal
    /// </remarks>
    /// <param name="TipId">Unique identfier used to refer to the Tip; returned by ActivateTip</param>
    protected static bool RemoveTip(int TipId)
    {
        return false;
    }

    /// <summary>
    /// Enables mouse events in the Tip Tracker
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <param name="Player">The player whose Tip Tracker you want to enable</param>
    protected static bool EnableTipEvents(AttackableUnit Player)
    {
        return false;
    }

    /// <summary>
    /// Disables mouse events in the Tip Tracker
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <param name="Player">The player whose Tip Tracker you want to disable</param>
    protected static bool DisableTipEvents(AttackableUnit Player)
    {
        return false;
    }

    /// <summary>
    /// Tests to see if a Tip in the Tip Tracker or a Tip Dialogue has been clicked by the user
    /// </summary>
    /// <remarks>
    /// Used on Tip Ids returned by the ActivateTip and ActivateTipDialogue nodes. Use ReturnSuccessIf to control the output.
    /// This will return as if the Tip has NOT been clicked if the Tip Id is invalid.
    /// </remarks>
    /// <param name="TipId">Unique identfier used to refer to the Tip; returned by ActivateTip or ActivateTipDialogue</param>
    protected static bool TestTipClicked(int TipId)
    {
        return false;
    }

    /// <summary>
    /// Create a new Tip Dialogue and display it in the HUD
    /// </summary>
    /// <remarks>
    /// This should only accept localized strings
    /// </remarks>
    /// <param name="TipId">Gives a unique identifier to refer back to this Tip Dialogue.</param>
    /// <param name="Player">The player whose tip you want to activate.</param>
    /// <param name="TipName">The localized string for the Tip Name.</param>
    /// <param name="TipBody">The localized string for the Tip Body.</param>
    /// <param name="TipImage">Optional. The path+filename of the image to display in the tap dialog.</param>
    protected static bool ActivateTipDialogue(out int TipId, AttackableUnit Player, string TipName, string TipBody, string TipImage)
    {
        TipId = default;
        return false;
    }

    /// <summary>
    /// Enables mouse events in the Tip Dialogue
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <param name="Player">The player whose Tip Dialogue you want to enable</param>
    protected static bool EnableTipDialogueEvents(AttackableUnit Player)
    {
        return false;
    }

    /// <summary>
    /// Disables mouse events in the Tip Dialogue
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <param name="Player">The player whose Tip Dialogue you want to disable</param>
    protected static bool DisableTipDialogueEvents(AttackableUnit Player)
    {
        return false;
    }

    /// <summary>
    /// Creates a vector from three static components
    /// </summary>
    /// <remarks>
    /// If you want to copy a Vector, use SetVarVector.
    /// </remarks>
    /// <param name="Vector">OutputVector</param>
    /// <param name="X">X component</param>
    /// <param name="Y">Y component</param>
    /// <param name="Z">Z component</param>
    protected static bool MakeVector(out Vector3 Vector, float X, float Y, float Z)
    {
        Vector = default;
        return false;
    }

    /// <summary>
    /// Turn on or off a UI highlight for a specific UI Element
    /// </summary>
    /// <remarks>
    /// Set the enabled flag to control whether this node turns the element on or off
    /// </remarks>
    /// <param name="UIElement">UIElement; which element on the minimap do you want to highlight</param>
    /// <param name="Enabled">If true, turns on the UI Highlight, if false then turns off the UI Highlight</param>
    protected static bool ToggleUIHighlight(UIElement UIElement, bool Enabled)
    {
        return false;
    }

    /// <summary>
    /// Keeps track whether a player has opened his scoreboard.
    /// </summary>
    /// <remarks>
    /// Ticking this registers with the event system; disabling the tree unregisters the callback and clears the count
    /// </remarks>
    /// <param name="Output">Destination Reference; holds whether the scoreboard has been opened since the tree was enabled.</param>
    /// <param name="Unit">Handle of the attacking unit</param>
    protected static bool RegisterScoreboardOpened(out bool Output, AttackableUnit Unit)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Keeps track of the number of minions (not neutrals) not on the attacker's team killed by an attacker
    /// </summary>
    /// <remarks>
    /// Ticking this registers with the event system; disabling the tree unregisters the callback and clears the count
    /// </remarks>
    /// <param name="Output">Destination Reference; holds the number of units killed by the attacker</param>
    /// <param name="Unit">Handle of the attacking unit</param>
    protected static bool RegisterMinionKillCounter(out int Output, AttackableUnit Unit)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Returns an int containing the number of kills the champion has.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// TODO: Convert this into Hero only
    /// </remarks>
    /// <param name="Output">Destination Reference; holds the number of champions killed by the attacker</param>
    /// <param name="Unit">Handle of the champion to poll</param>
    protected static bool GetChampionKills(out int Output, AttackableUnit Unit)
    {
        Output = (Unit as Champion)?.ChampionStatistics.Kills ?? 0;
        return true;
    }

    /// <summary>
    /// Returns an int containing the number of deaths the champion has.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// TODO: Convert this into Hero only
    /// </remarks>
    /// <param name="Output">Destination Reference; holds the number of times the champion has been killed.</param>
    /// <param name="Unit">Handle of the champion to poll</param>
    protected static bool GetChampionDeaths(out int Output, AttackableUnit Unit)
    {
        Output = (Unit as Champion)?.ChampionStatistics.Deaths ?? 0;
        return true;
    }

    /// <summary>
    /// Returns an int containing the number of assists the champion has.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// TODO: Convert this into Hero only
    /// </remarks>
    /// <param name="Output">Destination Reference; holds the number of assists the champion has earned.</param>
    /// <param name="Unit">Handle of the champion to poll</param>
    protected static bool GetChampionAssists(out int Output, AttackableUnit Unit)
    {
        Output = (Unit as Champion)?.ChampionStatistics.Assists ?? 0;
        return true;
    }

    /// <summary>
    /// Gives target champion a variable amount of gold.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// TODO: Convert this into Hero only
    /// </remarks>
    /// <param name="Unit">Handle of the champion to give gold to.</param>
    /// <param name="GoldAmount">Amount of gold to give the champion.</param>
    protected static bool GiveChampionGold(AttackableUnit Unit, float GoldAmount)
    {
        //TODO: Test for Champion?
        Unit.GoldOwner.AddGold(GoldAmount);
        return true;
    }

    /// <summary>
    /// Orders a unit to stop its movement.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// How this block will interract with forced move orders (say from a skill) is currently untested.
    /// </remarks>
    /// <param name="Unit">Handle of the champion to order.</param>
    protected static bool StopUnitMovement(AttackableUnit Unit)
    {
        //TODO: Implement.
        return true;
    }

    /// <summary>
    /// Test if a hero has a specific item.
    /// </summary>
    /// <remarks>
    /// Use ReturnSuccessIf to control the output.
    /// This will return FAILURE if any parameters are incorrect.
    /// </remarks>
    /// <param name="Unit">Handle of the unit whose inventory you want to check.</param>
    /// <param name="ItemID">Numerical ID of the item to look for.</param>
    protected static bool TestChampionHasItem(AttackableUnit Unit, int ItemID)
    {
        return false;
    }

    /// <summary>
    /// Pause or unpause the game.
    /// </summary>
    /// <remarks>
    /// Be careful using this!  It is not fully protected for use in a production environment!
    /// </remarks>
    /// <param name="Pause">Pause or unpause the game.</param>
    protected static bool SetGamePauseState(bool Pause)
    {
        return false;
    }

    /// <summary>
    /// Pan the camera from its current position to a target point.
    /// </summary>
    /// <remarks>
    /// Once the pan starts this node will return RUNNING until the pan completes.
    /// After the pan completes the node will always return SUCCESS. This node locks camera movement while panning, and returns camera movement state to what it was before the pan started.
    /// Be careful if you change camera movement locking state while panning, because it will not stick.
    /// </remarks>
    /// <param name="Unit">The unit whose camera is being manipulated.</param>
    /// <param name="TargetPosition">3D Point containing the target camera position.</param>
    /// <param name="Time">The amount of time the pan should take; this will scale the pan speed. </param>
    protected static async Task<bool> PanCameraFromCurrentPositionToPoint(AttackableUnit Unit, Vector3 TargetPosition, float Time)
    {
        return false;
    }

    /// <summary>
    /// Returns the number of item slots filled for a particular champion.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Output">The number of items in the target's inventory.</param>
    /// <param name="Unit">Handle of the unit whose inventory you want to check.</param>
    protected static bool GetNumberOfInventorySlotsFilled(out int Output, AttackableUnit Unit)
    {
        //TODO: Implement.
        Output = default; //(Unit as ObjAIBase)?.Inventory... ?? 0;
        return true;
    }

    /// <summary>
    /// Returns the level of the target unit.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Output">The level of the target unit.</param>
    /// <param name="Unit">Handle of the unit whose level you want to check.</param>
    protected static bool GetUnitLevel(out int Output, AttackableUnit Unit)
    {
        //TODO: Implement.
        Output = default;
        return true;
    }

    /// <summary>
    /// Returns the current XP total of the target champion.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// Returns 0 if unit is not champion.
    /// </remarks>
    /// <param name="Output">The current XP of the target unit.</param>
    /// <param name="Unit">Handle of the unit whose XP total you want to get.</param>
    protected static bool GetUnitXP(out float Output, AttackableUnit Unit)
    {
        //TODO: Implement.
        Output = default;
        return true;
    }

    /// <summary>
    /// Returns the distance between the Unit and the Point
    /// </summary>
    /// <remarks>
    /// Distance is measured from the edge of the unit's bounding box
    /// </remarks>
    /// <param name="Output">Destination Reference; holds the distance from the unit to the point</param>
    /// <param name="Unit">Handle of the unit</param>
    /// <param name="Point">Point</param>
    protected static bool DistanceBetweenObjectAndPoint(out float Output, AttackableUnit Unit, Vector3 Point)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Returns a collection of units in the target area.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// Uses the reference unit for enemy/ally checks; must be present!
    /// </remarks>
    /// <param name="Output">Destination Reference; holds a collection of units discovered</param>
    /// <param name="Unit">Handle of the unit that serves as the reference for team flags.</param>
    /// <param name="TargetLocation">Center of the test</param>
    /// <param name="Radius">Radius of the unit test</param>
    /// <param name="SpellFlags">Associated spell flags for target filtering of the unit gathering check.</param>
    protected static bool GetUnitsInTargetArea(out IEnumerable<AttackableUnit> Output, AttackableUnit Unit, Vector3 TargetLocation, float Radius, SpellDataFlags SpellFlags)
    {
        //TODO: Implement.
        Output = new AttackableUnit[0];
        return true;
    }

    /// <summary>
    /// Test to see if unit is alive
    /// </summary>
    /// <remarks>
    /// Unit is not alive if it does not exist; use ReturnSuccessIf to invert the test
    /// </remarks>
    /// <param name="Unit">Unit to be tested</param>
    protected static bool TestUnitCondition(AttackableUnit Unit)
    {
        return false;
    }

    /// <summary>
    /// Test to see if unit is invulnerable
    /// </summary>
    /// <remarks>
    /// Unit is not invulnerable if it does not exist; use ReturnSuccessIf to invert the test
    /// </remarks>
    /// <param name="Unit">Unit to be tested</param>
    protected static bool TestUnitIsInvulnerable(AttackableUnit Unit)
    {
        return false;
    }

    /// <summary>
    /// Test to see if unit is in brush
    /// </summary>
    /// <remarks>
    /// Unit is not in brush if it does not exist; use ReturnSuccessIf to invert the test
    /// </remarks>
    /// <param name="Unit">Unit to be tested</param>
    protected static bool TestUnitInBrush(AttackableUnit Unit)
    {
        return false;
    }

    /// <summary>
    /// Test to see if unit has a specific buff
    /// </summary>
    /// <remarks>
    /// Unit does not have buff if it does not exist; use ReturnSuccessIf to invert the test
    /// </remarks>
    /// <param name="TargetUnit">Unit to be tested</param>
    /// <param name="CasterUnit">OPTIONAL.
    /// Additional filter to check if buff was cast by a specific unit</param>
    /// <param name="BuffName">Name of buff to be tested</param>
    protected static bool TestUnitHasBuff(AttackableUnit TargetUnit, AttackableUnit CasterUnit, string BuffName)
    {
        return false;
    }

    /// <summary>
    /// Test to see if a one unit has visibility of another unit
    /// </summary>
    /// <remarks>
    /// If either unit does not exist, then they are not visible; use ReturnSuccessIf to invert the test
    /// </remarks>
    /// <param name="Viewer">Can this unit see the other?</param>
    /// <param name="TargetUnit">Is this unit visible to the viewer unit?</param>
    protected static bool TestUnitVisibility(AttackableUnit Viewer, AttackableUnit TargetUnit)
    {
        return false;
    }

    /// <summary>
    /// Disabled or Enables all user input
    /// </summary>
    /// <remarks>
    /// Disables or Enables all user input, for all users.
    /// </remarks>
    /// <param name="Enabled">If False disables all input for all users. If True, enables it.</param>
    protected static bool ToggleUserInput(bool Enabled)
    {
        return false;
    }

    /// <summary>
    /// Disabled or Enables the texture for fog of war for all users.
    /// </summary>
    /// <remarks>
    /// This will not reveal any units in the fog of war; perception bubbles are necessary for that.
    /// </remarks>
    /// <param name="Enabled">If False disables the texture for all users for all users. If True, enables it.</param>
    protected static bool ToggleFogOfWarTexture(bool Enabled)
    {
        return false;
    }

    /// <summary>
    /// Plays a localized VO event
    /// </summary>
    /// <remarks>
    /// Event is a 2D one-shot audio event.
    /// A bad event name fails without complaint.
    /// This node always returns SUCCESS.
    /// </remarks>
    /// <param name="EventID">FMOD event ID</param>
    /// <param name="FolderName">Folder the FMOD event is in in the Dialogue folder of the VO sound bank</param>
    /// <param name="FireAndForget">If true, plays sound as fire-and-forget and the node will return SUCCESS immediately.
    /// If false, node will return RUNNING until the client tells the server that the VO is finished.</param>
    protected static async Task<bool> PlayVOAudioEvent(string EventID, string FolderName, bool FireAndForget)
    {
        //TODO: Implement.
        return true;
    }

    /// <summary>
    /// Returns the attack range for unit
    /// </summary>
    /// <remarks>
    /// Returns FAILURE if the unit is not valid
    /// </remarks>
    /// <param name="Output">Destination reference</param>
    /// <param name="Unit">Unit to poll.</param>
    protected static bool GetUnitAttackRange(out float Output, AttackableUnit Unit)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Disables or Enables initial neutral minion spawn.
    /// </summary>
    /// <remarks>
    /// Once neutral minion spawning has begun, this node no longer has any effect.
    /// </remarks>
    /// <param name="Enabled">If True, enables neutral minion spawning; if False, delays neutral minion spawning.</param>
    protected static bool SetNeutralSpawnEnabled(bool Enabled)
    {
        return false;
    }

    /// <summary>
    /// Returns the amount of gold the unit has
    /// </summary>
    /// <remarks>
    /// Returns FAILURE if the unit is not valid
    /// </remarks>
    /// <param name="Output">Destination reference.</param>
    /// <param name="Unit">Unit to poll.</param>
    protected static bool GetUnitGold(out float Output, AttackableUnit Unit)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Returns unit unspent skill points
    /// </summary>
    /// <remarks>
    /// Returns FAILURE if unit is invalid
    /// </remarks>
    /// <param name="Output">Destination reference.</param>
    /// <param name="Unit">Unit to poll.</param>
    protected static bool GetUnitSkillPoints(out int Output, AttackableUnit Unit)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Adds a unit Perception Bubble
    /// </summary>
    /// <remarks>
    /// Returns a BubbleID which you can use to remove the perception bubble
    /// </remarks>
    /// <param name="BubbleID">Unique identfier used to refer to the Perception Bubble</param>
    /// <param name="TargetUnit">Unit to attach the Perception Bubble to.</param>
    /// <param name="Radius">Radius of Perception Bubble. If set to 0, the bubble visibility radius matches the visibility radius of the target unit.</param>
    /// <param name="Duration">Duration of Perception Bubble in seconds.
    /// Bubbles can be removed earlier by using the RemovePerceptionBubble node.</param>
    /// <param name="Team">Team ID that has visibility of this bubble.</param>
    /// <param name="RevealStealth">If this is true then the bubble will reveal stealth for anything inside of that bubble.</param>
    /// <param name="SpecificUnitsClientOnly">OPTIONAL. If specified a client specific message will be sent only to this client about this bubble.
    /// Only that client will have that visiblity.</param>
    /// <param name="RevealSpecificUnitOnly">OPTIONAL. If set then only a units that have the RevealSpecificUnit state on are seeable by this bubble.</param>
    protected static bool AddUnitPerceptionBubble(out uint BubbleID, AttackableUnit TargetUnit, float Radius, float Duration, TeamId Team, bool RevealStealth, AttackableUnit SpecificUnitsClientOnly, AttackableUnit RevealSpecificUnitOnly)
    {
        BubbleID = default;
        return false;
    }

    /// <summary>
    /// Adds a position Perception Bubble
    /// </summary>
    /// <remarks>
    /// Returns a BubbleID which you can use to remove the perception bubble
    /// </remarks>
    /// <param name="BubbleID">Unique identfier used to refer to the Perception Bubble</param>
    /// <param name="Position">Position of the Perception Bubble.</param>
    /// <param name="Radius">Radius of Perception Bubble. If set to 0, the bubble visibility radius matches the visibility radius of the target unit.</param>
    /// <param name="Duration">Duration of Perception Bubble in seconds.
    /// Bubbles can be removed earlier by using the RemovePerceptionBubble node.</param>
    /// <param name="Team">Team ID that has visibility of this bubble.</param>
    /// <param name="RevealStealth">If this is true then the bubble will reveal stealth for anything inside of that bubble.</param>
    /// <param name="SpecificUnitsClientOnly">OPTIONAL. If specified a client specific message will be sent only to this client about this bubble.
    /// Only that client will have that visiblity.</param>
    /// <param name="RevealSpecificUnitOnly">OPTIONAL. If set then only a units that have the RevealSpecificUnit state on are seeable by this bubble.</param>
    protected static bool AddPositionPerceptionBubble(out uint BubbleID, Vector3 Position, float Radius, float Duration, TeamId Team, bool RevealStealth, AttackableUnit SpecificUnitsClientOnly, AttackableUnit RevealSpecificUnitOnly)
    {
        BubbleID = default;
        return false;
    }

    /// <summary>
    /// Removes Perception Bubble
    /// </summary>
    /// <remarks>
    /// Used on Bubble IDs returned by the AddUnitPerceptionBubble and AddPositionPerceptionBubble
    /// </remarks>
    /// <param name="BubbleID">Unique identfier used to refer to the Perception Bubble; returned by AddPerceptionBubble nodes</param>
    protected static bool RemovePerceptionBubble(uint BubbleID)
    {
        return false;
    }

    /// <summary>
    /// Adds a unit particle effect
    /// </summary>
    /// <remarks>
    /// Returns an EffectID which you can use to remove the perception bubble
    /// </remarks>
    /// <param name="EffectID">Unique identfier used to refer to the particle effect; used to remove particle.</param>
    /// <param name="BindObject">Unit to attach the particle effect to.</param>
    /// <param name="BoneName">OPTIONAL. Name of the bone to attach the particle effect to.</param>
    /// <param name="EffectName">File name of the particle effect file to use.</param>
    /// <param name="TargetObject">OPTIONAL. Unit to attach the far end of a beam particle to.
    /// Use either TargetObject or TargetPosition; if you have both, TargetObject wins.</param>
    /// <param name="TargetBoneName">OPTIONAL. Name of the bone to attach the far end of a beam particle to.
    /// Used in conjunction with TargetObject.</param>
    /// <param name="TargetPosition">OPTIONAL. A fixed position for the far end of a beam particle.
    /// Use either TargetObject or TargetPosition; if you have both, TargetObject wins.</param>
    /// <param name="OrientTowards">OPTIONAL. Particle effect will orient to face this point.</param>
    /// <param name="SpecificUnitOnly">OPTIONAL. If used, only sends this particle to this unit.
    /// Otherwise, all units will see the particle.</param>
    /// <param name="SpecificTeamOnly">OPTIONAL.
    /// If used, only this team will see the particle.
    /// Otherwise, all teams will see the particle.</param>
    /// <param name="FOWVisibilityRadius">Used with FOWTeam to determine particle visibility in the FoW.
    /// The particle will be visible if a unit has visibility into the area defined by this radius and the center of the particle.</param>
    /// <param name="FOWTeam">OPTIONAL.
    /// If the viewing unit is on the same team as set by this variable, that unit will see this particle even if it's in the Fog of War.
    /// Only used if FOWVisibilityRadius is non-zero.</param>
    /// <param name="SendIfOnScreenOrDiscard">If true, will only try to send the particle if a unit can see it when the particle spawns.
    /// Use for one-shot particles; saves a lot of bandwidth, so use as often as possible.</param>
    protected static bool CreateUnitParticle(out uint EffectID, AttackableUnit BindObject, string BoneName, string EffectName, AttackableUnit TargetObject, string TargetBoneName, Vector3 TargetPosition, Vector3 OrientTowards, AttackableUnit SpecificUnitOnly, TeamId SpecificTeamOnly, float FOWVisibilityRadius, TeamId FOWTeam, bool SendIfOnScreenOrDiscard)
    {
        EffectID = default;
        return false;
    }

    /// <summary>
    /// Adds a unit particle effect
    /// </summary>
    /// <remarks>
    /// Returns an EffectID which you can use to remove the perception bubble
    /// </remarks>
    /// <param name="EffectID">Unique identfier used to refer to the particle effect; used to remove particle.</param>
    /// <param name="Position">Position of the particle effect.</param>
    /// <param name="EffectName">File name of the particle effect file to use.</param>
    /// <param name="TargetObject">OPTIONAL. Unit to attach the far end of a beam particle to.
    /// Use either TargetObject or TargetPosition; if you have both, TargetObject wins.</param>
    /// <param name="TargetBoneName">OPTIONAL. Name of the bone to attach the far end of a beam particle to.
    /// Used in conjunction with TargetObject.</param>
    /// <param name="TargetPosition">OPTIONAL. A fixed position for the far end of a beam particle.
    /// Use either TargetObject or TargetPosition; if you have both, TargetObject wins.</param>
    /// <param name="OrientTowards">OPTIONAL. Particle effect will orient to face this point.</param>
    /// <param name="SpecificUnitOnly">OPTIONAL. If used, only sends this particle to this unit.
    /// Otherwise, all units will see the particle.</param>
    /// <param name="SpecificTeamOnly">OPTIONAL.
    /// If used, only this team will see the particle.
    /// Otherwise, all teams will see the particle.</param>
    /// <param name="FOWVisibilityRadius">Used with FOWTeam to determine particle visibility in the FoW.
    /// The particle will be visible if a unit has visibility into the area defined by this radius and the center of the particle.</param>
    /// <param name="FOWTeam">OPTIONAL.
    /// If the viewing unit is on the same team as set by this variable, that unit will see this particle even if it's in the Fog of War.
    /// Only used if FOWVisibilityRadius is non-zero.</param>
    /// <param name="SendIfOnScreenOrDiscard">If true, will only try to send the particle if a unit can see it when the particle spawns.
    /// Use for one-shot particles; saves a lot of bandwidth, so use as often as possible.</param>
    protected static bool CreatePositionParticle(out uint EffectID, Vector3 Position, string EffectName, AttackableUnit TargetObject, string TargetBoneName, Vector3 TargetPosition, Vector3 OrientTowards, AttackableUnit SpecificUnitOnly, TeamId SpecificTeamOnly, float FOWVisibilityRadius, TeamId FOWTeam, bool SendIfOnScreenOrDiscard)
    {
        EffectID = default;
        return false;
    }

    /// <summary>
    /// Removes Particle
    /// </summary>
    /// <remarks>
    /// Used on Effect IDs returned by the CreateUnitParticle and CreatePositionParticle
    /// </remarks>
    /// <param name="EffectID">Unique identfier used to refer to the particle effect; returned by CreateParticle nodes</param>
    protected static bool RemoveParticle(uint EffectID)
    {
        return false;
    }

    /// <summary>
    /// Returns unit Team ID
    /// </summary>
    /// <remarks>
    /// Returns FAILURE if unit is invalid
    /// </remarks>
    /// <param name="Output">Destination reference.</param>
    /// <param name="Unit">Unit to poll.</param>
    protected static bool GetUnitTeam(out TeamId Output, AttackableUnit Unit)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Sets unit state DisableAmbientGold.
    /// If disabled, unit does not get ambient gold gain (but still gets gold/5 from runes).
    /// </summary>
    /// <remarks>
    /// Returns FAILURE if the unit is not valid.
    /// </remarks>
    /// <param name="Unit">Sets state of this unit.</param>
    /// <param name="Disabled">If true, ambient gold gain is disabled.</param>
    protected static bool SetStateDisableAmbientGold(AttackableUnit Unit, bool Disabled)
    {
        return false;
    }

    /// <summary>
    /// Sets unit level cap.
    /// Level cap 0 means no cap.
    /// Otherwise unit will earn experience up to one XP less than the level cap.
    /// </summary>
    /// <remarks>
    /// Returns FAILURE if the unit is not valid.
    /// If unit is already higher than the cap, it will earn 0 XP.
    /// </remarks>
    /// <param name="Unit">Sets level cap of this unit.</param>
    /// <param name="LevelCap">If 0, no level cap; otherwise unit cannot get higher than this level.</param>
    protected static bool SetUnitLevelCap(AttackableUnit Unit, int LevelCap)
    {
        return false;
    }

    /// <summary>
    /// Locks all player cameras to their champions.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Lock">If true, locks all player cameras to their champions.
    /// If false, unlocks all player cameras from their champions.</param>
    protected static bool LockAllPlayerCameras(bool Lock)
    {
        //TODO: Implement.
        return true;
    }

    /// <summary>
    /// Test to see if Player has camera locking enabled (camera locked to hero).
    /// </summary>
    /// <remarks>
    /// Use ReturnSuccessIf to control the output.
    /// This will return FAILURE if any parameters are incorrect.
    /// </remarks>
    /// <param name="Player">Player to test.</param>
    protected static bool TestPlayerCameraLocked(AttackableUnit Player)
    {
        return false;
    }

    /// <summary>
    /// A Procedure call
    /// </summary>
    /// <remarks>
    /// Procedure
    /// </remarks>
    /// <param name="Output1">Destination reference contains float value.</param>
    /// <param name="Output2">Destination reference contains UnitType value.</param>
    /// <param name="PocedureName"> can not be empty </param>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="ChatMessage">Chat string</param>
    protected static bool Procedure2To2(out string Output1, out UnitType Output2, string PocedureName, AttackableUnit Unit, string ChatMessage)
    {
        Output1 = default;
        Output2 = default;
        return false;
    }

    /// <summary>
    /// Test if game started
    /// </summary>
    /// <remarks>
    /// Tests if game started. True if game started. False if not
    /// </remarks>
    protected static bool TestGameStarted()
    {
        return false;
    }

    /// <summary>
    /// Tests if the specified unit is under attack
    /// </summary>
    /// <remarks>
    /// Tests if the specified unit is under attack. May gather enemies of given unit to figure out if under attack
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    protected static bool TestUnitUnderAttack(AttackableUnit Unit)
    {
        return false;
    }

    /// <summary>
    /// Returns the type of a specific unit
    /// </summary>
    /// <remarks>
    /// Unit type
    /// </remarks>
    /// <param name="Output">Destination reference contains the type of the unit.</param>
    /// <param name="Unit">Unit to poll.</param>
    protected static bool GetUnitType(out UnitType Output, AttackableUnit Unit)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Returns the creature type of a specific unit
    /// </summary>
    /// <remarks>
    /// Unit creature type
    /// </remarks>
    /// <param name="Output">Destination reference contains the creature type of the unit.</param>
    /// <param name="Unit">Unit to poll.</param>
    protected static bool GetUnitCreatureType(out CreatureType Output, AttackableUnit Unit)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Tests if the specified unit can use the specified spell
    /// </summary>
    /// <remarks>
    /// Uses specified spellbook and specified spell to figure out if unit can cast spell
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    protected static bool TestCanCastSpell(AttackableUnit Unit, SpellbookType Spellbook, int SlotIndex)
    {
        return false;
    }

    /// <summary>
    /// Cast specified Spell
    /// </summary>
    /// <remarks>
    /// Spell cast
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    protected static bool CastUnitSpell(AttackableUnit Unit, SpellbookType Spellbook, int SlotIndex)
    {
        return false;
    }

    /// <summary>
    /// Set ignore visibility for a specific spell
    /// </summary>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    /// <param name="IgnoreVisibility">Ignore visibility ?</param>
    protected static bool SetUnitSpellIgnoreVisibity(AttackableUnit Unit, SpellbookType Spellbook, int SlotIndex, bool IgnoreVisibility)
    {
        return false;
    }

    /// <summary>
    /// Set specified Spell target position
    /// </summary>
    /// <param name="TargetLocation">Location to be targeted.</param>
    /// <param name="SlotIndex">Spell slot.</param>
    protected static bool SetUnitAISpellTargetLocation(Vector3 TargetLocation, int SlotIndex)
    {
        return false;
    }

    /// <summary>
    /// Set specified Spell target
    /// </summary>
    /// <param name="TargetUnit">Target Input.</param>
    /// <param name="SlotIndex">Spell slot.</param>
    protected static bool SetUnitAISpellTarget(AttackableUnit TargetUnit, int SlotIndex)
    {
        return false;
    }

    /// <summary>
    /// Clears specified Spell target
    /// </summary>
    /// <param name="SlotIndex">Spell slot.</param>
    protected static bool ClearUnitAISpellTarget(int SlotIndex)
    {
        return false;
    }

    /// <summary>
    /// Test validity of specified Spell target
    /// </summary>
    /// <param name="SlotIndex">Spell slot.</param>
    protected static bool TestUnitAISpellTargetValid(int SlotIndex)
    {
        return false;
    }

    /// <summary>
    /// Gets the cooldown value for the spell in a given slot
    /// </summary>
    /// <remarks>
    /// Cooldown for spell in given slot
    /// </remarks>
    /// <param name="Output">Destination reference contains cooldown</param>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    protected static bool GetSpellSlotCooldown(out float Output, AttackableUnit Unit, SpellbookType Spellbook, int SlotIndex)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Gets the cooldown value for the spell in a given slot
    /// </summary>
    /// <remarks>
    /// Cooldown for spell in given slot
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    /// <param name="Cooldown">Slot cooldown</param>
    protected static bool SetSpellSlotCooldown(AttackableUnit Unit, SpellbookType Spellbook, int SlotIndex, float Cooldown)
    {
        return false;
    }

    /// <summary>
    /// Returns the PAR type for specified unit
    /// </summary>
    /// <remarks>
    /// PAR Type
    /// </remarks>
    /// <param name="Output">Destination reference.</param>
    /// <param name="Unit">Unit to poll.</param>
    protected static bool GetUnitPARType(out PrimaryAbilityResourceType Output, AttackableUnit Unit)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Returns the cost for spell specified slot
    /// </summary>
    /// <remarks>
    /// Spell cost
    /// </remarks>
    /// <param name="Output">Destination reference</param>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    protected static bool GetUnitSpellCost(out float Output, AttackableUnit Unit, SpellbookType Spellbook, int SlotIndex)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Returns the cast range for spell specified slot
    /// </summary>
    /// <remarks>
    /// Spell cast range
    /// </remarks>
    /// <param name="Output">Destination reference</param>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    protected static bool GetUnitSpellCastRange(out float Output, AttackableUnit Unit, SpellbookType Spellbook, int SlotIndex)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Returns the level for spell specified slot
    /// </summary>
    /// <remarks>
    /// Spell level
    /// </remarks>
    /// <param name="Output">Destination reference</param>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    protected static bool GetUnitSpellLevel(out int Output, AttackableUnit Unit, SpellbookType Spellbook, int SlotIndex)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Levels up a specified spell
    /// </summary>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    protected static bool LevelUpUnitSpell(AttackableUnit Unit, SpellbookType Spellbook, int SlotIndex)
    {
        return false;
    }

    /// <summary>
    /// Tests if the specified unit can level up the specified spell
    /// </summary>
    /// <remarks>
    /// Uses specified spellbook and specified spell to figure out if unit can level up spell
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="SlotIndex">Spell slot.</param>
    protected static bool TestUnitCanLevelUpSpell(AttackableUnit Unit, int SlotIndex)
    {
        return false;
    }

    /// <summary>
    /// Gets a handle to the the unit running the behavior tree in OutputRef
    /// </summary>
    /// <remarks>
    /// Gets a handle to the the unit running the behavior tree
    /// </remarks>
    /// <param name="Output">Destination reference; holds a AI object handle</param>
    protected bool GetUnitAISelf(out AttackableUnit Output)
    {
        Output = Owner;
        return true;
    }

    /// <summary>
    /// Unit run logic for first time
    /// </summary>
    protected bool TestUnitAIFirstTime()
    {
        return timesExecuted == 1;
    }

    /// <summary>
    /// Sets unit to assist
    /// </summary>
    /// <remarks>
    /// This version is for AttackableUnit References
    /// </remarks>
    /// <param name="TargetUnit">Target unit</param>
    protected static bool SetUnitAIAssistTarget(AttackableUnit TargetUnit)
    {
        return false;
    }

    /// <summary>
    /// Sets unit to target
    /// </summary>
    /// <remarks>
    /// This version is for AttackableUnit References
    /// </remarks>
    /// <param name="TargetUnit">Source Reference</param>
    protected static bool SetUnitAIAttackTarget(AttackableUnit TargetUnit)
    {
        return false;
    }

    /// <summary>
    /// Gets unit being assisted
    /// </summary>
    /// <remarks>
    /// This version is for AttackableUnit References
    /// </remarks>
    /// <param name="Output">Destination reference</param>
    protected static bool GetUnitAIAssistTarget(out AttackableUnit Output)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Gets unit being targeted
    /// </summary>
    /// <remarks>
    /// This version is for AttackableUnit References
    /// </remarks>
    /// <param name="Output">Destination reference</param>
    protected static bool GetUnitAIAttackTarget(out AttackableUnit Output)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Issue Move Order
    /// </summary>
    /// <remarks>
    /// Move
    /// </remarks>
    protected static bool IssueMoveOrder()
    {
        return false;
    }

    /// <summary>
    /// Issue Move Order
    /// </summary>
    /// <remarks>
    /// Move
    /// </remarks>
    /// <param name="TargetUnit">Target Unit.</param>
    protected static bool IssueMoveToUnitOrder(AttackableUnit TargetUnit)
    {
        return false;
    }

    /// <summary>
    /// Issue Move Order
    /// </summary>
    /// <remarks>
    /// Move
    /// </remarks>
    /// <param name="Location">Position to move to</param>
    protected static bool IssueMoveToPositionOrder(Vector3 Location)
    {
        return false;
    }

    /// <summary>
    /// Issue Chase Order
    /// </summary>
    /// <remarks>
    /// Chase
    /// </remarks>
    protected static bool IssueChaseOrder()
    {
        return false;
    }

    /// <summary>
    /// Issue Attack Order
    /// </summary>
    /// <remarks>
    /// Attack
    /// </remarks>
    protected static bool IssueAttackOrder()
    {
        return false;
    }

    /// <summary>
    /// Issue Wander order
    /// </summary>
    /// <remarks>
    /// Wander
    /// </remarks>
    protected static bool IssueWanderOrder()
    {
        return false;
    }

    /// <summary>
    /// Issue Emote Order
    /// </summary>
    /// <remarks>
    /// Emote
    /// </remarks>
    /// <param name="EmoteIndex">Emote ID</param>
    protected static bool IssueAIEmoteOrder(uint EmoteIndex)
    {
        return false;
    }

    /// <summary>
    /// Issue Emote Order
    /// </summary>
    /// <remarks>
    /// Emote
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="EmoteIndex">Emote ID</param>
    protected static bool IssueGloabalEmoteOrder(AttackableUnit Unit, uint EmoteIndex)
    {
        return false;
    }

    /// <summary>
    /// Issue Chat Order
    /// </summary>
    /// <remarks>
    /// AI caht
    /// </remarks>
    /// <param name="ChatMessage">Chat message</param>
    /// <param name="ChatRcvr">Chat receiver</param>
    protected static bool IssueAIChatOrder(string ChatMessage, string ChatRcvr)
    {
        return false;
    }

    /// <summary>
    /// Issue Chat Order
    /// </summary>
    /// <remarks>
    /// AI caht
    /// </remarks>
    /// <param name="ChatMessage">Chat message</param>
    /// <param name="ChatRcvr">Chat receiver</param>
    protected static bool IssueImmediateChatOrder(string ChatMessage, string ChatRcvr)
    {
        return false;
    }

    /// <summary>
    /// Issue disable task
    /// </summary>
    /// <remarks>
    /// AI task
    /// </remarks>
    protected static bool IssueAIDisableTaskOrder()
    {
        return false;
    }

    /// <summary>
    /// Issue enable task
    /// </summary>
    /// <remarks>
    /// AI task
    /// </remarks>
    protected static bool IssueAIEnableTaskOrder()
    {
        return false;
    }

    /// <summary>
    /// Clear AI Attack target
    /// </summary>
    protected static bool ClearUnitAIAttackTarget()
    {
        return false;
    }

    /// <summary>
    /// Clear AI assist target
    /// </summary>
    protected static bool ClearUnitAIAssistTarget()
    {
        return false;
    }

    /// <summary>
    /// Teleport To base
    /// </summary>
    /// <remarks>
    /// Used for Teleporting home
    /// </remarks>
    protected static bool IssueTeleportToBaseOrder()
    {
        return false;
    }

    /// <summary>
    /// Returns the number of discrete attackers.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Output">Destination reference; contains collection of attacking units.</param>
    /// <param name="Unit">Unit to poll.</param>
    protected static bool GetUnitAIAttackers(out IEnumerable<AttackableUnit> Output, AttackableUnit Unit)
    {
        //TODO: Implement.
        Output = new AttackableUnit[0];
        return true;
    }

    /// <summary>
    /// Unit can buy next recommended item
    /// </summary>
    protected static bool TestUnitAICanBuyRecommendedItem()
    {
        return false;
    }

    /// <summary>
    /// Buy next recommended item
    /// </summary>
    protected static bool UnitAIBuyRecommendedItem()
    {
        return false;
    }

    /// <summary>
    /// Unit can buy item
    /// </summary>
    /// <param name="ItemID">Item to buy.</param>
    protected static bool TestUnitAICanBuyItem(uint ItemID)
    {
        return false;
    }

    /// <summary>
    /// Buy item
    /// </summary>
    /// <param name="ItemID">Item to buy.</param>
    protected static bool UnitAIBuyItem(uint ItemID)
    {
        return false;
    }

    /// <summary>
    /// Computes a position for spell cast
    /// </summary>
    /// <param name="TargetUnit">target unit</param>
    /// <param name="ReferenceUnit">Reference unit</param>
    /// <param name="Range">Spell range</param>
    /// <param name="UnitSide">Which side of target are we going to (in between our out)</param>
    protected static bool ComputeUnitAISpellPosition(AttackableUnit TargetUnit, AttackableUnit ReferenceUnit, float Range, bool UnitSide)
    {
        return false;
    }

    /// <summary>
    /// Retrieves a position for spell cast
    /// </summary>
    /// <param name="Output">Destination reference</param>
    protected static bool GetUnitAISpellPosition(out Vector3 Output)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Clears position for spell cast
    /// </summary>
    protected static bool ClearUnitAISpellPosition()
    {
        return false;
    }

    /// <summary>
    /// Unit precomputed cast location valid 
    /// </summary>
    protected static bool TestUnitAISpellPositionValid()
    {
        return false;
    }

    /// <summary>
    /// Unit at precomputed spell cast location
    /// </summary>
    /// <remarks>
    /// Unit at precomputed spell location
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Location">Source Reference</param>
    /// <param name="Error">Accepted error</param>
    protected static bool TestUnitAtLocation(AttackableUnit Unit, Vector3 Location, float Error)
    {
        return false;
    }

    /// <summary>
    /// Unit in safe range
    /// </summary>
    /// <param name="Range">Unit in safe Range</param>
    protected static bool TestUnitAIIsInSafeRange(float Range)
    {
        return false;
    }

    /// <summary>
    /// Computes a safe position for AI unit
    /// </summary>
    /// <param name="Range">safe range</param>
    /// <param name="UseDefender">If True, use defenders in search</param>
    /// <param name="UseEnemy">If True, use enemies to guide in search</param>
    protected static bool ComputeUnitAISafePosition(float Range, bool UseDefender, bool UseEnemy)
    {
        return false;
    }

    /// <summary>
    /// Retrieves a safe position for AI unit
    /// </summary>
    /// <param name="Output">Destination reference</param>
    protected static bool GetUnitAISafePosition(out Vector3 Output)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Clears position for safe
    /// </summary>
    protected static bool ClearUnitAISafePosition()
    {
        return false;
    }

    /// <summary>
    /// Unit precomputed safe location valid 
    /// </summary>
    protected static bool TestUnitAISafePositionValid()
    {
        return false;
    }

    /// <summary>
    /// Returns the base location of a given unit
    /// </summary>
    /// <remarks>
    /// Return SUCCES if we can find the base
    /// </remarks>
    /// <param name="Output">Destination reference;.</param>
    /// <param name="Unit">Unit to poll.</param>
    protected static bool GetUnitAIBasePosition(out Vector3 Output, AttackableUnit Unit)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Returns the radius AOE of spell in a given slot
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Output">Destination reference;.</param>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    protected static bool GetUnitSpellRadius(out float Output, AttackableUnit Unit, SpellbookType Spellbook, int SlotIndex)
    {
        //TODO: Implement.
        Output = default;
        return true;
    }

    /// <summary>
    /// Returns distance between 2 units
    /// </summary>
    /// <remarks>
    /// takes into account their BB
    /// </remarks>
    /// <param name="Output">Destination reference;.</param>
    /// <param name="SourceUnit">Source unit</param>
    /// <param name="DestinationUnit">Destination unit</param>
    protected static bool GetDistanceBetweenUnits(out float Output, AttackableUnit SourceUnit, AttackableUnit DestinationUnit)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Unit target is in range
    /// </summary>
    /// <param name="Error">Accepted error for unit location</param>
    protected static bool TestUnitAIAttackTargetInRange(float Error)
    {
        return false;
    }

    /// <summary>
    /// Unit has valid target
    /// </summary>
    /// <remarks>
    /// Unit has valid target, use before getting attack target.
    /// </remarks>
    protected static bool TestUnitAIAttackTargetValid()
    {
        return false;
    }

    /// <summary>
    /// Unit can see target
    /// </summary>
    /// <param name="Unit">Viewer Unit</param>
    /// <param name="TargetUnit">Target  Unit</param>
    protected static bool TestUnitIsVisible(AttackableUnit Unit, AttackableUnit TargetUnit)
    {
        return false;
    }

    /// <summary>
    /// Sets item target
    /// </summary>
    /// <remarks>
    /// This version is for AttackableUnit References
    /// </remarks>
    /// <param name="TargetUnit">Target</param>
    /// <param name="ItemID">Item ID</param>
    protected static bool SetUnitAIItemTarget(AttackableUnit TargetUnit, int ItemID)
    {
        return false;
    }

    /// <summary>
    /// Clears item target
    /// </summary>
    protected static bool ClearUnitAIItemTarget()
    {
        return false;
    }

    /// <summary>
    /// Unit can use item
    /// </summary>
    /// <param name="ItemID">Item ID</param>
    protected static bool TestUnitAICanUseItem(int ItemID)
    {
        return false;
    }

    /// <summary>
    /// Issue Use item Order
    /// </summary>
    /// <remarks>
    /// Use item
    /// </remarks>
    /// <param name="ItemID">Item ID</param>
    protected static bool IssueUseItemOrder(int ItemID)
    {
        return false;
    }

    /// <summary>
    /// Tests if specified slot has spell toggled ON
    /// </summary>
    /// <param name="Unit">Unit to poll</param>
    /// <param name="SlotIndex">spell slot ID</param>
    protected static bool TestUnitSpellToggledOn(AttackableUnit Unit, int SlotIndex)
    {
        return false;
    }

    /// <summary>
    /// Tests if unit is channeling
    /// </summary>
    /// <param name="Unit">Unit to poll</param>
    protected static bool TestUnitIsChanneling(AttackableUnit Unit)
    {
        return false;
    }

    /// <summary>
    /// Returns unit that casted a buff on input unit
    /// </summary>
    /// <param name="Output">Destination reference;.</param>
    /// <param name="Unit">Source unit</param>
    /// <param name="BuffName">Buff name</param>
    protected static bool GetUnitBuffCaster(out AttackableUnit Output, AttackableUnit Unit, string BuffName)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// AI Unit has an assigned task
    /// </summary>
    protected static bool TestUnitAIHasTask()
    {
        return false;
    }

    /// <summary>
    /// Returns position computed by a task assigned to the unit
    /// </summary>
    /// <param name="Output">Destination reference</param>
    protected static bool GetUnitAITaskPosition(out Vector3 Output)
    {
        Output = default;
        return false;
    }

    /// <summary>
    /// Permanently modifies a target unit's armor.
    /// </summary>
    /// <remarks>
    /// This modifies the permanent CharInter, so be sure to pair it with a decrement of equal value later.
    /// </remarks>
    /// <param name="Unit">Unit to modify.</param>
    /// <param name="Delta">Delta</param>
    protected static bool IncPermanentFlatArmorMod(AttackableUnit Unit, float Delta)
    {
        return false;
    }

    /// <summary>
    /// Permanently modifies a target unit's magic resistance.
    /// </summary>
    /// <remarks>
    /// This modifies the permanent CharInter, so be sure to pair it with a decrement of equal value later.
    /// </remarks>
    /// <param name="Unit">Unit to modify.</param>
    /// <param name="Delta">Delta</param>
    protected static bool IncPermanentFlatMagicResistanceMod(AttackableUnit Unit, float Delta)
    {
        return false;
    }

    /// <summary>
    /// Permanently modifies a target unit's max health.
    /// This will heal the target.
    /// </summary>
    /// <remarks>
    /// This modifies the permanent CharInter, so be sure to pair it with a decrement of equal value later.
    /// Further, this later needs to be converted to a non-healing implementation; it is using the healing approach until Kuo fixes a bug.
    /// </remarks>
    /// <param name="Unit">Unit to modify.</param>
    /// <param name="Delta">Delta</param>
    protected static bool IncPermanentFlatMaxHealthMod(AttackableUnit Unit, float Delta)
    {
        return false;
    }

    /// <summary>
    /// Permanently modifies a target unit's attack damage.
    /// </summary>
    /// <remarks>
    /// This modifies the permanent CharInter, so be sure to pair it with a decrement of equal value later.
    /// </remarks>
    /// <param name="Unit">Unit to modify.</param>
    /// <param name="Delta">Delta</param>
    protected static bool IncPermanentFlatAttackDamageMod(AttackableUnit Unit, float Delta)
    {
        return false;
    }
}