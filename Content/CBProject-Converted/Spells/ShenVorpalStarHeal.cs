namespace Buffs
{
    public class ShenVorpalStarHeal : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Shen Vorpal Star Heal",
            BuffTextureName = "Shen_VorpalBlade.dds",
        };
        float lifeTapMod;
        float lastTimeExecuted;
        public ShenVorpalStarHeal(float lifeTapMod = default)
        {
            this.lifeTapMod = lifeTapMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.lifeTapMod);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, true))
            {
                SpellEffectCreate(out _, out _, "shen_vorpalStar_lifetap_tar_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
                float temp1 = GetHealthPercent(target, PrimaryAbilityResourceType.MANA);
                if (temp1 < 1)
                {
                    IncHealth(owner, lifeTapMod, attacker);
                    ApplyAssistMarker(attacker, owner, 10);
                }
            }
        }
    }
}