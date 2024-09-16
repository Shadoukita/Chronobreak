namespace Buffs
{
    public class SpiritFireAoE : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float initialDamage;
        float damage;
        float armorReduction;
        Vector3 targetPos;
        EffectEmitter c;
        EffectEmitter boom2;
        EffectEmitter boom;
        float count;
        float lastTimeExecuted;
        public SpiritFireAoE(float initialDamage = default, float damage = default, float armorReduction = default, Vector3 targetPos = default)
        {
            this.initialDamage = initialDamage;
            this.damage = damage;
            this.armorReduction = armorReduction;
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            //RequireVar(this.initialDamage);
            //RequireVar(this.damage);
            //RequireVar(this.armorReduction);
            //RequireVar(this.targetPos);
            Vector3 targetPos = this.targetPos;
            TeamId teamOfOwner = GetTeamID_CS(owner);
            SpellEffectCreate(out c, out _, "nassus_spiritFire_afterburn.troy", default, teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out boom2, out boom, "nassus_spiritFire_tar_green.troy", "nassus_spiritFire_tar_red.troy", teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, target, default, default, false, false, false, false, false);
            Vector3 nextBuffVars_TargetPos = targetPos;
            float nextBuffVars_ArmorReduction = armorReduction;
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, false))
            {
                ApplyDamage(attacker, unit, initialDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 0, false, false, attacker);
                AddBuff(attacker, unit, new Buffs.SpiritFireArmorReduction(nextBuffVars_TargetPos, nextBuffVars_ArmorReduction), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.SHRED, 0, true, false, false);
            }
            count = 0;
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(boom);
            SpellEffectRemove(boom2);
            SpellEffectRemove(c);
        }
        public override void OnUpdateActions()
        {
            Vector3 targetPos = this.targetPos;
            if (count < 5 && ExecutePeriodically(0.9f, ref lastTimeExecuted, false))
            {
                count++;
                Vector3 nextBuffVars_TargetPos = targetPos;
                float nextBuffVars_ArmorReduction = armorReduction;
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, false))
                {
                    float totalDamage = damage / 5;
                    ApplyDamage(attacker, unit, totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.12f, 0, false, false, attacker);
                    AddBuff(attacker, unit, new Buffs.SpiritFireArmorReduction(nextBuffVars_TargetPos, nextBuffVars_ArmorReduction), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.SHRED, 0, true, false, false);
                }
            }
        }
    }
}