using Chronobreak.GameServer.API;

namespace Buffs
{
    internal class AscWarp : BuffScript
    {
        public override BuffScriptMetaData BuffMetaData { get; } = new()
        {
            BuffType = BuffType.COMBAT_ENCHANCER,
            BuffAddType = BuffAddType.REPLACE_EXISTING,
        };


        public override void OnActivate()
        {
            Buff.SetStatusEffect(StatusFlags.Stunned, true);
            target.IconInfo.ChangeBorder("Teleport", "AscWarp");
            //AddParticleLink(target, "Global_Asc_teleport", target, target, Buff.Duration);
        }

        public override void OnDeactivate(bool expired)
        {
            target.IconInfo.ResetBorder();
            if (target is ObjAIBase obj)
            {
                OldAPI.AddBuff("AscWarpReappear", 10.0f, 1, null, target, obj);
            }
            OldAPI.AddBuff("AscWarpProtection", 2.5f, 1, null, target, owner);
        }
    }
}