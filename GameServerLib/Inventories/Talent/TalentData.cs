namespace Chronobreak.GameServer.Content;
public class TalentData
{
    public string Id { get; init; }
    public byte MaxLevel { get; init; }

    public TalentData(string id)
    {
        Id = id;
        MaxLevel = 1;
    }

    public TalentData(INIContentFile file) : this(file.Name)
    {
        MaxLevel = (byte)file.GetValue("SpellData", "Ranks", 1);
    }
}
