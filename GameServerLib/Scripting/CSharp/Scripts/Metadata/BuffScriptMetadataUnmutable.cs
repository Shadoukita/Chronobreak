using GameServerCore.Enums;

namespace Chronobreak.GameServer.Scripting.CSharp;

public class BuffScriptMetadataUnmutable
{
    public string BuffName = "";
    public string BuffTextureName = "";
    public string MinimapIconTextureName = "";
    public string MinimapIconEnemyTextureName = "";
    public string?[]? PopupMessage;

    public string AutoBuffActivateEvent { get; set; } = "";
    public EffCreate AutoBuffActivateEffectFlags = 0;
    public string?[] AutoBuffActivateEffect = new string[0];
    public string?[] AutoBuffActivateAttachBoneName = new string[0];

    public int SpellToggleSlot { get; set; } = 0; // [1-4]

    public bool NonDispellable { get; set; } = false;
    public bool PersistsThroughDeath { get; set; } = false;
    public bool IsPetDurationBuff { get; set; } = false;
    public bool IsDeathRecapSource { get; set; } = false;

    public int OnPreDamagePriority { get; set; } = 0;
    public bool DoOnPreDamageInExpirationOrder { get; set; } = false;
}