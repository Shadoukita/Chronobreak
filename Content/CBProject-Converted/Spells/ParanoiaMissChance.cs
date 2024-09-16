namespace Buffs
{
    public class ParanoiaMissChance : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Paranoia",
            BuffTextureName = "Fiddlesticks_Paranoia.dds",
        };
        public override void OnActivate()
        {
            IncPermanentFlatSpellBlockMod(owner, -10);
            SpellEffectCreate(out _, out _, "ConsecrationAura_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, true, owner, default, default, target, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            IncPermanentFlatSpellBlockMod(owner, 10);
        }
        public override void OnUpdateActions()
        {
            if (IsDead(attacker))
            {
                SpellBuffRemoveCurrent(owner);
            }
            else
            {
                float dist = DistanceBetweenObjects(attacker, owner);
                if (dist >= 800)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}