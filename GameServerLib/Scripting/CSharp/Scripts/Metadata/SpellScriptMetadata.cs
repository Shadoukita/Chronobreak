using System.Numerics;
using GameServerCore.Enums;
using Chronobreak.GameServer.GameObjects;

namespace Chronobreak.GameServer.Scripting.CSharp
{

    public class SpellScriptMetadata
    {
        // Metadata specific to CB and not found in League scripts.
        #region CB_Specific_Metadata
        public string SpellEvolveDesc { get; set; } = "";
        public int AmmoPerCharge { get; set; } = 1;
        /// <summary>
        /// Whether or not the caster should automatically face the end position of the spell.
        /// </summary>
        public bool AutoFaceDirection { get; set; } = true;
        public bool CooldownIsAffectedByCDR { get; set; } = true;
        public bool IsNonDispellable { get; set; } = false;
        public string PopupMessage1 { get; set; } = "";
        public SectorParameters? SectorParameters { get; set; } = null;
        /// <summary>
        /// Optional spell flags which determine what units this spell affects.
        /// If 0, the spell will use the original SpellData flags.
        /// </summary>
        public SpellDataFlags OverrideFlags { get; set; } = 0;
        #endregion

        // Metadata found in League scripts.
        #region League_Metadata
        public float[] AutoCooldownByLevel { get; set; } = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
        public float[] AutoTargetDamageByLevel { get; set; } = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
        public float CastTime { get; set; } = 0.0f;
        /// <summary>
        /// Determines how how long the spell should be channeled (overrides content based channel duration).
        /// Triggers on channel (and post) if value is above 0.
        /// </summary>
        public float ChannelDuration { get; set; } = 0.0f;
        public MissileParameters? MissileParameters { get; set; } = null;
        public MissileParameters? ChainMissileParameters
        {
            get => MissileParameters;
            set => MissileParameters = value;
        }
        public float PhysicalDamageRatio { get; set; } = 0.0f;
        public float SpellDamageRatio { get; set; } = 0.0f;

        public string[] SpellFXOverrideSkins { get; set; } = { };
        public string[] SpellVOOverrideSkins { get; set; } = { };

        /// <summary>
        /// Determines whether or not the spell stops movement and triggers spell casts (and post).
        /// Usually should not be true if the spell is an item active, summoner spell, missile spell, or otherwise purely buff related spell.
        /// </summary>
        public bool TriggersSpellCasts { get; set; } = false;

        // Metadata found in the SpellMetaData.txt file
        #region SpellMetaData_txt
        /// <summary> This should be false for all spells besides toggle spells. </summary>
        public bool DoesntTriggerSpellCasts => !TriggersSpellCasts;
        /// <summary> Used to override whether or not 'triggers spell casts' should break stealth when cast. </summary>
        public bool CastingBreaksStealth { get; set; } = false;
        /// <summary> If set to true, the spell's execution won't break shields.  Useful for spells like Absolute Zero. </summary>
        public bool DoesntBreakShields { get; set; } = false;
        /// <summary> Set to true if this spell deals damage. </summary>
        public bool IsDamagingSpell { get; set; } = false;
        /// <summary> Any spell that doesn't have a single target cursor should have this true. </summary>
        public bool NotSingleTargetSpell { get; set; } = false;
        #endregion

        //public string AutoAuraBuffName { get; set; } = "";
        //public string AutoItemActivateEffect { get; set; } = "";
        //public bool IsDebugMode { get; set; } = false;
        #endregion

    }

