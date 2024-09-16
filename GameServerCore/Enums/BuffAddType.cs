namespace GameServerCore.Enums
{
    /// <summary>
    /// Determines how a buff should be treated when added.
    /// </summary>
    public enum BuffAddType
    {
        /// <summary>
        /// A new Buff is created and replaces any existing buffs of the same name.
        /// </summary>
        REPLACE_EXISTING,
        /// <summary>
        /// Restarts the timer on any buffs of the same name already applied to the buff's Target, or create a new Buff
        /// </summary>
        RENEW_EXISTING,
        /// <summary>
        /// Adds a stack to any buffs of the same name already applied and restarts the timer. When the timer ends all stacks are removed
        /// </summary>
        STACKS_AND_RENEWS,
        /// <summary>
        /// Adds a stack to any buffs of the same name already applied and continues the timer. When the timer ends one stack is removed
        /// </summary>
        STACKS_AND_CONTINUE,
        /// <summary>
        /// If the Target have a buff with the same name and source, it only restart the timer of the given buff.
        /// </summary>
        STACKS_AND_OVERLAPS
    }
}