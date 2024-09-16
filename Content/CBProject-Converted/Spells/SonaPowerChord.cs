namespace Buffs
{
    public class SonaPowerChord : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "SonaPowerChordReady.troy", },
            BuffName = "SonaPowerChordReady",
            BuffTextureName = "Sona_PowerChordCharged.dds",
        };
        public override void OnActivate()
        {
            SetDodgePiercing(owner, true);
            CancelAutoAttack(owner, false);
            SetAutoAcquireTargets(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetDodgePiercing(owner, false);
            SetAutoAcquireTargets(owner, true);
            SpellBuffRemove(owner, nameof(Buffs.SonaHymnofValorCheck), (ObjAIBase)owner, 0);
            SpellBuffRemove(owner, nameof(Buffs.SonaAriaofPerseveranceCheck), (ObjAIBase)owner, 0);
            SpellBuffRemove(owner, nameof(Buffs.SonaSongofDiscordCheck), (ObjAIBase)owner, 0);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SonaAriaofPerseverance)) > 0)
            {
                OverrideAutoAttack(3, SpellSlotType.ExtraSlots, owner, 1, false);
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SonaHymnofValor)) > 0)
            {
                OverrideAutoAttack(4, SpellSlotType.ExtraSlots, owner, 1, false);
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SonaSongofDiscord)) > 0)
            {
                OverrideAutoAttack(5, SpellSlotType.ExtraSlots, owner, 1, false);
            }
            else
            {
                OverrideAutoAttack(5, SpellSlotType.ExtraSlots, owner, 1, false);
            }
        }
    }
}