    /// <summary>
    /// Parameters which determine how a missile behaves.
    /// </summary>
    public class MissileParameters
    {
        public bool CanHitCaster { get; set; } = false;
        public bool CanHitEnemies { get; set; } = false;
        public bool CanHitFriends { get; set; } = false;
        /// <summary>
        /// Whether or not the missile should be able to hit something multiple times.
        /// Will only hit again if the missile has bounced to a different unit.
        /// Is overridden by CanHitSameTargetConsecutively.
        /// </summary>
        public bool CanHitSameTarget { get; set; } = false;
        /// <summary>
        /// Whether or not the missile should be able to hit something multiple times in a row,
        /// regardless of if it has bounced to another unit.
        /// Overrides CanHitSameTarget.
        /// </summary>
        public bool CanHitSameTargetConsecutively { get; set; } = false;
        /// <summary>
        /// Maximum number of times the missile can hit something before being removed.
        /// </summary>
        public int MaximumHits { get; set; } = 0;
        public int[] MaximumHitsByLevel = { 0, 0, 0, 0, 0 };
    }

    /// <summary>
    /// Parameters which determine how a sector behaves.
    /// </summary>
    public class SectorParameters
    {
        /// <summary>
        /// Optional object the sector should be bound to. The sector will be attached to this object and will use its facing direction.
        /// </summary>
        public GameObject? BindObject { get; set; } = null;
        /// <summary>
        /// Distance from the bottom of the sector to the top.
        /// If this is larger than Width, it will be used as the area around the sector to check for collisions.
        /// Scales the distance (in y) between PolygonVertices.
        /// </summary>
        public float Length { get; set; } = 0f;
        /// <summary>
        /// Distance from the left side of the sector to the right side.
        /// If this is larger than Length, it will be used as the area around the sector to check for collisions.
        /// Scales the distance (in x) between PolygonVertices.
        /// </summary>
        public float Width { get; set; } = 0f;
        /// <summary>
        /// If the Type is Cone, this will filter collisions that are in front of the sector and are within this angle from the sector's center.
        /// Should be a value from 0->360
        /// </summary>
        public float ConeAngle { get; set; } = 0f;
        /// <summary>
        /// If the Type is Polygon, this will represent the vertices of the sector.
        /// Vertices are relative to the origin (SpellCastLaunchPosition or target position & direction).
        /// If the distance between points exceeds HalfLength/Width, that distance will be used instead as the collision radius check for the sector.
        /// Points should be ordered such that each point connects to the next (with the last point connecting to the first point).
        /// Due to HalfLength and Width scaling the distance between vertices, it is recommended that points be arranged with x and y values between 0 and 1.
        /// </summary>
        public Vector2[] PolygonVertices { get; set; } = new Vector2[0];
        /// <summary>
        /// Maximum amount of time this spell sector should last (in seconds) before being automatically removed.
        /// Setting to -1 will cause the spell sector to last until manually removed.
        /// </summary>
        public float Lifetime { get; set; } = -1f;
        /// <summary>
        /// Whether or not the sector should only tick once before being removed (Lifetime must be greater than a single tick).
        /// </summary>
        public bool SingleTick { get; set; } = false;
        /// <summary>
        /// How many times a second the spell sector should check for hitbox collisions.
        /// </summary>
        public int Tickrate { get; set; } = 0;
        /// <summary>
        /// Whether or not the spell sector should be able to hit something multiple times.
        /// Will only hit again if the unit hit re-enters the hitbox (constant per-collision hitbox).
        /// Is overridden by CanHitSameTargetConsecutively.
        /// </summary>
        public bool CanHitSameTarget { get; set; } = false;
        /// <summary>
        /// Whether or not the spell sector should be able to hit something multiple times in a row,
        /// regardless of if it has left and re-entered the hitbox (costant hitbox).
        /// Overrides CanHitSameTarget.
        /// </summary>
        public bool CanHitSameTargetConsecutively { get; set; } = false;
        /// <summary>
        /// Maximum number of times the spell sector can hit something before being removed. A value of 0 or less means this variable will be unused.
        /// </summary>
        public int MaximumHits { get; set; } = int.MaxValue;
        /// <summary>
        /// Optional spell flags which determine what units this spell sector affects.
        /// If 0, the sector will use the SpellOrigin's spell flags.
        /// </summary>
        public SpellDataFlags OverrideFlags { get; set; } = 0;
        /// <summary>
        /// What kind of shape this sector has.
        /// </summary>
        public SectorType Type { get; set; } = SectorType.Area;
    }
}
