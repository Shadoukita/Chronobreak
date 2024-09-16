namespace Spells
{
    public class UrgotSwapMissile2 : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int count = GetBuffCountFromAll(target, nameof(Buffs.UrgotSwapMarker));
            if (count != 0)
            {
                DestroyMissile(missileNetworkID);
            }
        }
    }
}
namespace Buffs
{
    public class UrgotSwapMissile2 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "DarkBinding_tar.troy", "", },
            BuffName = "Dark Binding",
            BuffTextureName = "FallenAngel_DarkBinding.dds",
            PopupMessage = new[] { "game_floatingtext_Snared", },
        };
        public override void OnActivate()
        {
            Vector3 ownerPos = GetUnitPosition(owner);
            SetCameraPosition((Champion)owner, ownerPos);
        }
    }
}