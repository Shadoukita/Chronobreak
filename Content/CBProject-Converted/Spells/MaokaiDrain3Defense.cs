namespace Buffs
{
    public class MaokaiDrain3Defense : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "MaokaiDrainDefense",
            BuffTextureName = "Maokai_VengefulMaelstromBuff.dds",
        };
        float defenseBonus;
        Vector3 targetPos;
        EffectEmitter particle4; // UNUSED
        public MaokaiDrain3Defense(float defenseBonus = default, Vector3 targetPos = default)
        {
            this.defenseBonus = defenseBonus;
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            //RequireVar(this.targetPos);
            //RequireVar(this.defenseBonus);
            //RequireVar(this.cCReduction);
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (damageType != DamageType.DAMAGE_TYPE_TRUE && attacker is not BaseTurret)
            {
                float drainAmount = damageAmount;
                ObjAIBase caster = GetBuffCasterUnit();
                damageAmount *= defenseBonus;
                float damageAbsorbed = 1 - defenseBonus;
                drainAmount *= damageAbsorbed;
                float nextBuffVars_DrainAmount = drainAmount;
                AddBuff(attacker, caster, new Buffs.MaokaiDrain3Tally(nextBuffVars_DrainAmount), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                if (GetBuffCountFromCaster(caster, caster, nameof(Buffs.MaokaiDrain3Toggle)) == 0)
                {
                    Vector3 targetPos = this.targetPos;
                    AddBuff(caster, caster, new Buffs.MaokaiDrain3Toggle(), 1, 1, 0.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    SpellEffectCreate(out particle4, out _, "maoki_torrent_damage_pulse.troy", default, TeamId.TEAM_NEUTRAL, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, target, default, default, true, default, default, false);
                }
            }
        }
    }
}