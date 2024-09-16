namespace Buffs
{
    public class AlphaStrikeTarget : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "AlphaStrike_prison.troy", },
            BuffName = "Alpha Strike",
            BuffTextureName = "MasterYi_LeapStrike.dds",
        };
        float baseDamage;
        float chanceToKill;
        public AlphaStrikeTarget(float baseDamage = default, float chanceToKill = default)
        {
            this.baseDamage = baseDamage;
            this.chanceToKill = chanceToKill;
        }
        public override void OnActivate()
        {
            //RequireVar(this.baseDamage);
            //RequireVar(this.chanceToKill);
        }
        public override void OnUpdateActions()
        {
            int count = GetBuffCountFromAll(attacker, nameof(Buffs.AlphaStrike));
            if (count == 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectCreate(out _, out _, "AlphaStrike_Slash.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            if (owner is Champion)
            {
                ApplyDamage(attacker, owner, baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, default, false, false);
            }
            else
            {
                if (RandomChance() < chanceToKill)
                {
                    float bonusDamage = baseDamage + 400;
                    ApplyDamage(attacker, owner, bonusDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, default, false, false);
                }
                else
                {
                    ApplyDamage(attacker, owner, baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, default, false, false);
                }
            }
        }
    }
}