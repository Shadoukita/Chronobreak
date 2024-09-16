#nullable enable

namespace ScriptsCore.Scripts;

public class BuffScript : CBuffScript
{
    private AllCharVars? _charVars;
    public AllCharVars charVars => _charVars ??= owner.GetComponent<AllCharVars>();
    private AllAvatarVars? _avatarVars;
    public AllAvatarVars avatarVars => _avatarVars ??= owner.GetComponent<AllAvatarVars>();
}