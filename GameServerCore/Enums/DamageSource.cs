namespace GameServerCore.Enums
{
    /// <summary>
    /// Source types for damage. Used in determining when damage is applied, such as before mitigation.
    /// </summary>
    public enum DamageSource
    {
        /// <summary>
        /// Unmitigated.
        /// </summary>
        DAMAGE_SOURCE_RAW = 0x00,
        /// <summary>
        /// Executes, pure.
        /// </summary>
        DAMAGE_SOURCE_INTERNALRAW = 0x01,
        /// <summary>
        /// Buff spell dots.
        /// </summary>
        DAMAGE_SOURCE_PERIODIC = 0x02,
        /// <summary>
        /// Causes Proc (spell specific or attack based) events to fire, pre initial damage.
        /// </summary>
        DAMAGE_SOURCE_PROC = 0x03,
        /// <summary>
        /// On proc.
        /// </summary>
        DAMAGE_SOURCE_REACTIVE = 0x04,
        /// <summary>
        /// Unknown, self-explanatory?
        /// </summary>
        DAMAGE_SOURCE_ONDEATH = 0x05,
        /// <summary>
        /// Single instance spell damage.
        /// </summary>
        DAMAGE_SOURCE_SPELL = 0x06,
        /// <summary>
        /// Attack based spells (proc onhit effects).
        /// </summary>
        DAMAGE_SOURCE_ATTACK = 0x07,
        /// <summary>
        /// Buff Summoner spell damage (single and multi instance)
        /// </summary>
        DAMAGE_SOURCE_DEFAULT = 0x08,
        /// <summary>
        /// Any area based spells.
        /// </summary>
        DAMAGE_SOURCE_SPELLAOE = 0x09,
        /// <summary>
        /// Passive, on update or timed repeat.
        /// </summary>
        DAMAGE_SOURCE_SPELLPERSIST = 0x0A,
        /// <summary>
        /// Unknown, self-explanatory?
        /// </summary>
        DAMAGE_SOURCE_PET = 0x0B
    }

}
