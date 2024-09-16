namespace Chronobreak.GameServer.Chatbox;

/// <summary>
/// Class containing several HTML configuration tools.
/// </summary>
public static class HTMLFHelperUtils
{
    /// <summary>
    /// Contains Hex color strings.
    /// </summary>
    public struct Colors
    {
        public const string RED = "#FF0000";
        public const string ORANGE = "#FFA500";
        public const string YELLOW = "#AFBF00";
        public const string GREEN = "#00D90E";
        public const string BLUE = "#006EFF";
        public const string INDIGO = "#4B0082";
        public const string VIOLET = "#8F00FF";
        public const string PURPLE = "#A020F0";
        public const string PINK = "#E175FF";

        public const string BLACK = "#000000";
        public const string WHITE = "#FFFFFF";

        public const string NEUTRAL = "#000000";
        public const string ALL = "#FFFFFF";
        public const string UNKNOWN = "#FFFFFF";
        public const string PRIVATE = "#EDB458";
    }

    /// <summary>
    /// Appends a font HTML tag to the provided text.
    /// </summary>
    /// <param name="fontSize">font size to use, CB defaults to 20.</param>
    /// <param name="fontColor">Color to display text in.</param>
    /// <param name="text">Text the the tag will be appended to.</param>
    /// <returns>A new string of the combined html tag and text.</returns>
    public static string Font(int fontSize, string fontColor, string text) => "<font size=\"" + fontSize + "\" color =\"" + fontColor + "\">" + text + "</font>";
    /// <summary>
    /// Appends a font HTML tag to the provided text.
    /// </summary>
    /// <param name="fontColor">Color to display text in.</param>
    /// <param name="text">Text the the tag will be appended to.</param>
    /// <returns>A new string of the combined html tag and text.</returns>
    public static string Font(string fontColor, string text) => "<font color =\"" + fontColor + "\"></font>" + text;


    /// <summary>
    /// Surrounds text in the HTML bold tags.
    /// </summary>
    /// <param name="text">Text the the tags will be placed around.</param>
    /// <returns>A new string of the combined html tags and text.</returns>
    public static string Bold(string text) => "<b>" + text + "</b>";
    /// <summary>
    /// Surrounds text in the HTML italic tags.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string Italic(string text) => "<i>" + text + "</i>";
    /// <summary>
    /// Surrounds text in the HTML underline tags.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string Underline(string text) => "<u>" + text + "</u>";
    /// <summary>
    /// Surrounds text in the HTML strikethrough tags.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string Strikethrough(string text) => "<s>" + text + "</s>"; //TODO: Verify
    /// <summary>
    /// Surrounds text in the HTML list-item tags.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string ListItem(string text) => "<li>" + text + "</li>";
}