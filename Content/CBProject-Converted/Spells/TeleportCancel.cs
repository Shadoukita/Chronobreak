namespace Spells
{
    public class TeleportCancel : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SummonerTeleport)) > 0)
            {
                charVars.TeleportCancelled = true;
                SpellBuffRemove(owner, nameof(Buffs.SummonerTeleport), owner, 0);
            }
        }
    }
}
namespace Buffs
{
    public class TeleportCancel : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Teleport",
            BuffTextureName = "Summoner_teleport.dds",
        };
    }
}